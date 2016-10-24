using System;
using System.Collections.Generic;
using System.Diagnostics;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.DataRule
{
    internal class LsnDataRuleImpl : ILsnDataRule
    {
        protected DataRuleImpl ThisModule { get; private set; }
        public LsnDataRuleImpl(DataRuleImpl thisModule)
        {
            this.ThisModule = thisModule;
        }

        //���·���Ϊ�������
        public void RemoveClsLsn(EnClsLesson clsLsn)
        {
            ThisModule.Dac.Rule.DeleteRuleOfEty(clsLsn);
            ThisModule.Dac.Lsn.DeleteClsLesson(clsLsn);
        }

        public void TeacherIsDelete(EnTeacher teacher)
        {
            foreach (EnClsLesson clsLsn in eachClsLesson())
                if (clsLsn.Teacher != null && clsLsn.Teacher.Id == teacher.Id)
                {
                    clsLsn.Teacher = null;
                    ThisModule.Dac.Lsn.SaveClsLesson(clsLsn);
                }
        }

        public void CourseIsDelete(EnSubject course)
        {
            IList<EnLesson> Lsns = new List<EnLesson>();
            foreach (EnLesson Lsn in ThisModule.Dac.Lsn.eachLesson())
                if (Lsn.Course.Id == course.Id)
                    Lsns.Add(Lsn);

            foreach (EnLesson Lsn in Lsns)
                RemoveLsn(Lsn);
        }

        public void SquadGroupIsDelete(EnSquadGroup squadGroup)
        {
            foreach (EnLesson lsn in GetLessons(squadGroup))
                RemoveLsn(lsn);
        }

        public void SquadIsDelete(EnSquad squad)
        {
            IList<EnClsLesson> ClsLsns = new List<EnClsLesson>();
            foreach (EnClsLesson clsLsn in eachClsLesson())
                if (clsLsn.Squad.Id == squad.Id)
                    ClsLsns.Add(clsLsn);

            foreach (EnClsLesson clsLsn in ClsLsns)
                this.RemoveClsLsn(clsLsn);
        }

        public void SquadRelationIsRelease(EnSquadGroup grp, EnSquad mbr)
        {
            //�Ƴ��༶��༶���ϵ�󣬴˰༶ͨ�������õĿ����Żᱻɾ��
            IList<EnClsLesson> ClsLsns = new List<EnClsLesson>();
            foreach (EnClsLesson clsLsn in eachClsLesson())
                if (clsLsn.Lesson.SquadGroup.Id == grp.Id 
                    && clsLsn.Squad.Id == mbr.Id)
                    ClsLsns.Add(clsLsn);

            foreach (EnClsLesson clsLsn in ClsLsns)
                this.RemoveClsLsn(clsLsn);
        }

        public void SquadRelationIsCreate(EnSquadGroup grp, EnSquad mbr)
        {
            //���Ӱ༶��༶���ϵ�󣬴˰༶��Ŀ����Ż��Զ�Ӧ�õ��˰༶
            IList<EnLesson> Lsns = GetLessons(grp);
            foreach (EnLesson lsn in Lsns)
            {
                EnClsLesson clsLsn = new EnClsLesson();
                clsLsn.Lesson = lsn;
                clsLsn.SharedTime = lsn.SharedTime;
                clsLsn.Squad = mbr;
                SaveClsLsnTree(clsLsn);
            }
        }

        //���Ϸ���Ϊ�������

        public void RemoveLsn(EnLesson Lsn)
        {
            foreach(EnClsLesson clsLsn in GetCLsLessons(Lsn))
                ThisModule.Dac.Rule.DeleteRuleOfEty(clsLsn);

            ThisModule.Dac.Rule.DeleteRuleOfEty(Lsn);
            ThisModule.Dac.Lsn.DeleteLesson(Lsn);

            ThisModule.SendDataChanged();
        }

        public IList<EnLesson> GetLessons(EnSquadGroup SquadGrp)
        {
            return ThisModule.Dac.Lsn.GetLessons(SquadGrp);
        }

        public IList<EnClsLesson> GetCLsLessons(EnLesson lsn)
        {
            return ThisModule.Dac.Lsn.GetClsLessons(lsn);
        }

        private Boolean TestMaxSharedTime(EnLesson Lsn, IList<EnClsLesson> ClsLsns)
        //��֤SharedTime��������,����û����
        {
            if (Lsn.SharedTime < 0 || Lsn.SharedTime > VcTimeLogic.cMaxSharedTime)
                return false;

            foreach(EnClsLesson clsLsn in ClsLsns)
                if (clsLsn.SharedTime < 0 || clsLsn.SharedTime > VcTimeLogic.cMaxSharedTime)
                    return false;

            return true;
        }

        public void SaveLsnTree(EnLesson Lsn, IList<EnClsLesson> ClsLsns)
        {
            Debug.Assert(TestMaxSharedTime(Lsn, ClsLsns), "�Ͽ�ʱ�����ݳ�����Χ(0--" + VcTimeLogic.cMaxSharedTime + ")!");

            Lsn = ThisModule.Dac.Lsn.SaveLesson(Lsn);
            foreach (EnClsLesson ClsLsn in ClsLsns)
            {
                ClsLsn.Lesson = Lsn;
                SaveClsLsnTree(ClsLsn);
            }

            ThisModule.SendDataChanged();
        }

        internal void SaveClsLsnTree(EnClsLesson ClsLsn)
        {
            EnClsLesson clsLsn = ThisModule.Dac.Lsn.SaveClsLesson(ClsLsn);

            IList<EnLsnAct> Acts = ThisModule.Dac.Lsn.GetLsnActs(clsLsn);
            if (ClsLsn.SharedTime > Acts.Count)
            {
                IList<EnLsnAct> WAdd = new List<EnLsnAct>();
                for (Int32 i = 0; i < ClsLsn.SharedTime - Acts.Count; i++)
                {
                    EnLsnAct act = new EnLsnAct();
                    act.ClsLesson = clsLsn;
                    WAdd.Add(act);
                }
                ThisModule.Lsn.SaveLsnActs(WAdd);
            }
            else if (ClsLsn.SharedTime < Acts.Count)
            {
                //��ʱ���٣������Ƴ�ʱ����Ч�Ŀ�
                IList<EnLsnAct> WDel = new List<EnLsnAct>();
                Int32 WDelCount = Acts.Count - ClsLsn.SharedTime;

                foreach (EnLsnAct act in Acts)
                    if (!act.Time.HasValue && WDel.Count < WDelCount)
                        WDel.Add(act);

                foreach (EnLsnAct act in Acts)
                    if (act.Time.HasValue && WDel.Count < WDelCount)
                        WDel.Add(act);

                ThisModule.Dac.Lsn.DeleteLsnActs(WDel);
            }

            ThisModule.SendDataChanged();
        }
        
        public IEnumerable<EnClsLesson> eachClsLesson()
        {
            return ThisModule.Dac.Lsn.eachClsLesson();
        }

        public IEnumerable<EnLsnAct> eachLsnAct()
        {
            return ThisModule.Dac.Lsn.eachLsnAct();
        }

        public IList<EnLsnAct> GetLsnActs(BaseEntity Ety)
        {
            Debug.Assert(Ety is EnSquad || Ety is EnTeacher, "ֻ�а༶���ʦ���пα�" + Ety);

            IList<EnLsnAct> Result = new List<EnLsnAct>();
            if (Ety is EnTeacher)
            {
                foreach (EnClsLesson clsLsn in ThisModule.Dac.Lsn.eachClsLesson())
                    if (clsLsn.Teacher.Id == Ety.Id)
                        ExIList.Append<EnLsnAct>(ThisModule.Dac.Lsn.GetLsnActs(clsLsn), Result);
            }
            else
                foreach (EnClsLesson clsLsn in ThisModule.Dac.Lsn.eachClsLesson())
                    if (clsLsn.Squad.Id == Ety.Id)
                        ExIList.Append<EnLsnAct>(ThisModule.Dac.Lsn.GetLsnActs(clsLsn), Result);

            return Result;
        }

        public void SaveLsnActs(IList<EnLsnAct> Values)
        {
            ThisModule.Dac.Lsn.SaveLsnActs(Values);
        }

        public void ClearAllLesson()
        {
            ThisModule.Dac.Rule.DeleteRuleOfKind(typeof(EnClsLesson));
            ThisModule.Dac.Rule.DeleteRuleOfKind(typeof(EnLesson));
            //VcLsnAct�����ڹ���

            ThisModule.Dac.Lsn.ClearAll();

            ThisModule.SendDataChanged();
        }
    }
}
