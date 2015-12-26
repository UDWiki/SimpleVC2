using System.Collections.Generic;
using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.BizLogic.DataRule
{
    internal class SqdDataRuleImpl : ISqdDataRule
    {
        protected DataRuleImpl ThisModule { get; private set; }
        public SqdDataRuleImpl(DataRuleImpl thisModule)
        {
            this.ThisModule = thisModule;
        }

        public IList<EnSquadGroup> GrpList
        {
            get
            {
                return ThisModule.Dac.Sqd.GrpDAC.List;
            }
        }

        public EnSquadGroup GetGrp(long Id)
        {
            return ThisModule.Dac.Sqd.GrpDAC.Get(Id);
        }

        public EnSquadGroup SaveNewGrp(EnSquadGroup Value)
        {
            return ThisModule.Dac.Sqd.GrpDAC.SaveNew(Value);
        }

        public EnSquadGroup SaveExistGrp(EnSquadGroup Value)
        {
            EnSquadGroup Result = ThisModule.Dac.Sqd.GrpDAC.SaveExist(Value);
            ThisModule.SendDataChanged();

            return Result;
        }

        public bool GrpNameExist(EnSquadGroup grp, string Name)
        {
            return ThisModule.Dac.Sqd.GrpDAC.NameExist(grp, Name);
        }

        public IList<EnSquad> MbrList
        {
            get
            {
                return ThisModule.Dac.Sqd.MbrDAC.List;
            }
        }

        public EnSquad GetMbr(long Id)
        {
            return ThisModule.Dac.Sqd.MbrDAC.Get(Id);
        }

        public EnSquad SaveNewMbr(EnSquad Value)
        {
            return ThisModule.Dac.Sqd.MbrDAC.SaveNew(Value);
        }

        public EnSquad SaveExistMbr(EnSquad Value)
        {
            EnSquad Result = ThisModule.Dac.Sqd.MbrDAC.SaveExist(Value);
            ThisModule.SendDataChanged();

            return Result;
        }

        public bool MbrNameExist(EnSquad mbr, string Name)
        {
            return ThisModule.Dac.Sqd.MbrDAC.NameExist(mbr, Name);
        }

        public void DeleteGrp(EnSquadGroup grp)
        {
            //删除班级组后，此组相关的所有课务会被删除
            ThisModule.Lsn.SquadGroupIsDelete(grp);

            ThisModule.Dac.Rule.DeleteRuleOfEty(grp);
            ThisModule.Dac.Sqd.DeleteGrp(grp);

            ThisModule.SendDataChanged();
        }

        public void DeleteMbr(EnSquad mbr)
        {
            //删除班级后，此班级的课务安排会被删除
            ThisModule.Lsn.SquadIsDelete(mbr);

            ThisModule.Dac.Rule.DeleteRuleOfEty(mbr);
            ThisModule.Dac.Sqd.DeleteMbr(mbr);

            ThisModule.SendDataChanged();
        }

        public IList<EnSquad> GetMembes(EnSquadGroup grp)
        {
            return ThisModule.Dac.Sqd.GetMembes(grp);
        }

        public IList<EnSquadGroup> GetGroups(EnSquad mbr)
        {
            return ThisModule.Dac.Sqd.GetGroups(mbr);
        }

        public void RemoveMember(EnSquadGroup grp, EnSquad mbr)
        {
            //移除班级与班级组关系后，此班级通过此组获得的课务安排会被删除
            ThisModule.Lsn.SquadRelationIsRelease(grp, mbr);

            ThisModule.Dac.Sqd.ReleaseRelation(grp, mbr);

            ThisModule.SendDataChanged();
        }

        public void AddMember(EnSquadGroup grp, EnSquad mbr)
        {
            //增加班级与班级组关系后，此班级组的课务安排会自动应用到此班级
            ThisModule.Lsn.SquadRelationIsCreate(grp, mbr);

            ThisModule.Dac.Sqd.CreateRelation(grp, mbr);

            ThisModule.SendDataChanged();
        }
    }
}
