using System;
using System.Windows.Forms;
using SGLibrary.Extend.ControlEx;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.VCControl;

namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    public partial class TopScheduleFm : Form
    {
        private void TopScheduleFm_Load(object sender, EventArgs e)
        {
            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");

            STchLab.Text = "";
            TTchLab.Text = "";
            ScheduleUpdate(eScheduleUpdateKind.RefreshMatrix);
            ScheduleUpdate(eScheduleUpdateKind.RefreshAct);
        }

        private EnTeacher _LastTch;
        public EnTeacher LastTch
        {
            get
            {
                return _LastTch;
            }
            set
            {
                if (_LastTch == value)
                    return;

                _LastTch = value;
                STchLab.Text = _LastTch == null ? "" : _LastTch.Name;
                ScheduleUpdate(eScheduleUpdateKind.RefreshMatrix);
                ScheduleUpdate(eScheduleUpdateKind.RefreshAct);
            }
        }

        public VcTime _LastTime;
        public VcTime LastTime
        {
            get
            {
                return _LastTime;
            }
            set
            {
                //if (_LastTime != null && _LastTime.HasValue)
                //    MatrixLast[_LastTime].BackColor = ViewStyle.Color_CellDefBackColor;
                _LastTime = value;
                //if (_LastTime != null && _LastTime.HasValue)
                //    MatrixLast[_LastTime].BackColor = ViewStyle.Color_CellFocus;
            }
        }

        private EnTeacher _FocusTch;
             public EnTeacher FocusTch
        {
            get
            {
                return _FocusTch;
            }
            set
            {
                if (_FocusTch == value)
                    return;

                _FocusTch = value;
                TTchLab.Text = _FocusTch == null ? "" : _FocusTch.Name;

                ScheduleUpdate(eScheduleUpdateKind.RefreshMatrix);
                ScheduleUpdate(eScheduleUpdateKind.RefreshAct);
            }
        }

        private TchScheduleGrid MatrixLast;
        private TchScheduleGrid MatrixFocus;

        public TopScheduleFm()
        {
            InitializeComponent();

            MatrixLast = new TchScheduleGrid(this.Spanel);
            MatrixLast.Padding = 5;
            MatrixFocus = new TchScheduleGrid(this.Tpanel);
            MatrixFocus.Padding = 5;

            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");
        }

        public void ScheduleUpdate(eScheduleUpdateKind Kind)
        {
            switch (Kind)
            {
                case eScheduleUpdateKind.RefreshMatrix:
                    MatrixLast.Solution = VC2WinFmApp.DataRule.Solution;
                    MatrixFocus.Solution = VC2WinFmApp.DataRule.Solution;
                    break;
                
                //特别的响应，即使RefreshRule也要更新课表，为处理调课完成后
                case eScheduleUpdateKind.RefreshAct:  
                    var mtxLast = VC2WinFmApp.Engine.GetTchMatrix(this._LastTch);
                    var mtxFocus = VC2WinFmApp.Engine.GetTchMatrix(this._FocusTch);

                    foreach (VcTime time in mtxLast.eachTime())
                    {
                        MatrixLast[time].Acts = mtxLast[time];
                        MatrixFocus[time].Acts = mtxFocus[time];
                    }
                    break;

                case eScheduleUpdateKind.RefreshRule:
                    foreach (TchScheduleCell cell in MatrixLast.eachCell())
                        cell.Rule = eRule.common;
                    foreach (TchScheduleCell cell in MatrixFocus.eachCell())
                        cell.Rule = eRule.common;


                    var SlnMatrix = new DtMatrix<bool>(VC2WinFmApp.DataRule.Solution);
                    var ruleS = VC2WinFmApp.DataRule.Rule.GetRules(this._LastTch);
                    var ruleT = VC2WinFmApp.DataRule.Rule.GetRules(this._FocusTch);
                    foreach (var rls in ruleS)
                        if (SlnMatrix.TestTime(rls.Time))
                            MatrixLast[rls.Time].Rule = rls.Rule;
                    foreach (var rls in ruleT)
                        if (SlnMatrix.TestTime(rls.Time))
                            MatrixFocus[rls.Time].Rule = rls.Rule;

                    break;
                case eScheduleUpdateKind.Invalidate:
                    if (_LastTch != null && !VC2WinFmApp.Engine.EntityIsEnabled(_LastTch))
                        LastTch = null;
                    if (_FocusTch != null && !VC2WinFmApp.Engine.EntityIsEnabled(_FocusTch))
                        FocusTch = null;

                    break;
            }
        }
    }
}
