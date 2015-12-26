using System.Collections.Generic;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.DataRule
{
    internal class RuleDataRuleImpl : IRuleDataRule
    {
        protected DataRuleImpl ThisModule { get; private set; }
        public RuleDataRuleImpl(DataRuleImpl thisModule)
        {
            this.ThisModule = thisModule;
        }

        public IList<VcRuleCell> GetRules(BaseEntity Ety)
        {
            if (Ety == null)
                return new List<VcRuleCell>();
            else
                return ThisModule.Dac.Rule.GetRules(Ety);
        }

        public void SetRules(BaseEntity Ety, IList<VcRuleCell> Value)
        {
            ThisModule.Dac.Rule.SetRules(Ety, Value);

            ThisModule.SendRuleChanged();
        }

        public eRule GetRuleOfTime(BaseEntity Ety, VcTime time)
        {
            foreach (VcRuleCell rt in GetRules(Ety))
                if (rt.Time == time)
                    return rt.Rule;

            return eRule.common;
        }
    }
}
