using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.WinFormApp.VCControl
{
    public class HeadButton : Button, IFcMatrixBorderCell
    {
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            float ftSize = DrawComm.SuitFontSize(this.Height - 10);
            if (ftSize < 8)
                ftSize = 8;
            if (ftSize > 18)
                ftSize = 18;

            this.Font = new Font(this.Font.FontFamily,
                (float)(ftSize), FontStyle.Bold);
        }
    }

    public class VcGridV2<T>
        where T : Control, IFcMatrixMatterCell, new()
    {
        public class FcMatrixGrid : FcMatrix<HeadButton, T>
        {
            public FcMatrixGrid(Control parent, EnSolution solution)
                : base(solution)
            {
                foreach (T t in this.eachCell())
                    t.Parent = parent;
                foreach (HeadButton hb in this.eachBorderCell())
                    hb.Parent = parent;
            }
        }


        private Control Parent;

        public VcGridV2(Control parent)
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

                if (Cells != null)
                    Cells.Dispose();

                Cells = new FcMatrixGrid(Parent, solution);
                //Cells.HeighWidthProportionLow = 40;
                //Cells.HeighWidthProportionHigh = 60;

                if (Parent.Visible)
                    ResizeGrid(null, null);
            }
        }

        private FcMatrixGrid Cells;

        private void ParentVisible(object sender, EventArgs e)
        {
            if (Parent.Visible)
                ResizeGrid(sender, e);
        }

        public IEnumerable<T> eachCell()
        {
            return Cells.eachCell();
        }

        public T this[VcTime time]
        {
            get
            {
                return Cells[time];
            }
        }

        private void ResizeGrid(object sender, EventArgs e)
        {
            if (Cells == null)
                return;

            foreach (T t in Cells.eachCell())
                t.Visible = false;
            foreach (HeadButton hb in Cells.eachBorderCell())
                hb.Visible = false;

            Cells.Rect = new Rectangle(padding, padding,
                    Parent.ClientSize.Width - padding * 2, Parent.ClientSize.Height - padding * 2);

            if (Parent.Visible)
            {
                foreach (T t in Cells.eachCell())
                    t.Visible = true;
                foreach (HeadButton hb in Cells.eachBorderCell())
                    hb.Visible = true;
            }
        }
    }
}
