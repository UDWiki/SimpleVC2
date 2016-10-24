using System;
using System.Collections.Generic;
using SGLibrary.Framework.GridBind;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp
{
    public class CheckSelectEty
    {
        public CheckSelectEty(BaseEntity ety)
        {
            this.Obj = ety;
        }

        private BaseEntity obj;

        public BaseEntity Obj
        {
            get { return obj; }
            set { obj = value; }
        }

        private Boolean isChecked;

        public Boolean IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public String Name
        {
            get { return Obj.Name; }
        }
    }



    public class VcEffect
    {
        private EnClsLesson clsLesson;

        public EnClsLesson ClsLesson
        {
            get { return clsLesson; }
            set { clsLesson = value; }
        }

        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public const String cWillBeDelete = "将被删除";
        public const String cWillNoTeacher = "将无教师授课";
        public const String cWillNoGuider = "将无辅导";
        public const String cWillBeCreate = "将被创建";
    }


    public class FcClsLesson
    {
        public FcClsLesson(FcLesson FcLsn)
        {
            this.FcLsn = FcLsn;
            FcLsn.FcClsLsns.Add(this);
        }

        internal FcLesson FcLsn;
        public EnSquad squad;
        public EnTeacher teacher;

        [GridBindColumn(Title = "班级")]
        public String Squad
        {
            get { return squad == null ? "" : squad.Name; }
        }

        private int sharedTime;

        [GridBindColumn(Title = "课时")]
        public int SharedTime
        {
            get { return sharedTime; }
            set
            {
                if (value != sharedTime && value >= 0 && value <= VcTimeLogic.cMaxSharedTime)
                {
                    sharedTime = value;
                    FcLsn.LsnEdit.Modify = true;
                }
            }
        }

        [GridBindColumn(Title = "教师")]
        public String Teacher
        {
            get { return teacher == null ? "" : teacher.Name; }
        }
    }

    //这是一个为具体Form服务的接口，不放在BizFacadeInterface中发布
    public interface LsnEditInterface
    {
        Boolean Modify { get; set; }
        EnSquadGroup Grp { get; set; }

        IList<FcLesson> FcLsns { get;}
        void Save();

        void SetCourses(IList<BaseEntity> Crses);
        IList<BaseEntity> GetCourses();

        void SetTeacher(FcClsLesson ClsLsn, EnTeacher Tch);
    }

    public class FcLesson
    {
        public FcLesson(LsnEditInterface LsnEdit)
        {
            this.LsnEdit = LsnEdit;
        }

        internal LsnEditInterface LsnEdit;

        public EnSubject course;

        [GridBindColumn(Title = "课程")]
        public String Course
        {
            get { return course == null ? "" : course.Name; }
        }

        private Boolean isSelfStudy;

        [GridBindColumn(Title = "自习")]
        public Boolean IsSelfStudy
        {
            get { return isSelfStudy; }
            set
            {
                if (isSelfStudy != value)
                {
                    isSelfStudy = value;
                    LsnEdit.Modify = true;
                }
            }
        }

        private int sharedTime;

        [GridBindColumn(Title = "课时")]
        public int SharedTime
        {
            get { return sharedTime; }
            set
            {
                if (value != sharedTime && value >= 0 && value <= VcTimeLogic.cMaxSharedTime)
                {
                    foreach (FcClsLesson ClsLsn in FcClsLsns)
                        if (ClsLsn.SharedTime == SharedTime)
                            ClsLsn.SharedTime = value;

                    sharedTime = value;
                    LsnEdit.Modify = true;
                }
            }
        }

        public IList<FcClsLesson> FcClsLsns = new List<FcClsLesson>();
    }

}
