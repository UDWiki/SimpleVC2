using System;
using System.Collections.Generic;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.EngineV2
{
    public partial class EngineV2 : IEngine
    {
        /// <summary>
        /// �����ʼ��
        /// </summary>
        public void Init( )
        {
            SetDataList();             //����ȫУ��Ч���顢�пεİ༶���пεĽ�ʦ
            SetSqdScheduleListData();  //���ɰ༶�α�Ŀ�
            SetClsLsnRuleList();       //����ClsLesson�Ĺ����
            SetSqdScheduleListRule();  //���ɰ༶�α�Ĺ���,Ӧ��clsLsn�Ĺ��򲢱�ǳ���ʦ�ڿγ�ͻ
            SetFailActs();             //����ȫУҪ���������
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
        /// ��ȡһ����ĳ�Ա������"ȫ��"��"ȫ���༶"��"ȫ����ʦ"
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
        /// �пα��༭��
        /// </summary>
        public bool IsModified(BaseEntity entity)
        {
            if (entity is EnSquad)
            {
                //���༶�α��򿪣�ɾ���༶����Ҫ�رմ򿪵Ŀα�Form��
                //�α�Form�ر�ʱ��ҪIsModified�������Ǵ˰༶�Ѿ�����SqdScheduleList��
                //ֱ�ӷ���һ��false���򻯳����߼�(����һ������,ҪС��)
                if (SqdScheduleList.ContainsKey(entity as EnSquad))
                    return SqdScheduleList[entity as EnSquad].Modified;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// ĳ�༶���д����ŵĿ�
        /// </summary>
        public IList<EnLsnAct> GetFailLsnActs(EnSquad squad)
        {
            IList<EnLsnAct> Result = new List<EnLsnAct>();
            ExIList.Append<EnLsnAct>(SqdScheduleList[squad].FailLsnActs, Result);

            return Result;
        }

        /// <summary>
        ///  ��õ�ǰ����̬�Ŀα�δ������ڴ��еĿα�
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
        ///  ��õ�ǰ����̬�Ŀα����
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
        /// ��ȡ��ʦ�α�
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
        /// ����ĳ�༶�α�
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
        /// �������б༭���Ŀα�
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
        /// ����ĳ�༶�α�ı༭
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
        /// �������пα�ı༭
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
        /// �α��Ϸź�
        /// </summary>
        public void Move(EnSquad squad, EnLsnAct sAct, VcTime tTime)
        {
            SqdSchedule sch = SqdScheduleList[squad];

            //�����Դ��act��ʱ��sTime
            VcTime sTime = new VcTime();
            if (!sch.FailLsnActs.Contains(sAct))
                foreach (VcTime tt in sch.Matrix.eachTime())
                    if (sch.Matrix[tt].LsnAct == sAct)
                    {
                        tt.CopyFieldTo(sTime);
                        break;
                    }


            if (sTime == tTime)   //ԭ����
                return;
            else if (sTime.HasValue && !tTime.HasValue)  //�ӿα��ϵ�FailLsnActs
            {
                EnLsnAct ta = sch.Matrix[sTime].LsnAct;
                sch.FailLsnActs.Add(ta);
                sch.Matrix[sTime].LsnAct = null;
            }
            else if (!sTime.HasValue && tTime.HasValue)    //��FailLsnActs�ϵ��α�
            {
                if (sch.Matrix[tTime].LsnAct != null)
                    return;

                sch.Matrix[tTime].LsnAct = sAct;
                sch.FailLsnActs.Remove(sAct);
            }
            else if (sTime.HasValue && tTime.HasValue)    //�α��ϵ��α�
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
        ///  ��õ�ǰ����̬�Ŀα�ĳ�ڵĹ����б����ͻ
        /// </summary>
        public IList<VcActEtyRelation> GetSqdClash(EnSquad squad, VcTime time)
        {
            List<VcActEtyRelation> Result = new List<VcActEtyRelation>();
            if (!this.TimeIsEnabled(time))
                return Result;

            //���VcLsnAct
            EnLsnAct act = SqdScheduleList[squad].Matrix[time].LsnAct;
            if (act == null)
                return Result;

            //����ڿν�ʦ
            if (act.ClsLesson.Teacher != null)
            {
                Result.Add(new VcActEtyRelation(act.ClsLesson.Teacher, eActEtyRelation.teach));

                //�������ͻ�γ�
                foreach (SqdSchedule sch in SqdScheduleList.Values)
                    if (sch.Matrix[time].LsnAct != null
                        && sch.Matrix[time].LsnAct.ClsLesson.Squad != squad
                        && sch.Matrix[time].LsnAct.ClsLesson.Teacher == act.ClsLesson.Teacher)
                        Result.Add(new VcActEtyRelation(sch.Matrix[time].LsnAct.ClsLesson,
                            eActEtyRelation.clash, eRule.crisscross));
            }

            //���Lsn/ClsLsn��Rule
            Result.Add(new VcActEtyRelation(act.ClsLesson.Lesson, eActEtyRelation.rule,
                DataRule.Rule.GetRuleOfTime(act.ClsLesson.Lesson, time)));

            Result.Add(new VcActEtyRelation(act.ClsLesson, eActEtyRelation.rule,
                DataRule.Rule.GetRuleOfTime(act.ClsLesson, time)));

            //����Act��Ԫ��,���Rule
            foreach (BaseEntity ety in this.eachClsLsnComponent(act.ClsLesson))
                Result.Add(new VcActEtyRelation(ety, eActEtyRelation.rule,
                    DataRule.Rule.GetRuleOfTime(ety, time)));

            return Result;
        }
    }
}