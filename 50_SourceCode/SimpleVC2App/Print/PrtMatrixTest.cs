using System.Drawing;
using System.Drawing.Printing;
using Telossoft.SimpleVC.WinFormApp.VCControl;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.Print
{
    internal class PrtMatrixTest : IPrtMatrix
    {
        private BaseEntity entity;
        public PrtMatrixTest(BaseEntity entity)
        {
            this.entity = entity;
        }

        public void PrintMatrix(PrintDocument Doc, Graphics graphics, Rectangle rect)
        {
            Rectangle titleRect = new Rectangle(rect.Left, rect.Top + 20, rect.Width, 100);
            graphics.DrawString(entity.ToString(), 
                new Font("ËÎÌו", DrawComm.SuitFontSize((double)titleRect.Height * 0.5), FontStyle.Bold), 
                new SolidBrush(Color.Black), titleRect, DrawComm.CenterStringFormat);

            FcMatrixPtr<PrtMatterCell> Matrix = new FcMatrixPtr<PrtMatterCell>(VC2WinFmApp.DataRule.Solution);
            Matrix.HeighWidthProportionLow = 30;
            Matrix.HeighWidthProportionHigh = 40;
            Matrix.Rect = new Rectangle(rect.Left, rect.Top + 130, rect.Width, rect.Height - 130);
            Matrix.Draw(graphics);
        }
    }
}
