using System.Drawing;

namespace Telossoft.SimpleVC.WinFormApp.VCControl
{
    public class DrawComm
    {
        static DrawComm()
        {
            CenterStringFormat = new StringFormat();
            CenterStringFormat.Alignment = StringAlignment.Center;
            CenterStringFormat.LineAlignment = StringAlignment.Center;
        }

        public static Pen Black1Pen = new Pen(Color.Black);
        public static Pen Black2Pen = new Pen(Color.Black, 2);
        public static StringFormat CenterStringFormat;

        public static float SuitFontSize(double height)
        {
            return (float)(height/3);
        }
    }
}
