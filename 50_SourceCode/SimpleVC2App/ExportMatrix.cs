using System;
using System.Collections.Generic;
using Telossoft.SimpleVC.Export;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp
{
    public class ExportMatrix
    {
        public static VCExportDataGroup GetExportData(String name, IList<BaseEntity> entities)
        {
            VCExportDataGroup result = new VCExportDataGroup();
            result.Name = name;
            result.Matrix = GetExportMatrix();
            result.Data = new List<VCExportData>();
            foreach (BaseEntity entity in entities)
            {
                VCExportData data = new VCExportData();
                data.Name = entity.Name;
                data.Matrix = result.Matrix;
                data.Data = GetMatrixStringArray(result.Matrix, entity);

                result.Data.Add(data);
            }

            return result;
        }

        public static VCExportData GetExportData(BaseEntity entity)
        {
            VCExportData result = new VCExportData();
            result.Name = entity.Name;
            result.Matrix = GetExportMatrix();
            result.Data = GetMatrixStringArray(result.Matrix, entity);

            return result;
        }

        private static string[] GetMatrixStringArray(VCExportMatrix Matrix, BaseEntity entity)
        {
            String[] result = new String[VCExportMatrix.cFlatIdxLen];
            if (entity is EnTeacher)
            {
                DtMatrix<IList<EnLsnAct>> DtMatrix = VC2WinFmApp.Engine.GetTchMatrix(entity as EnTeacher);

                foreach (VcTime time in DtMatrix.eachTime())
                {
                    IList<EnLsnAct> acts = DtMatrix[time];
                    if (acts != null && acts.Count > 0)
                    {
                        MatrixCoordinate mc;
                        mc.Enabled = true;
                        mc.Week = (Int32)time.Week;
                        mc.Section = (Int32)time.BetideNode;
                        mc.Idx = time.Order - 1;
                        Int32 idx = VCExportMatrix.MatrixCoordinateToFlat(mc);
                        String value = null;
                        foreach (EnLsnAct act in acts)
                            value = (String.IsNullOrEmpty(value) ? "" : value + "  ") + act.Squad + " " + act.ClsLesson.Lesson.Course.Name;

                        result[idx] = value;
                    }
                }
            }
            else if (entity is EnSquad)
            {
                DtMatrix<EnLsnAct> DtMatrix = VC2WinFmApp.Engine.GetSqdMatrix(entity as EnSquad);

                foreach (VcTime time in DtMatrix.eachTime())
                {
                    EnLsnAct act = DtMatrix[time];
                    if (act != null)
                    {
                        MatrixCoordinate mc;
                        mc.Enabled = true;
                        mc.Week = (Int32)time.Week;
                        mc.Section = (Int32)time.BetideNode;
                        mc.Idx = time.Order - 1;
                        Int32 idx = VCExportMatrix.MatrixCoordinateToFlat(mc);
                        String value = act.ClsLesson.Lesson.Course.Name;

                        result[idx] = value;
                    }
                }
            }

            return result;
        }

        public static VCExportMatrix GetExportMatrix()
        {
            VCExportMatrix result = new VCExportMatrix();

            result.Horizontal = ViewStyle.Horizontal;
            
            for (Int32 i = 0; i < result.ActiveWeekArr.Length; i++)
                result.ActiveWeekArr[i] = VC2WinFmApp.DataRule.Solution.ActiveWeekArr[i];

            for (Int32 i = 0; i < result.LessonNumberArr.Length; i++)
                result.LessonNumberArr[i] = VC2WinFmApp.DataRule.Solution.LessonNumberArr[i];

            return result;
        }
    }
}
