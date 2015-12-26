using System;
using System.Collections.Generic;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.EngineV2
{
    public partial class EngineV2 : IEngine
    {
        /// <summary>
        /// 引擎初始化
        /// </summary>
        public void Init( )
        {
            SetDataList();             //设置全校有效的组、有课的班级和有课的教师
            SetSqdScheduleListData();  //生成班级课表的课
            SetClsLsnRuleList();       //生成ClsLesson的规则表
            SetSqdScheduleListRule();  //生成班级课表的规则,应用clsLsn的规则并标记出教师授课冲突
            SetFailActs();             //生成全校要处理的任务
        }

        public void InitUI()
        {
            if (this.GroupChange != null)
                this.GroupChange(this.GroupList);
            if (this.FailActChange != null)
                this.FailActChange(this.FailActs);
            if (this.ModifiedChange != null)
                this.ModifiedChange();
        }

        /// <summary>
        /// 获取一个组的成员，包括"全部"、"全部班级"、"全部教师"
        /// </summary>
        public IList<BaseEntity> GetEnabledMember(BaseEntity Grp)
        {
            if (Grp == AllMember)
            {
                IList<BaseEntity> AllMemberResult = new List<BaseEntity>();
                foreach (EnSquad sqd in SquadList)
                    AllMemberResult.Add(sqd);
                foreach (EnTeacher tch in TeacherList)
                    AllMemberResult.Add(tch);

                return AllMemberResult;
            }
            else if (Grp == AllSquad)
                return new GIListTypeChange<EnSquad, BaseEntity>(SquadList);
            else if (Grp == AllTeacher)
                return new GIListTypeChange<EnTeacher, BaseEntity>(TeacherList);
            else if (Grp is EnSquadGroup)
                return GetEnabledMember(Grp as EnSquadGroup);
            else
                return GetEnabledMember(Grp as EnTeacherGroup);
        }

        private Boolean modified;
        public bool Modified
        {
            get { return modified; }
            set { modified = value; }
        }

        public Boolean TimeIsEnabled(VcTime time)
        {
            return TimeTestMatrix.TestTime(time);
        }

        /// <summary>
        /// 有课表被编辑过
        /// </summary>
        public bool IsModified(BaseEntity entity)
        {
            if (entity is EnSquad)
            {
                //当班级课表被打开，删除班级后需要关闭打开的课表Form，
                //课表Form关闭时需要IsModified，但这是此班级已经不在SqdScheduleList中
                //直接返回一个false，简化程序逻辑(这是一个陷阱,要小心)
                if (SqdScheduleList.ContainsKey(entity as EnSquad))
                    return SqdScheduleList[entity as EnSquad].Modified;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 某班级所有待安排的课
        /// </summary>
        public IList<EnLsnAct> GetFailLsnActs(EnSquad squad)
        {
            IList<EnLsnAct> Result = new List<EnLsnAct>();
            ExIList.Append<EnLsnAct>(SqdScheduleList[squad].FailLsnActs, Result);

            return Result;
        }

        /// <summary>
        ///  获得当前虚拟态的课表（未保存的内存中的课表）
        /// </summary>
        public DtMatrix<EnLsnAct> GetSqdMatrix(EnSquad squad)
        {
            DtMatrix<EnLsnAct> Result = new DtMatrix<EnLsnAct>(DataRule.Solution);
            SqdSchedule sch = SqdScheduleList[squad];

            foreach (VcTime time in Result.eachTime())
                Result[time] = sch.Matrix[time].LsnAct;

            return Result;
        }

        /// <summary>
        ///  获得当前虚拟态的课表规则
        /// </summary>
        public DtMatrix<eRule> GetSqdRule(EnSquad squad)
        {
            DtMatrix<eRule> Result
                = new DtMatrix<eRule>(DataRule.Solution);
            SqdSchedule sch = SqdScheduleList[squad];

            foreach (VcTime time in Result.eachTime())
            {
                if (sch.Matrix[time].LsnAct == null)
                    Result[time] = eRule.common;
                else
                    Result[time] = sch.Matrix[time].Rule;
            }
            return Result;
        }

        /// <summary>
        /// 获取教师课表
        /// </summary>
        public DtMatrix<IList<EnLsnAct>> GetTchMatrix(EnTeacher teacher)
        {
            DtMatrix<IList<EnLsnAct>> Result = new DtMatrix<IList<EnLsnAct>>(DataRule.Solution);

            foreach (SqdSchedule sch in SqdScheduleList.Values)
                if (sch.Teaches.Contains(teacher))
                    foreach (VcTime time in sch.Matrix.eachTime())
                        if (sch.Matrix[time].LsnAct != null
                            && sch.Matrix[time].LsnAct.ClsLesson.Teacher == teacher)
                        {
                            if (Result[time] == null)
                                Result[time] = new List<EnLsnAct>();

                            Result[time].Add(sch.Matrix[time].LsnAct);
                        }

            return Result;
        }

        /// <summary>
        /// 保存某班级课表
        /// </summary>
        public void Save(EnSquad squad)
        {
            SqdSchedule sch = SqdScheduleList[squad];
            IList<EnLsnAct> Acts = HardeningAct(sch);

            DataRule.Lsn.SaveLsnActs(Acts);
            sch.Modified = false;
            UpdateModified();
            if (this.ModifiedChange != null)
                this.ModifiedChange();
        }

        /// <summary>
        /// 保存所有编辑过的课表
        /// </summary>
        public void SaveAll(Boolean updateInterflow)
        {
            foreach (SqdSchedule sch in SqdScheduleList.Values)
                if (sch.Modified)
                {
                    IList<EnLsnAct> Acts = HardeningAct(sch);

                    DataRule.Lsn.SaveLsnActs(Acts);
                    sch.Modified = false;
                }

            this.modified = false;
            if (this.ModifiedChange != null)
                this.ModifiedChange();
        }

        /// <summary>
        /// 放弃某班级课表的编辑
        /// </summary>
        public void Cancel(EnSquad squad)
        {
            SqdSchedule sch = SqdScheduleList[squad];
            CollectAct(sch);
            PutActToMatrix(sch);
            sch.Modified = false;

            UpdateModified();
            AfterScheduleChanged(squad);
        }

        /// <summary>
        /// 放弃所有课表的编辑
        /// </summary>
        public void CancelAll(Boolean updateInterflow)
        {
            foreach (SqdSchedule sch in SqdScheduleList.Values)
                if (sch.Modified)
                {
                    CollectAct(sch);
                    PutActToMatrix(sch);
                    sch.Modified = false;
                }

            this.modified = false;
            AfterScheduleChanged(null);
        }

        /// <summary>
        /// 课表拖放后
        /// </summary>
        public void Move(EnSquad squad, EnLsnAct sAct, VcTime tTime)
        {
            SqdSchedule sch = SqdScheduleList[squad];

            //获得来源的act的时间sTime
            VcTime sTime = new VcTime();
            if (!sch.FailLsnActs.Contains(sAct))
                foreach (VcTime tt in sch.Matrix.eachTime())
                    if (sch.Matrix[tt].LsnAct == sAct)
                    {
                        tt.CopyFieldTo(sTime);
                        break;
                    }


            if (sTime == tTime)   //原地拖
                return;
            else if (sTime.HasValue && !tTime.HasValue)  //从课表拖到FailLsnActs
            {
                EnLsnAct ta = sch.Matrix[sTime].LsnAct;
                sch.FailLsnActs.Add(ta);
                sch.Matrix[sTime].LsnAct = null;
            }
            else if (!sTime.HasValue && tTime.HasValue)    //从FailLsnActs拖到课表
            {
                if (sch.Matrix[tTime].LsnAct != null)
                    return;

                sch.Matrix[tTime].LsnAct = sAct;
                sch.FailLsnActs.Remove(sAct);
            }
            else if (sTime.HasValue && tTime.HasValue)    //课表拖到课表
            {
                EnLsnAct ta = sch.Matrix[sTime].LsnAct;
                sch.Matrix[sTime].LsnAct = sch.Matrix[tTime].LsnAct;
                sch.Matrix[tTime].LsnAct = ta;
            }

            sch.Modified = true;
            this.Modified = true;
            this.AfterScheduleChanged(squad);
        }

        /// <summary>
        ///  获得当前虚拟态的课表某节的规则列表与冲突
        /// </summary>
        public IList<VcActEtyRelation> GetSqdClash(EnSquad squad, VcTime time)
        {
            List<VcActEtyRelation> Result = new List<VcActEtyRelation>();
            if (!this.TimeIsEnabled(time))
                return Result;

            //获得VcLsnAct
            EnLsnAct act = SqdScheduleList[squad].Matrix[time].LsnAct;
            if (act == null)
                return Result;

            //添加授课教师
            if (act.ClsLesson.Teacher != null)
            {
                Result.Add(new VcActEtyRelation(act.ClsLesson.Teacher, eActEtyRelation.teach));

                //整理出冲突课程
                foreach (SqdSchedule sch in SqdScheduleList.Values)
                    if (sch.Matrix[time].LsnAct != null
                        && sch.Matrix[time].LsnAct.ClsLesson.Squad != squad
                        && sch.Matrix[time].LsnAct.ClsLesson.Teacher == act.ClsLesson.Teacher)
                        Result.Add(new VcActEtyRelation(sch.Matrix[time].LsnAct.ClsLesson,
                            eActEtyRelation.clash, eRule.crisscross));
            }

            //添加Lsn/ClsLsn的Rule
            Result.Add(new VcActEtyRelation(act.ClsLesson.Lesson, eActEtyRelation.rule,
                DataRule.Rule.GetRuleOfTime(act.ClsLesson.Lesson, time)));

            Result.Add(new VcActEtyRelation(act.ClsLesson, eActEtyRelation.rule,
                DataRule.Rule.GetRuleOfTime(act.ClsLesson, time)));

            //迭代Act的元素,添加Rule
            foreach (BaseEntity ety in this.eachClsLsnComponent(act.ClsLesson))
                Result.Add(new VcActEtyRelation(ety, eActEtyRelation.rule,
                    DataRule.Rule.GetRuleOfTime(ety, time)));

            return Result;
        }
    }
}