using SGLibrary.Framework.ORM;
using System;

namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///班级
    /// </summary>
    [OrmEntity(TableName = "TSquad",
        FieldDefaultPrefix = "F")]
    public class EnSquad : IBaseEntity
    {
        [OrmPK]
        public Int64 ID { get; set; }

        [OrmValue(Size = 50)]
        public String Name { get; set; }

        public EnSquad Clone()
        {
            return this.MemberwiseClone() as EnSquad;
        }
    }
}
