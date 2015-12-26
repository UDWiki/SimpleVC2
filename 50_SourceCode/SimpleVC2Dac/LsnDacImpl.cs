using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.Dac
{
    /// <summary>
    /// 处理课务安排数据，删除总会级联，保存不会级联
    /// </summary>
    internal class LsnDacImpl : ILsnDac
    {
        protected DataAccessImpl ThisModule { get; private set; }

        //有重复sql操作，有时间全部进化成参数sql
        public IDictionary<Int64, LsnContainer> Lsns
            = new SortedDictionary<Int64, LsnContainer>();
        public IDictionary<Int64, ClsLsnContainer> ClsLsns
            = new SortedDictionary<Int64, ClsLsnContainer>();

        public LsnDacImpl(DataAccessImpl thisModule)
        {
            this.ThisModule = thisModule;

            LoadLesson();
            LoadClsLesson();
            LoadAct();
        }

        public IList<EnLsnAct> GetLsnActs(EnClsLesson ClsLsn)
        {
            IList<EnLsnAct> Result = new List<EnLsnAct>();
            foreach (EnLsnAct act in ClsLsns[ClsLsn.Id].Acts)
                Result.Add(act);

            return Result;
        }

        public void DeleteLsnActs(IList<EnLsnAct> Values)
        {
            foreach (EnLsnAct act in Values)
            {
                IList<EnLsnAct> Acts = ClsLsns[act.ClsLesson.Id].Acts;
                for (Int32 i = 0; i < Acts.Count; i++)
                    if (Acts[i].Id == act.Id)
                    {
                        Acts.RemoveAt(i);
                        break;
                    }

                ThisModule.OleDB.ExecuteNonQuery("Delete from TLsnAct where Id = " + act.Id);
            }
        }

        public EnClsLesson SaveClsLesson(EnClsLesson Value)
        {
            //这样更高效但难理解
            //foreach(VcClsLesson clsLsn in Lsns[Value.Lesson.Id].ClsLessons)
            //    if (clsLsn.Squad == Value.Squad)
            //    {
            //        ClsLsnContainer clsLsnCnt = ClsLsns[clsLsn.Id];

            foreach (ClsLsnContainer clsLsnCnt in this.ClsLsns.Values)
                if (clsLsnCnt.ClsLsn.Squad == Value.Squad
                    && clsLsnCnt.ClsLsn.Lesson.Id == Value.Lesson.Id)
                {
                    clsLsnCnt.ClsLsn.SharedTime = Value.SharedTime;
                    clsLsnCnt.ClsLsn.Teacher = Value.Teacher;

                    ThisModule.OleDB.ExecuteNonQuery("update TClsLesson set "
                        + " FSharedTime = " + Value.SharedTime
                        + ", FTeacher = " + (Value.Teacher == null ? 0 : Value.Teacher.Id)
                        + " where Id = " + clsLsnCnt.ClsLsn.Id);

                    return clsLsnCnt.ClsLsn;
                }

            EnClsLesson ClsLsn = new EnClsLesson();
            ClsLsn.Lesson = Lsns[Value.Lesson.Id].Lsn;
            ClsLsn.SharedTime = Value.SharedTime;
            ClsLsn.Squad = Value.Squad;
            ClsLsn.Teacher = Value.Teacher;
            ClsLsn.Id = Convert.ToInt64(ThisModule.OleDB.InsertAndReturnId_MS("insert into TClsLesson"
                + " (FLesson, FSharedTime, FSquad, FTeacher) values ("
                + Value.Lesson.Id
                + ", " + Value.SharedTime
                + ", " + Value.Squad.Id
                + ", " + (Value.Teacher == null ? 0 : Value.Teacher.Id)
                + ")"));

            ClsLsnContainer clc = new ClsLsnContainer();
            clc.ClsLsn = ClsLsn;
            ClsLsns.Add(ClsLsn.Id, clc);
            Lsns[ClsLsn.Lesson.Id].ClsLessons.Add(ClsLsn);

            return ClsLsn;
        }

        public IList<EnClsLesson> GetClsLessons(EnLesson Lesson)
        {
            IList<EnClsLesson> Result = new List<EnClsLesson>();
            foreach (EnClsLesson clsLsn in Lsns[Lesson.Id].ClsLessons)
                Result.Add(clsLsn);

            return Result;
        }

        public void DeleteClsLesson(EnClsLesson Value)
        {
            IList<EnClsLesson> clsLsns = Lsns[Value.Lesson.Id].ClsLessons;
            for (Int32 i = 0; i < clsLsns.Count; i++)
                if (clsLsns[i].Id == Value.Id)
                {
                    clsLsns.RemoveAt(i);
                    break;
                }

            ClsLsns.Remove(Value.Id);
            ThisModule.OleDB.ExecuteNonQuery("delete from TLsnAct where FClsLesson = " + Value.Id);
            ThisModule.OleDB.ExecuteNonQuery("delete from TClsLesson where Id = " + Value.Id);
        }

        public EnLesson SaveLesson(EnLesson Value)
        {
            foreach (LsnContainer LsnCnt in Lsns.Values)
                if (LsnCnt.Lsn.SquadGroup == Value.SquadGroup
                    && LsnCnt.Lsn.Course == Value.Course)
                {
                    LsnCnt.Lsn.SharedTime = Value.SharedTime;
                    LsnCnt.Lsn.IsSelfStudy = Value.IsSelfStudy;

                    ThisModule.OleDB.ExecuteNonQuery("update TLesson set "
                        + " FSharedTime = " + Value.SharedTime
                        + ", FIsSelfStudy = " + (Value.IsSelfStudy ? "True" : "False")
                        + " where Id = " + LsnCnt.Lsn.Id);

                    return LsnCnt.Lsn;
                }

            LsnContainer lc = new LsnContainer();
            lc.Lsn = new EnLesson();
            lc.Lsn.SharedTime = Value.SharedTime;
            lc.Lsn.IsSelfStudy = Value.IsSelfStudy;
            lc.Lsn.SquadGroup = Value.SquadGroup;
            lc.Lsn.Course = Value.Course;
            lc.Lsn.Id = Convert.ToInt64(ThisModule.OleDB.InsertAndReturnId_MS("insert into TLesson"
                + " (FSquadGroup, FCourse, FIsSelfStudy, FSharedTime) values ("
                + Value.SquadGroup.Id
                + ", " + Value.Course.Id
                + ", " + (Value.IsSelfStudy ? "True" : "False")
                + ", " + Value.SharedTime
                + ")"));
            Lsns.Add(lc.Lsn.Id, lc);

            return lc.Lsn;
        }

        public IList<EnLesson> GetLessons(EnSquadGroup SquadGrp)
        {
            IList<EnLesson> Result = new List<EnLesson>();
            foreach (LsnContainer LsnCnt in Lsns.Values)
                if (LsnCnt.Lsn.SquadGroup.Id == SquadGrp.Id)
                    Result.Add(LsnCnt.Lsn);

            return Result;
        }

        public void DeleteLesson(EnLesson Lesson)
        {
            LsnContainer LsnCnt = Lsns[Lesson.Id];
            String SWhere = "";
            foreach (EnClsLesson ClsLsn in LsnCnt.ClsLessons)
            {
                if (!String.IsNullOrEmpty(SWhere))
                    SWhere = SWhere + " or ";
                SWhere = SWhere + "FClsLesson = " + ClsLsn.Id;

                ClsLsns.Remove(ClsLsn.Id);
            }

            Lsns.Remove(Lesson.Id);
            if (!String.IsNullOrEmpty(SWhere))
                ThisModule.OleDB.ExecuteNonQuery("delete from TLsnAct where " + SWhere);
            ThisModule.OleDB.ExecuteNonQuery("delete from TClsLesson where FLesson = " + Lesson.Id);
            ThisModule.OleDB.ExecuteNonQuery("delete from TLesson where Id = " + Lesson.Id);
        }

        private void LoadClsLesson()
        {
            foreach (OleDbDataReader reader in ThisModule.OleDB.EachRows(
                "select Id, FLesson, FSharedTime, FSquad, FTeacher"
                + " from TClsLesson"))
            {
                EnClsLesson ClsLsn = new EnClsLesson();
                ClsLsn.Id = Convert.ToInt64(reader[0]);
                ClsLsn.SharedTime = Convert.ToInt32(reader[2]);
                if (ClsLsn.SharedTime < 0 || ClsLsn.SharedTime > CommLogic.cMaxSharedTime)
                {
                    ThisModule.ErrorLog.Error("VcClsLesson恢复错误：SharedTime越界"
                        + "  ClsLsn: " + ClsLsn.SharedTime);
                    continue;
                }

                Int64 LsnId = Convert.ToInt64(reader[1]);
                LsnContainer LsnCnt;
                if (!Lsns.TryGetValue(LsnId, out LsnCnt))
                {
                    ThisModule.ErrorLog.Error("VcClsLesson恢复错误：ID对应的实体不存在  "
                        + "  Lesson: " + LsnId);
                    continue;
                }
                ClsLsn.Lesson = LsnCnt.Lsn;

                ClsLsn.Squad = ThisModule.Sqd.MbrDAC.Get(Convert.ToInt64(reader[3]));
                if (ClsLsn.Squad == null)
                {
                    ThisModule.ErrorLog.Error("VcClsLesson恢复错误：ID对应的实体不存在  "
                        + "  Squad: " + reader[3]);
                    continue;
                }

                Int64 TchID = Convert.ToInt64(reader[4]);
                if (TchID > 0)
                {
                    ClsLsn.Teacher = ThisModule.Tch.MbrDAC.Get(TchID);
                    if (ClsLsn.Teacher == null)
                    {
                        ThisModule.ErrorLog.Error("VcClsLesson恢复错误：ID对应的实体不存在  "
                            + "  Teacher: " + TchID);
                        continue;
                    }
                }

                LsnCnt.ClsLessons.Add(ClsLsn);
                ClsLsnContainer ClsLsnCnt = new ClsLsnContainer();
                ClsLsnCnt.ClsLsn = ClsLsn;
                ClsLsns.Add(ClsLsn.Id, ClsLsnCnt);
            }
        }

        private void LoadLesson()
        {
            foreach (OleDbDataReader reader in ThisModule.OleDB.EachRows(
                "select Id, FSquadGroup, FCourse, FIsSelfStudy, FSharedTime "
                + " from TLesson"))
            {
                EnLesson Lsn = new EnLesson();
                Lsn.Id = Convert.ToInt64(reader[0]);
                Lsn.SquadGroup = ThisModule.Sqd.GrpDAC.Get(Convert.ToInt64(reader[1]));
                Lsn.Course = ThisModule.Crs.MbrDAC.Get(Convert.ToInt64(reader[2]));
                Lsn.IsSelfStudy = Convert.ToBoolean(reader[3]);
                Lsn.SharedTime = Convert.ToInt32(reader[4]);

                if (Lsn.SquadGroup == null)
                {
                    ThisModule.ErrorLog.Error("VcLesson恢复错误：ID对应的实体不存在  "
                        + "  SquadGroup: " + reader[1]);
                    continue;
                }

                if (Lsn.Course == null)
                {
                    ThisModule.ErrorLog.Error("VcLesson恢复错误：ID对应的实体不存在  "
                        + "  Course: " + reader[2]);
                    continue;
                }

                LsnContainer LsnCnt = new LsnContainer();
                LsnCnt.Lsn = Lsn;
                Lsns.Add(LsnCnt.Lsn.Id, LsnCnt);
            }
        }

        public IEnumerable<EnLesson> eachLesson()
        {
            foreach (LsnContainer Lsncnt in Lsns.Values)
                yield return Lsncnt.Lsn;
        }

        public IEnumerable<EnLsnAct> eachLsnAct()
        {
            foreach (ClsLsnContainer clsLsnCnt in ClsLsns.Values)
                foreach (EnLsnAct act in clsLsnCnt.Acts)
                    yield return act;
        }

        public IEnumerable<EnClsLesson> eachClsLesson()
        {
            foreach (LsnContainer Lsncnt in Lsns.Values)
                foreach (EnClsLesson clsLsn in Lsncnt.ClsLessons)
                    yield return clsLsn;
        }

        public void ClearAll()
        {
            Lsns.Clear();
            ClsLsns.Clear();

            ThisModule.OleDB.ExecuteNonQuery("delete from TLsnAct");
            ThisModule.OleDB.ExecuteNonQuery("delete from TClsLesson");
            ThisModule.OleDB.ExecuteNonQuery("delete from TLesson");
        }

        private void LoadAct()
        {
            foreach (OleDbDataReader reader in ThisModule.OleDB.EachRows(
                "select Id, FClsLesson, FTime, FLocked"
                + " from TLsnAct"))
            {
                EnLsnAct Act = new EnLsnAct();
                Act.Id = Convert.ToInt64(reader[0]);
                CommLogic.GetTimeFromInt32(Convert.ToInt32(reader[2])).CopyFieldTo(Act.Time);
                Act.Locked = Convert.ToBoolean(reader[3]);
                Int64 ClsLsnId = Convert.ToInt64(reader[1]);

                ClsLsnContainer ClsLsnCnt;
                if (!ClsLsns.TryGetValue(ClsLsnId, out ClsLsnCnt))
                {
                    ThisModule.ErrorLog.Error("VcLsnAct恢复错误：ID对应的实体不存在  "
                        + "  ClsLesson: " + ClsLsnId);
                    continue;
                }

                Act.ClsLesson = ClsLsnCnt.ClsLsn;
                ClsLsnCnt.Acts.Add(Act);
            }
        }

        public void SaveLsnActs(IList<EnLsnAct> Values)
        {
            foreach (EnLsnAct act in Values)
            {
                IList<EnLsnAct> Acts = ClsLsns[act.ClsLesson.Id].Acts;
                if (Acts.Contains(act))
                {
                    ThisModule.OleDB.ExecuteNonQuery("update TLsnAct set"
                        + " FLocked = " + (act.Locked ? "True" : "false")
                        + ", FTime = " + CommLogic.TimeToInt32(act.Time)
                        + " where Id = " + act.Id);
                }
                else
                {
                    Debug.Assert(act.Id == 0, "act.Id != 0");

                    EnLsnAct nAct = new EnLsnAct();
                    nAct.Locked = act.Locked;
                    nAct.ClsLesson = act.ClsLesson;
                    act.Time.CopyFieldTo(nAct.Time);
                    nAct.Id = Convert.ToInt64(ThisModule.OleDB.InsertAndReturnId_MS("Insert into TLsnAct"
                        + "(FClsLesson, FLocked, FTime) values ("
                        + nAct.ClsLesson.Id
                        + ", " + (act.Locked ? "True" : "false")
                        + ", " + CommLogic.TimeToInt32(nAct.Time)
                        + ")"));

                    Acts.Add(nAct);
                }
            }
        }

    }

    internal class LsnContainer
    {
        public EnLesson Lsn;
        public IList<EnClsLesson> ClsLessons = new List<EnClsLesson>();
    }

    internal class ClsLsnContainer
    {
        public EnClsLesson ClsLsn;
        public IList<EnLsnAct> Acts = new List<EnLsnAct>();
    }
}
