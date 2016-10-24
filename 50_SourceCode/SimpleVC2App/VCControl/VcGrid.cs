using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.WinFormApp.VCControl
{
    /// <summary>
    /// ����һ����ʱ���࣬����ʹ��VcGridV2����
    /// VcGridV2�ǻ���FcMatrixʵ�ֵģ�FcMatrix���α�ı����㷨����ͼ�����������ͳһ�˴������ʾ���ӡ
    /// </summary>
    public class VcGrid<T>
        where T : Control, IFcMatrixMatterCell, new()
    {
        private Control Parent;

        public VcGrid(Control parent)
        {
            this.Parent = parent;
            parent.Resize += this.ResizeGrid;
            parent.VisibleChanged += this.ParentVisible;
        }

        private Int32 padding;

        public Int32 Padding
        {
            get { return padding; }
            set
            {
                if (value > 0)
                {
                    padding = value;
                    if (Parent.Visible)
                        ResizeGrid(null, null);
                }
            }
        }

        private EnSolution solution;

        public EnSolution Solution
        {
            get { return solution; }
            set
            {
                solution = value;
                ClearControls();

                CreateCells();
                if (Parent.Visible)
                    ResizeGrid(null, null);
            }
        }

        private void ClearControls()
        {
            foreach (IList<Control> Row in Cells)
                foreach (Control ctl in Row)
                    ctl.Dispose();

            Cells = new List<IList<Control>>();
        }


        private IList<IList<Control>> Cells = new List<IList<Control>>();

        private void ParentVisible(object sender, EventArgs e)
        {
            if (Parent.Visible)
                ResizeGrid(sender, e);
        }

        public IEnumerable<T> eachCell()
        {
            foreach (IList<Control> Row in Cells)
                foreach (Control ctl in Row)
                    if (ctl is T)
                        yield return ctl as T;
        }

        private IEnumerable<Control> eachControl()
        {
            foreach (IList<Control> Row in Cells)
                foreach (Control ctl in Row)
                    yield return ctl;
        }

        /// <summary>
        /// ��Ч������С��ʹ��
        /// </summary>
        public T this[VcTime time]
        {
            get
            {
                foreach(T t in eachCell())
                    if (t.Time == time)
                        return t;
                
                throw new Exception("��Ч��ʱ�䣡");
            }
        }

        private void CreateCells()
        {
            IList<Control> ColumnHeades = new List<Control>();
            Control CRh = new Button();
            CRh.Visible = false;
            ColumnHeades.Add(CRh);
            for (Int32 i = 0; i < Solution.ActiveWeekArr.Length; i++)
                if (Solution.ActiveWeekArr[i])
                {
                    Control Ch = new HeadButton();
                    Ch.Visible = false;
                    Ch.Text = ExDateTime.DayOfWeekToChiese((DayOfWeek)i, "��");
                    ColumnHeades.Add(Ch);
                }
            foreach (Control ctl in ColumnHeades)
                ctl.Parent = this.Parent;
            Cells.Add(ColumnHeades);

            IList<Control> Row;
            Boolean HasSeparator = true;
            for (Int32 i = 0; i < Solution.LessonNumberArr.Length; i++)
                if (Solution.LessonNumberArr[i] > 0)
                {
                    if (!HasSeparator)
                    {
                        Row = new List<Control>();
                        Button Separator = new HeadButton();
                        Separator.Visible = false;
                        Separator.Enabled = false;
                        Separator.Parent = this.Parent;
                        Row.Add(Separator);
                        Cells.Add(Row);
                    }
                    HasSeparator = false;

                    for (Int32 j = 0; j < Solution.LessonNumberArr[i]; j++)
                    {
                        Row = new List<Control>();
                        Button Rh = new HeadButton();
                        Rh.Text = VcTimeLogic.BetideNodeToString((eBetideNode)i) + " " + (j + 1);
                        Rh.Parent = this.Parent;
                        Row.Add(Rh);
                        for (Int32 k = 0; k < Solution.ActiveWeekArr.Length; k++)
                            if (Solution.ActiveWeekArr[k])
                            {
                                T Cell = new T();
                                Cell.Visible = false;
                                Cell.Time.Week = (DayOfWeek)k;
                                Cell.Time.BetideNode = (eBetideNode)i;
                                Cell.Time.Order = j + 1;

                                Cell.Parent = this.Parent;
                                Row.Add(Cell);
                            }

                        Cells.Add(Row);
                    }
                }
        }

        private void ResizeGrid(object sender, EventArgs e)
        {
            if (Solution == null)
                return;

            foreach (Control ctl in eachControl())
                ctl.Visible = false;

            Int32 GrdLeft = padding;
            Int32 GrdTop = padding;
            Int32 GrdWidth = Parent.ClientSize.Width - padding * 2;
            Int32 GrdHeight = Parent.ClientSize.Height - padding * 2;

            //��ȡ��/����
            Int32 CellColumnNum = 1;
            for (Int32 i = 0; i < Solution.ActiveWeekArr.Length; i++)
                if (Solution.ActiveWeekArr[i])
                    CellColumnNum++;

            Int32 CellRowNum = 0;
            foreach (IList<Control> Row in Cells)
                CellRowNum = CellRowNum + (Row.Count == 1 ? 1 : 3);

            Int32 CellWidth = GrdWidth / CellColumnNum;
            Int32 CellHeight = GrdHeight / CellRowNum;

            //����Cellλ��
            T t = new T();
            Int32 tmpTop = GrdTop;
            foreach (IList<Control> Row in Cells)
            {
                Int32 tmpLeft = GrdLeft;
                if (Row.Count == 1)
                {
                    Row[0].Left = tmpLeft;
                    Row[0].Top = tmpTop;
                    Row[0].Height = CellHeight - 1;
                    Row[0].Width = CellWidth * CellColumnNum;
                    
                    tmpTop = tmpTop + CellHeight;
                }
                else
                {
                    foreach (Control ctl in Row)
                    {
                        ctl.Left = tmpLeft;
                        ctl.Top = tmpTop;
                        ctl.Height = CellHeight * 3 - 1;
                        ctl.Width = CellWidth - 1;

                        tmpLeft = tmpLeft + CellWidth;
                    }
                    tmpTop = tmpTop + CellHeight * 3;
                }
            }

            foreach (Control ctl in eachControl())
                ctl.Visible = true;
        }
    }
}
