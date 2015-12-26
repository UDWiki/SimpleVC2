using System;
using System.Collections.Generic;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.Facade
{
    //Biz层都是有类型的，为了界面复用，本层提供了基于VcEntity的接口
    public interface IDataFacade
    {
        EnSolution Solution { set; get; }

        IGrpMbrDataFacade Crs { get; }
        IGrpMbrDataFacade Tch { get; }
        ISqdDataFacade Sqd { get; }

        IRuleDataFacade Rule { get; }
    }

    public interface IGrpMbrDataFacade
    {
        IEtyDataFacade Grp { get; }
        IEtyDataFacade Mbr { get; }

        IList<BaseEntity> GetRlns(BaseEntity Ety);
        void CreateRln(BaseEntity Ety1, BaseEntity Ety2);
        void ReleaseRln(BaseEntity Ety1, BaseEntity Ety2);

        IList<VcEffect> EffectOfDelete(BaseEntity Ety);
        IList<VcEffect> EffectOfCreateRln(BaseEntity Ety1, BaseEntity Ety2);
        IList<VcEffect> EffectOfReleaseRln(BaseEntity Ety1, BaseEntity Ety2);
    }

    public interface ISqdDataFacade: IGrpMbrDataFacade
    {
        Int32 InitSqd(String SqdGrpName, Int32 SqdCount, Boolean Override);
    }

    public interface IEtyDataFacade
    {
        String Kind { get; }
        IList<BaseEntity> EtyList { get; }

        BaseEntity NewEty();
        BaseEntity SaveEty(BaseEntity Value);
        void Delete(BaseEntity Value);
        Boolean NameExist(BaseEntity Ety);
    }

    public interface IRuleDataFacade
    {
        IList<VcRuleCell> GetRules(BaseEntity Ety);
        void SetRules(BaseEntity Ety, IList<VcRuleCell> Value);
    }
}
