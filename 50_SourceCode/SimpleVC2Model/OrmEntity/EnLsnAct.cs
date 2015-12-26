using System;
using System.Collections.Generic;
using System.Text;

namespace Telossoft.SimpleVC.Model
{
    public class EnLsnAct : BaseEntity
    {
        private Boolean locked;

        public Boolean Locked
        {
            get { return locked; }
            set { locked = value; }
        }

        private EnClsLesson clsLesson;

        public EnClsLesson ClsLesson
        {
            get { return clsLesson; }
            set { clsLesson = value; }
        }

        private VcTime time = new VcTime();

        public VcTime Time
        {
            get { return time; }
        }

        public override string ToString()
        {
            return ClsLesson.ToString() + " " + Time.ToString();
        }

        public override String Name
        {
            get
            {
                return this.ToString();
            }
            set
            {
            }
        }

        /// <summary>
        /// 课程名称串
        /// </summary>
        public String Course
        {
            get { return ClsLesson.Lesson.Course.Name; }
        }

        /// <summary>
        /// 班级名称串
        /// </summary>
        public String Squad
        {
            get { return ClsLesson.Squad.Name; }
        }
    }
}
