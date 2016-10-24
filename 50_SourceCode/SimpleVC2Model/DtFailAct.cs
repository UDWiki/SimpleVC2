
namespace Telossoft.SimpleVC.Model
{
    public class EnFailAct
    {
        public EnFailAct(EnLsnAct act)
        {
            this.act = act;
        }

        public EnFailAct(EnLsnAct act, VcTime time)
        {
            this.act = act;
            time.CopyFieldTo(this.time);
        }

        private EnLsnAct act;

        public EnLsnAct Act
        {
            get { return act; }
        }

        private VcTime time = new VcTime();

        public VcTime Time
        {
            get { return time; }
        }

        public string Name
        {
            get
            {
                return Act.ClsLesson.Name + "  " + Time.ToString();
            }
        }
    }
}
