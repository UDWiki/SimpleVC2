using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SGLibrary.Extend;

namespace Telossoft.SimpleVC.Export
{
    public static class ToHtml
    {
        public static void ExportDataToHtml(String fileName, String title, String[,] mData)
        {
            StreamWriter txt = new StreamWriter(fileName, false);
            Int32 hCount = mData.GetLength(0);
            Int32 lCount = mData.GetLength(1);
            WriteHead(txt, title, lCount);

            //写列标题
            txt.WriteLine(@"<tr height=""18"">");
            txt.WriteLine(@"    <td colspan=""6"">");
            txt.WriteLine(@"        <div id=""toptb"" style=""left: 0px; width: " + cCellWidth * lCount + "px; position: absolute; top: 0px; height: " + cCellTop + @""">");
            txt.WriteLine(@"            <table cellspacing=""0"" cellpadding=""0"" width=""" + cCellWidth * lCount + @""" bgcolor=""#ffefff"" border=""1"">");
            txt.WriteLine(@"                <tbody>");
            txt.WriteLine(@"                    <tr>");

            for (Int32 i = 0; i < lCount; i++)
            {
                txt.WriteLine(@"                        <td width=" + cCellWidth + ">");
                txt.WriteLine(@"                            " + GetHtmlStr(mData[0, i]));
                txt.WriteLine(@"                        </td>");
            }
            txt.WriteLine(@"                    <td>");
            txt.WriteLine(@"                    </td>");

            txt.WriteLine(@"                    </tr>");
            txt.WriteLine(@"                </tbody>");
            txt.WriteLine(@"            </table>");
            txt.WriteLine(@"        </div>");
            txt.WriteLine(@"    </td>");
            txt.WriteLine(@"</tr>");

            //写行标题
            txt.WriteLine(@"<tr>");
            txt.WriteLine(@"    <td width=" + cCellWidth + @" rowspan=""" + hCount + @""">");
            txt.WriteLine(@"        <div id=""lefttb"" style=""left: 1px; width: 0px; position: absolute; top: " + cCellTop + @""">");
            txt.WriteLine(@"            <table cellspacing=""0"" cellpadding=""0"" width=""" + cCellWidth + @""" bgcolor=""#ffefff"" border=""1"">");
            txt.WriteLine(@"                <tbody>");

            for (Int32 i = 1; i < hCount; i++)
            {
                txt.WriteLine(@"                    <tr>");
                txt.WriteLine(@"                        <td width=" + cCellWidth + ">");
                txt.WriteLine(@"                            " + GetHtmlStr(mData[i, 0]));
                txt.WriteLine(@"                        </td>");
                txt.WriteLine(@"                    </tr>");
            }

            txt.WriteLine(@"                </tbody>");
            txt.WriteLine(@"            </table>");
            txt.WriteLine(@"        </div>");
            txt.WriteLine(@"    </td>");
            txt.WriteLine(@"</tr>");

            for (Int32 i = 1; i < hCount; i++)
            {
                txt.WriteLine(@"<tr>");
                for (Int32 j = 1; j < lCount; j++)
                {
                    txt.WriteLine(@"    <td width=" + cCellWidth + ">");
                    txt.WriteLine(@"        " + GetHtmlStr(mData[i, j]));
                    txt.WriteLine(@"    </td>");
                }
                txt.WriteLine(@"    <td>");
                txt.WriteLine(@"    </td>");

                txt.WriteLine(@"</tr>");
            }

            WriteFoot(txt);
            txt.Close();
        }

        private const Int32 cCellWidth = 100;
        private const String cCellTop = "18px";

        private static String GetHtmlStr(String value)
        {
            value = ExString.SafeTrim(value);

            if (String.IsNullOrEmpty(value))
                return "&nbsp";
            else
                return value;
        }

        private static void WriteHead(StreamWriter txt, String name, Int32 lCount)
        {
            txt.WriteLine(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            txt.WriteLine(@"<html>");
            txt.WriteLine(@"<head>");
            txt.WriteLine(@"    <title>" + name + "--终极排课自动生成</title>");
            txt.WriteLine(@"    <meta http-equiv=""Content-Type"" content=""text/html; charset=Unicode"">");

            txt.WriteLine(@"    <script language=""javascript"" id=""clientEventHandlersJS"">");
            txt.WriteLine(@"<!--");
            txt.WriteLine(@"    function window_onscroll() ");
            txt.WriteLine(@"    {");
            txt.WriteLine(@"        window.lefttb.style.left=document.body.scrollLeft+1");
            txt.WriteLine(@"        window.toptb.style.posTop =document.body.scrollTop");
            txt.WriteLine(@"    }");
            txt.WriteLine(@"//-->");
            txt.WriteLine(@"    </script>");

            txt.WriteLine(@"    <style type=""text/css"">");
            txt.WriteLine(@"        TD");
            txt.WriteLine(@"        {");
            txt.WriteLine(@"            font-size: 9pt;");
            txt.WriteLine(@"            color: #000000;");
            txt.WriteLine(@"            font-family: ""宋体"";");
            txt.WriteLine(@"            text-decoration: none;");
            txt.WriteLine(@"        }");
            txt.WriteLine(@"    </style>");
            txt.WriteLine(@"    <meta content=""MSHTML 6.00.6000.16705"" name=""GENERATOR"">");
            txt.WriteLine(@"</head>");
            txt.WriteLine(@"<body language=""javascript"" onscroll=""return window_onscroll()"">");
            txt.WriteLine(@"    <table style=""left: 0px; position: absolute; top: 0px"" cellspacing=""0"" cellpadding=""0""");
            txt.WriteLine(@"        width=""" + cCellWidth * lCount + @""" border=""1"">");
            txt.WriteLine(@"        <tbody>");
        }

        private static void WriteFoot(StreamWriter txt)
        {
            txt.WriteLine(@"        </tbody>");
            txt.WriteLine(@"    </table>");
            txt.WriteLine(@"</body>");
            txt.WriteLine(@"</html>");
        }

    }
}
