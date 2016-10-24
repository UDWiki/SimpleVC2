using SGLibrary.Framework.ORM;
using System;

namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///内容课
    /// </summary>
    [OrmEntity(TableName = "TSubject",
        FieldDefaultPrefix = "F")]
    public class EnSubject : IBaseEntity
    {
        [OrmPK]
        public Int64 ID { get; set; }

        [OrmValue(Size = 50)]
        public String Name { get; set; }

        public EnSubject Clone()
        {
            return this.MemberwiseClone() as EnSubject;
        }
    }
}
