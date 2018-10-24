using Less.MultiThread;
using Less.Windows;

namespace Form
{
    public partial class TestForm1 : System.Windows.Forms.Form
    {
        public TestForm1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            Asyn.Exec(()=> {
                this.Invoke(() =>
                {
                    this.Close();
                });
            });
        }
    }
}
