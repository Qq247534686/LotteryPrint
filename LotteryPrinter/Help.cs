using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotteryPrinter
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            Thread th = new Thread(StartGo);
            th.Name = "TH_Help";
            th.IsBackground = true;
            th.Start();
        }

        public void StartGo()
        {
            Help_Load(this, new EventArgs());
        }
        private void Help_Load(object sender, EventArgs e)
        {
            //this.Visible = false;
            lab_warning.Text = "    本软件版权归软件\n开发商所有，任何人不\n得非法拷贝，跟踪调试\n及非授权使用！用户因\n使用破解版本而造成的\n系统损坏开发商概不负\n责！";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logon logon = new Logon();
            logon.ShowDialog();
        }
    }
}
