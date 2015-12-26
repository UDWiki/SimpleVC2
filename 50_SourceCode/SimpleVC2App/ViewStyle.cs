using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp
{
    public static class ViewStyle
    {
        private static Color Color_RuleCommon = Color.White;
        private static Color Color_AdvantageCommon = Color.Black;

        //红绿方案
        public static Color cRG_Crisscross = Color.FromArgb(255, 0, 0);
        public static Color cRG_Ill = Color.FromArgb(255, 128, 128);
        public static Color cRG_Fine = Color.FromArgb(128, 255, 128);
        public static Color cRG_Excellent = Color.FromArgb(0, 192, 0);

        //红蓝方案
        public static Color cRB_Crisscross = Color.FromArgb(255, 0, 0);
        public static Color cRB_Ill = Color.FromArgb(255, 128, 128);
        public static Color cRB_Fine = Color.FromArgb(128, 128, 255);
        public static Color cRB_Excellent = Color.FromArgb(0, 0, 255);

        private static Color color_Crisscross = cRG_Crisscross;
        public static Color Color_Crisscross 
        {
            get { return color_Crisscross; } 
            set { color_Crisscross = value; } 
        }

        private static Color color_Ill = cRG_Ill;
        public static Color Color_Ill
        {
            get { return color_Ill; }
            set { color_Ill = value; }
        }

        private static Color color_Fine = cRG_Fine;
        public static Color Color_Fine
        {
            get { return color_Fine; }
            set { color_Fine = value; }
        }

        private static Color color_Excellent = cRG_Excellent;
        public static Color Color_Excellent
        {
            get { return color_Excellent; }
            set { color_Excellent = value; }
        }
        
        public static Color Color_CellFocus = Color.Gray;
        public static Color Color_CellDefForeColor = Color.Black;
        public static Color Color_CellDefBackColor = Color.White; 

        /// <summary>
        ///规则的显示颜色，无定义，显示白色
        /// </summary>
        public static Color RuleToColor(eRule rule)
        {
            switch (rule)
            {
                case eRule.crisscross:
                    return Color_Crisscross;
                case eRule.ill:
                    return Color_Ill;
                case eRule.fine:
                    return Color_Fine;
                case eRule.excellent:
                    return Color_Excellent;
                default:
                    return Color_RuleCommon;
            }
        }

        /// <summary>
        ///优势的显示颜色，无定义，显示黑色
        /// </summary>
        public static Color AdvantageToColor(eRule rule)
        {
            switch (rule)
            {
                case eRule.crisscross:
                    return Color_Crisscross;
                case eRule.ill:
                    return Color_Ill;
                case eRule.fine:
                    return Color_Fine;
                case eRule.excellent:
                    return Color_Excellent;
                default:
                    return Color_AdvantageCommon;
            }
        }

        /// <summary>
        /// 是否节次为列
        /// </summary>
        public static Boolean Horizontal{ get; set; }  
        public static String Description { get; set; }
    }
}
