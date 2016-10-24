using System.Collections.Generic;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.DataRule
{
    internal class CrsDataRuleImpl : ICrsDataRule
    {
        protected DataRuleImpl ThisModule { get; private set; }
        public CrsDataRuleImpl(DataRuleImpl thisModule)
        {
            this.ThisModule = thisModule;
        }

        public IList<EnCourseGroup> GrpList
        {
            get
            {
                return ThisModule.Dac.Crs.GrpDAC.List;
            }
        }

        public EnCourseGroup GetGrp(long Id)
        {
            return ThisModule.Dac.Crs.GrpDAC.Get(Id);
        }

        public EnCourseGroup SaveNewGrp(EnCourseGroup Value)
        {
            return ThisModule.Dac.Crs.GrpDAC.SaveNew(Value);
        }

        public EnCourseGroup SaveExistGrp(EnCourseGroup Value)
        {
            EnCourseGroup Result =  ThisModule.Dac.Crs.GrpDAC.SaveExist(Value);
            ThisModule.SendDataChanged();

            return Result;
        }

        public bool GrpNameExist(EnCourseGroup grp, string Name)
        {
            return ThisModule.Dac.Crs.GrpDAC.NameExist(grp, Name);
        }

        public IList<EnSubject> MbrList
        {
            get
            {
                return ThisModule.Dac.Crs.MbrDAC.List;
            }
        }

        public EnSubject GetMbr(long Id)
        {
            return ThisModule.Dac.Crs.MbrDAC.Get(Id);
        }

        public EnSubject SaveNewMbr(EnSubject Value)
        {
            return ThisModule.Dac.Crs.MbrDAC.SaveNew(Value);
        }

        public EnSubject SaveExistMbr(EnSubject Value)
        {
            EnSubject Result = ThisModule.Dac.Crs.MbrDAC.SaveExist(Value);
            ThisModule.SendDataChanged();

            return Result;
        }

        public bool MbrNameExist(EnSubject mbr, string Name)
        {
            return ThisModule.Dac.Crs.MbrDAC.NameExist(mbr, Name);
        }

        public void DeleteGrp(EnCourseGroup grp)
        {
            ThisModule.Dac.Rule.DeleteRuleOfEty(grp);
            ThisModule.Dac.Crs.DeleteGrp(grp);

            ThisModule.SendDataChanged();
        }

        public void DeleteMbr(EnSubject mbr)
        {
            //课程删除后对应的课务安排会被删除
            ThisModule.Lsn.CourseIsDelete(mbr);

            ThisModule.Dac.Rule.DeleteRuleOfEty(mbr);
            ThisModule.Dac.Crs.DeleteMbr(mbr);

            ThisModule.SendDataChanged();
        }

        public IList<EnSubject> GetMembes(EnCourseGroup grp)
        {
            return ThisModule.Dac.Crs.GetMembes(grp);
        }

        public IList<EnCourseGroup> GetGroups(EnSubject mbr)
        {
            return ThisModule.Dac.Crs.GetGroups(mbr);
        }

        public void AddMember(EnCourseGroup grp, EnSubject mbr)
        {
            ThisModule.Dac.Crs.CreateRelation(grp, mbr);

            ThisModule.SendDataChanged();
        }

        public void RemoveMember(EnCourseGroup grp, EnSubject mbr)
        {
            ThisModule.Dac.Crs.ReleaseRelation(grp, mbr);

            ThisModule.SendDataChanged();
        }
    }
}
