using System;
using System.Collections.Generic;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.Facade
{
    internal class TchDataFacadeImpl : GrpMbrDataFacade<EnTeacherGroup, EnTeacher>, 
        IGrpMbrDataFacade
    {
        protected class MbrBisFacade : MbrDataFacade<EnTeacherGroup, EnTeacher>, 
            IEtyDataFacade
        {
            public MbrBisFacade(IDataRule dataRule)
            {
                Biz = dataRule.Tch;
            }

            public String Kind
            {
                get { return "��ʦ"; }
            }

        }

        protected class GrpBisFacade : GrpDataFacade<EnTeacherGroup, EnTeacher>, 
            IEtyDataFacade
        {
            public GrpBisFacade(IDataRule dataRule)
            {
                Biz = dataRule.Tch;
            }

            public String Kind
            {
                get { return "��ʦ��"; }
            }

        }

        protected MbrBisFacade mbr;
        protected GrpBisFacade grp;
        protected IDataRule DataRule { get; private set; }
        
        public TchDataFacadeImpl(IDataRule dataRule)
        {
            this.DataRule = dataRule;
            Biz = DataRule.Tch;

            mbr = new MbrBisFacade(DataRule);
            grp = new GrpBisFacade(DataRule);
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
            //��ʦɾ�����Ӧ�Ŀ����Żᱻ����Ϊ���ڿ�/������ʦ
            if (!(Ety is EnTeacher))
                return null;

            IList<VcEffect> Result = new List<VcEffect>();
            EnTeacher tch = Ety as EnTeacher;
            foreach (EnClsLesson clsLsn in VC2WinFmApp.DataRule.Lsn.eachClsLesson())
                if (clsLsn.Teacher != null && clsLsn.Teacher.Id == tch.Id)
                {
                    VcEffect eft = new VcEffect();
                    eft.ClsLesson = clsLsn;
                    eft.Description = clsLsn.Lesson.IsSelfStudy ? VcEffect.cWillNoGuider: VcEffect.cWillNoTeacher;
                    Result.Add(eft);
                }

            return Result;
        }
    }
}
