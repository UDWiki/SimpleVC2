using System.Collections.Generic;
using System.Windows.Forms;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.BaseData;
using Telossoft.SimpleVC.WinFormApp.UserInterface;

namespace Telossoft.SimpleVC.WinFormApp
{
    //这个类用来处理整个系统的消息
    //消息主要有: 基础数据变更\课务数据变更\规则变更三类
    public class MessageSwitch
    {
        /// <summary>
        /// 全校有效的组变动了
        /// </summary>
        public void OnGroupChange(IList<BaseEntity> Groups)
        {
            VC2WinFmApp.MainFm.SetNewGroup(Groups);
        }

        /// <summary>
        /// 课表需要更新（课表、课和各节课的规则）
        /// </summary>
        public void OnScheduleUpdate(eScheduleUpdateKind Kind)
        {
            VC2WinFmApp.MainFm.OnScheduleUpdate(Kind);
            if (VC2WinFmApp.TopFm != null && !VC2WinFmApp.TopFm.IsDisposed)
                VC2WinFmApp.TopFm.ScheduleUpdate(Kind);
        }

        /// <summary>
        /// 全校待处理的科变动了
        /// </summary>
        public void OnFailActChange(IList<EnFailAct> FailActs)
        {
            VC2WinFmApp.MainFm.SetFailAct(FailActs);
        }

        /// <summary>
        /// 编辑状态改变
        /// </summary>
        public void OnModifiedChange()
        {
            VC2WinFmApp.MainFm.OnModifiedChange();
        }

        public void SetSlnProperty()
        {
            VC2WinFmApp.DataRule.LockRefresh();
            try
            {
                SlnPropertyFm SlnFm = new SlnPropertyFm();
                SlnFm.Value = VC2WinFmApp.DataRule.Solution.Clone() as EnSolution;

                if (SlnFm.ShowDialog() == DialogResult.OK)
                    VC2WinFmApp.DataRule.Solution = SlnFm.Value;
            }
            finally
            {
                VC2WinFmApp.DataRule.unLockRefresh();
            }
        }

        public void CrsDataMng()
        {
            VC2WinFmApp.DataRule.LockRefresh();
            try
            {
                GroupMemberFm GrpMbrFm = new GroupMemberFm();
                GrpMbrFm.GrpMbrBF = VC2WinFmApp.DataFacade.Crs;

                GrpMbrFm.ShowDialog();
            }
            finally
            {
                VC2WinFmApp.DataRule.unLockRefresh();
            }
        }

        public void TchDataMng()
        {
            VC2WinFmApp.DataRule.LockRefresh();
            try
            {
                GroupMemberFm GrpMbrFm = new GroupMemberFm();
                GrpMbrFm.GrpMbrBF = VC2WinFmApp.DataFacade.Tch;

                GrpMbrFm.ShowDialog();
            }
            finally
            {
                VC2WinFmApp.DataRule.unLockRefresh();
            }
        }

        public void SqdDataMng()
        {
            VC2WinFmApp.DataRule.LockRefresh();
            try
            {
                GroupMemberFm GrpMbrFm = new GroupMemberFm();
                GrpMbrFm.GrpMbrBF = VC2WinFmApp.DataFacade.Sqd;

                GrpMbrFm.ShowDialog();
            }
            finally
            {
                VC2WinFmApp.DataRule.unLockRefresh();
            }
        }

        public void LsnDataMng()
        {
            VC2WinFmApp.DataRule.LockRefresh();
            try
            {
                LessonEditFm Fm = new LessonEditFm();
                Fm.ShowDialog();
            }
            finally
            {
                VC2WinFmApp.DataRule.unLockRefresh();
            }
        }

        public void SetLastTch(EnTeacher tch, VcTime time)
        {
            if (VC2WinFmApp.TopFm != null && !VC2WinFmApp.TopFm.IsDisposed)
            {
                VC2WinFmApp.TopFm.LastTch = tch;
                VC2WinFmApp.TopFm.LastTime = time;
            }
        }

        public void SetFocusTch(EnTeacher tch)
        {
            if (VC2WinFmApp.TopFm != null && !VC2WinFmApp.TopFm.IsDisposed)
            {
                if (tch == VC2WinFmApp.TopFm.LastTch)
                    VC2WinFmApp.TopFm.FocusTch = null;
                else
                    VC2WinFmApp.TopFm.FocusTch = tch;
            }
        }
    }
}
