using System.Collections.Generic;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.Facade
{
    internal class MbrDataFacade<Grp, Mbr>
        where Grp : BaseEntity, new()
        where Mbr : BaseEntity, new()
    {
        internal IGrpMbrDataRule<Grp, Mbr> Biz;

        public IList<BaseEntity> EtyList
        {
            get
            {
                return new GIListTypeChange<Mbr, BaseEntity>(Biz.MbrList);
            }
        }

        public BaseEntity NewEty()
        {
            return new Mbr();
        }

        public BaseEntity SaveEty(BaseEntity Value)
        {
            if (Value.Id == 0)
                return Biz.SaveNewMbr(Value as Mbr);
            else
                return Biz.SaveExistMbr(Value as Mbr);
        }

        public void Delete(BaseEntity Value)
        {
            Biz.DeleteMbr(Value as Mbr);
        }

        public bool NameExist(BaseEntity Ety)
        {
            return Biz.MbrNameExist(Ety as Mbr, Ety.Name);
        }
    }

    internal class GrpDataFacade<Grp, Mbr>
        where Grp : BaseEntity, new()
        where Mbr : BaseEntity, new()
    {
        internal IGrpMbrDataRule<Grp, Mbr> Biz;

        public IList<BaseEntity> EtyList
        {
            get
            {
                return new GIListTypeChange<Grp, BaseEntity>(Biz.GrpList);
            }
        }

        public virtual bool ReadOnly
        {
            get { return false; }
        }

        public BaseEntity NewEty()
        {
            return new Grp();
        }

        public BaseEntity SaveEty(BaseEntity Value)
        {
            if (Value.Id == 0)
                return Biz.SaveNewGrp(Value as Grp);
            else
                return Biz.SaveExistGrp(Value as Grp);
        }

        public void Delete(BaseEntity Value)
        {
            Biz.DeleteGrp(Value as Grp);
        }

        public bool NameExist(BaseEntity Ety)
        {
            return Biz.GrpNameExist(Ety as Grp, Ety.Name);
        }
    }


    internal class GrpMbrDataFacade<Grp, Mbr>
        where Grp : BaseEntity
        where Mbr : BaseEntity
    {
        internal IGrpMbrDataRule<Grp, Mbr> Biz;

        public IList<BaseEntity> GetRlns(BaseEntity Ety)
        {
            if (Ety is Grp)
                return new GIListTypeChange<Mbr, BaseEntity>(Biz.GetMembes(Ety as Grp));
            else
                return new GIListTypeChange<Grp, BaseEntity>(Biz.GetGroups(Ety as Mbr));
        }

        public void CreateRln(BaseEntity Ety1, BaseEntity Ety2)
        {
            if (Ety1 is Grp)
                Biz.AddMember(Ety1 as Grp, Ety2 as Mbr);
            else
                Biz.AddMember(Ety2 as Grp, Ety1 as Mbr);
        }

        public void ReleaseRln(BaseEntity Ety1, BaseEntity Ety2)
        {
            if (Ety1 is Grp)
                Biz.RemoveMember(Ety1 as Grp, Ety2 as Mbr);
            else
                Biz.RemoveMember(Ety2 as Grp, Ety1 as Mbr);
        }

        public virtual IList<VcEffect> EffectOfDelete(BaseEntity Ety)
        {
            return null;
        }

        public virtual IList<VcEffect> EffectOfCreateRln(BaseEntity Ety1, BaseEntity Ety2)
        {
            return null;
        }

        public virtual IList<VcEffect> EffectOfReleaseRln(BaseEntity Ety1, BaseEntity Ety2)
        {
            return null;
        }
    }
}
