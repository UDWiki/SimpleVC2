using System;
using System.Collections.Generic;
using System.Drawing;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.VCControl
{
    public interface IFcMatrixCell
    {
        Int32 Left { get; set; }
        Int32 Top { get; set; }
        Int32 Height { get; set; }
        Int32 Width { get; set; }
    }

    public interface IFcMatrixMatterCell : IFcMatrixCell
    {
        VcTime Time { get; }
    }

    public interface IFcMatrixBorderCell : IFcMatrixCell
    {
        String Text { get; set; }
    }

    /// <summary>
    /// FcMatrix将课表的表现算法（绘图）抽象出来，统一了窗体的显示与打印
    /// </summary>
    public abstract class FcMatrix<BorderCell, MatterCell> : IDisposable
        where BorderCell : IFcMatrixBorderCell, new()
        where MatterCell : IFcMatrixMatterCell, new()
    {
        protected class BorderDirect
        {
            private BorderCell border;

            public BorderCell Border
            {
                get { return border; }
                set { border = value; }
            }

            private Int32 clm;

            public Int32 Clm
            {
                get { return clm; }
                set { clm = value; }
            }

            private Int32 row;

            public Int32 Row
            {
                get { return row; }
                set { row = value; }
            }

            private Int32 wdh;

            public Int32 Wdh
            {
                get { return wdh; }
                set { wdh = value; }
            }

            private Int32 hgh;

            public Int32 Hgh
            {
                get { return hgh; }
                set { hgh = value; }
            }
        }

        protected const Int32 cRate = 3;

        private EnSolution solution;

        protected BorderDirect[ ] TopBordes = new BorderDirect[8];
        protected BorderDirect[,] LeftBordes = new BorderDirect[4, 7];
        protected Int32 ClmCount;
        protected Int32 RowCount;

        protected DtMatrix<MatterCell> mattes;

        protected void CreateMattes()
        { 
            mattes = new DtMatrix<MatterCell>(solution);
            foreach (VcTime time in mattes.eachTime())
            {
                MatterCell cl = this.GetNewMatterCell();
                time.CopyFieldTo(cl.Time);

                mattes[time] = cl;
            }

            ClmCount = 1;
            RowCount = cRate;
            BorderDirect bd0 = new BorderDirect();
            bd0.Border = this.GetNewBorderCell();
            bd0.Clm = ClmCount - 1;
            bd0.Row = 0;
            bd0.Wdh = 1;
            bd0.Hgh = cRate;

            TopBordes[0] = bd0;
            for (Int32 i = 0; i < 7; i++)
                if (solution.ActiveWeekArr[i])
                {
                    ClmCount++;
                    BorderDirect bd = new BorderDirect();
                    bd.Border = this.GetNewBorderCell();
                    bd.Border.Text = ExDateTime.DayOfWeekToChiese((DayOfWeek)i, "周");
                    bd.Clm = ClmCount - 1;
                    bd.Row = 0;
                    bd.Wdh = 1;
                    bd.Hgh = cRate;

                    TopBordes[i + 1] = bd;
                }

            Boolean IsOpening = false;
            for (Int32 i = 0; i < 4; i++)
                if (solution.LessonNumberArr[i] > 0)
                {
                    if (IsOpening)
                    {
                        BorderDirect bd = new BorderDirect();
                        bd.Border = this.GetNewBorderCell();
                        bd.Clm = 0;
                        bd.Row = RowCount;
                        bd.Wdh = ClmCount;
                        bd.Hgh = 1;

                        LeftBordes[i, 0] = bd;
                        RowCount = RowCount + 1;
                    }
                    for (Int32 j = 1; j <= solution.LessonNumberArr[i]; j++)
                    {
                        BorderDirect bd  = new BorderDirect();
                        bd.Border = this.GetNewBorderCell();
                        bd.Border.Text = j == 1 ?
                            VcTimeLogic.BetideNodeToString((eBetideNode)i) + " " + (j)
                            : j.ToString();
                        bd.Clm = 0;
                        bd.Row = RowCount;
                        bd.Wdh = 1;
                        bd.Hgh = cRate;
                        
                        LeftBordes[i, j] = bd;
                        RowCount = RowCount + cRate;
                    }
                    IsOpening = true;
                }
        }

        public FcMatrix(EnSolution solution)
        {
            this.solution = solution;

            this.CreateMattes();
        }

        protected BorderCell GetNewBorderCell()
        {
            return new BorderCell();
        }

        protected MatterCell GetNewMatterCell()
        {
            return new MatterCell();
        }

        private Int32 heighWidthProportionLow;

        public Int32 HeighWidthProportionLow
        {
            get { return heighWidthProportionLow; }
            set 
            {
                if (value < 0 || value > 100)
                    throw new Exception("HeighWidthProportion: 0--100");

                heighWidthProportionLow = value;
            }
        }

        private Int32 heighWidthProportionHigh;

        public Int32 HeighWidthProportionHigh
        {
            get { return heighWidthProportionHigh; }
            set 
            {
                if (value < 0 || value > 100)
                    throw new Exception("HeighWidthProportion: 0--100");

                heighWidthProportionHigh = value;
            }
        }

        public MatterCell this[VcTime time]
        {
            get
            {
                return mattes[time];
            }
        }

        public IEnumerable<MatterCell> eachCell()
        {
            return mattes.eachElement();
        }

        public IEnumerable<BorderCell> eachBorderCell()
        {
            foreach (BorderDirect bd in TopBordes)
                if (bd != null)
                    yield return  bd.Border;

            foreach (BorderDirect bd in LeftBordes)
                if (bd != null)
                    yield return bd.Border;
        }

        private Rectangle rect = new Rectangle(0, 0, 0, 0);

        public Rectangle Rect
        {
            get { return rect; }
            set 
            {
                 SetRect(value); 
            }
        }

        //允许校正课表，防止比例失调
        private Rectangle GetRealRect(Rectangle rect, 
            out Int32 CellWidth, out Int32 CellHeight)
        {
            if (ViewStyle.Horizontal)
            {
                CellWidth = rect.Width / RowCount;  //实际是1/n的宽
                CellHeight = rect.Height / ClmCount;
 
                //校正可能有问题
                if (heighWidthProportionLow > 0
                    && CellHeight * 100 / (CellWidth * cRate) < heighWidthProportionLow)
                    CellWidth = CellHeight * 100 / (heighWidthProportionLow * cRate);
                    //&& CellHeight * cRate * 100 / CellWidth < heighWidthProportionLow)
                    //CellWidth = CellHeight * cRate * 100 / heighWidthProportionLow;

                if (HeighWidthProportionHigh > 0
                    && CellHeight * 100 / (CellWidth * cRate) > HeighWidthProportionHigh)
                    CellHeight = CellWidth * cRate * HeighWidthProportionHigh / 100;

                return new Rectangle(rect.X, rect.Y, CellWidth * RowCount, CellHeight * ClmCount);
            }
            else
            {
                CellWidth = rect.Width / ClmCount;
                CellHeight = rect.Height / RowCount;
                if (heighWidthProportionLow > 0
                    && CellHeight * cRate * 100 / CellWidth < heighWidthProportionLow)
                    CellWidth = CellHeight * cRate * 100 / heighWidthProportionLow;

                if (HeighWidthProportionHigh > 0
                    && CellHeight * cRate * 100 / CellWidth > HeighWidthProportionHigh)
                    CellHeight = CellWidth * HeighWidthProportionHigh / (100 * cRate);

                return new Rectangle(rect.X, rect.Y, CellWidth * ClmCount, CellHeight * RowCount);
            }
        }

        private void SetRect(Rectangle rect)
        {
            Int32 CellWidth;
            Int32 CellHeight;
            this.rect = GetRealRect(rect, out CellWidth, out CellHeight);

            if (ViewStyle.Horizontal)   
            {
                foreach (BorderDirect bd in TopBordes)
                    if (bd != null)
                    {
                        bd.Border.Left = rect.Left;
                        bd.Border.Top = rect.Top + CellHeight * bd.Clm;
                        bd.Border.Width = CellWidth * bd.Hgh;
                        bd.Border.Height = CellHeight; 
                    }

                foreach (BorderDirect bd in LeftBordes)
                    if (bd != null)
                    {
                        bd.Border.Left = rect.Left + CellWidth * bd.Row;
                        bd.Border.Top = rect.Top;
                        bd.Border.Width = CellWidth * bd.Hgh;
                        bd.Border.Height = CellHeight * bd.Wdh;
                    }

                foreach (VcTime time in mattes.eachTime())
                {
                    this[time].Left = rect.Left
                        + CellWidth * LeftBordes[(Int32)time.BetideNode, (Int32)time.Order].Row;
                    this[time].Top = rect.Top
                        + CellHeight  * TopBordes[(Int32)time.Week + 1].Clm;
                    this[time].Width = CellWidth * LeftBordes[(Int32)time.BetideNode, (Int32)time.Order].Hgh;
                    this[time].Height = CellHeight;
                }
            }
            else
            {
                foreach (BorderDirect bd in TopBordes)
                    if (bd != null)
                    {
                        bd.Border.Left = rect.Left + CellWidth * bd.Clm;
                        bd.Border.Top = rect.Top + CellHeight * bd.Row;
                        bd.Border.Width = CellWidth;
                        bd.Border.Height = CellHeight * bd.Hgh;
                    }

                foreach (BorderDirect bd in LeftBordes)
                    if (bd != null)
                    {
                        bd.Border.Left = rect.Left + CellWidth * bd.Clm;
                        bd.Border.Top = rect.Top + CellHeight * bd.Row;
                        bd.Border.Width = CellWidth * bd.Wdh;
                        bd.Border.Height = CellHeight * bd.Hgh;
                    }

                foreach (VcTime time in mattes.eachTime())
                {
                    this[time].Left = rect.Left + CellWidth * TopBordes[(Int32)time.Week + 1].Clm;
                    this[time].Top = rect.Top
                        + CellHeight * LeftBordes[(Int32)time.BetideNode, (Int32)time.Order].Row;
                    this[time].Width = CellWidth;
                    this[time].Height = CellHeight * LeftBordes[(Int32)time.BetideNode, (Int32)time.Order].Hgh;
                }
            }
        }

        public void Dispose()
        {
            foreach (BorderDirect bd in TopBordes)
                if (bd != null && bd.Border is IDisposable)
                    (bd.Border as IDisposable).Dispose();

            foreach (BorderDirect bd in LeftBordes)
                if (bd != null && bd.Border is IDisposable)
                    (bd.Border as IDisposable).Dispose();

            foreach (VcTime time in mattes.eachTime())
                if (this[time] is IDisposable)
                    (this[time] as IDisposable).Dispose();
        }
    }
}
