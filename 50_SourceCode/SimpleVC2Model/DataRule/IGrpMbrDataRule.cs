using System;
using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    public interface IGrpMbrDataRule<Grp, Mbr>
        where Grp : BaseEntity
        where Mbr : BaseEntity
    {
        IList<Grp> GrpList { get; }
        Grp GetGrp(Int64 Id);
        Grp SaveNewGrp(Grp Value);
        Grp SaveExistGrp(Grp Value);
        Boolean GrpNameExist(Grp grp, String Name);

        IList<Mbr> MbrList { get; }
        Mbr GetMbr(Int64 Id);
        Mbr SaveNewMbr(Mbr Value);
        Mbr SaveExistMbr(Mbr Value);
        Boolean MbrNameExist(Mbr mbr, String Name);

        void DeleteGrp(Grp grp);
        void DeleteMbr(Mbr mbr);

        IList<Mbr> GetMembes(Grp grp);
        IList<Grp> GetGroups(Mbr mbr);

        void AddMember(Grp grp, Mbr mbr);
        void RemoveMember(Grp grp, Mbr mbr);
    }
}
