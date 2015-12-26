using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Telossoft.SimpleVC.WinFormApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool isFistApplicationInstance;
            var mutexName = (Assembly.GetEntryAssembly().FullName + " " + Application.StartupPath).GetHashCode().ToString();
            using (Mutex mutex = new Mutex(false, mutexName, out isFistApplicationInstance))
            {
                if (isFistApplicationInstance)
                    Application.Run(new VC2MainFm());
                else
                    MessageBox.Show("同一文件夹不能重复启动");
            }
        }
    }
}