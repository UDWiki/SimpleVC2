using System;
using System.Collections.Generic;
using System.Text;

namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///课务集
    /// </summary>
    public class EnLesson : BaseEntity
    {
        private EnSquadGroup squadGroup;

        public EnSquadGroup SquadGroup
        {
            get { return squadGroup; }
            set { squadGroup = value; }
        }

        private EnCourse course;

        public EnCourse Course
        {
            get { return course; }
            set { course = value; }
        }

        private Boolean isSelfStudy;  //自习，不需要教师

        public Boolean IsSelfStudy
        {
            get { return isSelfStudy; }
            set { isSelfStudy = value; }
        }

        /// <summary>
        /// 周上课节数
        /// </summary>
        private int sharedTime;

        public int SharedTime
        {
            get { return sharedTime; }
            set { sharedTime = value; }
        }

        public override string ToString()
        {
            return Course.Name + " " + SquadGroup.Name;
        }

        public override string Name
        {
            get
            {
                return ToString();
            }
            set
            {
            }
        }
    }

}
