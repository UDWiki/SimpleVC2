using System;
using System.Collections.Generic;
using System.Diagnostics;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.EngineV2
{
    public partial class EngineV2
    {
        public EngineV2(IDataRule dataRule)
        {
            this.DataRule = dataRule;

            DataRule.SlnChanged += this.OnDataRuleSlnChanged;
            DataRule.DataChanged += this.OnDataRuleDataChanged;
            DataRule.RuleChanged += this.OnDataRuleRuleChanged;
        }

        private VcVirtualGroup AllMember = new VcVirtualGroup("所有");
        private VcVirtualGroup AllTeacher = new VcVirtualGroup("所有教师");
        private VcVirtualGroup AllSquad = new VcVirtualGroup("所有班级");

        protected IDataRule DataRule { get; private set; }

        /// <summary>
        /// 课表属性改变后
        /// </summary>
        private void OnDataRuleSlnChanged()
        {
            SetSqdScheduleListData();  //生成班级课表的课
            SetClsLsnRuleList();       //生成ClsLesson的规则表
            SetSqdScheduleListRule();  //生成班级课表的规则,应用clsLsn的规则并标记出教师授课冲突
            SetFailActs();             //生成全校要处理的任务
            if (this.FailActChange != null)
                this.FailActChange(this.FailActs);

            this.SendScheduleUpdate(eScheduleUpdateKind.RefreshMatrix);
            //foreach (EnSquad ss in ActSquadList)
            //    this.SendScheduleUpdate(ss, eScheduleUpdateKind.RefreshMatrix);
            //foreach (EnTeacher ts in ActTeacherList)
            //    this.SendScheduleUpdate(ts, eScheduleUpdateKind.RefreshMatrix);
        }

        public Boolean EntityIsEnabled(BaseEntity entity)  //当前实体是否还有意义（没有被删除）
        {
            Debug.Assert(entity != null);

            if (entity is EnSquad)
                return SquadList.Contains(entity as EnSquad);
            else if (entity is EnTeacher)
                return TeacherList.Contains(entity as EnTeacher);
            else
                return false;
        }

        /// <summary>
        /// 基础数据或课务安排改变后
        /// </summary>
        private void OnDataRuleDataChanged()
        {
            SetDataList();             //设置全校有效的组、有课的班级和有课的教师
            SetSqdScheduleListData();  //生成班级课表的课
            SetClsLsnRuleList();       //生成ClsLesson的规则表
            SetSqdScheduleListRule();  //生成班级课表的规则,应用clsLsn的规则并标记出教师授课冲突
            SetFailActs();             //生成全校要处理的任务

            this.SendScheduleUpdate(eScheduleUpdateKind.Invalidate);
            this.SendScheduleUpdate(eScheduleUpdateKind.RefreshAct);

            //IList<EnSquad> WillRemoveSqd = new List<EnSquad>();
            //foreach (EnSquad ss in ActSquadList)
            //    if (!this.SquadList.Contains(ss))
            //        WillRemoveSqd.Add(ss);
            //IList<EnTeacher> WillRemoveTch = new List<EnTeacher>();
            //foreach (EnTeacher ts in ActTeacherList)
            //    if (!this.TeacherList.Contains(ts))
            //        WillRemoveTch.Add(ts);


            //foreach (EnSquad ss in WillRemoveSqd)
            //{
            //    this.SendScheduleUpdate(ss, eScheduleUpdateKind.Invalidate);
            //    ActSquadList.Remove(ss);
            //}
            //foreach (EnTeacher ts in WillRemoveTch)
            //{
            //    this.SendScheduleUpdate(ts, eScheduleUpdateKind.Invalidate);
            //    ActTeacherList.Remove(ts);
            //}

            //foreach (EnSquad ss in ActSquadList)
            //    this.SendScheduleUpdate(ss, eScheduleUpdateKind.RefreshAct);

            //foreach (EnTeacher ts in ActTeacherList)
            //    this.SendScheduleUpdate(ts, eScheduleUpdateKind.RefreshAct);

            if (this.GroupChange != null)
                this.GroupChange(this.GroupList);
            if (this.FailActChange != null)
                this.FailActChange(this.FailActs);
        }

        /// <summary>
        /// 规则改变后
        /// </summary>
        private void OnDataRuleRuleChanged()
        {
            SetClsLsnRuleList();       //生成ClsLesson的规则表
            SetSqdScheduleListRule();  //生成班级课表的规则,应用clsLsn的规则并标记出教师授课冲突
            SetFailActs();             //生成全校要处理的任务

            if (this.FailActChange != null)
                this.FailActChange(this.FailActs);

            this.SendScheduleUpdate(eScheduleUpdateKind.RefreshRule);
            //foreach (EnSquad ss in ActSquadList)
            //    this.SendScheduleUpdate(ss, eScheduleUpdateKind.RefreshRule);
            //foreach (EnTeacher ts in ActTeacherList)
            //    this.SendScheduleUpdate(ts, eScheduleUpdateKind.RefreshRule);
        }

        /// <summary>
        /// 设置全校有效的组、有课的班级和有课的教师(GroupList、SquadList、TeacherList)
        /// </summary>
        private void SetDataList()
        {
            GroupList.Clear();
            SquadList.Clear();
            TeacherList.Clear();

            foreach (EnClsLesson clsLsn in DataRule.Lsn.eachClsLesson())
                if (clsLsn.SharedTime > 0)
                {
                    if (!SquadList.Contains(clsLsn.Squad))
                        SquadList.Add(clsLsn.Squad);
                    if (clsLsn.Teacher != null && !TeacherList.Contains(clsLsn.Teacher))
                        TeacherList.Add(clsLsn.Teacher);
                }

            GroupList.Add(AllMember);
            GroupList.Add(AllSquad);
            GroupList.Add(AllTeacher);

            foreach (EnSquad sqd in SquadList)
                foreach (EnSquadGroup grp in DataRule.Sqd.GetGroups(sqd))
                    if (!GroupList.Contains(grp))
                        GroupList.Add(grp);

            foreach (EnTeacher tch in TeacherList)
                foreach (EnTeacherGroup grp in DataRule.Tch.GetGroups(tch))
                    if (!GroupList.Contains(grp))
                        GroupList.Add(grp);
        }


        /// <summary>
        /// 获得某班级组的成员(有课的)
        /// </summary>
        private IList<BaseEntity> GetEnabledMember(EnSquadGroup Grp)
        {
            IList<BaseEntity> Result = new List<BaseEntity>();
            foreach (EnSquad sqd in DataRule.Sqd.GetMembes(Grp))
                if (SquadList.Contains(sqd))
                    Result.Add(sqd);

            return Result;
        }

        /// <summary>
        /// 获得某教师组的成员(有课的)
        /// </summary>
        private IList<BaseEntity> GetEnabledMember(EnTeacherGroup Grp)
        {
            IList<BaseEntity> Result = new List<BaseEntity>();
            foreach (EnTeacher tch in DataRule.Tch.GetMembes(Grp))
                if (TeacherList.Contains(tch))
                    Result.Add(tch);

            return Result;
        }

        /// <summary>
        /// 迭代一个VcClsLesson的构成元素，包含组；不包含VcClsLesson自身和VcLesson
        /// </summary>
        private IEnumerable<BaseEntity> eachClsLsnComponent(EnClsLesson clsLsn)
        {
            yield return clsLsn.Lesson.Course;
            foreach (EnCourseGroup crsGrp in DataRule.Crs.GetGroups(clsLsn.Lesson.Course))
                yield return crsGrp;

            yield return clsLsn.Squad;
            foreach (EnSquadGroup sqdGrp in DataRule.Sqd.GetGroups(clsLsn.Squad))
                yield return sqdGrp;

            if (clsLsn.Teacher != null)
            {
                yield return clsLsn.Teacher;
                foreach (EnTeacherGroup tchGrp in DataRule.Tch.GetGroups(clsLsn.Teacher))
                    yield return tchGrp;
            }
        }

        /// <summary>
        /// 生成ClsLesson的规则表(ClsLsnRuleList)
        /// </summary>
        private void SetClsLsnRuleList()
        {
            ClsLsnRuleList.Clear();

            foreach (EnClsLesson clsLsn in DataRule.Lsn.eachClsLesson())
            {
                DtMatrix<eRule> clsLsnRules
                    = new DtMatrix<eRule>(DataRule.Solution);
                ClsLsnRuleList.Add(clsLsn, clsLsnRules);

                foreach (VcRuleCell rt in DataRule.Rule.GetRules(clsLsn))
                    if (clsLsnRules.TestTime(rt.Time))
                        clsLsnRules[rt.Time] = CommLogic.RuleAdd(clsLsnRules[rt.Time], rt.Rule);
                foreach (VcRuleCell rt in DataRule.Rule.GetRules(clsLsn.Lesson))
                    if (clsLsnRules.TestTime(rt.Time))
                        clsLsnRules[rt.Time] = CommLogic.RuleAdd(clsLsnRules[rt.Time], rt.Rule);

                foreach (BaseEntity ety in eachClsLsnComponent(clsLsn))
                    foreach (VcRuleCell rt in DataRule.Rule.GetRules(ety))
                        if (clsLsnRules.TestTime(rt.Time))
                            clsLsnRules[rt.Time] = CommLogic.RuleAdd(clsLsnRules[rt.Time], rt.Rule);
            }
        }

        /// <summary>
        /// 把sqdSch.Matrix中所有LsnAct收集到sqdSch.FailLsnActs中
        ///   同时把单元格的Rule、LsnAct清除
        /// </summary>
        private void CollectAct(SqdSchedule sqdSch)
        {
            foreach (ScheduleNode node in sqdSch.Matrix.eachElement())
            {
                node.Rule = eRule.common;

                if (node.LsnAct != null)
                {
                    sqdSch.FailLsnActs.Add(node.LsnAct);
                    node.LsnAct = null;
                }
            }
        }

        /// <summary>
        /// 生成全校待处理任务(FailActs)
        /// </summary>
        private void SetFailActs()
        {
            FailActs.Clear();

            foreach (SqdSchedule sqdSch in SqdScheduleList.Values)
            {
                foreach (EnLsnAct act in sqdSch.FailLsnActs)
                    FailActs.Add(new EnFailAct(act));

                foreach (VcTime time in sqdSch.Matrix.eachTime())
                {
                    ScheduleNode node = sqdSch.Matrix[time];

                    if (node.Rule == eRule.crisscross
                        && node.LsnAct != null)
                        FailActs.Add(new EnFailAct(node.LsnAct, time));
                }
            }
        }

        /// <summary>
        /// 把sqdSch.FailLsnActs中所有LsnAct安排进sqdSch.Matrix
        ///   不准备环境(即不清除各单元的Rule、LsnAct)
        /// </summary>
        private void PutActToMatrix(SqdSchedule sqdSch)
        {
            EnLsnAct FillAct = new EnLsnAct();
            IList<EnLsnAct> tmpActs = sqdSch.FailLsnActs == null ? null : new List<EnLsnAct>(sqdSch.FailLsnActs);
            sqdSch.FailLsnActs.Clear();

            foreach (EnLsnAct act in tmpActs)
                if (sqdSch.Matrix.TestTime(act.Time))
                {
                    ScheduleNode node = sqdSch.Matrix[act.Time];
                    if (node.LsnAct == null)
                        node.LsnAct = act;
                    else
                    {
                        sqdSch.FailLsnActs.Add(act);

                        if (node.LsnAct != FillAct)
                        {
                            sqdSch.FailLsnActs.Add(node.LsnAct);
                            node.LsnAct = FillAct;
                        }
                    }
                }
                else
                    sqdSch.FailLsnActs.Add(act);

            foreach (ScheduleNode node in sqdSch.Matrix.eachElement())
                if (node.LsnAct == FillAct)
                    node.LsnAct = null;
        }

        /// <summary>
        /// 生成班级课表(SqdScheduleList),不会生成各节课的规则
        /// </summary>
        private void SetSqdScheduleListData()
        {
            TimeTestMatrix = new DtMatrix<bool>(DataRule.Solution);

            SqdScheduleList.Clear();

            foreach (EnLsnAct act in DataRule.Lsn.eachLsnAct())
            {
                SqdSchedule sqdSch;
                if (!SqdScheduleList.TryGetValue(act.ClsLesson.Squad, out sqdSch))
                {
                    sqdSch = new SqdSchedule(DataRule.Solution, act.ClsLesson.Squad);
                    SqdScheduleList.Add(act.ClsLesson.Squad, sqdSch);
                }
                sqdSch.FailLsnActs.Add(act);  //暂时存下
            }

            foreach (SqdSchedule sqdSch in SqdScheduleList.Values)
            {
                foreach (EnLsnAct act in sqdSch.FailLsnActs)
                    if (act.ClsLesson.Teacher != null
                        && !sqdSch.Teaches.Contains(act.ClsLesson.Teacher))
                        sqdSch.Teaches.Add(act.ClsLesson.Teacher);

                PutActToMatrix(sqdSch);
            }
        }

        /// <summary>
        /// 迭代全校某时间的课
        /// </summary>
        private IEnumerable<ScheduleNode> eachEnabledScheduleNode(VcTime time)
        {
            foreach (SqdSchedule sqdSch in SqdScheduleList.Values)
                if (sqdSch.Matrix[time].LsnAct != null)
                    yield return sqdSch.Matrix[time];
        }

        /// <summary>
        /// 生成班级课表的规则,应用clsLsn的规则并标记出教师授课冲突
        /// </summary>
        private void SetSqdScheduleListRule()
        {
            //没课的单元格必须设为eRule.common
            foreach (SqdSchedule sqdSch in SqdScheduleList.Values)
                foreach (ScheduleNode node in sqdSch.Matrix.eachElement())
                    node.Rule = eRule.common;

            DtMatrix<Boolean> Times = new DtMatrix<bool>(DataRule.Solution);
            IDictionary<EnTeacher, Int32> TchsClsCount = new Dictionary<EnTeacher, Int32>();

            foreach (VcTime time in Times.eachTime())
            {
                TchsClsCount.Clear();
                foreach (ScheduleNode schNode in eachEnabledScheduleNode(time))
                {
                    EnClsLesson clsLsn = schNode.LsnAct.ClsLesson;
                    schNode.Rule = ClsLsnRuleList[clsLsn][time];   //程序逻辑正常则必定存在

                    if (clsLsn.Teacher != null)
                        if (TchsClsCount.ContainsKey(clsLsn.Teacher))
                            TchsClsCount[clsLsn.Teacher]++;
                        else
                            TchsClsCount.Add(clsLsn.Teacher, 1);
                }

                foreach (KeyValuePair<EnTeacher, Int32> pair in TchsClsCount)
                    if (pair.Value > 1)
                        foreach (ScheduleNode schNode in eachEnabledScheduleNode(time))
                            if (schNode.LsnAct.ClsLesson.Teacher == pair.Key)
                                schNode.Rule = eRule.crisscross;
            }
        }

        /// <summary>
        /// 更新Modified
        /// </summary>
        private void UpdateModified()
        {
            this.modified = false;

            foreach (SqdSchedule sch in this.SqdScheduleList.Values)
                if (sch.Modified)
                {
                    this.modified = true;
                    return;
                }
        }

        /// <summary>
        /// 一个班级的课表改变后,entity允许为null(自动排课时)
        /// </summary>
        private void AfterScheduleChanged(EnSquad squad)
        {
            SetSqdScheduleListRule();  //生成班级课表的规则,应用clsLsn的规则并标记出教师授课冲突
            SetFailActs();             //生成全校要处理的任务
            if (this.FailActChange != null)
                this.FailActChange(this.FailActs);

            this.SendScheduleUpdate(eScheduleUpdateKind.RefreshAct);
            this.SendScheduleUpdate(eScheduleUpdateKind.RefreshRule);
            //旧的实现很高效
            //foreach (EnSquad ss in ActSquadList)
            //    if (squad == null || ss == squad)
            //        this.SendScheduleUpdate(ss, eScheduleUpdateKind.RefreshAct);
            //    else
            //        this.SendScheduleUpdate(ss, eScheduleUpdateKind.RefreshRule);

            ////一个指定班级课表改变后只更新相关的教师课表
            //if (squad == null)
            //{
            //    foreach (EnTeacher ts in ActTeacherList)
            //        this.SendScheduleUpdate(ts, eScheduleUpdateKind.RefreshAct);
            //}
            //else
            //{
            //    SqdSchedule sch = SqdScheduleList[squad];
            //    foreach (EnTeacher ts in ActTeacherList)
            //        if (sch.Teaches.Contains(ts))
            //            this.SendScheduleUpdate(ts, eScheduleUpdateKind.RefreshAct);
            //}

            if (this.ModifiedChange != null)
                this.ModifiedChange();
        }

        /// <summary>
        /// 固化当前班级的课表，包括FailLsnActs
        /// </summary>
        private IList<EnLsnAct> HardeningAct(SqdSchedule sqdSch)
        {
            IList<EnLsnAct> Result = new List<EnLsnAct>();

            foreach (EnLsnAct failAct in sqdSch.FailLsnActs)
            {
                new VcTime().CopyFieldTo(failAct.Time);
                Result.Add(failAct);
            }

            foreach (VcTime time in sqdSch.Matrix.eachTime())
                if (sqdSch.Matrix[time].LsnAct != null)
                {
                    time.CopyFieldTo(sqdSch.Matrix[time].LsnAct.Time);
                    Result.Add(sqdSch.Matrix[time].LsnAct);
                }

            return Result;
        }
    }
}
