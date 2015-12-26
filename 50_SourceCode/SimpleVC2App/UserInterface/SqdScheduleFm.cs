using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SGLibrary.Extend;
using SGLibrary.Extend.ControlEx;
using SGLibrary.Framework.GridBind;
using SGLibrary.Framework.GridBind.WinForm;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.VCControl;

namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    internal enum eSchState { common, focused, readOnly, inDrop };

    public partial class SqdScheduleFm : Form, IScheduleFm
    {
        private EnSquad squad;

        public BaseEntity Entity
        {
            get { return squad; }
            set { squad = value as EnSquad; }
        }

        private SqdScheduleGrid Matrix;
        private GridViewBind<EnLsnAct> TaskGridBind;

        public SqdScheduleFm()
        {
            InitializeComponent();

            TaskGridBind = new GridViewBind<EnLsnAct>(grid: TaskGrid,
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Course", new GridBindColumnAttribute { Title = "课程" })));

            Matrix = new SqdScheduleGrid(MainSplit.Panel2);
            //Matrix.Padding = 15;

            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");  //窗体记忆
        }

        private void SqdScheduleFm_Shown(object sender, EventArgs e)
        {
            BindTaskGridEvent();
        }

        public event dlgOpenScheduleFm OpenScheduleFm;

        public void ScheduleUpdate(eScheduleUpdateKind Kind)
        {
            switch (Kind)
            {
                case eScheduleUpdateKind.RefreshMatrix:
                    RefreshMatrix();
                    break;
                case eScheduleUpdateKind.RefreshAct:
                    RefreshAct();
                    RefreshRule();
                    break;
                case eScheduleUpdateKind.RefreshRule:
                    RefreshRule();
                    break;
                case eScheduleUpdateKind.Invalidate:
                    if (!VC2WinFmApp.Engine.EntityIsEnabled(this.squad))
                        Close();
                    break;
            }
        }

        public void RefreshMatrix()
        {
            Matrix.Solution = VC2WinFmApp.DataRule.Solution;
            BindMatrixCellEvent();
        }

        public void RefreshAct()
        {
            var oldCount = TaskGridBind.Count;
            var oldPosition = TaskGridBind.Position;
            this.TaskGridBind.Binding(VC2WinFmApp.Engine.GetFailLsnActs(this.squad));
            if (TaskGridBind.Count == oldCount - 1)
                oldPosition = oldPosition - 1;
            this.TaskGridBind.Position = oldPosition;

            DtMatrix<EnLsnAct> mtx
                = VC2WinFmApp.Engine.GetSqdMatrix(this.squad);
            foreach (VcTime time in mtx.eachTime())
                Matrix[time].Act = mtx[time];
        }

        public void RefreshRule()
        {
            DtMatrix<eRule> rles
                = VC2WinFmApp.Engine.GetSqdRule(this.squad);
            foreach (VcTime time in rles.eachTime())
                Matrix[time].Rule = rles[time];

            VcTime focuseTime = new VcTime();
            foreach (SqdScheduleCell cell in Matrix.eachCell())
                if (cell.CellState == eCellState.focused)
                    cell.Time.CopyFieldTo(focuseTime);

            RenewRuleGrid(focuseTime);
        }

        private Boolean readOnly;

        public Boolean ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;

                foreach (SqdScheduleCell cell in Matrix.eachCell())
                    cell.ReadOnly = readOnly;
            }
        }

        private void BindTaskGridEvent()
        {
            this.TaskGrid.AllowDrop = true;

            this.TaskGrid.DragDrop += thisDragDrop;
            this.TaskGrid.DragEnter += thisDragEnter;
            this.TaskGrid.QueryContinueDrag += thisQueryContinueDrag;

            this.TaskGrid.MouseDown += thisMouseDown;
            this.TaskGrid.MouseMove += thisMouseMove;
        }

        private void BindMatrixCellEvent()
        {
            foreach (SqdScheduleCell cell in Matrix.eachCell())
            {
                cell.AllowDrop = true;

                cell.DragDrop += thisDragDrop;
                cell.DragEnter += thisDragEnter;
                cell.QueryContinueDrag += thisQueryContinueDrag;

                cell.MouseDown += thisMouseDown;
                cell.MouseMove += thisMouseMove;
                cell.MouseClick += thisCellClick;
                cell.MouseDoubleClick += thisCellDoubleClick;
            }
        }

        private eSchState schState;

        private void ToInDropState(EnLsnAct Act, VcTime sTime)
        {
            DtMatrix<eRule> rles
                = VC2WinFmApp.Engine.GetActChangeRule(this.squad, Act, sTime);
            foreach (SqdScheduleCell cell in Matrix.eachCell())
            {
                //存在重复更新，为简化逻辑，不要管
                if (cell.Time == sTime)
                    cell.CellState = eCellState.isDropSrc;
                else if (cell.Act != null && cell.Act.ClsLesson == Act.ClsLesson)
                    cell.CellState = eCellState.likeDropSrc;
                else
                {
                    cell.CellState = eCellState.inDrop;
                    cell.Rule = rles[cell.Time];
                }
            }

            RenewRuleGrid(sTime);
            sTime.CopyFieldTo(DropSrcTime);
            schState = eSchState.inDrop;  //进入拖放状态
            if (Act == null || Act.ClsLesson == null)
                VC2WinFmApp.MessageSwitch.SetLastTch(null, null);
            else
                VC2WinFmApp.MessageSwitch.SetLastTch(Act.ClsLesson.Teacher, Act.Time);
        }

        private void thisQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (e.Action == DragAction.Drop)
                ToCommonState(DropSrcTime);
        }

        private void ToCommonState(VcTime focuseTime)
        {
            EnLsnAct Act = null;
            DtMatrix<eRule> rles
                = VC2WinFmApp.Engine.GetSqdRule(this.squad);
            foreach (SqdScheduleCell cell in Matrix.eachCell())
            {
                if (cell.Time == focuseTime)
                {
                    cell.CellState = eCellState.focused;
                    Act = cell.Act;
                }
                else
                    cell.CellState = eCellState.common;

                cell.Rule = rles[cell.Time];
            }

            RenewRuleGrid(focuseTime);

            schState = eSchState.common;
            if (Act == null || Act.ClsLesson == null)
                VC2WinFmApp.MessageSwitch.SetLastTch(null, null);
            else
                VC2WinFmApp.MessageSwitch.SetLastTch(Act.ClsLesson.Teacher, Act.Time);
        }

        private void ToFocusedState(EnLsnAct sendAct)
        {
            foreach (SqdScheduleCell cell in Matrix.eachCell())
                if (cell.Act != null && cell.Act.ClsLesson == sendAct.ClsLesson)
                    cell.CellState = eCellState.focused;
                else
                    cell.CellState = eCellState.common;

            RenewRuleGrid(new VcTime());
            this.schState = eSchState.focused;

            if (sendAct == null || sendAct.ClsLesson == null)
                VC2WinFmApp.MessageSwitch.SetLastTch(null, null);
            else
                VC2WinFmApp.MessageSwitch.SetLastTch(sendAct.ClsLesson.Teacher, sendAct.Time);
        }

        private VcTime DropSrcTime = new VcTime();

        private void RenewRuleGrid(VcTime focuseTime)
        {
            IList<VcActEtyRelation> clashRules
                = VC2WinFmApp.Engine.GetSqdClash(this.squad, focuseTime);

            IList<VcActEtyRelation> clashRules2 = new List<VcActEtyRelation>();
            foreach (VcActEtyRelation rln in clashRules)
                if (rln.Relation == eActEtyRelation.teach)
                    clashRules2.Add(rln);
            foreach (VcActEtyRelation rln in clashRules)
                if (rln.Relation == eActEtyRelation.clash)
                    clashRules2.Add(rln);
            foreach (VcActEtyRelation rln in clashRules)
                if (rln.Relation == eActEtyRelation.rule && rln.Rule == eRule.crisscross)
                    clashRules2.Add(rln);
            foreach (VcActEtyRelation rln in clashRules)
                if (rln.Relation == eActEtyRelation.rule && rln.Rule != eRule.crisscross)
                    clashRules2.Add(rln);

            RuleGrid.DataSource = clashRules2;
        }

        private void thisCellClick(object sender, EventArgs e)
        {
            this.ToCommonState((sender as SqdScheduleCell).Time);
        }

        private void thisCellDoubleClick(object sender, MouseEventArgs e)
        {
            SqdScheduleCell sendCell = sender as SqdScheduleCell;

            if (sendCell.Act == null)
                this.ToCommonState(sendCell.Time);
            else
            {
                this.ToFocusedState(sendCell.Act);
                if (sendCell.Act.ClsLesson.Teacher != null)
                    this.OpenScheduleFm(sendCell.Act.ClsLesson.Teacher);
            }
        }

        private void RuleGrid_DoubleClick(object sender, EventArgs e)
        {
            CurrencyManager cm = (CurrencyManager)this.BindingContext[this.RuleGrid.DataSource];
            if (cm.Count == 0)
                return;

            Object obj = cm.Current;
            if (obj == null || !(obj is VcActEtyRelation))
                return;

            VcActEtyRelation actEtyRln = obj as VcActEtyRelation;
            if (actEtyRln.Relation == eActEtyRelation.rule)
                RuleSetupFm.SetEntityRule(actEtyRln.Entity);
            else if (actEtyRln.Relation == eActEtyRelation.clash)
                this.OpenScheduleFm((actEtyRln.Entity as EnClsLesson).Squad);
            else  //教师
                this.OpenScheduleFm(actEtyRln.Entity);
        }

        private void RuleGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 2)
                return;
            VcActEtyRelation rln
                 = RuleGrid.Rows[e.RowIndex].DataBoundItem as VcActEtyRelation;

            e.CellStyle.BackColor = ViewStyle.RuleToColor(rln.Rule);
        }

        private void RuleGrid_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn clm in RuleGrid.Columns)
                clm.Visible = clm.DataPropertyName == "Entity"
                    || clm.DataPropertyName == "RelationDsp";
        }

        private void CMenuScheduleFail_Click(object sender, EventArgs e)
        {
            IList<EnSquad> Squads = new List<EnSquad>();
            Squads.Add(this.squad);
            VC2WinFmApp.Engine.Automatic(Squads, true, false, false);
        }

        private void TaskGridMenu_Opening(object sender, CancelEventArgs e)
        {
            if (this.ReadOnly)
                e.Cancel = true;
        }

        private void thisDragDrop(object sender, DragEventArgs e)
        {
            EnLsnAct act = e.Data.GetData(typeof(EnLsnAct)) as EnLsnAct;
            VcTime tTime = new VcTime();
            if (sender is SqdScheduleCell)
                (sender as SqdScheduleCell).Time.CopyFieldTo(tTime);

            //Move将触发更新，存在重复更新，为简化逻辑，不要管
            ToCommonState(tTime);
            VC2WinFmApp.Engine.Move(this.squad, act, tTime);
        }

        private void thisDragEnter(object sender, DragEventArgs e)
        {
            //不是从本窗体拖来的
            if ((e.Data.GetData(typeof(EnLsnAct)) as EnLsnAct).ClsLesson.Squad 
                != (ExControl.GetControlParentForm(sender as Control) as SqdScheduleFm).Entity)
            {
                e.Effect = DragDropEffects.None;
                VC2WinFmApp.MessageSwitch.SetFocusTch(null);
                return;
            }

            if (DropSrcTime.HasValue   //源自课表中拖，总是可以的
                || (sender is SqdScheduleCell && (sender as SqdScheduleCell).Act == null))  //拖入空Cell，总是可以的
            {
                e.Effect = DragDropEffects.Move;

                var focusAct = sender is SqdScheduleCell ? (sender as SqdScheduleCell).Act : null;
                VC2WinFmApp.MessageSwitch.SetFocusTch(focusAct == null || focusAct.ClsLesson == null ? null : focusAct.ClsLesson.Teacher);
            }
            else
            {
                e.Effect = DragDropEffects.None;
                VC2WinFmApp.MessageSwitch.SetFocusTch(null);
            }
        }

        private Point DownXY;
        private void thisMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                DownXY = e.Location;
        }

        private void thisMouseMove(object sender, MouseEventArgs e)
        {
            CheckFocusTch(sender, e.Location);  //取当前聚焦的

            if (ReadOnly
                || (schState != eSchState.common && schState != eSchState.focused)
                || e.Button != MouseButtons.Left
                || schState == eSchState.inDrop)
                return;

            if (Math.Abs(e.X - DownXY.X) < 5 && Math.Abs(e.Y - DownXY.Y) < 5)
                return;

            EnLsnAct Act = null;
            VcTime sTime = new VcTime();

            if (sender == TaskGrid && TaskGridBind.Count > 0)
            {
                DataGridView.HitTestInfo hi = TaskGrid.HitTest(e.X, e.Y);
                TaskGridBind.Position = hi.RowIndex;

                TaskGrid.Refresh(); //有用的，让TaskGrid更新当前选中行为被选中状态
                Act = TaskGridBind.Current as EnLsnAct;
            }
            else if (sender is SqdScheduleCell)
            {
                Act = (sender as SqdScheduleCell).Act;
                (sender as SqdScheduleCell).Time.CopyFieldTo(sTime);
            }

            if (Act != null)
            {
                ToInDropState(Act, sTime);

                (sender as Control).DoDragDrop(Act, DragDropEffects.Move);
            }
        }

        private void CheckFocusTch(object sender, Point location)
        {
            Object sdr = this.MainSplit.Panel2.GetChildAtPoint(this.MainSplit.Panel2.PointToClient((sender as Control).PointToScreen(location)));

            if (sdr != null && sdr is SqdScheduleCell)
            {
                var focusAct = (sdr as SqdScheduleCell).Act;
                if (focusAct != null && focusAct.ClsLesson != null && focusAct.ClsLesson.Teacher != null)
                    VC2WinFmApp.MessageSwitch.SetFocusTch(focusAct.ClsLesson.Teacher);
                else
                    VC2WinFmApp.MessageSwitch.SetFocusTch(null);
            }
        }

        private void CloseTs_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.CloseOtherScheduleFm(this);
        }
    }
}