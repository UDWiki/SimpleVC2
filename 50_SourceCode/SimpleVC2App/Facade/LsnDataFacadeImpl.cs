using System;
using System.Collections.Generic;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.Facade
{

    delegate void OnModifyChanged(Boolean HasGrp, Boolean Modify);
    internal class LsnDataFacadeImpl : LsnEditInterface
    {
        protected IDataRule DataRule { get; private set; }
        protected OnModifyChanged ModifyChanged;
 
        public LsnDataFacadeImpl(IDataRule dataRule, OnModifyChanged ModifyChanged)
        {
            this.DataRule = dataRule;
            this.ModifyChanged = ModifyChanged;
            this.Modify = false;
        }

        private const Int32 cSharedTime = 4;
        private EnSquadGroup grp;

        public EnSquadGroup Grp
        {
            get
            {
                return grp;
            }
            set
            {
                grp = value;

                FcLsns.Clear();
                Load();
            }
        }

        private IList<FcLesson> fcLsns = new List<FcLesson>();
        public IList<FcLesson> FcLsns
        {
            get { return fcLsns; }
        }

        public IList<BaseEntity> GetCourses()
        {
            IList<BaseEntity> Result = new List<BaseEntity>();
            foreach (FcLesson lsn in FcLsns)
                Result.Add(lsn.course);

            return Result;
        }

        public void SetCourses(IList<BaseEntity> Crses)
        //此方法有问题的
        {
            IList<BaseEntity> CrsesCopy = new List<BaseEntity>();
            ExIList.Append<BaseEntity>(Crses, CrsesCopy);

            for (Int32 i = FcLsns.Count - 1; i >= 0; i--)
                if (!CrsesCopy.Contains(FcLsns[i].course))
                {
                    FcLsns.RemoveAt(i);
                    this.Modify = true;
                }
                else
                    CrsesCopy.Remove(FcLsns[i].course);


            if (CrsesCopy.Count == 0)
                return;

            IList<BaseEntity> Sqds = new GIListTypeChange<EnSquad, BaseEntity>(DataRule.Sqd.GetMembes(this.Grp));
            foreach (EnCourse crs in CrsesCopy)
            {
                FcLesson Lsn = new FcLesson(this);
                Lsn.course = crs;
                Lsn.SharedTime = cSharedTime;
                foreach (EnSquad sqd in Sqds)
                {
                    FcClsLesson ClsLsn = new FcClsLesson(Lsn);
                    ClsLsn.squad = sqd;
                    ClsLsn.SharedTime = Lsn.SharedTime;
                }
                FcLsns.Add(Lsn);
            }
        }

        public void SetTeacher(FcClsLesson ClsLsn, EnTeacher Tch)
        {
            if (ClsLsn.teacher != Tch)
            {
                ClsLsn.teacher = Tch;
                this.Modify = true;
            }
        }

        private bool modify;

        public bool Modify
        {
            get
            {
                return modify;
            }
            set
            {
                if (modify != value)
                {
                    modify = value;
                    ModifyChanged(this.grp != null, modify);
                }
            }
        }

        private void Load()
        {
            foreach (EnLesson Lsn in DataRule.Lsn.GetLessons(this.Grp))
            {
                FcLesson FcLsn = new FcLesson(this);
                FcLsn.course = Lsn.Course;
                FcLsn.IsSelfStudy = Lsn.IsSelfStudy;
                FcLsn.SharedTime = Lsn.SharedTime;
                foreach (EnClsLesson ClsLsn in DataRule.Lsn.GetCLsLessons(Lsn))
                {
                    FcClsLesson FcClsLsn = new FcClsLesson(FcLsn);
                    FcClsLsn.SharedTime = ClsLsn.SharedTime;
                    FcClsLsn.squad = ClsLsn.Squad;
                    FcClsLsn.teacher = ClsLsn.Teacher;
                }
                this.FcLsns.Add(FcLsn);
            }

            Modify = false;
        }


        private Boolean InLsns(EnCourse Crs)
        {
            foreach (FcLesson Lsn in this.FcLsns)
                if (Lsn.course.Id == Crs.Id)
                    return true;

            return false;
        }

        public void Save()
        {
            //删除课程
            IList<EnLesson> Lessons = DataRule.Lsn.GetLessons(Grp);
            foreach (EnLesson Lsn in Lessons)
                if (!InLsns(Lsn.Course))
                    DataRule.Lsn.RemoveLsn(Lsn);

            //保存与更新
            foreach(FcLesson FcLsn in this.FcLsns)
            {
                EnLesson Lsn = new EnLesson();
                IList<EnClsLesson> ClsLsns = new List<EnClsLesson>();
                Lsn.SquadGroup = this.Grp;
                Lsn.Course = FcLsn.course;
                Lsn.IsSelfStudy = FcLsn.IsSelfStudy;
                Lsn.SharedTime = FcLsn.SharedTime;
                foreach(FcClsLesson FcClsLsn in FcLsn.FcClsLsns)
                {
                    EnClsLesson ClsLsn = new EnClsLesson();
                    ClsLsn.Lesson = Lsn;
                    ClsLsn.Squad = FcClsLsn.squad;
                    ClsLsn.Teacher = FcClsLsn.teacher;
                    ClsLsn.SharedTime = FcClsLsn.SharedTime;
                    ClsLsns.Add(ClsLsn);
                }

                DataRule.Lsn.SaveLsnTree(Lsn, ClsLsns);
            }

            Modify = false;
        }
    }
}
