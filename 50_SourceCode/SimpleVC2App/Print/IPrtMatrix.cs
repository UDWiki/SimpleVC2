using System.Drawing;
using System.Drawing.Printing;

namespace Telossoft.SimpleVC.WinFormApp.Print
{
    public interface IPrtMatrix
    {
        void PrintMatrix(PrintDocument Doc, Graphics graphics, Rectangle rect);
    }
}
