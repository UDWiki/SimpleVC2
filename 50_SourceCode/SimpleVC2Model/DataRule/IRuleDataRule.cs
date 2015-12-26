using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    public interface IRuleDataRule
    {
        eRule GetRuleOfTime(BaseEntity Ety, VcTime time);
        IList<VcRuleCell> GetRules(BaseEntity Ety);
        void SetRules(BaseEntity Ety, IList<VcRuleCell> Value);
    }
}
