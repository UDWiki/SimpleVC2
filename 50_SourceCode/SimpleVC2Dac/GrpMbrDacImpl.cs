using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using SGLibrary.Extend.DB;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.Dac
{
    //废弃不用的代码
    internal class GrpMbrDacImpl<Grp, Mbr> : IGrpMbrDac<Grp, Mbr>
        where Grp : BaseEntity, new()
        where Mbr : BaseEntity, new()
    {

        protected class AtomDtAccess<T> : IEntityDac<T>
            where T : BaseEntity, new()
        {
            internal IDbAccess OleDB { get; private set; }

            public IDictionary<Int64, T> Entities
                = new SortedDictionary<Int64, T>();

            private Boolean IsCourse;
            public String Kind;

            public AtomDtAccess(IDbAccess OleDB)
            {
                this.OleDB = OleDB;

                IsCourse = typeof(T) == typeof(EnCourse);
                Kind = typeof(T).Name;

                foreach (OleDbDataReader reader in OleDB.EachRows(
                    "select Id, FName, FColor from TAtomAndGrp"
                    + " where FKind = '" + Kind + "'"))
                {
                    T ety = new T();
                    ety.Id = Convert.ToInt64(reader[0]);
                    ety.Name = reader[1].ToString();
                    if (IsCourse)
                        (ety as EnCourse).Color = Color.FromArgb(ExConvert.TryToInt32(reader[2], 0));

                    Entities.Add(ety.Id, ety);
                }
            }

            public IList<T> List
            {
                get
                {
                    IList<T> Result = new List<T>();
                    foreach (T t in Entities.Values)
                        Result.Add(t);

                    return Result;
                }
            }

            public T Get(long Id)
            {
                if (Id == 0)
                    return null;

                T Result;

                return Entities.TryGetValue(Id, out Result) ? Result : null;
            }

            public bool NameExist(T ety, string Name)
            {
                foreach (T t in Entities.Values)
                    if (t.Id != ety.Id && t.Name == Name)
                        return true;

                return false;
            }

            public virtual T SaveNew(T Value)
            {
                T ety = Value.Clone() as T;
                Insert(ety);

                Debug.Assert(ety.Id != 0, "SaveNewEty后没有得到Id");

                Entities.Add(ety.Id, ety);
                return ety;
            }

            public virtual T SaveExist(T Value)
            {
                //要注意 Entities中的对象可能被其它类引用，所以只能更新其值
                //不可以new一个对象替换掉现有的
                T ety = this.Get(Value.Id);
                Debug.Assert(ety != null, "SaveExist的对象不存在");
                
                Value.CopyFieldTo(ety);

                Update(ety);
                return ety;
            }

            public void Insert(T t)
            {
                t.Id = Convert.ToInt64(
                    OleDB.InsertAndReturnId_MS(
                        "insert into TAtomAndGrp (FKind, FName, FColor) values ("
                         + "'" + Kind + "', "
                         + "@FName, "
                         + (IsCourse ? (t as EnCourse).Color.ToArgb().ToString() : "0")
                         + ")",

                         new KeyValue<String, Object>("@FName", t.Name)
                         ));
            }

            public void Update(T t)
            {
                OleDB.ExecuteNonQuery(
                    "update TAtomAndGrp set "

                     + " FName = @FName, "
                     + " FColor = " + (IsCourse ? (t as EnCourse).Color.ToArgb().ToString() : "0")

                     + " where FKind = '" + Kind + "' "
                     + " and Id = " + t.Id,

                     new KeyValue<String, Object>("@FName", t.Name)
                     );
            }

            public void Delete(T t)
            {
                OleDB.ExecuteNonQuery("delete from TAtomAndGrp"
                    + " where FKind = '" + Kind + "' and Id = " + t.Id);

                Entities.Remove(t.Id);
            }
        }

        protected AtomDtAccess<Grp> GrpDtAccess;
        protected AtomDtAccess<Mbr> MbrDtAccess;
        protected DataAccessImpl ThisModule { get; private set; }


        public GrpMbrDacImpl(DataAccessImpl thisModule)
        {
            this.ThisModule = thisModule;

            CreateGrpMbrDtAccess();

            foreach (OleDbDataReader reader in ThisModule.OleDB.EachRows(
                "select FGrpId, FMbrId from TRln"
                + " where FKind = '" + MbrDtAccess.Kind + "'"))
            {
                Grp grp = GrpDtAccess.Get(Convert.ToInt64(reader[0]));
                Mbr mbr = MbrDtAccess.Get(Convert.ToInt64(reader[1]));

                if (grp == null || mbr == null)
                    ThisModule.ErrorLog.Error("关系恢复错误：ID对应的实体不存在  "
                    + "类型: " + MbrDtAccess.Kind 
                    + "  组Id: " + reader[0] 
                    + "  成员Id: " + reader[1]);
                else
                    AddRlnToGroupsMembers(grp, mbr);
            }
        }

        public virtual void CreateGrpMbrDtAccess()
        {
            GrpDtAccess = new AtomDtAccess<Grp>(ThisModule.OleDB);
            MbrDtAccess = new AtomDtAccess<Mbr>(ThisModule.OleDB);
        }

        public IEntityDac<Grp> GrpDAC
        {
            get { return GrpDtAccess; }
        }

        public IEntityDac<Mbr> MbrDAC
        {
            get { return MbrDtAccess; }
        }

        public IDictionary<Int64, IList<Mbr>> Membes
            = new SortedDictionary<Int64, IList<Mbr>>();
        public IDictionary<Int64, IList<Grp>> Groups
            = new SortedDictionary<Int64, IList<Grp>>();

        public void DeleteGrp(Grp grp)
        {
            ThisModule.OleDB.ExecuteNonQuery("delete from TRln"
                + " where FKind = '" + MbrDtAccess.Kind + "'" 
                + " and FGrpId = " + grp.Id);
           
            IList<Mbr> Mbrs;
            if (Membes.TryGetValue(grp.Id, out Mbrs))
                foreach(Mbr mbr in Mbrs)
                    Groups[mbr.Id].Remove(grp);
            Membes.Remove(grp.Id);

            GrpDtAccess.Delete(grp);
        }

        public void DeleteMbr(Mbr mbr)
        {
            ThisModule.OleDB.ExecuteNonQuery("delete from TRln"
                + " where FKind = '" + MbrDtAccess.Kind + "'"
                + " and FMbrId = " + mbr.Id);

            IList<Grp> Grps;
            if (Groups.TryGetValue(mbr.Id, out Grps))
                foreach (Grp grp in Grps)
                    Membes[grp.Id].Remove(mbr);
            Groups.Remove(mbr.Id);

            MbrDtAccess.Delete(mbr);
        }

        public IList<Mbr> GetMembes(Grp grp)
        {
            IList<Mbr> Result;
            if (!Membes.TryGetValue(grp.Id, out Result))
                Result = new List<Mbr>();

            return Result;
        }

        public IList<Grp> GetGroups(Mbr mbr)
        {
            IList<Grp> Result;
            if (!Groups.TryGetValue(mbr.Id, out Result))
                Result = new List<Grp>();

            return Result;
        }

        public void CreateRelation(Grp grp, Mbr mbr)
        {
            ThisModule.OleDB.ExecuteNonQuery("insert into TRln(FKind, FGrpId, FMbrId) values ("
                + "'" + MbrDtAccess.Kind + "', " 
                + grp.Id + ", " 
                + mbr.Id + ")");


            AddRlnToGroupsMembers(grp, mbr);
        }

        public void AddRlnToGroupsMembers(Grp grp, Mbr mbr)
        {
            IList<Grp> Grps;
            if (!Groups.TryGetValue(mbr.Id, out Grps))
            {
                Grps = new List<Grp>();
                Groups.Add(mbr.Id, Grps);
            }
            Grps.Add(grp);

            IList<Mbr> Mbrs;
            if (!Membes.TryGetValue(grp.Id, out Mbrs))
            {
                Mbrs = new List<Mbr>();
                Membes.Add(grp.Id, Mbrs);
            }
            Mbrs.Add(mbr);
        }

        public void ReleaseRelation(Grp grp, Mbr mbr)
        {
            ThisModule.OleDB.ExecuteNonQuery("delete from TRln"
                + " where FKind = '" + MbrDtAccess.Kind + "'"
                + " and FGrpId = " + grp.Id
                + " and FMbrId = " + mbr.Id);

            IList<Grp> Grps;
            Groups.TryGetValue(mbr.Id, out Grps);
            Grps.Remove(grp);

            IList<Mbr> Mbrs;
            Membes.TryGetValue(grp.Id, out Mbrs);
            Mbrs.Remove(mbr);
        }

        public void ClearAll()
        { 
        }
    }
}
