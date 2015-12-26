using System.Drawing;
using System.Drawing.Printing;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.VCControl;

namespace Telossoft.SimpleVC.WinFormApp.Print
{
    internal class PrtMatrixSqd : IPrtMatrix
    {
        private EnSquad squad;
        public PrtMatrixSqd(EnSquad sqd)
        {
            squad = sqd;
        }

        public void PrintMatrix(PrintDocument Doc, Graphics graphics, Rectangle rect)
        {
            Rectangle titleRect = new Rectangle(rect.Left, rect.Top + 20, rect.Width, 100);
            graphics.DrawString(squad.ToString(),
                new Font("宋体", DrawComm.SuitFontSize((double)titleRect.Height * 1), FontStyle.Bold),
                new SolidBrush(Color.Black), titleRect, DrawComm.CenterStringFormat);

            graphics.DrawString(ViewStyle.Description,
                new Font("宋体", DrawComm.SuitFontSize((double)titleRect.Height * 0.5)),
                new SolidBrush(Color.Black),
                rect.Left, rect.Top + 100);

            FcMatrixPtr<PrtMatterCellSqd> Matrix = new FcMatrixPtr<PrtMatterCellSqd>(VC2WinFmApp.DataRule.Solution);
            Matrix.HeighWidthProportionLow = 30;
            Matrix.HeighWidthProportionHigh = 80;
            DtMatrix<EnLsnAct> SqdMatrix = VC2WinFmApp.Engine.GetSqdMatrix(squad);
            foreach(VcTime time in SqdMatrix.eachTime())
                if (SqdMatrix[time] != null)
                    Matrix[time].Course = SqdMatrix[time].ClsLesson.Lesson.Course;

            Matrix.Rect = new Rectangle(rect.Left, rect.Top + 130, rect.Width, rect.Height - 130);
            Matrix.Draw(graphics);

            //todo  打印授课教师表格
        }
    }

    public class PrtMatterCellSqd : PrtMatterCell
    {
        private EnCourse course;

        public EnCourse Course
        {
            get { return course; }
            set { course = value; }
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(DrawComm.Black1Pen, this.Rect);
            if (Course != null)
            graphics.DrawString(Course.ToString(),
                new Font("宋体", DrawComm.SuitFontSize((double)Rect.Height * 1), FontStyle.Regular),
                new SolidBrush(Color.Black), Rect, DrawComm.CenterStringFormat);
        }
    }
}
