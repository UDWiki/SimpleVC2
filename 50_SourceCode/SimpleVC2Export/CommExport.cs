using System;
using System.Collections.Generic;

namespace Telossoft.SimpleVC.Export
{
    /// <summary>
    ///排课方案, 通常是一个学期的
    /// </summary>
    public class VCExportMatrix
    {
        public Boolean Horizontal { get; set; }

        private Boolean[] activeWeekArr = new Boolean[7];
        public Boolean[] ActiveWeekArr
        {
            get { return activeWeekArr; }
        }

        private Int32[] lessonNumberArr = new Int32[4];    //最大6节
        public Int32[] LessonNumberArr
        {
            get { return lessonNumberArr; }
        }

        public static Int32 MatrixCoordinateToFlat(MatrixCoordinate value)
        {
            if (value.Week < 0 || value.Week > 6
                || value.Section < 0 || value.Section >= 4
                || value.Idx < 0 || value.Idx >= 6)
                return -1;

            return value.Week * (4 * 6) + value.Section * 6 + value.Idx;
        }

        public static MatrixCoordinate FlatToMatrixCoordinate(Int32 value)
        {
            MatrixCoordinate result;
            result.Enabled = value >= 0 && value < cFlatIdxLen;
            result.Week = value / (4 * 6);
            value = value % (4 * 6);
            result.Section = value / 6;
            result.Idx = value % 6;

            return result;
        }

        public const Int32 cFlatIdxLen = 7 * 4 * 6;
    }

    public struct MatrixCoordinate
    {
        public Boolean Enabled;
        public Int32 Week;
        public Int32 Section;
        public Int32 Idx;
    }


    public class VCExportDataGroup
    {
        public String Name { get; set; }
        public VCExportMatrix Matrix { get; set; }
        public IList<VCExportData> Data { get; set; }
    }

    public class VCExportData
    {
        public String Name { get; set; }
        public VCExportMatrix Matrix { get; set; }
        public String[] Data { get; set; }
    }
}
