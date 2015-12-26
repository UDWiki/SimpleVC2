using System;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.DataRule
{
    public class DataRuleImpl : IDataRule
    {
        public DataRuleImpl(IDataAccess dac)
        {
            this.Dac = dac;
            
            Crs = new CrsDataRuleImpl(this);
            Tch = new TchDataRuleImpl(this);
            Sqd = new SqdDataRuleImpl(this);
            Lsn = new LsnDataRuleImpl(this);
            Rule = new RuleDataRuleImpl(this);

            if (this.Dac.Solution == null)
                this.Dac.Solution = GetDefaultSln();
        }

        internal IDataAccess Dac { get; private set; }

        internal CrsDataRuleImpl Crs { get; private set; }
        internal TchDataRuleImpl Tch { get; private set; }
        internal SqdDataRuleImpl Sqd { get; private set; }
        internal LsnDataRuleImpl Lsn { get; private set; }
        internal RuleDataRuleImpl Rule { get; private set; }

        private Boolean InLocked;

        public void LockRefresh()
        {
            InLocked = true;
        }

        public void unLockRefresh()
        {
            InLocked = false;

            if (EventActed[0])
            {
                if (SlnChanged != null)
                    SlnChanged();
            }
            else if (EventActed[1])
            {
                if (DataChanged != null)
                    DataChanged();
            }
            else if (EventActed[2])
            {
                if (RuleChanged != null)
                    RuleChanged();
            }

            EventActed[0] = false;
            EventActed[1] = false;
            EventActed[2] = false;
        }

        private Boolean[] EventActed = new Boolean[3];

        public void SendSlnChanged()
        {
            if (InLocked)
                EventActed[0] = true;
            else if (SlnChanged != null)
                SlnChanged();
        }

        public void SendDataChanged()
        {
            if (InLocked)
                EventActed[1] = true;
            else if (DataChanged != null)
                DataChanged();
        }

        public void SendRuleChanged()
        {
            if (InLocked)
                EventActed[2] = true;
            else if (RuleChanged != null)
                RuleChanged();
        }

        public static EnSolution GetDefaultSln()
        {
            EnSolution Result = new EnSolution();

            for (Int32 i = 1; i <= 5; i++)
                Result.ActiveWeekArr[i] = true;

            Result.LessonNumberArr[0] = 1;
            Result.LessonNumberArr[1] = 4;
            Result.LessonNumberArr[2] = 4;
            Result.LessonNumberArr[3] = 2;

            return Result;
        }

        //SlnChange 〉DataChange 〉 RuleChange
        public event dlgAfterBizChanged SlnChanged;
        public event dlgAfterBizChanged DataChanged;
        public event dlgAfterBizChanged RuleChanged;

        public EnSolution Solution
        {
            get
            {
                return Dac.Solution;
            }
            set
            {
                if (!ExIList.MemberEquals<Boolean>(Dac.Solution.ActiveWeekArr, value.ActiveWeekArr)
                    || !ExIList.MemberEquals<Int32>(Dac.Solution.LessonNumberArr, value.LessonNumberArr))
                {
                    Dac.Solution = value;
                    SendSlnChanged();
                }
            }
        }

        ICrsDataRule IDataRule.Crs
        {
            get { return this.Crs; }
        }

        ITchDataRule IDataRule.Tch
        {
            get { return this.Tch; }
        }

        ISqdDataRule IDataRule.Sqd
        {
            get { return this.Sqd; }
        }

        ILsnDataRule IDataRule.Lsn
        {
            get { return this.Lsn; }
        }

        IRuleDataRule IDataRule.Rule
        {
            get { return this.Rule; }
        }

        public void SysInit()
        {
            Dac.Rule.ClearAll();
            Dac.Lsn.ClearAll();
            Dac.Crs.ClearAll();
            Dac.Sqd.ClearAll();
            Dac.Tch.ClearAll();

            SendDataChanged();
        }

        public bool ReadOnly { get; set; }
    }
}
