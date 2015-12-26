using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SGLibrary.Extend.ControlEx;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.VCControl;

namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    public partial class TchScheduleFm : Form, IScheduleFm
    {
        private TchScheduleGrid Matrix;

        public bool ReadOnly
        {
            get { return true; }
        }

        public TchScheduleFm()
        {
            InitializeComponent();

            Matrix = new TchScheduleGrid(this.panel1);
            //Matrix.Padding = 15;

            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");
        }

        public event dlgOpenScheduleFm OpenScheduleFm;

        private EnTeacher teacher;

        public BaseEntity Entity
        {
            get { return teacher; }
            set { teacher = value as EnTeacher; }
        }

        public void ScheduleUpdate(eScheduleUpdateKind Kind)
        {
            switch (Kind)
            {
                case eScheduleUpdateKind.RefreshMatrix:
                    Matrix.Solution = VC2WinFmApp.DataRule.Solution;
                    BindMatrixCellEvent();
                    break;
                case eScheduleUpdateKind.RefreshAct:
                    DtMatrix<IList<EnLsnAct>> mtx
                        = VC2WinFmApp.Engine.GetTchMatrix(this.teacher);
                    
                    foreach (VcTime time in mtx.eachTime())
                        Matrix[time].Acts = mtx[time]; 

                    break;
                case eScheduleUpdateKind.RefreshRule:
                    foreach (TchScheduleCell cell in Matrix.eachCell())
                        cell.Rule = eRule.common;


                    DtMatrix<Boolean> SlnMatrix
                        = new DtMatrix<bool>(VC2WinFmApp.DataRule.Solution); 
                    IList<VcRuleCell> rules = VC2WinFmApp.DataRule.Rule.GetRules(this.teacher);
                    foreach (VcRuleCell rls in rules)
                        if (SlnMatrix.TestTime(rls.Time))
                            Matrix[rls.Time].Rule = rls.Rule;

                    break;
                case eScheduleUpdateKind.Invalidate:
                    if (!VC2WinFmApp.Engine.EntityIsEnabled(this.teacher))
                        Close();
                    break;
            }
        }

        private void BindMatrixCellEvent()
        {
            foreach (TchScheduleCell cell in Matrix.eachCell())
                cell.DoubleClick += cellDoubleClick;
        }

        private void cellDoubleClick(object sender, EventArgs e)
        {
            TchScheduleCell send = sender as TchScheduleCell;
            if (send.Acts == null || send.Acts.Count == 0)
                return;

            foreach(EnLsnAct act in send.Acts)
                this.OpenScheduleFm(act.ClsLesson.Squad);
        }

        private void CloseTs_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.CloseOtherScheduleFm(this);
        }
    }
}