using System;
using System.Collections.Generic;
using SGLibrary.Extend;
using SGLibrary.ExcelImportExportImpl_Aspose;

namespace Telossoft.SimpleVC.Export
{
    public static class VcExport
    {
        public static IList<String> GetExportList()
        {
            List<String> result = new List<string>();
            result.Add(cExportToHtml);
            result.Add(cExportToExcel);

            return result;
        }

        private const String cExportToHtml = "输出到HTML";
        private const String cExportToExcel = "输出到Excel";

        public static void ExportData(String kind, VCExportDataGroup data)
        {
            if (kind == cExportToHtml)
            {
                String fileName = ExIO.GetSaveFileName(ExIO.FileFilter_Html, data.Name + ".htm",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                if (!String.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        String[,] mData = ArrangeData(data);
                        ToHtml.ExportDataToHtml(fileName, data.Name, mData);
                        ExUI.ShowInfo("成功输出" + data.Name + ": " + fileName);
                    }
                    catch(Exception e)
                    {
                        ExUI.ShowInfo("输出失败信息: " + e.Message);
                    }
                }
            }
            else if (kind == cExportToExcel)
            {
                String fileName = ExIO.GetSaveFileName(ExIO.FileFilter_Excel, data.Name + ".xls", 
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                if (!String.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        String[,] mData = ArrangeData(data);
                        new BindToExcelImpl().ArrayToExcel(mData, fileName);
                        ExUI.ShowInfo("成功输出" + data.Name + ": " + fileName);
                    }
                    catch (Exception e)
                    {
                        ExUI.ShowInfo("输出失败信息: " + e.Message);
                    }
                }
            }
        }

        public static void ExportData(String kind, VCExportData data)
        {
            ExUI.ShowInfo("暂未实现");
        }

        /// <summary>
        /// 把课表整理到一个二维数组中
        /// </summary>
        private static String[,] ArrangeData(VCExportDataGroup data)
        {
            //获取有效课节数
            Int32 timeCount = data.Matrix.LessonNumberArr[0]
                + data.Matrix.LessonNumberArr[1]
                + data.Matrix.LessonNumberArr[2]
                + data.Matrix.LessonNumberArr[3];
            Int32 dCount = 0;
            foreach (Boolean b in data.Matrix.ActiveWeekArr)
                if (b)
                    dCount++;
            timeCount = timeCount * dCount;

            //定义返回值
            String[,] result;
            if (data.Matrix.Horizontal)
                result = new String[timeCount + 1, data.Data.Count + 1];
            else
                result = new String[data.Data.Count + 1, timeCount + 1];

            //班级/教师名写到行/列首
            for (Int32 etyIdx = 0; etyIdx < data.Data.Count; etyIdx++)
                if (data.Matrix.Horizontal)
                    result[0, etyIdx + 1] = data.Data[etyIdx].Name;
                else
                    result[etyIdx + 1, 0] = data.Data[etyIdx].Name;

            String[] cSection = new String[] { "早晨", "上午", "下午", "晚间" };
            Int32 timeIdx = 1;
            for (Int32 i = 0; i < data.Matrix.ActiveWeekArr.Length; i++)
                if (data.Matrix.ActiveWeekArr[i])
                    for (Int32 j = 0; j < data.Matrix.LessonNumberArr.Length; j++)
                        for (Int32 k = 0; k < data.Matrix.LessonNumberArr[j]; k++)
                        {
                            //写时间到列/行首
                            String timeTitle = "周" + "日一二三四五六"[i] + " " + cSection[j] + "123456"[k];
                            if (data.Matrix.Horizontal)
                                result[timeIdx, 0] = timeTitle;
                            else
                                result[0, timeIdx] = timeTitle;

                            //获取时间对于的数组Idx
                            MatrixCoordinate mc;
                            mc.Week = i;
                            mc.Section = j;
                            mc.Idx = k;
                            mc.Enabled = true;
                            Int32 actIdx = VCExportMatrix.MatrixCoordinateToFlat(mc);

                            //写实际的课程
                            for (Int32 etyIdx = 0; etyIdx < data.Data.Count; etyIdx++)
                                if (data.Matrix.Horizontal)
                                    result[timeIdx, etyIdx + 1] = data.Data[etyIdx].Data[actIdx];
                                else
                                    result[etyIdx + 1, timeIdx] = data.Data[etyIdx].Data[actIdx];

                            timeIdx++;
                        }

            return result;
        }
    }

}
