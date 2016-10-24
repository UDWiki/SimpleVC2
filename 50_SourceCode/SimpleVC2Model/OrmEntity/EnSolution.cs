using SGLibrary.Framework.ORM;
using System;

namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///课表方案, 通常是一个学期的
    /// </summary>
    [OrmEntity(TableName = "TSolution",
        FieldDefaultPrefix = "F")]
    public class EnSolution : IBaseEntity
    {
        [OrmPK]
        public Int64 ID { get; set; }

        [OrmValue(Size = 50)]
        public String Name { get; set; }

        [OrmValue]
        public String ActiveWeekStr { get; set; }

        [OrmValue]
        public String LessonNumberArr { get; set; }

        public EnSolution Clone()
        {
            return this.MemberwiseClone() as EnSolution;
        }
    }
}
