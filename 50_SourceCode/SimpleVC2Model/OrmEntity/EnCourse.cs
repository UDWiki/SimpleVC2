using System.Drawing;

namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///内容课
    /// </summary>
    public class EnCourse : BaseEntity
    {
        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public override void CopyFieldTo(object obj)
        {
            base.CopyFieldTo(obj);
            (obj as EnCourse).Color = this.Color;
        }
    }
}
