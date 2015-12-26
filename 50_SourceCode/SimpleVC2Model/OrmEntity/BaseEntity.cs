using System;
using SGLibrary.Extend;

namespace Telossoft.SimpleVC.Model
{
    public class BaseEntity
    {
        private Int64 id;
        public Int64 Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private String name;
        public virtual String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = ExString.SafeSubstring(value, 0, CommLogic.cNameMaxLen);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return (Int32)this.Id;
        }

        public virtual object Clone()
        {
            BaseEntity Result = this.MemberwiseClone() as BaseEntity;
            return Result;
        }

        public virtual void CopyFieldTo(object obj)
        {
            (obj as BaseEntity).name = this.name;
        }
    }
}
