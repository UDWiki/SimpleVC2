using System;
using System.Collections.Generic;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.EngineV2
{
    public partial class EngineV2
    {
        /// <summary>
        /// 一个班级的课表
        /// </summary>
        internal class SqdSchedule
        {
            private EnSquad squad;
            private IList<EnLsnAct> failLsnActs = new List<EnLsnAct>();
            private DtMatrix<ScheduleNode> matrix;
            private IList<EnTeacher> teaches = new List<EnTeacher>();

            public SqdSchedule(EnSolution Sln, EnSquad Squad)
            {
                this.squad = Squad;

                matrix = new DtMatrix<ScheduleNode>(Sln);
                foreach (VcTime time in matrix.eachTime())
                    matrix[time] = new ScheduleNode();
            }

            /// <summary>
            /// 班级
            /// </summary>
            public EnSquad Squad
            {
                get { return squad; }
            }

            private Boolean modified;

            public Boolean Modified
            {
                get { return modified; }
                set { modified = value; }
            }

            /// <summary>
            ///课表
            /// </summary>
            public DtMatrix<ScheduleNode> Matrix
            {
                get { return matrix; }
            }

            /// <summary>
            ///没地方可排的课
            /// </summary>
            public IList<EnLsnAct> FailLsnActs
            {
                get { return failLsnActs; }
            }

            /// <summary>
            ///本班级的所有教师
            /// </summary>
            public IList<EnTeacher> Teaches
            {
                get { return teaches; }
            }
        }

        internal class VcVirtualGroup : BaseEntity
        {
            public VcVirtualGroup(String Name)
            {
                this.Name = Name;
            }
        }

        /// <summary>
        /// 虚拟课表的元素
        /// </summary>
        internal class ScheduleNode
        {
            /// <summary>
            /// LsnAct当前合适度
            /// </summary>
            public eRule Rule;

            public EnLsnAct LsnAct;
        }

        //===========================================
        /// <summary>
        /// 测试时间是否有效用
        /// </summary>
        DtMatrix<Boolean> TimeTestMatrix;

        /// <summary>
        /// 全校有效的组
        /// </summary>
        private IList<BaseEntity> GroupList = new List<BaseEntity>();

        /// <summary>
        /// 全校有课的班级
        /// </summary>
        private IList<EnSquad> SquadList = new List<EnSquad>();

        /// <summary>
        /// 全校有课的教师列表
        /// </summary>
        private IList<EnTeacher> TeacherList = new List<EnTeacher>();

        /// <summary>
        /// ClsLesson的规则合成,为提高速度
        /// SlnChanged、DataChanged或RuleChanged更新需要更新
        /// </summary>
        private IDictionary<EnClsLesson, DtMatrix<eRule>> ClsLsnRuleList
            = new Dictionary<EnClsLesson, DtMatrix<eRule>>();

        /// <summary>
        /// 全校课表，虚拟态和持久数据结合
        /// </summary>
        internal IDictionary<EnSquad, SqdSchedule> SqdScheduleList = new Dictionary<EnSquad, SqdSchedule>();

        /// <summary>
        /// 全校的待处理任务
        /// </summary>
        private IList<EnFailAct> FailActs = new List<EnFailAct>();


        public event dlgGroupChange GroupChange;
        public event dlgFailActChange FailActChange;
        public event dlgModifiedChange ModifiedChange;
        public event dlgScheduleUpdate ScheduleUpdate;

        private void SendScheduleUpdate(eScheduleUpdateKind Kind)
        {
            if (ScheduleUpdate != null)
                ScheduleUpdate(Kind);
        }
    }
}