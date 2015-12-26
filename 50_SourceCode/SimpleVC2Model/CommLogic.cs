using System;
using SGLibrary.Extend;

namespace Telossoft.SimpleVC.Model
{
    //����6�ڿ�
    public enum eBetideNode
    {
        Morn = 0, Forenoon = 1, Afternoon = 2, Evening = 3
    }

    public enum eRule
    {
        crisscross = -2, ill = -1, common = 0, fine = 1, excellent = 2
    }

    public static class CommLogic
    {
        public const Int32 cNameMaxLen = 32;
        public const Int32 cMaxSharedTime = 30;  //ͬһ�Σ�ÿ������Ͽν���
        public const Int32 cMaxTimeOrder = 6;

        public static Int32 TimeToInt32(VcTime Value)
        {
            if (Value != null && Value.HasValue)
                return (Int32)Value.Week * 100
                    + (Int32)Value.BetideNode * 10
                    + Value.Order;
            else
                return 0;
        }

        public static VcTime GetTimeFromInt32(Int32 Value)
        {
            VcTime Result = new VcTime();
            Result.Week = (DayOfWeek)(Value / 100);
            Result.BetideNode = (eBetideNode)(Value % 100 / 10);
            Result.Order = Value % 10;

            return Result;
        }


        public static String BetideNodeToString(eBetideNode value)
        {
            switch (value)
            {
                case eBetideNode.Morn:
                    return "�糿";
                case eBetideNode.Forenoon:
                    return "����";
                case eBetideNode.Afternoon:
                    return "����";
                case eBetideNode.Evening:
                    return "����";
                default:
                    return "����";
            }
        }

        public static eRule RuleAdd(eRule Value1, eRule Value2)
        {
            if (Value1 < 0 || Value2 < 0)
                return (eRule)Math.Min((Int32)Value1, (Int32)Value2);
            else
                return (eRule)Math.Max((Int32)Value1, (Int32)Value2);
        }
    }

    public class VcRuleCell
    {
        private eRule rule;

        public eRule Rule
        {
            get { return rule; }
            set { rule = value; }
        }

        private VcTime time = new VcTime();

        public VcTime Time
        {
            get { return time; }
            set { time = value; }
        }

        public void CopyFieldTo(VcRuleCell obj)
        {
            obj.Rule = Rule;
            Time.CopyFieldTo(obj.Time);
        }

        public static Boolean operator ==(VcRuleCell Left, VcRuleCell Right)
        {
            if ((Object)Left == (Object)Right)
                return true;
            else if ((Object)Left == null ^ (Object)Right == null)
                return false;
            else
                return Left.Rule == Right.Rule
                    && Left.Time == Right.Time;
        }

        public static Boolean operator !=(VcRuleCell Left, VcRuleCell Right)
        {
            return !(Left == Right);
        }

        public override bool Equals(object obj)
        {
            return this == obj as VcRuleCell;
        }

        public override int GetHashCode()
        {
            return (Int32)Rule * 1000
                + (Int32)Time.Week * 100
                + (Int32)Time.BetideNode * 10
                + Time.Order;
        }
    }

    public class VcTime
    {
        private DayOfWeek week;

        public DayOfWeek Week
        {
            get { return week; }
            set
            {
                week = value;
            }
        }

        private eBetideNode betideNode;

        public eBetideNode BetideNode
        {
            get { return betideNode; }
            set
            {
                betideNode = value;
            }
        }

        private Int32 order;  //�ر�ע�⣬��1��ʼ

        public Int32 Order
        {
            get { return order; }
            set
            {
                order = value;
            }
        }

        public Boolean HasValue
        {
            get
            {
                return Enum.IsDefined(typeof(DayOfWeek), this.Week)
                    && Enum.IsDefined(typeof(eBetideNode), this.BetideNode)
                    && (this.Order >= 1 && this.Order <= CommLogic.cMaxTimeOrder);
            }
        }

        public VcTime Clone()
        {
            return this.MemberwiseClone() as VcTime;
        }

        public void CopyFieldTo(VcTime obj)
        {
            obj.Week = Week;
            obj.BetideNode = BetideNode;
            obj.Order = Order;
        }

        public static Boolean operator ==(VcTime Left, VcTime Right)
        {
            if ((Object)Left == (Object)Right)
                return true;
            else if ((Object)Left == null ^ (Object)Right == null)
                return false;
            else
                return Left.Week == Right.Week
                    && Left.BetideNode == Right.BetideNode
                    && Left.Order == Right.Order;
        }

        public static Boolean operator !=(VcTime Left, VcTime Right)
        {
            return !(Left == Right);
        }

        public override bool Equals(object obj)
        {
            return this == obj as VcTime;
        }

        public override int GetHashCode()
        {
            return (Int32)Week * 100
                + (Int32)BetideNode * 10
                + Order;
        }

        public override string ToString()
        {
            if (Order == 0)
                return "δ�����ʱ��";
            else
                return ExDateTime.DayOfWeekToChiese(this.Week, "��")
                    + " " + CommLogic.BetideNodeToString(BetideNode)
                    + " ��" + this.Order + "��";
        }
    }

}
