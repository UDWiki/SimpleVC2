using System;
using System.Collections.Generic;
using System.Diagnostics;
using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.BizLogic.EngineV2
{
    public partial class EngineV2
    {
        /// <summary>
        /// 获取交换时优势
        /// </summary>
        public DtMatrix<eRule> GetActChangeRule(EnSquad squad,
            EnLsnAct sAct, VcTime sTime)
        {
            Debug.Assert(sAct != null, "为简化程序逻辑，空课不可拖。");

            //这个方法是本程序中最复杂的逻辑
            //步骤：
            //一、获得所有位置当前优势
            //二、获得sAct当前优势
            //三、获得所有位置的Act移动到sTime的优势值
            //四、获得sAct移动到所有位置的优势值
            //五、比较当前优势和移动后的优势，获得交换的价值

            SqdSchedule NowSch = SqdScheduleList[squad];

            //一、获得所有位置当前优势 
            DtMatrix<eRule> NowRules = this.GetSqdRule(squad);

            //二、获得sAct当前优势 
            eRule sActNowRule;

            if (NowRules.TestTime(sTime))
                sActNowRule = NowRules[sTime];
            else
                sActNowRule = eRule.common;  //未排

            //三、获得所有位置的Act移动到sTime的优势值
            DtMatrix<eRule> FutureRules = new DtMatrix<eRule>(DataRule.Solution);

            if (FutureRules.TestTime(sTime))
            {
                IList<EnTeacher> sTimeTchs = new List<EnTeacher>();
                foreach (SqdSchedule sch in this.SqdScheduleList.Values)
                    if (sch != NowSch && sch.Matrix[sTime].LsnAct != null)
                    {
                        EnLsnAct act = sch.Matrix[sTime].LsnAct;
                        if (act.ClsLesson.Teacher != null
                            && !sTimeTchs.Contains(act.ClsLesson.Teacher))
                            sTimeTchs.Add(act.ClsLesson.Teacher);
                    }

                foreach (VcTime time in FutureRules.eachTime())
                    if (NowSch.Matrix[time].LsnAct != null)
                    {
                        EnLsnAct act = NowSch.Matrix[time].LsnAct;
                        if (act.ClsLesson.Teacher != null
                            && sTimeTchs.Contains(act.ClsLesson.Teacher))
                            FutureRules[time] = eRule.crisscross;  //此课程移到sTime后导致教师冲突
                        else
                            FutureRules[time] = ClsLsnRuleList[act.ClsLesson][sTime]; //移到sTime后的优势
                    }
            }
            else
                //所有Act移动到未排，则优势一定是eRule.common
                foreach (VcTime time in FutureRules.eachTime())
                    FutureRules[time] = eRule.common;

            //四、获得sAct移动到所有位置的优势值
            DtMatrix<eRule> sActFutureRules = new DtMatrix<eRule>(DataRule.Solution);

            DtMatrix<eRule> TmpRules = ClsLsnRuleList[sAct.ClsLesson];
            foreach (VcTime time in TmpRules.eachTime())
                sActFutureRules[time] = TmpRules[time];

            //检测课程冲突
            if (sAct.ClsLesson.Teacher != null)
            {
                EnTeacher NowTch = sAct.ClsLesson.Teacher;
                DtMatrix<IList<EnLsnAct>> sTchLsns = this.GetTchMatrix(NowTch);
                foreach (VcTime time in sTchLsns.eachTime())
                    if (sTchLsns[time] != null && sTchLsns[time].Count > 0)
                        foreach (EnLsnAct act in sTchLsns[time])
                            if (act != NowSch.Matrix[time].LsnAct   //当前课表的,会被移走
                                && act.ClsLesson.Teacher == NowTch)
                            {
                                sActFutureRules[time] = eRule.crisscross;  //sAct移到time后将导致冲突
                                break;
                            }
            }

            //五、比较当前优势和移动后的优势，获得交换的价值矩阵
            DtMatrix<eRule> Result = new DtMatrix<eRule>(DataRule.Solution);

            //NowRules、sActNowRule、FutureRules、sActFutureRules
            //所有位置当前优势\拖动源当前优势\所有位置在拖动源的优势\拖动源拖到所有位置的优势

            foreach (VcTime time in Result.eachTime())
            {
                if (NowSch.Matrix[time].LsnAct != null
                    && NowSch.Matrix[time].LsnAct.ClsLesson == sAct.ClsLesson)
                {
                    Result[time] = eRule.crisscross; //同样的课移动没意义的
                    continue;
                }

                Result[time] = ComparerActChangeRule(NowRules[time], sActNowRule,
                    FutureRules[time], sActFutureRules[time]);
            }

            return Result;
        }

        public eRule ComparerActChangeRule(eRule NowRule, eRule sActNowRule,
            eRule FutureRule, eRule sActFutureRule)
        {
            //NowRules、sActNowRule、FutureRules、sActFutureRules
            //所有位置当前优势\拖动源当前优势\所有位置在拖动源的优势\拖动源拖到所有位置的优势

            var crisscrossCount_Now = NowRule == eRule.crisscross ? 1 : 0;
            crisscrossCount_Now = crisscrossCount_Now + (sActNowRule == eRule.crisscross ? 1 : 0);

            var crisscrossCount_Future = FutureRule == eRule.crisscross ? 1 : 0;
            crisscrossCount_Future = crisscrossCount_Future + (sActFutureRule == eRule.crisscross ? 1 : 0);

            if (crisscrossCount_Now != crisscrossCount_Future)
                return crisscrossCount_Now > crisscrossCount_Future ? eRule.excellent : eRule.crisscross;

            var sum_Now = (Int32)NowRule + (Int32)sActNowRule;
            var sum_Future = (Int32)FutureRule + (Int32)sActFutureRule;

            if (sum_Now < sum_Future) //优化了
                return sum_Now - sum_Future < -2 ? eRule.excellent : eRule.fine;
            else if (sum_Now > sum_Future)  //状态变差严重否？
                return sum_Now - sum_Future > 2 ? eRule.crisscross : eRule.ill;
            else
                return eRule.common;
        }

        /// <summary>
        /// 自动排课，目前是代用的方法
        /// </summary>
        public void Automatic(IList<EnSquad> Squads,
            Boolean OnlyFail, bool LockIsEnabled, Boolean AutoSave)
        {
            if (!OnlyFail)
                foreach (EnSquad sqd in Squads)
                    CollectAct(SqdScheduleList[sqd]);

            Boolean Changed = false;
            foreach (EnSquad sqd in Squads)
                Changed = Changed | Automatic(sqd);

            if (!Changed)
                return;

            foreach (EnSquad sqd in Squads)
                magic(sqd, LockIsEnabled, 200);

            if (AutoSave)
                foreach (EnSquad sqd in Squads)
                    this.Save(sqd);

            this.Modified = true;
            this.AfterScheduleChanged(null);
        }


        private IList<EnLsnAct> GetOrderFailLsnActs(IList<EnLsnAct> FailLsnActs)
        {
            IList<EnLsnAct> Result = new List<EnLsnAct>();
            IList<EnClsLesson> ClsLsns = new List<EnClsLesson>();

            foreach (EnLsnAct act in FailLsnActs)
                if (!ClsLsns.Contains(act.ClsLesson))
                    ClsLsns.Add(act.ClsLesson);

            foreach (EnClsLesson clsLsn in ClsLsns)
                foreach (EnLsnAct act in FailLsnActs)
                    if (act.ClsLesson == clsLsn)
                        Result.Add(act);

            return Result;
        }

        /// <summary>
        /// 仅负责把空课安排到合适的位置，不会动已排的课
        /// </summary>
        private Boolean Automatic(EnSquad squad)
        {
            if (SqdScheduleList[squad].FailLsnActs.Count == 0)
                return false;

            //策略：
            //第一步：整理FailLsnActs到有序（同VcClsLesson相邻，为提高速度）
            //对每一VcLsnAct：计算优势值（靠评价函数），安排到优势值最大的Time

            SqdSchedule sch = SqdScheduleList[squad];
            sch.Modified = true;
            IList<EnLsnAct> OrderFailLsnActs = GetOrderFailLsnActs(sch.FailLsnActs);
            Int32 DaySum = 0;
            foreach (Boolean bl in DataRule.Solution.ActiveWeekArr)
                if (bl)
                    DaySum++;

            EnClsLesson frontClsLsn = null;
            DtMatrix<Boolean> TchConcretes = null;
            DtMatrix<eRule> ClsLsnRules = null;
            Int32[] CourseCnt = new Int32[7];
            Int32 CourseAverage = 0;

            foreach (EnLsnAct act in OrderFailLsnActs)
            {
                if (frontClsLsn == null || frontClsLsn != act.ClsLesson)
                {
                    frontClsLsn = act.ClsLesson;
                    TchConcretes = new DtMatrix<bool>(DataRule.Solution);
                    ClsLsnRules = this.ClsLsnRuleList[act.ClsLesson];

                    if (act.ClsLesson.Teacher != null)
                        foreach (VcTime time in sch.Matrix.eachTime())
                            if (sch.Matrix[time].LsnAct == null)
                                foreach (SqdSchedule sqdSch in SqdScheduleList.Values)
                                    if (sqdSch.Matrix[time].LsnAct != null
                                        && sqdSch.Matrix[time].LsnAct.ClsLesson.Teacher == act.ClsLesson.Teacher)
                                    {
                                        TchConcretes[time] = true;
                                        break;
                                    }

                    //foreach (Int32 cnt in CourseCnt)
                    //    cnt = 0;
                    for (Int32 i = 0; i <= 6; i++)
                        CourseCnt[i] = 0;
                    Int32 CourseSum = 0;
                    foreach (VcTime time in sch.Matrix.eachTime())
                        if (sch.Matrix[time].LsnAct != null
                            && sch.Matrix[time].LsnAct.ClsLesson.Lesson.Course == act.ClsLesson.Lesson.Course)
                        {
                            CourseCnt[(Int32)time.Week]++;
                            CourseSum++;
                        }
                    foreach (EnLsnAct failAct in sch.FailLsnActs)
                        if (failAct.ClsLesson.Lesson.Course == act.ClsLesson.Lesson.Course)
                            CourseSum++;
                    if (DaySum == 0)
                        CourseAverage = 0;
                    else
                        CourseAverage = (CourseSum + DaySum - 1) / DaySum;
                }

                //TchConcretes true教师冲突
                //ClsLsnRules 规则
                //CourseCnt 此课每天已经上的节数（数组）
                //CourseSum 此课每周总课时
                //CourseAverage 每天平均上课节数

                DtMatrix<Int32> Advantages = new DtMatrix<Int32>(DataRule.Solution);
                foreach (VcTime time in sch.Matrix.eachTime())
                    if (sch.Matrix[time].LsnAct != null)
                        Advantages[time] = Int32.MinValue;
                    else
                    {
                        Advantages[time] = TchConcretes[time] ? -2 : (Int32)ClsLsnRules[time];
                        if (CourseCnt[(Int32)time.Week] >= CourseAverage)
                            if (Advantages[time] > -1)
                                Advantages[time] = -1;   //这一天上课比较多
                    }

                Int32 MaxAdvantage = Int32.MinValue;
                VcTime MaxAdvantageTime = new VcTime();
                foreach (VcTime time in Advantages.eachTime())
                    if (MaxAdvantage < Advantages[time])
                    {
                        MaxAdvantage = Advantages[time];
                        time.CopyFieldTo(MaxAdvantageTime);
                    }

                if (MaxAdvantage == Int32.MinValue)
                    return true; //没地方排了

                if (MaxAdvantage >= -1)
                {
                    CourseCnt[(Int32)MaxAdvantageTime.Week]++;
                    sch.Matrix[MaxAdvantageTime].LsnAct = act;
                    sch.FailLsnActs.Remove(act);
                }
            }

            foreach (VcTime time in sch.Matrix.eachTime())
                if (sch.FailLsnActs.Count > 0 && sch.Matrix[time].LsnAct == null)
                {
                    sch.Matrix[time].LsnAct = sch.FailLsnActs[sch.FailLsnActs.Count - 1];
                    sch.FailLsnActs.RemoveAt(sch.FailLsnActs.Count - 1);
                }

            return true;
        }

        /// <summary>
        /// 优化课表
        /// </summary>
        public void Optimization(IList<EnSquad> Squads, bool LockIsEnabled, Boolean AutoSave)
        {
            for (var i = 0; i < 2; i++)
                foreach (EnSquad sqd in Squads)
                    magic(sqd, LockIsEnabled, 200);

            if (AutoSave)
                foreach (EnSquad sqd in Squads)
                    this.Save(sqd);

            this.Modified = true;
            this.AfterScheduleChanged(null);
        }

        //花指定的时间(ms)优化课表
        private void magic(EnSquad squad, bool LockIsEnabled, Int32 time)
        {
        }
    }
}