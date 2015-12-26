using System;
using System.Collections.Generic;
using System.Text;
using SGLibrary.Extend;

namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///排课方案, 通常是一个学期的
    /// </summary>
    public class EnSolution : BaseEntity
    {
        private Boolean[] activeWeekArr = new Boolean[7];
        public Boolean[] ActiveWeekArr
        {
            get { return activeWeekArr; }
        }

        private Int32[] lessonNumberArr = new Int32[4];
        public Int32[] LessonNumberArr
        {
            get { return lessonNumberArr; }
        }

        public override object Clone()
        {
            //VcEntity Result = this.MemberwiseClone() as VcEntity;

            //VcSolution ts = Result as VcSolution;
            //ts.activeWeekArr = new Boolean[7];
            //ts.lessonNumberArr = new Int32[4];
            //CopyFieldTo(ts);

            //return Result;

            EnSolution Result = new EnSolution();
            CopyFieldTo(Result);

            return Result;
        }

        public override void CopyFieldTo(object obj)
        {
            base.CopyFieldTo(obj);

            EnSolution ts = obj as EnSolution;
            for (var i = 0; i < ActiveWeekArr.Length; i++)
                ts.ActiveWeekArr[i] = ActiveWeekArr[i];

            for (var i = 0; i < LessonNumberArr.Length; i++)
                ts.LessonNumberArr[i] = LessonNumberArr[i];
        }
    }
}
