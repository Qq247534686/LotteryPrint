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
using Lottery.BLL;

namespace LotteryPrinter
{
    public partial class CountAward : Form
    {
        public CountAward()
        {
            InitializeComponent();
        }
        private void CountAward_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            timer_Time.Start();
           
        }

        void CountAward_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
        
        public void SJ()
        {
          
            SuanJiangTxt sj = new SuanJiangTxt(); dataGridView1.DataSource = null;
            if (dateTimeStart.Value <= dateTimeEnd.Value && dateTimeStart.Value <= DateTime.Now)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                double SumMoney = 0.00;
                sj.SelectUrl(dateTimeStart.Value, dateTimeEnd.Value);//创建中奖文本
                sj.SelectDate(dateTimeStart.Value, dateTimeEnd.Value);
                dataGridView1.DataSource=sj.GetTableTitck(dateTimeStart.Value, dateTimeEnd.Value);
               label_TitckNum.Text = dataGridView1.Rows.Count.ToString();
               if (dataGridView1.Rows.Count > 0)
               {
                   for (int i = 0; i < dataGridView1.Rows.Count; i++)
                   {
                       SumMoney += double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                   }
               }
               dataGridView1.ScrollBars = ScrollBars.Both;
               label6.Text = "￥" + Math.Round(SumMoney).ToString();
               pictureBox1.Visible = false;
               pictureBox2.Visible = false;
            }
            else
            {
                MessageBox.Show("请检查\"开始时间\"是否正确", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
           
        }

        private void timer_Time_Tick(object sender, EventArgs e)
        {
            label_Time.Text = DateTime.Now.ToString();
        }
        Thread th = null;
        private void button_Titck_Click(object sender, EventArgs e)
        {
                th = new Thread(SJ);
                th.Name = "thSJ";
                th.Start();
        }
    }
}
