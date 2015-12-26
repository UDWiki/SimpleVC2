using System;
using System.Drawing;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.VCControl;

namespace Telossoft.SimpleVC.WinFormApp.Print
{
    public class PrtCell : IFcMatrixCell
    {
        private Int32 left;
        public int Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        private Int32 top;
        public int Top
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
            }
        }

        private Int32 height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        private Int32 width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public Rectangle Rect
        {
            get 
            {
                return new Rectangle(this.left, this.top, 
                    this.width, this.height);
            }
        }
    }

    public class PrtBorderCell : PrtCell, IFcMatrixBorderCell
    {
    
        private String text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(DrawComm.Black2Pen, this.Rect);
            graphics.DrawString(this.Text,
                new Font("宋体", DrawComm.SuitFontSize((double)Rect.Height * 0.8), FontStyle.Bold), 
                new SolidBrush(Color.Black), Rect, DrawComm.CenterStringFormat);
        }
    }

    public class PrtMatterCell : PrtCell, IFcMatrixMatterCell
    {
        private VcTime time = new VcTime();
        public VcTime Time
        {
            get
            {
                return time;
            }
        }

        public virtual void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(DrawComm.Black1Pen, this.Rect);
            graphics.DrawString(this.Time.ToString(),
                new Font("宋体", DrawComm.SuitFontSize((double)Rect.Height * 0.6), FontStyle.Regular), 
                new SolidBrush(Color.Black), Rect, DrawComm.CenterStringFormat);
        }
    }

    /// <summary>
    /// 定位在协助绘制课表
    /// </summary>
    public class FcMatrixPtr<MatterCell> : FcMatrix<PrtBorderCell, MatterCell>
        where MatterCell : PrtMatterCell, new()
    {
        public FcMatrixPtr(EnSolution solution)
            : base(solution)
        {
        }

        public virtual void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(DrawComm.Black2Pen, this.Rect);

            foreach (PrtBorderCell bc in this.eachBorderCell())
                bc.Draw(graphics);

            foreach (PrtMatterCell mc in this.eachCell())
                mc.Draw(graphics);
        }
    }
}
