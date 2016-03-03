using System;
using SGLibrary.Framework.Config;
using SGLibrary.Framework.Log;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.Facade;
using Telossoft.SimpleVC.WinFormApp.UserInterface;

namespace Telossoft.SimpleVC.WinFormApp
{
    public static class VC2WinFmApp
    {
        public static String ProductHomePage = "http://www.telossoft.com.cn";
        public static String ProductId = "VC2";
        public const String Name = "终极排课软件2";

        public static ISimpleLog ErrorLog;

        public static String cConnStr =
            @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Mode=ReadWrite|Share Deny None;Persist Security Info=True";
        public static IConfig Cfg { get; set; }

        public static IDataRule DataRule{ get; set; }
        public static IEngine Engine { get; set; }
        public static IDataFacade DataFacade { get; set; }
        public static MessageSwitch MessageSwitch { get; set; }

        public static VC2MainFm MainFm { get; set; }
        public static TopScheduleFm TopFm { get; set; }

        /// <summary>
        /// 关闭其他课表窗体
        /// </summary>
        public static void CloseOtherScheduleFm(Object sender)
        {
            foreach (var fm in MainFm.MdiChildren)
                if (fm is IScheduleFm
                    && fm != sender)
                    fm.Close();
        }
    }
}
