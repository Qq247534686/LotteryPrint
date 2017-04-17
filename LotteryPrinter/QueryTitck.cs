using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lottery.BLL;

namespace LotteryPrinter
{
    public partial class QueryTitck : Form
    {
        public QueryTitck()
        {
            //Cursor sc
            InitializeComponent();
            //this.Cursor = sc;
        }
        ObtainData db = new ObtainData();
        SubstringWbeFootball wc = new SubstringWbeFootball();
        private void QueryTitck_Load(object sender, EventArgs e)
        {
            Thread th = new Thread(DataDownAccess);
            th.Name = "TH_QueryTitck";
            th.IsBackground = true;
            th.Start();
            //DataDownAccess();
        }

        private void DataDownAccess()
        {

            DataTable tb = new DataTable();
            tb = db.Get_TableData(20);
            if (tb.Rows.Count > 0)
            {
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["PlayType"].ToString().Contains("900"))
                    {
                        dataGridView1.Rows.Add();
                        string spVaalue = BianXiangZuHedata(tb.Rows[i]["BuyTitckID"].ToString(), tb.Rows[i]["PlayType"].ToString(), tb.Rows[i]["TouZhuValue"].ToString(), int.Parse(tb.Rows[i]["Multiple"].ToString()));
                        string biJiaoSp = BiJiaoSP(wc.SelectUrl(tb.Rows[i]["PlayType"].ToString(), tb.Rows[i]["TouZhu"].ToString()),tb.Rows[i]["TouZhuValue"].ToString(), tb.Rows[i]["BuyTitckID"].ToString(),i);
                        dataGridView1.Rows[i].Cells[0].Value = biJiaoSp;
                        dataGridView1.Rows[i].Cells[1].Value = tb.Rows[i]["BuyTitckID"].ToString();
                        dataGridView1.Rows[i].Cells[2].Value = tb.Rows[i]["Multiple"].ToString();
                        dataGridView1.Rows[i].Cells[3].Value = tb.Rows[i]["PlayType"].ToString();
                        dataGridView1.Rows[i].Cells[4].Value = spVaalue;
                        dataGridView1.Rows[i].Cells[5].Value = tb.Rows[i]["IsTitck"].ToString() == "0" ? "否" : "是";
                        dataGridView1.Rows[i].Cells[6].Value = tb.Rows[i]["TouZhu"].ToString();
                        dataGridView1.Rows[i].Cells[7].Value = tb.Rows[i]["BiaoShi"].ToString();
                        dataGridView1.Rows[i].Cells[8].Value = tb.Rows[i]["CreateTime"].ToString();
                        dataGridView1.Rows[i].Cells[9].Value = tb.Rows[i]["UpTime"].ToString();
                        dataGridView1.Rows[i].Cells[10].Value = tb.Rows[i]["TouZhuValue"].ToString();
                    }

                }
            }
            label_countD.Text = dataGridView1.Rows.Count.ToString();
        }

        private string BiJiaoSP(string p1,string p2,string p3,int str_i)
        {
            string strbj = string.Empty;
            try
            {
                DataGridViewCellStyle strColor = new DataGridViewCellStyle();
                strColor.Alignment = DataGridViewContentAlignment.MiddleLeft;
                List<string> strbj1 = new List<string>();
                strbj1 = p1.Split(',').ToList();
                List<string> strbj2 = new List<string>();
                strbj2 = p2.Split(',').ToList();
                for (int i = 0; i < strbj2.Count; i++)
                {
                     float f1=float.Parse(Regex.Match(strbj1[i], @".+?\((\d+.\d+)\)").Groups[1].Value);
                     float f2=float.Parse(Regex.Match(strbj2[i], @".+?\((\d+.\d+)\)").Groups[1].Value);
                    if (f1==f2)
                    {
                        strbj += "P["+f1.ToString()+"]";
                        strColor.ForeColor = Color.Blue;
                        
                        dataGridView1.Rows[str_i].Cells[0].Style = strColor;
                    }
                    else
                    {
                       
                        if (f1 > f2)
                        {
                             string result=String.Format("{0:F}",(f1 - f2));
                             strbj += "↑" + result;
                             strColor.ForeColor = Color.Purple;
                            dataGridView1.Rows[str_i].Cells[0].Style = strColor;

                        }
                        else if (f2 > f1)
                        {
                            string result = String.Format("{0:F}", (f1 - f2));
                            strbj += "↓" + result;
                            strColor.ForeColor = Color.Red;
                            dataGridView1.Rows[str_i].Cells[0].Style = strColor;
                        }
                    }

                }
            }
            catch
            {
                DataGridViewCellStyle sccv = new DataGridViewCellStyle();
                sccv.BackColor = Color.Gray;
                sccv.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Rows[str_i].DefaultCellStyle = sccv;
                dataGridView1.Rows[str_i].Cells[0].Style.ForeColor = Color.White;
                strbj = "过期了";
                //MessageBox.Show(p3 + "的单号出现错误:", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return strbj;
        }
        private string BianXiangZuHedata(string Id,string playID,string theNumber,int BeiShu)
        {
            string ssc = string.Empty; float SumSpValue=1;
            SD_Football sd = new SD_Football();
            List<string> lst = sd.SelectUrl(Id, playID, theNumber, "NO");
            if (lst != null)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                   SumSpValue*=float.Parse(lst[i]);
                }
            }
            string xc = string.Empty; string xb = string.Empty;
            SumSpValue = (SumSpValue * 2) * BeiShu;
            ssc = String.Format("{0:F}", SumSpValue);
            return ssc;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectNowRow(0);
        }
        public void SelectNowRow(int TheIndex)
        {
            label_ID.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label_Munitlp.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label_SpSum.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            lotteryAction lp = new lotteryAction();
            List<string> lst = lp.ContextPlay(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            if (lst.Count > 0)
            {
                label_Type.Text = lst[0]; label_PlayID.Text = lst[1];
            }
            label_Guan.Text = ChuanGuan(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
        }

        private string ChuanGuan(string p)
        {
            string mat = string.Empty;
            MatchCollection mts = Regex.Matches(p, @"\|(\d+\*\d+)");
            foreach (Match item in mts)
            {
                if (item.Success)
                {
                    mat = item.Groups[1].Value;
                    if (string.IsNullOrWhiteSpace(mat))
                    {
                        mat = "单关";
                    }
                }
            }
            return mat;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString().Equals("否"))
            {
                dataGridView1.SelectedRows[0].Cells[5].Value = "是";
                DataGridViewCellStyle sty = new DataGridViewCellStyle();
                sty.BackColor = Color.Green;
                dataGridView1.SelectedRows[0].DefaultCellStyle = sty;
                if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString().Equals("是"))
                {
                    db.Set_Data(dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sk = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[5].Value.ToString().Equals("是"))
                {
                    sk++;
                }
            }
            if (sk == dataGridView1.Rows.Count)
            {
                dataGridView1.Rows.Clear();
                DataDownAccess();
            }
            else
            {
                int countInt = dataGridView1.Rows.Count - sk;
                MessageBox.Show("还有" + countInt + "张票未打完", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            db.mR();
            DataDownAccess();
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                dataGridView1.Rows[0].Selected = false;
            }
        }

        #region  显示的方法
        public void Input_i(int i ,string isAddStr)
        {
            if (isAddStr.Equals("Yes"))
            {
                label_ID.Text = dataGridView1.Rows[i + 1].Cells[1].Value.ToString();
                label_Munitlp.Text = dataGridView1.Rows[i + 1].Cells[2].Value.ToString();
                lotteryAction lp = new lotteryAction();
                List<string> lst = lp.ContextPlay(dataGridView1.Rows[i + 1].Cells[3].Value.ToString());
                label_Type.Text = lst[0]; label_PlayID.Text = lst[1];
                string ssc = dataGridView1.Rows[i + 1].Cells[4].Value.ToString();
                label_SpSum.Text = ssc;
                label_Guan.Text = ChuanGuan(dataGridView1.Rows[i + 1].Cells[6].Value.ToString());
            }
            else
            {
                label_ID.Text = dataGridView1.Rows[i - 1].Cells[1].Value.ToString();
                label_Munitlp.Text = dataGridView1.Rows[i - 1].Cells[2].Value.ToString();
                lotteryAction lp = new lotteryAction();
                List<string> lst = lp.ContextPlay(dataGridView1.Rows[i - 1].Cells[3].Value.ToString());
                label_Type.Text = lst[0]; label_PlayID.Text = lst[1];
                string ssc = dataGridView1.Rows[i - 1].Cells[4].Value.ToString();
                label_SpSum.Text = ssc;
                label_Guan.Text = ChuanGuan(dataGridView1.Rows[i - 1].Cells[6].Value.ToString());
            }
        }
        #endregion

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewCellStyle strYello = new DataGridViewCellStyle();
            strYello.BackColor = Color.Yellow;
            if (e.KeyCode == Keys.Enter)
            {
                //label2.Text =dataGridView1.SelectedRows[.Cells[3].Value.ToString();
                var scvt = dataGridView1.Rows;
                for (int i = 0; i < scvt.Count; i++)
                {
                    if (scvt[i].Selected && (i + 1 < scvt.Count))
                    {
                            Input_i(i, "Yes");
                           
                        //SelectNowRow()
                    }
                }
                if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString().Equals("否"))
                {
                    dataGridView1.SelectedRows[0].Cells[5].Value = "是";
                    DataGridViewCellStyle sty = new DataGridViewCellStyle();
                    sty.BackColor = Color.Green;
                    dataGridView1.SelectedRows[0].DefaultCellStyle = sty;
                    if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString().Equals("是"))
                    {
                        db.Set_Data(dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    }
                }
                int sk = 0;
                for (int v = 0; v < dataGridView1.Rows.Count; v++)
                {
                    if (dataGridView1.Rows[v].Cells[5].Value.ToString().Equals("是"))
                    {
                        sk++;
                    }
                }
                if (sk == dataGridView1.Rows.Count)
                {
                    dataGridView1.Rows.Clear();
                    DataDownAccess();
                }
                else
                {
                    int countInt = (dataGridView1.Rows.Count) - sk;
                    label_countD.Text = countInt.ToString();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                var scvt = dataGridView1.Rows;
                for (int i = 0; i < scvt.Count; i++)
                {
                    if (scvt[i].Selected && (i >= 1))
                    {
                        Input_i(i, "No");
                    }
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                var scvt = dataGridView1.Rows;
                for (int i = 0; i < scvt.Count; i++)
                {
                    if (scvt[i].Selected && (i + 1 < scvt.Count))
                    {
                        Input_i(i, "Yes");
                    }
                }
            }
        }
    }
}
