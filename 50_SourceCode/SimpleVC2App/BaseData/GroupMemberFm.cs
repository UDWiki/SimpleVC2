using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SGLibrary.Extend;
using SGLibrary.Extend.DB;
using SGLibrary.Framework.GridBind;
using SGLibrary.Framework.GridBind.WinForm;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.EntitySelect;
using Telossoft.SimpleVC.WinFormApp.Facade;
using Telossoft.SimpleVC.WinFormApp.UserInterface;
using SGLibrary.Extend.ControlEx;

namespace Telossoft.SimpleVC.WinFormApp.BaseData
{
    public enum eDataEditState { esUnknow, esBrowse, esEdit, esInsert };

    public partial class GroupMemberFm : Form
    {
        public GroupMemberFm()
        {
            InitializeComponent();
        }

        #region MainGrd 过滤 ====================

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ExGridView.GridFilter(MainGrd, tbFilter.Text.Trim());
        }

        private void tbFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnFilter_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                if (String.IsNullOrEmpty(tbFilter.Text))
                    btnFilter_Click(null, null);
                else
                    tbFilter.Text = "";
        }

        #endregion  ====================

        #region 属性 GrpMbrBF EditState MainDataIsGroup ====================

        private IGrpMbrDataFacade grpMbrBF;
        private Boolean mainDataIsGroup;
        private eDataEditState editState;

        public IGrpMbrDataFacade GrpMbrBF
        {
            get { return grpMbrBF; }
            set
            {
                grpMbrBF = value;
            }
        }

        protected eDataEditState EditState
        {
            get { return editState; }
            set
            {
                editState = value;
                SetControlEnabled();
            }
        }

        protected Boolean MainDataIsGroup
        {
            get { return mainDataIsGroup; }
            set
            {
                SaveNowSlt();
                mainDataIsGroup = value;

                tsbEntityGroupSwitch.Text = "切换到<"
                    + (mainDataIsGroup ? grpMbrBF.Mbr.Kind : grpMbrBF.Grp.Kind)
                    + ">视图";
                gbGroupEntity.Text = mainDataIsGroup ? "成员列表" : "所属组";
                MainBF = mainDataIsGroup ? GrpMbrBF.Grp : GrpMbrBF.Mbr;
                IsCourse = MainBF.NewEty() is EnCourse;
                this.Text = "数据维护: " + MainBF.Kind;

                LoadDataToGrid();
            }
        }

        #endregion  ====================

        private void tsbEntityGroupSwitch_Click(object sender, EventArgs e)
        {
            MainDataIsGroup = !MainDataIsGroup;
        }

        private void GroupMemberFm_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            Debug.Assert(GrpMbrBF != null, "显示前必须设置GrpMbrBF");

            MainGrdBind = new GridViewBind<BaseEntity>(this.MainGrd,
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute { Title = "名称" })));
            AttachGrdBind = new GridViewBind<BaseEntity>(this.AttachGrd,
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute { Title = "名称" })));

            MainDataIsGroup = false;

            new FormMemoryImpl(this,
                VC2WinFmApp.Cfg, "FormRem");  //窗体记忆
        }

        private IEtyDataFacade MainBF;
        private GridViewBind<BaseEntity> MainGrdBind;
        private GridViewBind<BaseEntity> AttachGrdBind;

        private Boolean IsUserEdit = true;
        private Boolean IsCourse;


        private void SetControlEnabled()
        //在有些情况下此方法会被无意义地多次调用，为简化逻辑，通常不管
        {
            Boolean MainDtAtBrowse = EditState == eDataEditState.esBrowse;
            Boolean MainSelectOnlyOne = MainGrdBind.IsOnlyOneSelect;

            tsbEntityGroupSwitch.Enabled = MainDtAtBrowse;
            gbFilter.Enabled = MainDtAtBrowse;

            tsAdd.Enabled = MainDtAtBrowse;
            tsDelete.Enabled = MainDtAtBrowse
                && MainGrd.SelectedRows.Count > 0;

            tsSave.Enabled = (EditState == eDataEditState.esInsert
                || EditState == eDataEditState.esEdit);
            tsCancel.Enabled = tsSave.Enabled;

            tsRule.Enabled = MainSelectOnlyOne && MainDtAtBrowse;
            lbColor.Enabled = MainSelectOnlyOne;

            tbName.Enabled = MainGrd.SelectedRows.Count <= 1
                || EditState == eDataEditState.esInsert
                || EditState == eDataEditState.esEdit;

            btContinueAdd.Enabled = EditState == eDataEditState.esInsert;

            lbColor.Visible = IsCourse && !mainDataIsGroup;
            picGrp.Visible = mainDataIsGroup;

            tsAttachAdd.Enabled = MainSelectOnlyOne && MainDtAtBrowse;
            tsAttachRemove.Enabled = MainSelectOnlyOne && MainDtAtBrowse
                && AttachGrd.SelectedRows.Count > 0;
            tsAttachRule.Enabled = tsAttachRemove.Enabled 
                && AttachGrd.SelectedRows.Count == 1;
        }

        //获取当前活动的实体及组，以便切换实体与组后定位回
        private void SaveNowSlt()
        {
            LastGrp = mainDataIsGroup ?
                MainGrdBind.Current
                : AttachGrdBind.Current;

            LastMbr = mainDataIsGroup ?
                AttachGrdBind.Current
                : MainGrdBind.Current;
        }

        private void MainGrd_SelectionChanged(object sender, EventArgs e)
        {
            IsUserEdit = false;
            try
            {
                if (MainGrdBind.IsOnlyOneSelect) // && MainGrdBind.Current != null)
                {
                    DisplayEty(MainGrdBind.Current as BaseEntity);
                    var rlns = GrpMbrBF.GetRlns(MainGrdBind.Current as BaseEntity);
                    AttachGrdBind.Binding(rlns == null ? new List<BaseEntity>() : new List<BaseEntity>(rlns));
                }
                else
                {
                    DisplayEty(null);
                    AttachGrdBind.Binding(new List<BaseEntity>());
                }
            }
            finally
            {
                IsUserEdit = true;
            }

            SetControlEnabled();
        }

        private void MainGrd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //不需要了，焦点到MainGrd前会处理
                //case Keys.Escape:
                //    if (tsCancel.Enabled)
                //        tsCancel_Click(sender, null);
                //    break;
                case Keys.Insert:
                    if (tsAdd.Enabled)
                        tsAdd_Click(sender, null);
                    break;
                case Keys.Delete:
                    if (tsDelete.Enabled)
                        tsDelete_Click(sender, null);
                    break;
            }
        }

        private void panelGroupMember_Leave(object sender, EventArgs e)
        {
            IsUserEdit = false;
            try
            {
                if (!tsSave.Enabled)
                {
                    EditState = eDataEditState.esBrowse;
                    return;
                }

                if (EditState == eDataEditState.esInsert)
                    //插入，如果是空名字或存在则不插入
                { 
                    BaseEntity Ety = MainBF.NewEty();
                    SetEtyField(Ety);
                    if (String.IsNullOrEmpty(Ety.Name) || MainBF.NameExist(Ety))
                    {
                        EditState = eDataEditState.esBrowse;
                        return;
                    }
                }

                tsSave_Click(sender, e);
            }
            finally
            {
                IsUserEdit = true;
            }
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (EditState == eDataEditState.esInsert)
                    ; //ExControl.ToNextTab(sender, panelEdit.Controls);
                else
                {
                    if (Save())
                        EditState = eDataEditState.esBrowse;
                }

            else if (e.KeyCode == Keys.Escape && tsCancel.Enabled)
                tsCancel_Click(sender, null);
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            //程序自己在改变编辑的文本等则不应改变EditState
            if (!IsUserEdit)
                return;

            tsSave.Enabled = (EditState == eDataEditState.esInsert
                    || EditState == eDataEditState.esEdit)
                    && !String.IsNullOrEmpty(tbName.Text.Trim());

            if (EditState == eDataEditState.esBrowse)
                if (MainGrd.SelectedRows.Count == 0)
                    EditState = eDataEditState.esInsert;
                else if (MainGrd.SelectedRows.Count == 1)
                    EditState = eDataEditState.esEdit;
        }

        private void btContinueAdd_Click(object sender, EventArgs e)
        {
            if (Save())
                tsAdd_Click(sender, e);
        }

        protected void tsAdd_Click(object sender, EventArgs e)
        {
            EditState = eDataEditState.esInsert;

            //连续添加不清空编辑区
            if (sender != btContinueAdd)
                tbName.Text = "";

            tbName.Focus();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (Save())
                EditState = eDataEditState.esBrowse;
        }

        protected void tsCancel_Click(object sender, EventArgs e)
        {
            EditState = eDataEditState.esBrowse;

            MainGrd_SelectionChanged(sender, e);
        }

        protected void DisplayEty(BaseEntity ety)
        {
            this.tbName.Text = ety == null ? "" : ety.Name;
            lbColor.ForeColor = ety != null && IsCourse ? (ety as EnCourse).Color : Color.Black;
        }

        private void MainGrd_DoubleClick(object sender, EventArgs e)
        {
            if (this.tsRule.Enabled)
                tsRule_Click(sender, e);
        }

        protected void SetEtyField(BaseEntity ety)
        {
            ety.Name = tbName.Text.Trim();
            tbName.Text = ety.Name;
            if (IsCourse)
                (ety as EnCourse).Color = lbColor.ForeColor;
        }

        private Boolean Save()
        {
            BaseEntity Ety;
            if (EditState == eDataEditState.esInsert)
                Ety = MainBF.NewEty();
            else
            {
                Ety = MainGrdBind.Current as BaseEntity;
                Ety = Ety.Clone() as BaseEntity;
            }

            SetEtyField(Ety);

            //检测
            if (String.IsNullOrEmpty(Ety.Name))
            {
                ExUI.ShowInfo("名称必须填写");
                tbName.Focus();
                return false;
            }
            if (MainBF.NameExist(Ety)
                && !ExUI.Confirm(MainBF.Kind +": '" + Ety.Name + "'已存在，是否继续？"))
            {
                tbName.Focus();
                return false;
            }

            Ety = MainBF.SaveEty(Ety);
            if (EditState == eDataEditState.esInsert)
                MainGrdBind.Add(Ety);
            else
                MainGrdBind.Refresh();

            return true;
        }

        private void lbColor_DoubleClick(object sender, EventArgs e)
        {
            Debug.Assert(tbName.Enabled, "!tbName.Enabled不应调用此方法");

            ColorDialog Dlg = new ColorDialog();
            Dlg.Color = tbName.ForeColor;

            if (Dlg.ShowDialog() == DialogResult.OK
                && lbColor.ForeColor != Dlg.Color)
            {
                lbColor.ForeColor = Dlg.Color;
                tbName_TextChanged(sender, e);
            }
        }

        private void AttachGrd_DoubleClick(object sender, EventArgs e)
        {
            if (tsAttachAdd.Enabled)
                tsAttachAdd_Click(sender, e);
        }

        private void AttachGrd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert && tsAttachAdd.Enabled)
                tsAttachAdd_Click(sender, e);
            if (e.KeyCode == Keys.Delete && tsAttachRemove.Enabled)
                btnAttachRemove_Click(sender, e);
        }

        private void AttachGrd_SelectionChanged(object sender, EventArgs e)
        {
            SetControlEnabled();
        }

        private void btnAttachRemove_Click(object sender, EventArgs e)
        {
            //数据准备
            BaseEntity MainEty = MainGrdBind.Current as BaseEntity;

            //删除确认
            IList<VcEffect> Effects = new List<VcEffect>();
            foreach (BaseEntity ety in AttachGrdBind.SelectedObjects)
                ExIList.Append<VcEffect>(this.grpMbrBF.EffectOfReleaseRln(MainEty, ety), Effects);
            if (!ConfirmEffectFm.Confirm(Effects))
                return;

            //删除
            foreach (Object obj in AttachGrdBind.SelectedObjects)
                GrpMbrBF.ReleaseRln(MainEty, obj as BaseEntity);

            //界面更新
            MainGrd_SelectionChanged(sender, e);
        }

        private void tsAttachAdd_Click(object sender, EventArgs e)
        {
            //数据准备
            BaseEntity MainEty = MainGrdBind.Current as BaseEntity;
            IList<BaseEntity> OldRlns = new List<BaseEntity>();
            ExIList.Append<BaseEntity>(GrpMbrBF.GetRlns(MainEty), OldRlns);
            IList<BaseEntity> NewRlns = new List<BaseEntity>();
            ExIList.Append<BaseEntity>(OldRlns, NewRlns);

            //交互：用户选择
            if (!this.mainDataIsGroup)
            {
                NewRlns = SelectIF.GroupSelectMulti(GrpMbrBF, NewRlns);
                if (NewRlns == null)
                    return;
            }
            else
            {
                NewRlns = SelectIF.MemberSelectMulti(GrpMbrBF, NewRlns);
                if (NewRlns == null)
                    return;
            }

            //删除/增加确认
            IList<VcEffect> Effects = new List<VcEffect>();
            foreach (BaseEntity ety in OldRlns)
                if (!NewRlns.Contains(ety))
                    ExIList.Append<VcEffect>(this.grpMbrBF.EffectOfReleaseRln(MainEty, ety), Effects);
            foreach (BaseEntity ety in NewRlns)
                if (!OldRlns.Contains(ety))
                    ExIList.Append<VcEffect>(this.grpMbrBF.EffectOfCreateRln(MainEty, ety), Effects);
            if (!ConfirmEffectFm.Confirm(Effects))
                return;

            //删除/增加
            foreach (BaseEntity ety in OldRlns)
                if (!NewRlns.Contains(ety))
                    GrpMbrBF.ReleaseRln(MainEty, ety);
            foreach (BaseEntity ety in NewRlns)
                if (!OldRlns.Contains(ety))
                    GrpMbrBF.CreateRln(MainEty, ety);

            //界面更新
            MainGrd_SelectionChanged(sender, e);
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            //删除确认
            IList<VcEffect> Effects = new List<VcEffect>();
            foreach (BaseEntity ety in MainGrdBind.SelectedObjects)
                ExIList.Append<VcEffect>(this.grpMbrBF.EffectOfDelete(ety), Effects);
            if (!ConfirmEffectFm.Confirm(Effects))
                return;

            //删除
            MainGrd.SelectionChanged -= MainGrd_SelectionChanged;
            try
            {
                IList<BaseEntity> SelectObjects = MainGrdBind.SelectedObjects;
                foreach (BaseEntity ety in SelectObjects)
                    MainBF.Delete(ety);

                MainGrdBind.Remove(SelectObjects);
            }
            finally
            {
                MainGrd.SelectionChanged += MainGrd_SelectionChanged;
            }

            //界面更新
            //if (MainGrd.RowCount == 0)  //删没了后不会触发SelectionChanged
            MainGrd_SelectionChanged(sender, e);
        }

        protected void tsRule_Click(object sender, EventArgs e)
        {
            RuleSetupFm.SetEntityRule(MainGrdBind.Current as BaseEntity);
        }

        private void tsAttachRule_Click(object sender, EventArgs e)
        {
            RuleSetupFm.SetEntityRule(AttachGrdBind.Current as BaseEntity);
        }

        protected Object LastMbr, LastGrp;

        private void LoadDataToGrid()
        {
            MainGrdBind.Binding(this.MainBF.EtyList);

            //if (MainGrdBind.Count == 0)
            //    MainGrd_SelectionChanged(null, null);

            if (MainDataIsGroup)
            {
                MainGrdBind.PositionTo(LastGrp as BaseEntity);
                if (AttachGrdBind.DataSource != null)  //MainGrd无数据时AttachGrd Bind在null上，不可PositionTo
                    AttachGrdBind.PositionTo(LastMbr as BaseEntity);
            }
            else
            {
                MainGrdBind.PositionTo(LastMbr as BaseEntity);
                if (AttachGrdBind.DataSource != null)  //MainGrd无数据时AttachGrd Bind在null上，不可PositionTo
                    AttachGrdBind.PositionTo(LastGrp as BaseEntity);
            }

            EditState = eDataEditState.esBrowse;
        }
    }
}