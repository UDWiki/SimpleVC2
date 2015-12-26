using System.Collections.Generic;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.Facade
{
    internal class RuleDataFacadeImpl : IRuleDataFacade
    {
        protected IDataRule DataRule { get; private set; }
        public RuleDataFacadeImpl(IDataRule dataRule)
        {
            this.DataRule = dataRule;
        }

        public IList<VcRuleCell> GetRules(BaseEntity Ety)
        {
            return DataRule.Rule.GetRules(Ety);
        }

        public void SetRules(BaseEntity Ety, IList<VcRuleCell> Value)
        {
            DataRule.Rule.SetRules(Ety, Value);
        }
    }
}
