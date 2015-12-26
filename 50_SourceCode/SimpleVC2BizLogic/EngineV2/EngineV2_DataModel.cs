using System;
using System.Collections.Generic;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.EngineV2
{
    public partial class EngineV2
    {
        /// <summary>
        /// һ���༶�Ŀα�
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
            /// �༶
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
            ///�α�
            /// </summary>
            public DtMatrix<ScheduleNode> Matrix
            {
                get { return matrix; }
            }

            /// <summary>
            ///û�ط����ŵĿ�
            /// </summary>
            public IList<EnLsnAct> FailLsnActs
            {
                get { return failLsnActs; }
            }

            /// <summary>
            ///���༶�����н�ʦ
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
        /// ����α��Ԫ��
        /// </summary>
        internal class ScheduleNode
        {
            /// <summary>
            /// LsnAct��ǰ���ʶ�
            /// </summary>
            public eRule Rule;

            public EnLsnAct LsnAct;
        }

        //===========================================
        /// <summary>
        /// ����ʱ���Ƿ���Ч��
        /// </summary>
        DtMatrix<Boolean> TimeTestMatrix;

        /// <summary>
        /// ȫУ��Ч����
        /// </summary>
        private IList<BaseEntity> GroupList = new List<BaseEntity>();

        /// <summary>
        /// ȫУ�пεİ༶
        /// </summary>
        private IList<EnSquad> SquadList = new List<EnSquad>();

        /// <summary>
        /// ȫУ�пεĽ�ʦ�б�
        /// </summary>
        private IList<EnTeacher> TeacherList = new List<EnTeacher>();

        /// <summary>
        /// ClsLesson�Ĺ���ϳ�,Ϊ����ٶ�
        /// SlnChanged��DataChanged��RuleChanged������Ҫ����
        /// </summary>
        private IDictionary<EnClsLesson, DtMatrix<eRule>> ClsLsnRuleList
            = new Dictionary<EnClsLesson, DtMatrix<eRule>>();

        /// <summary>
        /// ȫУ�α�����̬�ͳ־����ݽ��
        /// </summary>
        internal IDictionary<EnSquad, SqdSchedule> SqdScheduleList = new Dictionary<EnSquad, SqdSchedule>();

        /// <summary>
        /// ȫУ�Ĵ���������
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