using System.Collections.Generic;
using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.BizLogic.DataRule
{
    internal class TchDataRuleImpl : ITchDataRule
    {
        protected DataRuleImpl ThisModule { get; private set; }
        public TchDataRuleImpl(DataRuleImpl thisModule)
        {
            this.ThisModule = thisModule;
        }

        public IList<EnTeacherGroup> GrpList
        {
            get
            {
                return ThisModule.Dac.Tch.GrpDAC.List;
            }
        }

        public EnTeacherGroup GetGrp(long Id)
        {
            return ThisModule.Dac.Tch.GrpDAC.Get(Id);
        }

        public EnTeacherGroup SaveNewGrp(EnTeacherGroup Value)
        {
            return ThisModule.Dac.Tch.GrpDAC.SaveNew(Value);
        }

        public EnTeacherGroup SaveExistGrp(EnTeacherGroup Value)
        {
            EnTeacherGroup Result = ThisModule.Dac.Tch.GrpDAC.SaveExist(Value);
            ThisModule.SendDataChanged();

            return Result;
        }

        public bool GrpNameExist(EnTeacherGroup grp, string Name)
        {
            return ThisModule.Dac.Tch.GrpDAC.NameExist(grp, Name);
        }

        public IList<EnTeacher> MbrList
        {
            get
            {
                return ThisModule.Dac.Tch.MbrDAC.List;
            }
        }

        public EnTeacher GetMbr(long Id)
        {
            return ThisModule.Dac.Tch.MbrDAC.Get(Id);
        }

        public EnTeacher SaveNewMbr(EnTeacher Value)
        {
            return ThisModule.Dac.Tch.MbrDAC.SaveNew(Value);
        }

        public EnTeacher SaveExistMbr(EnTeacher Value)
        {
            EnTeacher Result = ThisModule.Dac.Tch.MbrDAC.SaveExist(Value);
            ThisModule.SendDataChanged();

            return Result;
        }

        public bool MbrNameExist(EnTeacher mbr, string Name)
        {
            return ThisModule.Dac.Tch.MbrDAC.NameExist(mbr, Name);
        }

        public void DeleteGrp(EnTeacherGroup grp)
        {
            ThisModule.Dac.Rule.DeleteRuleOfEty(grp);
            ThisModule.Dac.Tch.DeleteGrp(grp);

            ThisModule.SendDataChanged();
        }

        public void DeleteMbr(EnTeacher mbr)
        {
            //教师删除后对应的课务安排会被设置为无授课/辅导教师
            ThisModule.Lsn.TeacherIsDelete(mbr);

            ThisModule.Dac.Rule.DeleteRuleOfEty(mbr);
            ThisModule.Dac.Tch.DeleteMbr(mbr);

            ThisModule.SendDataChanged();
        }

        public IList<EnTeacher> GetMembes(EnTeacherGroup grp)
        {
            return ThisModule.Dac.Tch.GetMembes(grp);
        }

        public IList<EnTeacherGroup> GetGroups(EnTeacher mbr)
        {
            return ThisModule.Dac.Tch.GetGroups(mbr);
        }

        public void AddMember(EnTeacherGroup grp, EnTeacher mbr)
        {
            ThisModule.Dac.Tch.CreateRelation(grp, mbr);
            ThisModule.SendDataChanged();
        }

        public void RemoveMember(EnTeacherGroup grp, EnTeacher mbr)
        {
            ThisModule.Dac.Tch.ReleaseRelation(grp, mbr);
            ThisModule.SendDataChanged();
        }
    }
}
