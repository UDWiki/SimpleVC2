using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///基础的数据 
    /// </summary>
    public interface IGrpMbrDac<Grp, Mbr>
        where Grp : BaseEntity
        where Mbr : BaseEntity
    {
        IEntityDac<Grp> GrpDAC { get; }
        IEntityDac<Mbr> MbrDAC { get; }

        void DeleteGrp(Grp grp);
        void DeleteMbr(Mbr mbr);

        IList<Mbr> GetMembes(Grp grp);
        IList<Grp> GetGroups(Mbr mbr);

        void CreateRelation(Grp grp, Mbr mbr);
        void ReleaseRelation(Grp grp, Mbr mbr);

        void ClearAll();
    }
}
