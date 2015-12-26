using System;
using System.Collections.Generic;
using System.Diagnostics;
using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.BizLogic.EngineV2
{
    public partial class EngineV2
    {
        /// <summary>
        /// ��ȡ����ʱ����
        /// </summary>
        public DtMatrix<eRule> GetActChangeRule(EnSquad squad,
            EnLsnAct sAct, VcTime sTime)
        {
            Debug.Assert(sAct != null, "Ϊ�򻯳����߼����տβ����ϡ�");

            //��������Ǳ���������ӵ��߼�
            //���裺
            //һ���������λ�õ�ǰ����
            //�������sAct��ǰ����
            //�����������λ�õ�Act�ƶ���sTime������ֵ
            //�ġ����sAct�ƶ�������λ�õ�����ֵ
            //�塢�Ƚϵ�ǰ���ƺ��ƶ�������ƣ���ý����ļ�ֵ

            SqdSchedule NowSch = SqdScheduleList[squad];

            //һ���������λ�õ�ǰ���� 
            DtMatrix<eRule> NowRules = this.GetSqdRule(squad);

            //�������sAct��ǰ���� 
            eRule sActNowRule;

            if (NowRules.TestTime(sTime))
                sActNowRule = NowRules[sTime];
            else
                sActNowRule = eRule.common;  //δ��

            //�����������λ�õ�Act�ƶ���sTime������ֵ
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
                            FutureRules[time] = eRule.crisscross;  //�˿γ��Ƶ�sTime���½�ʦ��ͻ
                        else
                            FutureRules[time] = ClsLsnRuleList[act.ClsLesson][sTime]; //�Ƶ�sTime�������
                    }
            }
            else
                //����Act�ƶ���δ�ţ�������һ����eRule.common
                foreach (VcTime time in FutureRules.eachTime())
                    FutureRules[time] = eRule.common;

            //�ġ����sAct�ƶ�������λ�õ�����ֵ
            DtMatrix<eRule> sActFutureRules = new DtMatrix<eRule>(DataRule.Solution);

            DtMatrix<eRule> TmpRules = ClsLsnRuleList[sAct.ClsLesson];
            foreach (VcTime time in TmpRules.eachTime())
                sActFutureRules[time] = TmpRules[time];

            //���γ̳�ͻ
            if (sAct.ClsLesson.Teacher != null)
            {
                EnTeacher NowTch = sAct.ClsLesson.Teacher;
                DtMatrix<IList<EnLsnAct>> sTchLsns = this.GetTchMatrix(NowTch);
                foreach (VcTime time in sTchLsns.eachTime())
                    if (sTchLsns[time] != null && sTchLsns[time].Count > 0)
                        foreach (EnLsnAct act in sTchLsns[time])
                            if (act != NowSch.Matrix[time].LsnAct   //��ǰ�α��,�ᱻ����
                                && act.ClsLesson.Teacher == NowTch)
                            {
                                sActFutureRules[time] = eRule.crisscross;  //sAct�Ƶ�time�󽫵��³�ͻ
                                break;
                            }
            }

            //�塢�Ƚϵ�ǰ���ƺ��ƶ�������ƣ���ý����ļ�ֵ����
            DtMatrix<eRule> Result = new DtMatrix<eRule>(DataRule.Solution);

            //NowRules��sActNowRule��FutureRules��sActFutureRules
            //����λ�õ�ǰ����\�϶�Դ��ǰ����\����λ�����϶�Դ������\�϶�Դ�ϵ�����λ�õ�����

            foreach (VcTime time in Result.eachTime())
            {
                if (NowSch.Matrix[time].LsnAct != null
                    && NowSch.Matrix[time].LsnAct.ClsLesson == sAct.ClsLesson)
                {
                    Result[time] = eRule.crisscross; //ͬ���Ŀ��ƶ�û�����
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
            //NowRules��sActNowRule��FutureRules��sActFutureRules
            //����λ�õ�ǰ����\�϶�Դ��ǰ����\����λ�����϶�Դ������\�϶�Դ�ϵ�����λ�õ�����

            var crisscrossCount_Now = NowRule == eRule.crisscross ? 1 : 0;
            crisscrossCount_Now = crisscrossCount_Now + (sActNowRule == eRule.crisscross ? 1 : 0);

            var crisscrossCount_Future = FutureRule == eRule.crisscross ? 1 : 0;
            crisscrossCount_Future = crisscrossCount_Future + (sActFutureRule == eRule.crisscross ? 1 : 0);

            if (crisscrossCount_Now != crisscrossCount_Future)
                return crisscrossCount_Now > crisscrossCount_Future ? eRule.excellent : eRule.crisscross;

            var sum_Now = (Int32)NowRule + (Int32)sActNowRule;
            var sum_Future = (Int32)FutureRule + (Int32)sActFutureRule;

            if (sum_Now < sum_Future) //�Ż���
                return sum_Now - sum_Future < -2 ? eRule.excellent : eRule.fine;
            else if (sum_Now > sum_Future)  //״̬������ط�
                return sum_Now - sum_Future > 2 ? eRule.crisscross : eRule.ill;
            else
                return eRule.common;
        }

        /// <summary>
        /// �Զ��ſΣ�Ŀǰ�Ǵ��õķ���
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
        /// ������ѿտΰ��ŵ����ʵ�λ�ã����ᶯ���ŵĿ�
        /// </summary>
        private Boolean Automatic(EnSquad squad)
        {
            if (SqdScheduleList[squad].FailLsnActs.Count == 0)
                return false;

            //���ԣ�
            //��һ��������FailLsnActs������ͬVcClsLesson���ڣ�Ϊ����ٶȣ�
            //��ÿһVcLsnAct����������ֵ�������ۺ����������ŵ�����ֵ����Time

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

                //TchConcretes true��ʦ��ͻ
                //ClsLsnRules ����
                //CourseCnt �˿�ÿ���Ѿ��ϵĽ��������飩
                //CourseSum �˿�ÿ���ܿ�ʱ
                //CourseAverage ÿ��ƽ���Ͽν���

                DtMatrix<Int32> Advantages = new DtMatrix<Int32>(DataRule.Solution);
                foreach (VcTime time in sch.Matrix.eachTime())
                    if (sch.Matrix[time].LsnAct != null)
                        Advantages[time] = Int32.MinValue;
                    else
                    {
                        Advantages[time] = TchConcretes[time] ? -2 : (Int32)ClsLsnRules[time];
                        if (CourseCnt[(Int32)time.Week] >= CourseAverage)
                            if (Advantages[time] > -1)
                                Advantages[time] = -1;   //��һ���ϿαȽ϶�
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
                    return true; //û�ط�����

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
        /// �Ż��α�
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

        //��ָ����ʱ��(ms)�Ż��α�
        private void magic(EnSquad squad, bool LockIsEnabled, Int32 time)
        {
        }
    }
}