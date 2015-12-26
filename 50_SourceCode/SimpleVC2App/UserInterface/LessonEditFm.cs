using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using SGLibrary.Extend.ControlEx;
using SGLibrary.Extend;
using SGLibrary.Framework.GridBind.WinForm;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.EntitySelect;
using Telossoft.SimpleVC.WinFormApp.Facade;

namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    //todo  要处理窗体记忆和最小窗体
    public partial class LessonEditFm : Form
    {
        private String cText = "课务安排";

        public LessonEditFm()
        {
            InitializeComponent();
        }

        private LsnEditInterface biz;

        public LsnEditInterface Biz
        {
            get { return biz; }
            set { biz = value; }
        }

        private void ModifyChanged(Boolean HasGrp, Boolean Modify)
        {
            mmOpen.Enabled = !Modify;
            tsOpen.Enabled = mmOpen.Enabled;

            mmSave.Enabled = HasGrp && Modify;
            tsSave.Enabled = mmSave.Enabled;

            mmCancel.Enabled = HasGrp && Modify;
            tsCancel.Enabled = mmCancel.Enabled;

            mmEdit.Enabled = HasGrp;
            tsSelectCrs.Enabled = mmEdit.Enabled;

            mmExit.Enabled = !Modify;
            ViewSharedTimeSum();
        }

        private GridViewBind<FcLesson> GrdBind;
        private GridViewBind<FcClsLesson> GrdSqdBind;

        private void mmExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LessonEditFm_Load(object sender, EventArgs e)
        {
            Biz = new LsnDataFacadeImpl(VC2WinFmApp.DataRule, this.ModifyChanged);
            Text = cText;
            GrdBind = new GridViewBind<FcLesson>(this.grdLesson, VC2WinFmApp.Cfg, null);
            GrdSqdBind = new GridViewBind<FcClsLesson>(this.grdSqdLesson, VC2WinFmApp.Cfg, null);

            GrdBind.Binding(Biz.FcLsns);
            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");  //窗体记忆
        }

        private void LessonEditFm_Shown(object sender, EventArgs e)
        {
            mmOpen_Click(sender, e);
            if (Biz.Grp == null)
                Close();

            if (Biz.FcLsns.Count == 0)
                grdLesson_SelectionChanged(null, null);
        }

        private void mmCancel_Click(object sender, EventArgs e)
        {
            Biz.Grp = Biz.Grp;
            GrdBind.Refresh();
        }

        private void mmSave_Click(object sender, EventArgs e)
        {
            Biz.Save();
        }

        private void grdLesson_SelectionChanged(object sender, EventArgs e)
        {
            FcLesson Lsn = GrdBind.Current as FcLesson;
            if (Lsn != null)
                GrdSqdBind.Binding(Lsn.FcClsLsns);
            else
            {
                GrdSqdBind.Binding(null);
                ModifyChanged(true, false);
            }
        }

        private void LessonEditFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Biz.Grp != null && Biz.Modify && !ExUI.Confirm("退出将丢失对当前数据所做的编辑"))
                e.Cancel = true;
        }

        private void grdLesson_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (grdLesson.Columns[e.ColumnIndex].Name == "SharedTime")
                GrdSqdBind.Refresh();

            ViewSharedTimeSum();
        }

        private void ViewSharedTimeSum()
        {
            Int32 SharedTimeSum = 0;
            foreach (FcLesson lsn in Biz.FcLsns)
                SharedTimeSum = SharedTimeSum + lsn.SharedTime;

            ts1.Text = "年级总课时：" + SharedTimeSum;
        }

        private void grdSqdLesson_DoubleClick(object sender, EventArgs e)
        {
            if (grdSqdLesson.SelectedCells.Count != 1)
                return;

            DataGridViewCell Cell = grdSqdLesson.SelectedCells[0];
            if (grdSqdLesson.Columns[Cell.ColumnIndex].Name != "Teacher")
                return;

            EnTeacher Tch = (GrdSqdBind.Current as FcClsLesson).teacher;
            if (!SelectTeacher(ref Tch))
                return;

            Biz.SetTeacher(GrdSqdBind.Current as FcClsLesson, Tch);
            GrdSqdBind.Refresh();
        }

        private Boolean SelectTeacher(ref EnTeacher Tch)
        {
            return SelectIF.TeacherSelect(ref Tch);
        }

        private void InputDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ExUI.ShowInfo("请输入正确的数据格式");
            e.Cancel = true;
        }

        private void mmOpen_Click(object sender, EventArgs e)
        {
            //Biz在后台支持，允许切换组，Biz.Grp表明当前编辑的组
            EnSquadGroup Grp = Biz.Grp;

            //if (!GrpSelect.SelectGrp(InterflowAPP.BizFacade.SqdGrpMbr.Grp.EtyList, ref Grp)
            //    || Biz.Grp == Grp)
            //    return;
            if (!SelectIF.SquadGroupSelect(ref Grp) || Biz.Grp == Grp)
                return;

            Biz.Grp = Grp == null ? null : Grp as EnSquadGroup;
            GrdBind.Refresh();
            Text = cText + ": " + Biz.Grp.Name;
            if (GrdBind.Count == 0)
                grdLesson_SelectionChanged(null, null);
        }

        private void mmSelectCrs_Click(object sender, EventArgs e)
        {
            IList<BaseEntity> Courses = Biz.GetCourses();
            //if (UserIF.SelectMbrs(InterflowAPP.BizFacade.CrsGrpMbr, Biz.Grp.Name, ref Courses))
            //    Biz.SetCourses(Courses);
            Courses = SelectIF.MemberSelectMulti(VC2WinFmApp.DataFacade.Crs, Courses);
            if (Courses != null)
                Biz.SetCourses(Courses);

            GrdBind.Refresh();
            ViewSharedTimeSum();
        }

        private void grdSqdLesson_SelectionChanged(object sender, EventArgs e)
        {
            tsTeacher.Enabled = GrdSqdBind.IsOnlyOneSelect;
        }

    }
}