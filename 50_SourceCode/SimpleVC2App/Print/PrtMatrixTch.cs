using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.VCControl;

namespace Telossoft.SimpleVC.WinFormApp.Print
{
    internal class PrtMatrixTch : IPrtMatrix
    {
        private EnTeacher teacher;
        public PrtMatrixTch(EnTeacher tch)
        {
            teacher = tch;
        }

        public void PrintMatrix(PrintDocument Doc, Graphics graphics, Rectangle rect)
        {
            Rectangle titleRect = new Rectangle(rect.Left, rect.Top + 20, rect.Width, 100);
            graphics.DrawString(teacher.ToString(),
                new Font("ËÎÌו", DrawComm.SuitFontSize((double)titleRect.Height * 1), FontStyle.Bold),
                new SolidBrush(Color.Black), titleRect, DrawComm.CenterStringFormat);

            graphics.DrawString(ViewStyle.Description,
                new Font("ËÎÌו", DrawComm.SuitFontSize((double)titleRect.Height * 0.5)),
                new SolidBrush(Color.Black),
                rect.Left, rect.Top + 100);

            FcMatrixPtr<PrtMatterCellTch> Matrix = new FcMatrixPtr<PrtMatterCellTch>(VC2WinFmApp.DataRule.Solution);
            Matrix.HeighWidthProportionLow = 30;
            Matrix.HeighWidthProportionHigh = 80;

            DtMatrix<IList<EnLsnAct>> TchMatrix = VC2WinFmApp.Engine.GetTchMatrix(teacher);
            foreach (VcTime time in TchMatrix.eachTime())
                Matrix[time].Acts = TchMatrix[time];

            Matrix.Rect = new Rectangle(rect.Left, rect.Top + 130, rect.Width, rect.Height - 130);
            Matrix.Draw(graphics);
        }
    }

    public class PrtMatterCellTch : PrtMatterCell
    {
        private IList<EnLsnAct> acts = new List<EnLsnAct>();

        public IList<EnLsnAct> Acts
        {
            get { return acts; }
            set
            {
                acts.Clear();
                ExIList.Append<EnLsnAct>(value, acts);
            }
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(DrawComm.Black1Pen, this.Rect);
            String S = "";
            foreach (EnLsnAct act in acts)
            {
                if (!String.IsNullOrEmpty(S))
                    S = S + Ex.cEntter;
                S = S + act.ClsLesson.Lesson.Course.Name
                    + Ex.cEntter + act.ClsLesson.Squad.Name;
            }

            if (!String.IsNullOrEmpty(S))
                graphics.DrawString(S,
                    new Font("ËÎÌו", DrawComm.SuitFontSize((double)Rect.Height * 0.58), FontStyle.Regular),
                    new SolidBrush(Color.Black), Rect, DrawComm.CenterStringFormat);
        }
    }
}
