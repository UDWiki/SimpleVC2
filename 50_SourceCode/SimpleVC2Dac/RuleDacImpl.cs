using System;
using System.Collections.Generic;
using System.Data.OleDb;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.Dac
{
    internal class RuleDacImpl : IRuleDac
    {
        protected DataAccessImpl ThisModule { get; private set; }

        public RuleDacImpl(DataAccessImpl thisModule)
        {
            this.ThisModule = thisModule;
        }

        private class KindIdent
        {
            public KindIdent(String Kind)
            {
                kind = Kind;
            }

            private string kind;

            public string Kind
            {
                get { return kind; }
            }

            private IList<VcRuleCell> rules = new List<VcRuleCell>();

            public IList<VcRuleCell> Rules
            {
                get { return rules; }
            }
        }

        private IDictionary<Int64, List<KindIdent>> RuleBuf = new Dictionary<Int64, List<KindIdent>>();

        private Boolean Find(BaseEntity Ety, out List<KindIdent> KindIdents, out Int32 RulesIdx)
        {
            RulesIdx = -1;
            if (RuleBuf.TryGetValue(Ety.Id, out KindIdents))
                for (Int32 i = 0; i < KindIdents.Count; i++)
                    if (KindIdents[i].Kind == Ety.GetType().Name)
                    {
                        RulesIdx = i;
                        return true;
                    }

            return false;
        }

        public IList<VcRuleCell> GetRules(BaseEntity Ety)
        {
            IList<VcRuleCell> Result = new List<VcRuleCell>();

            List<KindIdent> KindIdents;
            Int32 RulesIdx;
            if (Find(Ety, out KindIdents, out RulesIdx))
            {
                ExIList.Append<VcRuleCell>(KindIdents[RulesIdx].Rules, Result);
                return Result;
            }
            else
            {
                KindIdent ki = new KindIdent(Ety.GetType().Name);
                foreach (OleDbDataReader Reader in ThisModule.OleDB.EachRows("select FRule, FTime from TRule "
                    + " where FKind = '" + ki.Kind + "'"
                    + " and FId = " + Ety.Id))
                {
                    VcRuleCell rt = new VcRuleCell();
                    //rt.Rule = (eRule)ExMath.Bound(Convert.ToInt32(Reader[0]), (Int32)eRule.crisscross, (Int32)eRule.excellent, (Int32)eRule.common);

                    var rd = ExConvert.TryToInt32(Reader[0], 0);
                    rt.Rule = rd < (Int32)eRule.crisscross || rd > (Int32)eRule.excellent ? eRule.common : (eRule)rd;
                    rt.Time = CommLogic.GetTimeFromInt32(Convert.ToInt32(Reader[1]));
                    ki.Rules.Add(rt);
                }
                if (KindIdents == null)
                {
                    KindIdents = new List<KindIdent>();
                    KindIdents.Add(ki);
                    RuleBuf.Add(Ety.Id, KindIdents);
                }
                else
                    KindIdents.Add(ki);

                ExIList.Append<VcRuleCell>(ki.Rules, Result);
                return Result;
            }
        }

        public void SetRules(BaseEntity Ety, IList<VcRuleCell> Value)
        {
            List<KindIdent> KindIdents;
            Int32 RulesIdx;
            if (Find(Ety, out KindIdents, out RulesIdx))
            {
                KindIdents[RulesIdx].Rules.Clear();
                ExIList.Append<VcRuleCell>(Value, KindIdents[RulesIdx].Rules);
            }
            else
            {
                KindIdent ki = new KindIdent(Ety.GetType().Name);
                ExIList.Append<VcRuleCell>(Value, ki.Rules);
                if (KindIdents == null)
                {
                    KindIdents = new List<KindIdent>();
                    KindIdents.Add(ki);
                    RulesIdx = KindIdents.Count - 1;
                    RuleBuf.Add(Ety.Id, KindIdents);
                }
            }

            ThisModule.OleDB.ExecuteNonQuery("Delete from TRule "
                    + " where FKind = '" + Ety.GetType().Name + "'"
                    + " and FId = " + Ety.Id);

            foreach(VcRuleCell rt in KindIdents[RulesIdx].Rules)
                ThisModule.OleDB.ExecuteNonQuery("insert into TRule"
                    + " (FKind, FId, FRule, FTime) values ("
                    + "'" + Ety.GetType().Name + "'"
                    + ", " + Ety.Id 
                    + ", " + (Int32)rt.Rule
                    + ", " + CommLogic.TimeToInt32(rt.Time)
                    + ")");
        }

        public void DeleteRuleOfEty(BaseEntity Ety)
        {
            List<KindIdent> KindIdents;
            Int32 RulesIdx;
            if (Find(Ety, out KindIdents, out RulesIdx))
                KindIdents.RemoveAt(RulesIdx);

            ThisModule.OleDB.ExecuteNonQuery("Delete from TRule "
                    + " where FKind = '" + Ety.GetType().Name + "'"
                    + " and FId = " + Ety.Id);
        }


        public void DeleteRuleOfKind(Type type)
        {
            foreach (List<KindIdent> kiList in RuleBuf.Values)
                for (Int32 i = kiList.Count - 1; i >= 0; i--)
                    if (kiList[i].Kind == type.Name)
                        kiList.RemoveAt(i);

            ThisModule.OleDB.ExecuteNonQuery("Delete from TRule "
                    + " where FKind = '" + type.Name + "'");
        }

        public void ClearAll()
        {
            RuleBuf = new Dictionary<Int64, List<KindIdent>>();
            ThisModule.OleDB.ExecuteNonQuery("Delete from TRule");
        }
    }
}
