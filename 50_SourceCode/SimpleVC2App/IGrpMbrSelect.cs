using System;
using System.Collections.Generic;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.Facade;

namespace Telossoft.SimpleVC.WinFormApp
{
    public interface IGrpSelect
    {
        String Kind { get; }
        IList<BaseEntity> List { get; }
    }



    public class IGrpMbrBizFacadeToIGrpSelect : IGrpSelect
    {
        IGrpMbrDataFacade grpMbrBizFacade;
        public IGrpMbrBizFacadeToIGrpSelect(IGrpMbrDataFacade grpMbrBizFacade)
        {
            this.grpMbrBizFacade = grpMbrBizFacade;
        }

        public string Kind
        {
            get { return grpMbrBizFacade.Grp.Kind; }
        }

        public IList<BaseEntity> List
        {
            get { return grpMbrBizFacade.Grp.EtyList; }
        }
    }

    public class IGrpMbrBizFacadeToIMbrSelect : IMbrSelect
    {
        IGrpMbrDataFacade grpMbrBizFacade;
        public IGrpMbrBizFacadeToIMbrSelect(IGrpMbrDataFacade grpMbrBizFacade)
        {
            this.grpMbrBizFacade = grpMbrBizFacade;
            AllEty = new BaseEntity();
            AllEty.Name = "Ыљга" + grpMbrBizFacade.Mbr.Kind;
        }

        protected BaseEntity AllEty;
        public string Kind
        {
            get { return grpMbrBizFacade.Mbr.Kind; }
        }

        public IList<BaseEntity> GroupList
        {
            get 
            {
                IList<BaseEntity> Grps = new List<BaseEntity>();
                Grps.Add(AllEty);
                ExIList.Append<BaseEntity>(grpMbrBizFacade.Grp.EtyList, Grps);
                return Grps;
            }
        }

        public IList<BaseEntity> GetMbrList(BaseEntity grp)
        {
            if (grp == this.AllEty)
                return grpMbrBizFacade.Mbr.EtyList;
            else
                return grpMbrBizFacade.GetRlns(grp);
        }
    }
}
