using System;
using System.Drawing;
using System.Windows.Forms;
using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.WinFormApp.VCControl
{
    internal class SqdScheduleGrid : VcGridV2<SqdScheduleCell>
    {
        public SqdScheduleGrid(Control parent)
            : base(parent)
        {
        }
    }

    internal enum eCellState { common, focused, inDrop, isDropSrc, likeDropSrc };

    internal class SqdScheduleCell : Label, IFcMatrixMatterCell
    {
        public SqdScheduleCell()
        {
            this.AutoSize = false;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.BorderStyle = BorderStyle.Fixed3D;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Int32 ftSize = (this.Height - 10) / 2;
            if (ftSize < 8)
                ftSize = 8;
            if (ftSize > 18)
                ftSize = 18;

            this.Font = new Font(this.Font.FontFamily,
                (float)(ftSize), FontStyle.Bold);
        }

        private VcTime time = new VcTime();

        public VcTime Time
        {
            get { return time; }
        }

        private EnLsnAct act;

        public EnLsnAct Act
        {
            get { return act; }
            set
            {
                act = value;
            }
        }

        public override string ToString()
        {
            return time.ToString() + "  "
                + (Act == null ? "Act: null" : Act.ToString());
        }

        public override void Refresh()
        {
            if (this.Act == null)
                this.Text = this.CellState == eCellState.inDrop ? "***" : "";
            else
            {
                var txt = Act.ClsLesson.Lesson.Course.ToString();
                if (Act.ClsLesson.Teacher != null)
                    txt += " " + Act.ClsLesson.Teacher.Name;
                this.Text = txt;
            }

            Color FColor;
            if (ReadOnly && Act != null)
                FColor = Act.ClsLesson.Lesson.Course.Color;
            else
                FColor = ViewStyle.AdvantageToColor(this.Rule);

            Color BColor = CellState == eCellState.focused || CellState == eCellState.likeDropSrc ?
                ViewStyle.Color_CellFocus
                : ViewStyle.Color_CellDefBackColor;

            if (this.cellState == eCellState.isDropSrc)
            {
                this.ForeColor = BColor;
                this.BackColor = FColor;
            }
            else
            {
                if (CellState == eCellState.likeDropSrc)
                    this.ForeColor = ViewStyle.AdvantageToColor(eRule.crisscross);
                else
                    this.ForeColor = FColor;

                this.BackColor = BColor;
            }

            base.Refresh();
        }

        private eCellState cellState;

        public eCellState CellState
        {
            get { return cellState; }
            set
            {
                cellState = value;
                Refresh();
            }
        }

        private eRule rule;

        public eRule Rule  //规则与优势
        {
            get { return rule; }
            set
            {
                rule = value;
                this.Refresh();
            }
        }

        private Boolean readOnly;

        public Boolean ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                this.Refresh();
            }
        }
    }
}
