using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Telossoft.SimpleVC.WinFormApp.Assist
{
    public partial class AboutFm : Form
    {
        public AboutFm()
        {
            InitializeComponent();

            linkLabel1.Text = VC2WinFmApp.ProductHomePage;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }

        private void AboutFm_Load(object sender, EventArgs e)
        {
            var clientAppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.AssemblyVersionLab.Text += clientAppVersion;
        }
    }
}