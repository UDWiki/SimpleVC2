using System;
using System.Collections.Generic;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.Facade
{
    internal class SqdDataFacadeImpl : GrpMbrDataFacade<EnSquadGroup, EnSquad>,
        ISqdDataFacade
    {
        protected class MbrBisFacade : MbrDataFacade<EnSquadGroup, EnSquad>,
            IEtyDataFacade
        {
            public MbrBisFacade(IDataRule dataRule)
            {
                Biz = dataRule.Sqd;
            }

            public String Kind
            {
                get { return "班级"; }
            }
        }

        protected class GrpBisFacade : GrpDataFacade<EnSquadGroup, EnSquad>,
            IEtyDataFacade
        {
            public GrpBisFacade(IDataRule dataRule)
            {
                Biz = dataRule.Sqd;
            }

            public String Kind
            {
                get { return "班级组"; }
            }
        }

        protected MbrBisFacade mbr;
        protected GrpBisFacade grp;
        protected IDataRule DataRule { get; private set; }

        public SqdDataFacadeImpl(IDataRule dataRule)
        {
            this.DataRule = dataRule;
            Biz = DataRule.Sqd;

            mbr = new MbrBisFacade(dataRule);
            grp = new GrpBisFacade(dataRule);
        }

        public IEtyDataFacade Grp
        {
            get { return grp; }
        }

        public IEtyDataFacade Mbr
        {
            get { return mbr; }
        }

        public override IList<VcEffect> EffectOfDelete(BaseEntity Ety)
        {
            IList<VcEffect> Result = new List<VcEffect>();

            if (Ety is EnSquad)
            {
                //删除班级后，此班级的课务安排会被删除
                EnSquad Sqd = Ety as EnSquad;
                foreach (EnClsLesson clsLsn in DataRule.Lsn.eachClsLesson())
                    if (clsLsn.Squad.Id == Sqd.Id)
                    {
                        VcEffect eft = new VcEffect();
                        eft.ClsLesson = clsLsn;
                        eft.Description = VcEffect.cWillBeDelete;
                        Result.Add(eft);
                    }
            }
            else
            {
                //删除班级组后，此组相关的所有课务会被删除
                EnSquadGroup SqdGrp = Ety as EnSquadGroup;
                foreach (EnClsLesson clsLsn in DataRule.Lsn.eachClsLesson())
                    if (clsLsn.Lesson.SquadGroup.Id == SqdGrp.Id)
                    {
                        VcEffect eft = new VcEffect();
                        eft.ClsLesson = clsLsn;
                        eft.Description = VcEffect.cWillBeDelete;
                        Result.Add(eft);
                    }
            }

            return Result;
        }

        public override IList<VcEffect> EffectOfCreateRln(BaseEntity Ety1, BaseEntity Ety2)
        {
            //增加班级与班级组关系后，此班级组的课务安排会自动应用到此班级
            EnSquadGroup Grp = Ety1 is EnSquadGroup ? Ety1 as EnSquadGroup : Ety2 as EnSquadGroup;
            EnSquad Ety = Ety1 is EnSquad ? Ety1 as EnSquad : Ety2 as EnSquad;

            IList<VcEffect> Result = new List<VcEffect>();
            IList<EnLesson> Lsns = DataRule.Lsn.GetLessons(Grp);
            foreach (EnLesson Lsn in Lsns)
            {
                VcEffect eft = new VcEffect();
                eft.ClsLesson = new EnClsLesson();
                eft.ClsLesson.Lesson = Lsn;
                eft.ClsLesson.Squad = Ety;
                eft.Description = VcEffect.cWillBeCreate;
                Result.Add(eft);
            }

            return Result;
        }

        public override IList<VcEffect> EffectOfReleaseRln(BaseEntity Ety1, BaseEntity Ety2)
        {
            //移除班级与班级组关系后，此班级通过此组获得的课务安排会被删除
            EnSquadGroup Grp = Ety1 is EnSquadGroup ? Ety1 as EnSquadGroup : Ety2 as EnSquadGroup;
            EnSquad Ety = Ety1 is EnSquad ? Ety1 as EnSquad : Ety2 as EnSquad;

            IList<VcEffect> Result = new List<VcEffect>();
            foreach (EnClsLesson clsLsn in DataRule.Lsn.eachClsLesson())
                if (clsLsn.Lesson.SquadGroup.Id == Grp.Id
                    && clsLsn.Squad.Id == Ety.Id)
                {
                    VcEffect eft = new VcEffect();
                    eft.ClsLesson = clsLsn;
                    eft.Description = VcEffect.cWillBeDelete;
                    Result.Add(eft);
                }
            return Result;
        }

        private EnSquadGroup FindSqdGrpByName(String name)
        {
            foreach (EnSquadGroup grp in this.Grp.EtyList)
                if (grp.Name == name)
                    return grp;

            return null;
        }

        private EnSquad FindSqdByName(String name)
        {
            foreach (EnSquad sqd in this.Mbr.EtyList)
                if (sqd.Name == name)
                    return sqd;

            return null;
        }

        public Int32 InitSqd(String SqdGrpName, Int32 SqdCount, Boolean IfExistThenIgnore)
        {
            Int32 Result = 0;
            EnSquadGroup SqdGrp = null;
            if (IfExistThenIgnore)
                SqdGrp = FindSqdGrpByName(SqdGrpName);
            if (SqdGrp == null)
            {
                SqdGrp = new EnSquadGroup();
                SqdGrp.Name = SqdGrpName;
                SqdGrp = this.Grp.SaveEty(SqdGrp) as EnSquadGroup;
                if (SqdGrp == null)
                    return -1;
            }

            IList<EnSquad> Sqds = new List<EnSquad>();
            for (Int32 i = 1; i <= SqdCount; i++)
            {
                String SqdName = SqdGrpName + "(" + i + ")";
                EnSquad Squad = null;
                if (IfExistThenIgnore)
                    Squad = FindSqdByName(SqdName);
                if (Squad == null)
                {
                    Squad = new EnSquad();
                    Squad.Name = SqdName;
                    Squad = this.Mbr.SaveEty(Squad) as EnSquad;
                    if (Squad == null)
                        return -1;

                    Result++;
                }

                Sqds.Add(Squad);
            }

            IList<BaseEntity> Mbrs = this.GetRlns(SqdGrp);
            foreach (EnSquad sqd in Sqds)
                if (!Mbrs.Contains(sqd))
                    this.CreateRln(SqdGrp, sqd);

            return Result;
        }
    }
}
