using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
namespace LotteryPrinter
{
    public partial class XieYi : Form
    {
        public XieYi()
        {
            InitializeComponent();
        }

        private void XieYi_Load(object sender, EventArgs e)
        {
            checkBox1.Focus();
            using(StreamReader re=new StreamReader("XieYi.txt",Encoding.GetEncoding("GB2312")))
            {
                textBox1.Text = re.ReadToEnd();
                textBox1.ForeColor = Color.Gray;
                textBox1.SelectionStart=0;
            }
            
        }
        /// <summary>
        /// 注册表写入
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tovalue"></param>
        private void WTRegedit(string name, string tovalue)
        {
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey software = hklm.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            RegistryKey aimdir = software.CreateSubKey("Lottry");
            aimdir.SetValue(name, tovalue);
        }
        /// <summary>
        /// 删除注册表
        /// </summary>
        /// <param name="name"></param>
        private void DeleteRegist(string name)
        {
            string[] aimnames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",true);
            RegistryKey aimdir = software.OpenSubKey("Lottry", true);
            aimnames = aimdir.GetSubKeyNames();
            foreach (string aimKey in aimnames)
            {
                if (aimKey == name)
                    aimdir.DeleteSubKeyTree(name);
            }
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                File.Delete("XieYi.txt");
            }
            this.Close();
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.BackColor = Color.Blue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
        } 
    }
}
