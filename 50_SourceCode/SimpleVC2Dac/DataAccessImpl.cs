using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using SGLibrary.Extend;
using SGLibrary.DB;
using SGLibrary.Framework.Config;
using SGLibrary.Framework.Log;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.Dac
{
    public class DataAccessImpl : IDataAccess
    {
        public DataAccessImpl(String connStr, ISimpleLog errorLog)
        {
            this.ErrorLog = errorLog;
            var connProvider = new DbConnProviderImpl_MSAccessOleDB(connStr, 20);
            Application.ApplicationExit += connProvider.Close;
            OleDB = new DbAccessImpl(connProvider);
            Config = new ConfigProviderImpl_Orm(connProvider, "TConfig").GetConfig("SimpleVC");

            Crs = new GrpMbrDacImplV2<EnCourseGroup, EnSubject>(this);
            Tch = new GrpMbrDacImplV2<EnTeacherGroup, EnTeacher>(this);
            Sqd = new GrpMbrDacImplV2<EnSquadGroup, EnSquad>(this);

            Lsn = new LsnDacImpl(this);
            Rule = new RuleDacImpl(this);

            LoadSln();
        }

        internal ISimpleLog ErrorLog { get; private set; }
        internal IDbAccess OleDB { get; private set; }
        internal EnSolution Solution { get; private set; }


        internal GrpMbrDacImplV2<EnCourseGroup, EnSubject> Crs;
        internal GrpMbrDacImplV2<EnTeacherGroup, EnTeacher> Tch;
        internal GrpMbrDacImplV2<EnSquadGroup, EnSquad> Sqd;

        internal LsnDacImpl Lsn;
        internal RuleDacImpl Rule;

        public IConfig Config { get; private set; }

        EnSolution IDataAccess.Solution
        {
            get
            {
                return this.Solution;
            }
            set
            {
                if (Solution == null)
                    Solution = new EnSolution();

                for (var i = 0; i < value.ActiveWeekArr.Length; i++)
                    Solution.ActiveWeekArr[i] = value.ActiveWeekArr[i];

                for (var i = 0; i < value.LessonNumberArr.Length; i++)
                    Solution.LessonNumberArr[i] = value.LessonNumberArr[i];

                SaveSln(Solution);
            }
        }

        protected void SaveSln(EnSolution solution)
        {
            OleDB.ExecuteNonQuery("delete from TSolution");

            OleDB.ExecuteNonQuery("insert into TSolution (FActiveWeek, FLessonNumber) values ("
                + "'" + ExBuildListWithHexChar.GetHexStr(solution.ActiveWeekArr) + "'"
                + ",'" + ExBuildListWithHexChar.GetHexStr(solution.LessonNumberArr) + "'"
                + ")");
        }

        protected void LoadSln()
        {
            foreach (OleDbDataReader rd in OleDB.EachRows("select * from TSolution"))
            {
                Solution = new EnSolution();

                IList<Boolean> ActiveWeekArr = ExBuildListWithHexChar.HexStrToBooleanList(rd["FActiveWeek"].ToString());
                if (ActiveWeekArr.Count == Solution.ActiveWeekArr.Length)
                    for (var i = 0; i < ActiveWeekArr.Count; i++)
                        Solution.ActiveWeekArr[i] = ActiveWeekArr[i];

                IList<Int32> LessonNumberArr = ExBuildListWithHexChar.HexStrToInt32List(rd["FLessonNumber"].ToString());
                if (LessonNumberArr.Count == Solution.LessonNumberArr.Length)
                    for (var i = 0; i < LessonNumberArr.Count; i++)
                        Solution.LessonNumberArr[i] = LessonNumberArr[i];
            }
        }

        IGrpMbrDac<EnCourseGroup, EnSubject> IDataAccess.Crs
        {
            get { return this.Crs; }
        }

        IGrpMbrDac<EnTeacherGroup, EnTeacher> IDataAccess.Tch
        {
            get { return this.Tch; }
        }

        IGrpMbrDac<EnSquadGroup, EnSquad> IDataAccess.Sqd
        {
            get { return this.Sqd; }
        }

        ILsnDac IDataAccess.Lsn
        {
            get { return this.Lsn; }
        }

        IRuleDac IDataAccess.Rule
        {
            get { return this.Rule; }
        }
    }
}
