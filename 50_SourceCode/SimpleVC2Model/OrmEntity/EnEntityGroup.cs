using SGLibrary.Framework.ORM;
using System;

namespace Telossoft.SimpleVC.Model
{
    /// <summary>
    ///规则组
    /// </summary>
    [OrmEntity(TableName = "TEntityGroup",
        FieldDefaultPrefix = "F")]
    public class EnEntityGroup : IBaseEntity
    {
        [OrmPK]
        public Int64 ID { get; set; }

        [OrmValue(Size = 50)]
        public String Name { get; set; }

        [OrmValue(Size = 50)]
        public String MemberTypeName { get; set; }

        [OrmValue(ValueKind= OrmValueKind.Memo)]
        public String MemberIds { get; set; }

        public EnEntityGroup Clone()
        {
            return this.MemberwiseClone() as EnEntityGroup;
        }
    }
}
