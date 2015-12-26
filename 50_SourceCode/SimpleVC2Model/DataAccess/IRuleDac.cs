using System;
using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    public interface IRuleDac
    {
        IList<VcRuleCell> GetRules(BaseEntity Ety);
        void SetRules(BaseEntity Ety, IList<VcRuleCell> Value);
        void DeleteRuleOfEty(BaseEntity Ety);
        void DeleteRuleOfKind(Type type);

        void ClearAll();
    }
}
