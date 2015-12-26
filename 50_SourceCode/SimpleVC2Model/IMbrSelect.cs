using System;
using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    public interface IMbrSelect
    {
        String Kind { get; }

        IList<BaseEntity> GroupList { get; }
        IList<BaseEntity> GetMbrList(BaseEntity grp);
    }
}
