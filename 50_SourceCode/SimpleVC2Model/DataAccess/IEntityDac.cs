using System;
using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    public interface IEntityDac<Ety>
        where Ety : BaseEntity
    {
        IList<Ety> List { get; }
        Ety Get(Int64 Id);

        Ety SaveNew(Ety Value);
        Ety SaveExist(Ety Value);

        Boolean NameExist(Ety ety, String Name);
    }
}
