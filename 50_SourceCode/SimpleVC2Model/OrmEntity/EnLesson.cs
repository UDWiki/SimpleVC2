using SGLibrary.Framework.ORM;
using System;

namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///课务集
    /// </summary>
    [OrmEntity(TableName = "TLesson",
        FieldDefaultPrefix = "F")]
    public class EnLesson
    {
        [OrmPK]
        public Int64 ID { get; set; }

        [OrmValue(ValueKind = OrmValueKind.Memo)]
        public String SquadIds { get; set; }

        [OrmValue]
        public Int64 SubjectId { get; set; }

        [OrmValue(ValueKind = OrmValueKind.Memo)]
        public String TeacherIds { get; set; }

        [OrmValue]
        public Int32 ActCount { get; set; }

        [OrmValue(ValueKind = OrmValueKind.Memo)]
        public String ActValues { get; set; }

        public EnLesson Clone()
        {
            return this.MemberwiseClone() as EnLesson;
        }
    }
}
