using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using SGLibrary.Extend;
using SGLibrary.Extend.ControlEx;
using SGLibrary.Framework.GridBind;
using SGLibrary.Framework.GridBind.WinForm;
using SGLibrary.Framework.Log;
using Telossoft.SimpleVC.BizLogic.DataRule;
using Telossoft.SimpleVC.BizLogic.EngineV2;
using Telossoft.SimpleVC.Dac;
using Telossoft.SimpleVC.Export;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.Assist;
using Telossoft.SimpleVC.WinFormApp.EntitySelect;
using Telossoft.SimpleVC.WinFormApp.Facade;
using Telossoft.SimpleVC.WinFormApp.Print;
using Telossoft.SimpleVC.WinFormApp.UserInterface;
using System.IO;

namespace Telossoft.SimpleVC.WinFormApp
{
    public partial class VC2MainFm : Form
    {
        public VC2MainFm()
        {
            InitializeComponent();
        }

        private void MainFm_Load(object sender, EventArgs e)
        {
            this.WindowState = ExDbg.IsAttached ? FormWindowState.Normal : FormWindowState.Maximized;

            VC2WinFmApp.ErrorLog = new SimpleLogImpl_Text(ExApp_WinFm.GetAssistFileName("Log", "_Error.Log"), true, 10 * 1000, LogLevel.All);

            String connStr;
            var dbFile = Application.StartupPath + "\\VC.mdb";
            if (new FileInfo(dbFile).Exists)
                connStr = String.Format(VC2WinFmApp.cConnStr, dbFile);
            else
            {
                dbFile = Application.StartupPath + "\\..\\..\\VC.mdb";
                connStr = String.Format(VC2WinFmApp.cConnStr, dbFile);
            }

            var dac = new DataAccessImpl(connStr, VC2WinFmApp.ErrorLog);
            VC2WinFmApp.Cfg = dac.Config;

            VC2WinFmApp.MainFm = this;
            VC2WinFmApp.DataRule = new DataRuleImpl(dac);
            VC2WinFmApp.Engine = new EngineV2(VC2WinFmApp.DataRule);
            VC2WinFmApp.DataFacade = new DataFacadeImpl(VC2WinFmApp.DataRule);
            VC2WinFmApp.MessageSwitch = new MessageSwitch();

            VC2WinFmApp.Engine.GroupChange += VC2WinFmApp.MessageSwitch.OnGroupChange;
            VC2WinFmApp.Engine.FailActChange += VC2WinFmApp.MessageSwitch.OnFailActChange;
            VC2WinFmApp.Engine.ModifiedChange += VC2WinFmApp.MessageSwitch.OnModifiedChange;
            VC2WinFmApp.Engine.ScheduleUpdate += VC2WinFmApp.MessageSwitch.OnScheduleUpdate;

            ConfigViewFm.LoadViewStyle();
            this.Text = VC2WinFmApp.Name;
            AddExportMenu();

            DataGrpBind = new GridViewBind<BaseEntity>(GroupGrid,
                columnMng: new GridBindColumnMngImpl_Appoint(
              new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute { Title = "名称" })));
            DataMbrBind = new GridViewBind<BaseEntity>(MemberGrid,
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute { Title = "名称" })));
            FailActBind = new GridViewBind<EnFailAct>(FailActGrid,
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute { Title = "任务名称" })));

            Application.DoEvents();
            VC2WinFmApp.Engine.Init();
            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");

            try
            {
                if (!ExDbg.IsAttached)
                    Process.Start(VC2WinFmApp.ProductHomePage);
            }
            catch
            { 
            }
        }

        private void MainFm_Shown(object sender, EventArgs e)
        {
            VC2WinFmApp.Engine.InitUI();
        }

        private void MMenuSln_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.MessageSwitch.SetSlnProperty();
        }

        private void MMenuCourse_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.MessageSwitch.CrsDataMng();
        }

        private void MMenuTeacher_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.MessageSwitch.TchDataMng();
        }

        private void MMenuSquad_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.MessageSwitch.SqdDataMng();
        }

        private void MMenuLessonMng_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.MessageSwitch.LsnDataMng();
        }

        private GridViewBind<BaseEntity> DataGrpBind;
        private GridViewBind<BaseEntity> DataMbrBind;
        private GridViewBind<EnFailAct> FailActBind;

        private void GroupGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGrpBind.Current == null)
            {
                DataMbrBind.Binding(null);
                return;
            }

            DataMbrBind.Binding(VC2WinFmApp.Engine.GetEnabledMember(DataGrpBind.Current as BaseEntity));
            MemberGrid_SelectionChanged(null, null);
        }

        private void SetControlEnable()
        {
            var actFm = this.ActiveMdiChild as IScheduleFm;

            //文件
            Boolean hasCurrent = false;
            if (this.MainTab.SelectedTab == ListPage)
                hasCurrent = DataMbrBind.Current != null;
            else if (this.MainTab.SelectedTab == DutyPage)
                hasCurrent = FailActBind.Current != null;
            MMenuOpen.Enabled = hasCurrent;
            OpenTs.Enabled = MMenuOpen.Enabled;

            MMenuClose.Enabled = actFm != null;
            //所有子窗体关闭后MMenuCloseAll无效，想不到简单的方法
            //MMenuCloseAll.Enabled = this.MdiChildren.Length > 0;
            MMenuCloseAll.Enabled = MMenuWindow.DropDownItems.Count > 0;
            MMenuPrint.Enabled = actFm != null;
            PrintTs.Enabled = MMenuPrint.Enabled;
            MMenuPrintPreview.Enabled = MMenuPrint.Enabled;

            //编辑
            Boolean actFmIsModified = actFm != null
                && VC2WinFmApp.Engine.IsModified(actFm.Entity);
            MMenuEdit.Enabled = !VC2WinFmApp.DataRule.ReadOnly;

            MMenuSave.Enabled = actFmIsModified;
            SaveTs.Enabled = MMenuSave.Enabled;
            MMenuSaveAll.Enabled = VC2WinFmApp.Engine.Modified;
            SaveAllTs.Enabled = MMenuSaveAll.Enabled;
            MMenuCancel.Enabled = actFmIsModified;
            CancelTs.Enabled = MMenuCancel.Enabled;
            MMenuCancelAll.Enabled = VC2WinFmApp.Engine.Modified;

            MMenuStreamline.Enabled = !VC2WinFmApp.DataRule.ReadOnly && actFm != null && !actFm.ReadOnly;
            StreamlineTs.Enabled = MMenuStreamline.Enabled;
            MMenuSchedule.Enabled = !VC2WinFmApp.DataRule.ReadOnly && actFm != null && !actFm.ReadOnly;
            ScheduleTs.Enabled = MMenuSchedule.Enabled;

            MMenuStreamlineSome.Enabled = !VC2WinFmApp.DataRule.ReadOnly;
            MMenuScheduleSome.Enabled = !VC2WinFmApp.DataRule.ReadOnly;

            //基础数据
            MMenuData.Enabled = !VC2WinFmApp.DataRule.ReadOnly && !VC2WinFmApp.Engine.Modified;

            //课务数据
            MMenuLesson.Enabled = !VC2WinFmApp.DataRule.ReadOnly && !VC2WinFmApp.Engine.Modified;

            //工具
            MMenuSysInit.Enabled = !VC2WinFmApp.DataRule.ReadOnly && !VC2WinFmApp.Engine.Modified;
            MMenuDataWizard.Enabled = !VC2WinFmApp.DataRule.ReadOnly && !VC2WinFmApp.Engine.Modified;

            MMEdited.Checked = !VC2WinFmApp.DataRule.ReadOnly;

            MMEdited.Enabled = !VC2WinFmApp.Engine.Modified;

            foreach (ToolStripItem item in ExportMenuList)
                item.Enabled = actFm != null;
        }

        private void MainFm_MdiChildActivate(object sender, EventArgs e)
        {
            this.SetControlEnable();
        }

        private void MMenuOpen_Click(object sender, EventArgs e)
        {
            BaseEntity Ety = null;

            if (this.MainTab.SelectedTab == ListPage)
            {
                if (DataMbrBind.Current != null)
                    Ety = DataMbrBind.Current as BaseEntity;
            }
            else if (this.MainTab.SelectedTab == DutyPage)
                if (FailActBind.Current != null)
                    Ety = (FailActBind.Current as EnFailAct).Act.ClsLesson.Squad;

            if (Ety != null)
                OpenScheduleFm(Ety);

        }

        private void ItemMenuClick(object sender, EventArgs e)
        {
            Form Fm = (sender as ToolStripItem).Tag as Form;
            if (Fm.WindowState == FormWindowState.Minimized)
                Fm.WindowState = FormWindowState.Normal;

            Fm.Activate();
        }

        private void ItemFormClosing(object sender, FormClosingEventArgs e)
        {
            BaseEntity ety = (sender as IScheduleFm).Entity;
            if (VC2WinFmApp.Engine.IsModified(ety))
                switch (ExUI.ConfirmYesNoCancel("课表: " + ety + " 已经修改，是否保存？"))
                {
                    case DialogResult.Yes:
                        VC2WinFmApp.Engine.Save(ety as EnSquad);
                        break;

                    case DialogResult.No:
                        VC2WinFmApp.Engine.Cancel(ety as EnSquad);
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }

            RemoveItem(ety);
        }

        private void RemoveItem(BaseEntity entity)
        {
            ToolStripItem item = null;
            foreach (ToolStripItem itm in MMenuWindow.DropDownItems)
                if ((itm.Tag as IScheduleFm).Entity == entity)
                {
                    item = itm;
                    break;
                }

            Debug.Assert(item != null, "CloseFm时没找到Fm");

            MMenuWindow.DropDownItems.Remove(item);
            SetControlEnable();
        }

        private void MMenuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainFm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VC2WinFmApp.ErrorLog.End();
        }
        
        internal const int WM_CLOSE = 0x10;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != WM_CLOSE 
                || !VC2WinFmApp.Engine.Modified)
            {
                base.WndProc(ref m);
                return;
            }

            switch (ExUI.ConfirmYesNoCancel("课表被修改，是否保存？"))
            {
                case DialogResult.Yes:
                    VC2WinFmApp.Engine.SaveAll(false);
                    base.WndProc(ref m);
                    break;

                case DialogResult.No:
                    foreach (Form cfm in this.MdiChildren)
                        cfm.FormClosing -= ItemFormClosing;
                    base.WndProc(ref m);
                    break;

                case DialogResult.Cancel:
                    break;
            }
        }

        private void MemberGrid_SelectionChanged(object sender, EventArgs e)
        {
            this.SetControlEnable();
        }

        private void SetReadOnly(Boolean readOnly)
        {
            VC2WinFmApp.DataRule.ReadOnly = readOnly;
            foreach (Form cfm in this.MdiChildren)
                if (cfm is SqdScheduleFm)
                    (cfm as SqdScheduleFm).ReadOnly = VC2WinFmApp.DataRule.ReadOnly;

            this.SetControlEnable();
        }

        private void tsView_Click(object sender, EventArgs e)
        {
            SetReadOnly(true);
        }

        private void tsHandle_Click(object sender, EventArgs e)
        {
            SetReadOnly(false);
        }

        private void MMEdited_Click(object sender, EventArgs e)
        {
            SetReadOnly(!VC2WinFmApp.DataRule.ReadOnly);
        }

        private void MMenuSave_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.Engine.Save((ActiveMdiChild as IScheduleFm).Entity as EnSquad);
        }

        private void MMenuSaveAll_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.Engine.SaveAll(true);
        }

        private void MMenuCancel_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.Engine.Cancel((ActiveMdiChild as IScheduleFm).Entity as EnSquad);
        }

        private void MMenuCancelAll_Click(object sender, EventArgs e)
        {
            VC2WinFmApp.Engine.CancelAll(true);
        }

        private void MainTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlEnable();
        }

        private void MMenuClose_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.Close();

            this.SetControlEnable();
        }

        private void MMenuCloseAll_Click(object sender, EventArgs e)
        {
            if (VC2WinFmApp.Engine.Modified)
                switch (ExUI.ConfirmYesNoCancel("课表已经更改，是否保存？"))
                {
                    case DialogResult.Yes:
                        this.MMenuSaveAll_Click(null, null);  //低效简单的做法，无意义地刷新了界面
                        break;
                    case DialogResult.No:
                        this.MMenuCancelAll_Click(null, null);  //低效简单的做法，无意义地刷新了界面
                        break;
                    case DialogResult.Cancel:
                        return;
                }

            for (Int32 i = this.MdiChildren.Length - 1; i >= 0; i--)
                this.MdiChildren[i].Close();

            this.SetControlEnable();
        }

        private void MMenuSysInit_Click(object sender, EventArgs e)
        {
            if (ExUI.Confirm("系统初始化，将删除所有数据"))
            {
                VC2WinFmApp.DataRule.SysInit();
                ExUI.ShowInfo("系统初始化结束，当前是一个空白系统，建议使用'菜单\\工具\\数据向导'快速建立贵校数据");
            }
        }

        private void MMenuAbout_Click(object sender, EventArgs e)
        {
            AboutFm fm = new AboutFm();
            fm.ShowDialog();
        }

        private void MMenuWizardCourse_Click(object sender, EventArgs e)
        {
            Int32 AppendCount = 0;
            if (WizardCourseFm.WizardAddCourse(ref AppendCount))
                ExUI.ShowInfo("成功添加课程" + AppendCount + "项");
        }

        private void MMenuWizardSquad_Click(object sender, EventArgs e)
        {
            Int32 AppendCount = 0;
            if (WizardSquadFm.WizardAddSquad(ref AppendCount))
                ExUI.ShowInfo("成功添加班级" + AppendCount + "项");
        }

        private void MMenuPrintSet_Click(object sender, EventArgs e)
        {
            PageSetupDialog pd = new PageSetupDialog();
            pd.Document = VcPrintDocument.GetInstance();
            pd.ShowDialog();
        }

        private void MMenuPrint_Click(object sender, EventArgs e)
        {
            IList<BaseEntity> etis = new List<BaseEntity>();
            BaseEntity entity = (this.ActiveMdiChild as IScheduleFm).Entity;
            VcPrintDocument.GetInstance().DocumentName = entity.ToString();
            etis.Add(entity);
            VcPrintDocument.GetInstance().Entitis = etis;
            VcPrintDocument.GetInstance().Print();
        }

        private void MMenuPrintPreview_Click(object sender, EventArgs e)
        {
            IList<BaseEntity> etis = new List<BaseEntity>();
            etis.Add((this.ActiveMdiChild as IScheduleFm).Entity);
            VcPrintDocument.GetInstance().Entitis = etis;

            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = VcPrintDocument.GetInstance();
            dlg.ShowDialog();
        }

        private void PrintSome(Type type)
        {
            MbrSelectFmMulti Fm = new MbrSelectFmMulti();

            if (type == typeof(EnSquad))
                Fm.MbrSelect = VC2WinFmApp.Engine.GetSqdMbrSelect();
            else if (type == typeof(EnTeacher))
                Fm.MbrSelect = VC2WinFmApp.Engine.GetTchMbrSelect();
            else
                throw new Exception("未知的类型");

            if (Fm.ShowDialog() != DialogResult.OK)
                return;

            IList<BaseEntity> Eties = Fm.GetSelectEties();
            if (Eties == null || Eties.Count == 0)
                return;

            VcPrintDocument Doc = VcPrintDocument.GetInstance();
            PrintDialog pd = new PrintDialog();
            pd.Document = Doc;
            if (pd.ShowDialog() != DialogResult.OK)
                return;

            if (type == typeof(EnTeacher))
                Doc.DocumentName = "教师课表";
            else if (type == typeof(EnSquad))
                Doc.DocumentName = "班级课表";
            else if (type == typeof(EnCourse))
                Doc.DocumentName = "课程课表";

            Doc.Entitis = Eties;
            Doc.Print();
        }

        private void MMenuPrintSomeSqd_Click(object sender, EventArgs e)
        {
            PrintSome(typeof(EnSquad));
        }

        private void MMenuPrintSomeTch_Click(object sender, EventArgs e)
        {
            PrintSome(typeof(EnTeacher));
        }

        private void MMenuSchedule_Click(object sender, EventArgs e)
        {
            IList<EnSquad> Squads = new List<EnSquad>();
            Squads.Add((this.ActiveMdiChild as IScheduleFm).Entity as EnSquad);
            VC2WinFmApp.Engine.Automatic(Squads, false, false, false);
        }

        private void MemberGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (sender == MemberGrid)
            {
                DataGridView.HitTestInfo hi = MemberGrid.HitTest(e.X, e.Y);
                DataMbrBind.Position = hi.RowIndex;

                DataMbrBind.Refresh();
            }
            else if (sender == FailActGrid)
            {
                DataGridView.HitTestInfo hi = FailActGrid.HitTest(e.X, e.Y);
                FailActBind.Position = hi.RowIndex;

                FailActBind.Refresh();
            }
        }

        private void MctMenu_Opening(object sender, CancelEventArgs e)
        {
            if (FailActGrid.RowCount == 0)
                e.Cancel = true;
        }

        private void MMenuWizardTeacher_Click(object sender, EventArgs e)
        {
            Int32 AppendCount = 0;
            if (WizardTeacherFm.WizardAddTeacher(ref AppendCount))
                ExUI.ShowInfo("成功添加教师" + AppendCount + "名");
        }

        private IScheduleFm NewScheduleFm(BaseEntity entity)
        {
            Form fm;

            if (entity is EnSquad)
                fm = new SqdScheduleFm();
            else
                fm = new TchScheduleFm();
            (fm as IScheduleFm).Entity = entity;
            fm.Text = entity.Name;

            fm.MdiParent = this;
            fm.FormClosing += ItemFormClosing;

            ToolStripItem mi = new ToolStripMenuItem(entity.Name);
            mi.Tag = fm;
            mi.Click += ItemMenuClick;

            MMenuWindow.DropDownItems.Add(mi);
            fm.Show();

            return fm as IScheduleFm;
        }

        private void AddExportMenu()
        {
            foreach (String exportKind in VcExport.GetExportList())
            {
                ToolStripItem mi = new ToolStripMenuItem("当前组" + exportKind);
                mi.Tag = exportKind;
                mi.Click += ExportGroupMM_Click;
                ExportMM.DropDownItems.Add(mi);
            }

            //ExportMM.DropDownItems.Add(new ToolStripSeparator());
            //foreach (String exportKind in VcExport.GetExportList())
            //{
            //    ToolStripItem mi = new ToolStripMenuItem("当前课表" + exportKind);
            //    mi.Tag = exportKind;
            //    mi.Click += ExportMM_Click;
            //    ExportMM.DropDownItems.Add(mi);
            //    ExportMenuList.Add(mi);
            //}
        }

        private IList<ToolStripItem> ExportMenuList = new List<ToolStripItem>();

        private void ExportGroupMM_Click(object sender, EventArgs e)
        {
            VCExportDataGroup data = ExportMatrix.GetExportData(DataGrpBind.Current.Name, DataMbrBind.DataSource);

            VcExport.ExportData((sender as ToolStripItem).Tag as String, data);
        }

        private void ExportMM_Click(object sender, EventArgs e)
        {
            IScheduleFm actFm = this.ActiveMdiChild as IScheduleFm;
            VCExportData data = ExportMatrix.GetExportData(actFm.Entity);

            VcExport.ExportData((sender as ToolStripItem).Tag as String, data);
        }

        private void TopScheduleTs_Click(object sender, EventArgs e)
        {
            if (VC2WinFmApp.TopFm == null
                || VC2WinFmApp.TopFm.IsDisposed)
            {
                VC2WinFmApp.TopFm = new TopScheduleFm();
                this.AddOwnedForm(VC2WinFmApp.TopFm);
            }

            VC2WinFmApp.TopFm.Show();
        }

        private void MMenuConfigView_Click(object sender, EventArgs e)
        {
            ConfigViewFm Fm = new ConfigViewFm();
            if (Fm.ShowDialog() == DialogResult.OK)
                foreach (Form cfm in this.MdiChildren)
                    if (cfm is IScheduleFm)
                    {
                        (cfm as IScheduleFm).ScheduleUpdate(eScheduleUpdateKind.RefreshRule);

                        //补丁
                        cfm.Width = cfm.Width + 1;
                        cfm.Width = cfm.Width - 1;
                    }
        }

        public void SetNewGroup(IList<BaseEntity> Groups)
        {
            DataGrpBind.Binding(Groups);
        }

        public void SetFailAct(IList<EnFailAct> FailActs)
        {
            IScheduleFm actFm = this.ActiveMdiChild as IScheduleFm;

            if (actFm == null)
            {
                FailActBind.Binding(FailActs);
                return;
            }

            IList<EnFailAct> FailActs2 = new List<EnFailAct>();
            foreach (EnFailAct act in FailActs)
                if (act.Act.ClsLesson.Squad == actFm.Entity)
                    FailActs2.Add(act);
            foreach (EnFailAct act in FailActs)
                if (act.Act.ClsLesson.Squad != actFm.Entity)
                    FailActs2.Add(act);

            FailActBind.Binding(FailActs2);

        }

        public void OnModifiedChange()
        {
            this.SetControlEnable();
        }

        private Form FindChildrenFm(BaseEntity Ety)
        {
            foreach (Form fm in this.MdiChildren)
                if ((fm as IScheduleFm).Entity == Ety)
                    return fm;

            return null;
        }

        private void OpenScheduleFm(BaseEntity entity)
        {
            Form fm = FindChildrenFm(entity);

            if (fm == null)
            {
                IScheduleFm schFm = NewScheduleFm(entity);
                schFm.ScheduleUpdate(eScheduleUpdateKind.RefreshMatrix);
                schFm.ScheduleUpdate(eScheduleUpdateKind.RefreshAct);

                schFm.OpenScheduleFm += OpenScheduleFm;

                if (schFm is SqdScheduleFm)
                    (schFm as SqdScheduleFm).ReadOnly = VC2WinFmApp.DataRule.ReadOnly;
            }
            else
                fm.Activate();
        }

        //让Engine数据改变时能更新对应的界面
        public void OnScheduleUpdate(eScheduleUpdateKind Kind)
        {
            foreach (Form fm in this.MdiChildren)
                (fm as IScheduleFm).ScheduleUpdate(Kind);
        }

        private void MMenuScheduleSome_Click(object sender, EventArgs e)
        {
            MbrSelectFmMulti Fm = new MbrSelectFmMulti();
            Fm.MbrSelect = VC2WinFmApp.Engine.GetSqdMbrSelect();
            if (Fm.ShowDialog() != DialogResult.OK)
                return;

            IList<BaseEntity> Eties = Fm.GetSelectEties();
            if (Eties == null || Eties.Count == 0)
                return;

            IList<EnSquad> Squads = new List<EnSquad>();
            foreach (BaseEntity ety in Eties)
                Squads.Add(ety as EnSquad);
            VC2WinFmApp.Engine.Automatic(Squads, false, false, false);

            foreach (EnSquad sqd in Squads)
                this.OpenScheduleFm(sqd);
        }

        private void MMenuStreamline_Click(object sender, EventArgs e)
        {
            IList<EnSquad> Squads = new List<EnSquad>();
            Squads.Add((this.ActiveMdiChild as IScheduleFm).Entity as EnSquad);
            VC2WinFmApp.Engine.Optimization(Squads, false, false);
        }

        private void MMenuStreamlineSome_Click(object sender, EventArgs e)
        {
            MbrSelectFmMulti Fm = new MbrSelectFmMulti();
            Fm.MbrSelect = VC2WinFmApp.Engine.GetSqdMbrSelect();
            if (Fm.ShowDialog() != DialogResult.OK)
                return;

            IList<BaseEntity> Eties = Fm.GetSelectEties();
            if (Eties == null || Eties.Count == 0)
                return;

            IList<EnSquad> Squads = new List<EnSquad>();
            foreach (BaseEntity ety in Eties)
                Squads.Add(ety as EnSquad);
            VC2WinFmApp.Engine.Optimization(Squads, false, false);

            foreach (EnSquad sqd in Squads)
                this.OpenScheduleFm(sqd);
        }

        private void MMenuHelpList_Click(object sender, EventArgs e)
        {
            Process.Start("hh.exe", "SimpleVCHelp.chm");
        }

        private void mMenuWizard_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(VC2WinFmApp.ProductHomePage);
            }
            catch
            {
            }
        }

        private void CloseAllTs_Click(object sender, EventArgs e)
        {
            if (VC2WinFmApp.TopFm != null && !VC2WinFmApp.TopFm.IsDisposed)
                VC2WinFmApp.TopFm.Close();

            foreach (Form fm in this.MdiChildren)
                if (fm is IScheduleFm)
                    fm.Close();

            this.SetControlEnable();
        }
    }
}