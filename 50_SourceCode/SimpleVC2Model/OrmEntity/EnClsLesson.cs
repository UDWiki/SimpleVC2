
namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///课务执行实体
    /// </summary>
    public class EnClsLesson : BaseEntity
    {
        private EnLesson lesson;

        public EnLesson Lesson
        {
            get { return lesson; }
            set { lesson = value; }
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

        private EnSquad squad;

        public EnSquad Squad
        {
            get { return squad; }
            set { squad = value; }
        }

        private EnTeacher teacher;

        public EnTeacher Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }

        public override string ToString()
        {
            return Lesson.Course.Name + " " + Squad.Name;
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
