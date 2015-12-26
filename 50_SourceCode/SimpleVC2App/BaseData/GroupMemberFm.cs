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

        #region MainGrd ���� ====================

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

        #region ���� GrpMbrBF EditState MainDataIsGroup ====================

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

                tsbEntityGroupSwitch.Text = "�л���<"
                    + (mainDataIsGroup ? grpMbrBF.Mbr.Kind : grpMbrBF.Grp.Kind)
                    + ">��ͼ";
                gbGroupEntity.Text = mainDataIsGroup ? "��Ա�б�" : "������";
                MainBF = mainDataIsGroup ? GrpMbrBF.Grp : GrpMbrBF.Mbr;
                IsCourse = MainBF.NewEty() is EnCourse;
                this.Text = "����ά��: " + MainBF.Kind;

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
            Debug.Assert(GrpMbrBF != null, "��ʾǰ��������GrpMbrBF");

            MainGrdBind = new GridViewBind<BaseEntity>(this.MainGrd,
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute { Title = "����" })));
            AttachGrdBind = new GridViewBind<BaseEntity>(this.AttachGrd,
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute { Title = "����" })));

            MainDataIsGroup = false;

            new FormMemoryImpl(this,
                VC2WinFmApp.Cfg, "FormRem");  //�������
        }

        private IEtyDataFacade MainBF;
        private GridViewBind<BaseEntity> MainGrdBind;
        private GridViewBind<BaseEntity> AttachGrdBind;

        private Boolean IsUserEdit = true;
        private Boolean IsCourse;


        private void SetControlEnabled()
        //����Щ����´˷����ᱻ������ض�ε��ã�Ϊ���߼���ͨ������
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

        //��ȡ��ǰ���ʵ�弰�飬�Ա��л�ʵ�������λ��
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
                //����Ҫ�ˣ����㵽MainGrdǰ�ᴦ��
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
                    //���룬����ǿ����ֻ�����򲻲���
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
            //�����Լ��ڸı�༭���ı�����Ӧ�ı�EditState
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

            //������Ӳ���ձ༭��
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

            //���
            if (String.IsNullOrEmpty(Ety.Name))
            {
                ExUI.ShowInfo("���Ʊ�����д");
                tbName.Focus();
                return false;
            }
            if (MainBF.NameExist(Ety)
                && !ExUI.Confirm(MainBF.Kind +": '" + Ety.Name + "'�Ѵ��ڣ��Ƿ������"))
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
            Debug.Assert(tbName.Enabled, "!tbName.Enabled��Ӧ���ô˷���");

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
            //����׼��
            BaseEntity MainEty = MainGrdBind.Current as BaseEntity;

            //ɾ��ȷ��
            IList<VcEffect> Effects = new List<VcEffect>();
            foreach (BaseEntity ety in AttachGrdBind.SelectedObjects)
                ExIList.Append<VcEffect>(this.grpMbrBF.EffectOfReleaseRln(MainEty, ety), Effects);
            if (!ConfirmEffectFm.Confirm(Effects))
                return;

            //ɾ��
            foreach (Object obj in AttachGrdBind.SelectedObjects)
                GrpMbrBF.ReleaseRln(MainEty, obj as BaseEntity);

            //�������
            MainGrd_SelectionChanged(sender, e);
        }

        private void tsAttachAdd_Click(object sender, EventArgs e)
        {
            //����׼��
            BaseEntity MainEty = MainGrdBind.Current as BaseEntity;
            IList<BaseEntity> OldRlns = new List<BaseEntity>();
            ExIList.Append<BaseEntity>(GrpMbrBF.GetRlns(MainEty), OldRlns);
            IList<BaseEntity> NewRlns = new List<BaseEntity>();
            ExIList.Append<BaseEntity>(OldRlns, NewRlns);

            //�������û�ѡ��
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

            //ɾ��/����ȷ��
            IList<VcEffect> Effects = new List<VcEffect>();
            foreach (BaseEntity ety in OldRlns)
                if (!NewRlns.Contains(ety))
                    ExIList.Append<VcEffect>(this.grpMbrBF.EffectOfReleaseRln(MainEty, ety), Effects);
            foreach (BaseEntity ety in NewRlns)
                if (!OldRlns.Contains(ety))
                    ExIList.Append<VcEffect>(this.grpMbrBF.EffectOfCreateRln(MainEty, ety), Effects);
            if (!ConfirmEffectFm.Confirm(Effects))
                return;

            //ɾ��/����
            foreach (BaseEntity ety in OldRlns)
                if (!NewRlns.Contains(ety))
                    GrpMbrBF.ReleaseRln(MainEty, ety);
            foreach (BaseEntity ety in NewRlns)
                if (!OldRlns.Contains(ety))
                    GrpMbrBF.CreateRln(MainEty, ety);

            //�������
            MainGrd_SelectionChanged(sender, e);
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            //ɾ��ȷ��
            IList<VcEffect> Effects = new List<VcEffect>();
            foreach (BaseEntity ety in MainGrdBind.SelectedObjects)
                ExIList.Append<VcEffect>(this.grpMbrBF.EffectOfDelete(ety), Effects);
            if (!ConfirmEffectFm.Confirm(Effects))
                return;

            //ɾ��
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

            //�������
            //if (MainGrd.RowCount == 0)  //ɾû�˺󲻻ᴥ��SelectionChanged
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
                if (AttachGrdBind.DataSource != null)  //MainGrd������ʱAttachGrd Bind��null�ϣ�����PositionTo
                    AttachGrdBind.PositionTo(LastMbr as BaseEntity);
            }
            else
            {
                MainGrdBind.PositionTo(LastMbr as BaseEntity);
                if (AttachGrdBind.DataSource != null)  //MainGrd������ʱAttachGrd Bind��null�ϣ�����PositionTo
                    AttachGrdBind.PositionTo(LastGrp as BaseEntity);
            }

            EditState = eDataEditState.esBrowse;
        }
    }
}