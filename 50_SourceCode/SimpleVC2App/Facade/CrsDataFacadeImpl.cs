using System;
using System.Collections.Generic;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.Facade
{
    internal class CrsDataFacadeImpl : GrpMbrDataFacade<EnCourseGroup, EnCourse>,
        IGrpMbrDataFacade
    {
        protected class MbrBisFacade : MbrDataFacade<EnCourseGroup, EnCourse>,
            IEtyDataFacade
        {
            public MbrBisFacade(IDataRule dataRule)
            {
                Biz = dataRule.Crs;
            }

            public String Kind
            {
                get { return "课程"; }
            }
        }

        protected class GrpBisFacade : GrpDataFacade<EnCourseGroup, EnCourse>,
            IEtyDataFacade
        {
            public GrpBisFacade(IDataRule dataRule)
            {
                Biz = dataRule.Crs;
            }

            public String Kind
            {
                get { return "课程组"; }
            }
        }

        protected MbrBisFacade mbr;
        protected GrpBisFacade grp;
        protected IDataRule DataRule { get; private set; }

        public CrsDataFacadeImpl(IDataRule dataRule)
        {
            this.DataRule = dataRule;
            Biz = DataRule.Crs;

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
            //课程删除后对应的课务安排会被删除
            if (!(Ety is EnCourse))
                return null;

            IList<VcEffect> Result = new List<VcEffect>();
            EnCourse crs = Ety as EnCourse;
            foreach(EnClsLesson clsLsn in VC2WinFmApp.DataRule.Lsn.eachClsLesson())
                if (clsLsn.Lesson.Course.Id == crs.Id)
                {
                    VcEffect eft = new VcEffect();
                    eft.ClsLesson = clsLsn;
                    eft.Description = VcEffect.cWillBeDelete;
                    Result.Add(eft);
                }

            return Result;
        }
    }
}
