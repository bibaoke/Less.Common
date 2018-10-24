using System;
using System.Security;
using System.Windows.Forms;
using Less.WebBrowserQuery;
using Less.Windows;
using System.Linq;
using System.Drawing;

namespace TestForm2
{
    public partial class TestForm2 : Form
    {
        public TestForm2()
        {
            InitializeComponent();

            this.Width = 1100;
            this.Height = 768;

            this.StartPosition = FormStartPosition.CenterScreen;

            this.webBrowser1.ScriptErrorsSuppressed = true;

            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;

            try
            {
                RegEdit.UpdateWebBrowserVersion();
            }
            catch (SecurityException)
            {
                MessageBox.Show(@"程序需要修改注册表，让 WebBrowser 使用最新的 IE 版本，请以管理员身份运行");
            }
        }

        private void TestForm2_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.jd.com/");
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlElement item = this.webBrowser1.Query(".cate_menu_item").First();

            Point p = item.GetScreenCenter(this);

            Simulation.SetCursorPos(p.X, p.Y);
        }
    }
}
