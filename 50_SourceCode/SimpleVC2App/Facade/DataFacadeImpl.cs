using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.WinFormApp.Facade
{
    public class DataFacadeImpl : IDataFacade
    {
        public DataFacadeImpl(IDataRule dataRule)
        {
            this.DataRule = dataRule;

            Crs = new CrsDataFacadeImpl(dataRule);
            Tch = new TchDataFacadeImpl(dataRule);
            Sqd = new SqdDataFacadeImpl(dataRule);

            Rule = new RuleDataFacadeImpl(dataRule);
        }

        internal IDataRule DataRule { get; private set; }
        internal CrsDataFacadeImpl Crs{ get; private set; }
        internal TchDataFacadeImpl Tch{ get; private set; }
        internal SqdDataFacadeImpl Sqd{ get; private set; }
        internal RuleDataFacadeImpl Rule { get; private set; }

        public EnSolution Solution
        {
            get
            {
                return DataRule.Solution;
            }
            set
            {
                DataRule.Solution = value;
            }
        }

        IGrpMbrDataFacade IDataFacade.Crs
        {
            get
            {
                return this.Crs;
            }
        }

        IGrpMbrDataFacade IDataFacade.Tch
        {
            get
            {
                return this.Tch;
            }
        }

        ISqdDataFacade IDataFacade.Sqd
        {
            get
            {
                return this.Sqd;
            }
        }

        IRuleDataFacade IDataFacade.Rule
        {
            get
            {
                return this.Rule;
            }
        }
    }
}
