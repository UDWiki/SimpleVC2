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

        public const String cWillBeDelete = "����ɾ��";
        public const String cWillNoTeacher = "���޽�ʦ�ڿ�";
        public const String cWillNoGuider = "���޸���";
        public const String cWillBeCreate = "��������";
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

        [GridBindColumn(Title = "�༶")]
        public String Squad
        {
            get { return squad == null ? "" : squad.Name; }
        }

        private int sharedTime;

        [GridBindColumn(Title = "��ʱ")]
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

        [GridBindColumn(Title = "��ʦ")]
        public String Teacher
        {
            get { return teacher == null ? "" : teacher.Name; }
        }
    }

    //����һ��Ϊ����Form����Ľӿڣ�������BizFacadeInterface�з���
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

        [GridBindColumn(Title = "�γ�")]
        public String Course
        {
            get { return course == null ? "" : course.Name; }
        }

        private Boolean isSelfStudy;

        [GridBindColumn(Title = "��ϰ")]
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

        [GridBindColumn(Title = "��ʱ")]
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
