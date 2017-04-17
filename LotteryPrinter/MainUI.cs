using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lottery.BLL;
using Lottery.Model;
using Lottery_IBLL;
using Microsoft.Win32;
namespace LotteryPrinter
{
    public partial class MainUI : Form
    {
        #region 变量
        List<string> allType = new List<string>();//混合彩彩票类型
        List<string> allDataText = new List<string>();//混合彩彩票内容
        List<string> pass = new List<string>();//其他彩票的内容
        List<string> sumStr = new List<string>();
        List<string> Is_F = new List<string>();//红色机子:38400,白色机子:115200,9600
        SerialPort port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
        int myInt = 0; int herInt = 0;
        string lotteryType = string.Empty;//福彩还是体彩
        string strPlayFootball = string.Empty;//玩法
        string strPlay = string.Empty;//过关方式
        string numberNo = string.Empty;//票号
        string numberSafetyCode = string.Empty;//安全码
        string numberPassword = string.Empty;//密码
        string multiple = string.Empty;//倍数
        string sum = string.Empty;//合计
        string date = string.Empty;//日期
        string time = string.Empty;//时间
        List<string> fValue = new List<string>();
        List<string> passArray = new List<string>();//场数(关数)集合
        List<string> weekendArray = new List<string>();//周几集合
        List<string> hostAndVisitorArray = new List<string>();//主客对集合
        List<string> victoryOrDefeatArray = new List<string>();//内容-胜负/比分/。。。集合
        List<MyLotteryData> sv = new List<MyLotteryData>();//数据绑定源
        ServiceReference1.Service1SoapClient myWebServer = new ServiceReference1.Service1SoapClient();
        #endregion

        #region 构造方法
        public MainUI()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        #endregion

        #region 彩票的处理方法
        public string IsResult(string strResult)
        {
            string str = string.Empty;
            if (strPlayFootball.Contains("900"))
            {
                if (strPlayFootball != "9005")
                {
                    str = OutTypeStr(strResult);
                }
                else//混合彩
                {
                    string strSum = "";
                    for (int i = 0; i < allType.Count; i++)
                    {
                        strPlayFootball = "9005";
                        switch (allType[i].ToString())
                        {
                            case "让球胜平负": strPlayFootball = "9001";
                                strSum = OutTypeStr(allDataText[i].ToString());
                                sumStr.Add(strSum);
                                break;
                            case "总进球数": strPlayFootball = "9002";
                                strSum = OutTypeStr(allDataText[i].ToString());
                                sumStr.Add(strSum);
                                break;
                            case "比分": strPlayFootball = "9003";
                                strSum = OutTypeStr(allDataText[i].ToString());
                                sumStr.Add(strSum);
                                break;
                            case "半全场胜平负": strPlayFootball = "9004";
                                strSum = OutTypeStr(allDataText[i].ToString());
                                sumStr.Add(strSum);
                                break;
                            case "胜平负":
                                strPlayFootball = "9006";
                                strSum = OutTypeStr(allDataText[i].ToString());
                                sumStr.Add(strSum);
                                break;//"6_"
                            default: break;
                        }
                    }
                    for (int i = 0; i < sumStr.Count; i++)
                    {
                        str += sumStr[i].ToString() + ",";
                    }
                }
            }
            else if (strPlayFootball.Contains("910"))
            {

                if (strPlayFootball != "9105")
                {
                    str = OutTypeStr(strResult);
                }
                else//篮球混合彩
                {
                    string strSum = "";
                    for (int i = 0; i < allType.Count; i++)
                    {
                        strPlayFootball = "9105";
                        switch (allType[i].ToString())
                        {
                            case "让分胜负": strPlayFootball = "9101";
                                strSum = OutTypeStr(allDataText[i].ToString());
                                if (strSum != "" && strPlay != "")
                                {

                                    sumStr.Add("1_" + strSum);
                                }
                                break;
                            case "胜负": strPlayFootball = "9102";
                                strSum = OutTypeStr(allDataText[i].ToString());
                                if (strSum != "" && strPlay != "")
                                {
                                    sumStr.Add("2_" + strSum);
                                }
                                break;
                            case "胜分差": strPlayFootball = "9103";
                                strSum = OutTypeStr(allDataText[i].ToString());
                                if (strSum != "" && strPlay != "")
                                {
                                    sumStr.Add("3_" + strSum);
                                }
                                break;
                            case "大小分": strPlayFootball = "9104";
                                strSum = OutTypeStr(allDataText[i].ToString());
                                sumStr.Add("4_" + strSum);
                                break;
                            default: break;
                        }
                    }
                    for (int i = 0; i < sumStr.Count; i++)
                    {
                        str += sumStr[i].ToString() + ",";
                    }
                }
            }
            return str;
        }
        public string OutTypeStr(string strResult)
        {
            string str = string.Empty; string strRegex = string.Empty; string select = string.Empty;
            if (strPlayFootball == "9006" || strPlayFootball == "9102")
            {
                if (strPlayFootball == "9006")
                {
                    typess.Add("SPF");
                }
                else
                {
                    typess.Add("SFG");
                }
                strRegex = @"([\u4e00-\u9fa5])@(\d+.\d+)[\u4e00-\u9fa5]";
                MatchCollection matches = Regex.Matches(strResult.Replace("+", ""), strRegex);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        if (strPlayFootball == "9006")
                        {
                            switch (match.Groups[1].Value)
                            {
                                case "胜": select = "3"; break;
                                case "平": select = "1"; break;
                                case "负": select = "0"; break;
                                default: break;
                            }
                        }
                        else
                        {
                            switch (match.Groups[1].Value)
                            {
                                case "胜": select = "0"; break;
                                case "负": select = "1"; break;
                                default: break;
                            }
                        }
                        str += select + "#" + match.Groups[2].Value + "/";
                    }
                }
            }
            else if (strPlayFootball == "9003")
            {
                string strThis = ""; typess.Add("CBF");
                strRegex = @"\((\d+:\d+|[\u4e00-\u9fa5])([\u4e00-\u9fa5][\u4e00-\u9fa5])*\)@(\d+.\d+)[\u4e00-\u9fa5]";
                MatchCollection matches = Regex.Matches(strResult.Replace("+", ""), strRegex);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        switch (match.Groups[1].Value)
                        {
                            case "胜": strThis = "9:0"; break;
                            case "平": strThis = "9:9"; break;
                            case "负": strThis = "0:9"; break;
                            default: strThis = match.Groups[1].Value; break;
                        }
                        str += strThis + "#" + match.Groups[3].Value + "/";
                    }
                }
            }
            else if (strPlayFootball == "9002")
            {
                typess.Add("JQS");
                strRegex = @"\((\d+)\)@(\d+.\d+)[\u4e00-\u9fa5]";
                MatchCollection matches = Regex.Matches(strResult.Replace("+", ""), strRegex);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        str += match.Groups[1].Value + "#" + match.Groups[2].Value + "/";
                    }
                }
            }
            else if (strPlayFootball == "9004")
            {
                string orstr = ""; typess.Add("BQC");
                strRegex = @"([\u4e00-\u9fa5])([\u4e00-\u9fa5])@(\d+.\d+)[\u4e00-\u9fa5]";
                MatchCollection matches = Regex.Matches(strResult.Replace("+", ""), strRegex);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        switch (match.Groups[1].Value)
                        {
                            case "胜": select = "3"; break;
                            case "平": select = "1"; break;
                            case "负": select = "0"; break;
                            default: break;
                        }
                        switch (match.Groups[2].Value)
                        {
                            case "胜": orstr = "3"; break;
                            case "平": orstr = "1"; break;
                            case "负": orstr = "0"; break;
                            default: break;
                        }
                        str += select + "-" + orstr + "#" + match.Groups[3].Value + "/";
                    }
                }
            }
            else if (strPlayFootball == "9001" || strPlayFootball == "9101")
            {
                strRegex = @"([\u4e00-\u9fa5])@(\d+.\d+)[\u4e00-\u9fa5]";
                if (strPlayFootball == "9001")
                {
                    typess.Add("RSP");
                }
                else
                {
                    typess.Add("RSF");
                }
                MatchCollection matches = Regex.Matches(strResult.Replace("+", ""), strRegex);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        if (strPlayFootball == "9001")
                        {
                            switch (match.Groups[1].Value)
                            {
                                case "胜": select = "3"; break;
                                case "平": select = "1"; break;
                                case "负": select = "0"; break;
                                default: break;
                            }
                        }
                        else
                        {
                            switch (match.Groups[1].Value)
                            {
                                case "胜": select = "1"; break;
                                case "负": select = "2"; break;
                                default: break;
                            }
                        }
                        str += select + "#" + match.Groups[2].Value + "/";
                    }
                    if (strPlayFootball == "9101")
                    {
                        str = str.TrimEnd('/') + fValue[myInt] + "/";
                    }
                }
                myInt++;
            }
            else if (strPlayFootball == "9104")
            {
                typess.Add("SFC");
                strRegex = @"([\u4e00-\u9fa5])@(\d+.\d+)[\u4e00-\u9fa5]";
                MatchCollection matches = Regex.Matches(strResult.Replace("+", ""), strRegex);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        switch (match.Groups[1].Value)
                        {
                            case "大": select = "1"; break;
                            case "小": select = "2"; break;
                            default: break;
                        }
                        str += select + "#" + match.Groups[2].Value + "/";
                    }
                    if (strPlayFootball == "9104")
                    {
                        str = str.TrimEnd('/') + Is_F[herInt] + "/";
                    }
                }
                herInt++;
            }
            else if (strPlayFootball == "9103")
            {
                typess.Add("SFD");
                strRegex = @"\(([\u4e00-\u9fa5])(\d+\-\d+|\d+)\)@(\d+.\d+)[\u4e00-\u9fa5]";
                MatchCollection matches = Regex.Matches(strResult.Replace("+", ""), strRegex);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        if (match.Groups[1].Value == "主")
                        {
                            switch (match.Groups[2].Value)
                            {
                                case "1-5": select = "01"; break;
                                case "6-10": select = "02"; break;
                                case "11-15": select = "03"; break;
                                case "16-20": select = "04"; break;
                                case "21-25": select = "05"; break;
                                case "26": select = "06"; break;
                                default: break;
                            }
                        }
                        else if (match.Groups[1].Value == "客")
                        {
                            switch (match.Groups[2].Value)
                            {
                                case "1-5": select = "11"; break;
                                case "6-10": select = "12"; break;
                                case "11-15": select = "13"; break;
                                case "16-20": select = "14"; break;
                                case "21-25": select = "15"; break;
                                case "26": select = "16"; break;
                                default: break;
                            }
                        }
                        str += select + "#" + match.Groups[3].Value + "/";
                    }
                }
            }
            return str.TrimEnd('/');
        }
        #endregion

        #region 上传篮球或者足球数据
        /// <summary>
        /// 上传篮球数据
        /// </summary>
        /// <param name="str"></param>
        public void saveDataLotteryB(string str)
        {
            string str1 = string.Empty; string str2 = string.Empty; string str3 = string.Empty;
            string str4 = string.Empty; string str5 = string.Empty; string str6 = string.Empty;
            string str7 = string.Empty;
            string[] GetAllData = str.Split(',');
            List<string> dd = new List<string>();
            List<string> money = new List<string>();
            for (int i = 0; i < typess.Count; i++)
            {
                dd.Clear(); money.Clear(); string Fvalue = "";
                MatchCollection resg = Regex.Matches(GetAllData[i], @"((\d+\-\d+|\d+:\d+)|\d+)#(\d+.\d+)(F\[(\+|\-)*(\d+.\d+)\])*");
                foreach (Match item in resg)
                {
                    if (item.Success)
                    {
                        if (item.Groups[4].Value != "" && item.Groups[4].Value != null)
                        {
                            Fvalue = item.Groups[4].Value.Replace("F", "");
                        }
                        else
                        {
                            Fvalue = "";
                        }
                        dd.Add(item.Groups[1].Value + Fvalue);
                        money.Add(item.Groups[3].Value);
                    }
                }
                for (int j = 0; j < dd.Count; j++)
                {
                    str1 = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    str2 = date.Substring(2) + weekendArray[i];
                    str3 = typess[i];// item.Groups[1].Value;
                    str4 = dd[j];
                    str5 = money[j];
                    str6 = weekendArray[i];
                    str7 = date.Substring(2);
                    //Thread.Sleep(new Random().Next(10000, 20000));
                    string sv = str1 + "&" + str2 + "&" + str3 + "&" + str4 + "&" + str5 + "&" + str6 + "&" + str7;
                    if (myWebServer.SetDataMethod(sv) > 0)
                    {
                        countSCV++;
                        label_CountTitck.Text = countSCV.ToString();
                    }
                }
            }
            //if (bhn > 0)
            //{
            //    int k = myWebServer.upDate(dataGridView1.Rows[0].Cells[3].Value.ToString());
            //    if (k <= 0)
            //    {
            //        MessageBox.Show("打印失败!!!");
            //        timer_Lottery.Stop();
            //    }

            //}
        }
        /// <summary>
        /// 上传足球数据
        /// </summary>
        /// <param name="str"></param>
        public void saveDataLotteryF(string str)
        {
            string str1 = string.Empty; string str2 = string.Empty; string str3 = string.Empty;
            string str4 = string.Empty; string str5 = string.Empty; string str6 = string.Empty;
            string str7 = string.Empty;
            string[] GetAllData = str.Split(',');
            List<string> dd = new List<string>();
            List<string> money = new List<string>();
            for (int i = 0; i < typess.Count; i++)
            {
                dd.Clear(); money.Clear();
                MatchCollection resg = Regex.Matches(GetAllData[i], @"((\d+\-\d+|\d+:\d+)|\d+)#(\d+.\d+)");
                foreach (Match item in resg)
                {
                    if (item.Success)
                    {
                        dd.Add(item.Groups[1].Value);
                        money.Add(item.Groups[3].Value);
                    }
                }
                for (int j = 0; j < dd.Count; j++)
                {
                    str1 = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    str2 = date.Substring(2) + weekendArray[i];
                    str3 = typess[i];// item.Groups[1].Value;
                    str4 = dd[j];
                    str5 = money[j];
                    str6 = weekendArray[i];
                    str7 = date.Substring(2);
                    //Thread.Sleep(new Random().Next(10000, 20000));
                    string sv = str1 + "&" + str2 + "&" + str3 + "&" + str4 + "&" + str5 + "&" + str6 + "&" + str7;
                    if (myWebServer.SetDataMethod(sv) > 0)
                    {
                        countSCV++;
                        label_CountTitck.Text = countSCV.ToString();
                    }
                }
            }
            //if (bhn > 0)
            //{
            //    int k = 
            //    if (k <= 0)
            //    {
            //        MessageBox.Show("打印失败!!!");
            //        timer_Lottery.Stop();
            //    }
            //}
        }
        #endregion

        #region 处理足球和篮球
        /// <summary>
        /// 篮球
        /// </summary>
        /// <param name="strConvert"></param>
        public string IsBasketball(string strConvert)
        {
            string srv = string.Empty;
            if (strPlayFootball != "9105")
            {
                for (int i = 0; i < victoryOrDefeatArray.Count; i++)
                {
                    srv += IsResult(victoryOrDefeatArray[i].ToString()) + ",";
                }
            }
            else if (strPlayFootball == "9105")
            {
                srv = IsResult("");
            }
            return srv.TrimEnd(',');
        }
        List<string> typess = new List<string>();
        /// <summary>
        /// 足球
        /// </summary>
        /// <param name="strConvert"></param>
        public string IsFootabll(string strConvert)
        {
            string srv = string.Empty;
            if (strPlayFootball != "9005")
            {
                for (int i = 0; i < victoryOrDefeatArray.Count; i++)//hostAndVisitorArray
                {
                    srv += IsResult(victoryOrDefeatArray[i].ToString()) + ",";
                }

            }
            else if (strPlayFootball == "9005")
            {
                srv = IsResult("").TrimStart(',');
            }
            return srv.TrimEnd(',');
        }
        #endregion

        #region button事件

        private void button1_Click(object sender, EventArgs e)
        {
            string[] allNowPort = SerialPort.GetPortNames();
            if (allNowPort.Length > 0)
            {
                Setting setting = setting = new Setting(port3);
                setting.ShowDialog();
            }
            else
            {
                Setting setting = setting = new Setting();
                setting.ShowDialog();
            }
            //if (setting == null || setting.IsDisposed)
            //{
            //    Thread th = new Thread(ThreadShouw);
            //    th.SetApartmentState(ApartmentState.STA);
            //    th.IsBackground = true;
            //    th.Start();
            //}
        }
        public void ThreadShouw()
        {

        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }
        List<string> sportsOption = null;
        List<string> welfareOption = null;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                sportsOption = new List<string>();
                sportsOption.Add("足彩-进球彩");
                sportsOption.Add("足彩-胜负彩(含任九场)");
                sportsOption.Add("足彩-世界杯");
                sportsOption.Add("乐透彩");
                sportsOption.Add("数字彩");
                comboBox3.DataSource = sportsOption;

            }
            if (comboBox2.SelectedIndex == 1)
            {
                welfareOption = new List<string>();
                welfareOption.Add("双色球(6/33+1/16)");
                welfareOption.Add("乐透彩");
                welfareOption.Add("数字彩");
                comboBox3.DataSource = welfareOption;
            }
        }
        #endregion

        #region Load事件
        private void MainUI_Load(object sender, EventArgs e)
        {
            toolStripPort3.ToolTipText = "Prot3-关";
            toolStripPort4.ToolTipText = "Prot4-关";
            UserMouseControl userMouseControl = new UserMouseControl();
            this.Cursor = userMouseControl.MouseImage(@"Image\ani\Link_Vnd.ani");
            StartGO();
        }

        private void IsProtOpen()
        {
            string[] allNowPort = SerialPort.GetPortNames();
            if (allNowPort.Length > 0 && allNowPort != null)
            {

                for (int i = 0; i < allNowPort.Length; i++)
                {
                    if (allNowPort[i].Equals("COM3"))
                    {
                        toolStripPort3.Image = Image.FromFile(@"Image\OtherImage\lightbulb.png"); toolStripPort3.ToolTipText = "Prot3-开";
                        if (!port3.IsOpen)
                        {
                            port3.Open();
                        }

                    }
                    if (allNowPort[i].Equals("COM4"))
                    {
                        toolStripPort4.Image = Image.FromFile(@"Image\OtherImage\aas.png"); toolStripPort4.ToolTipText = "Prot4-开";
                        if (!port.IsOpen)
                        {
                            port.Open();
                        }
                    }
                }
            }
        }
        public void StartGO()
        {
            IsProtOpen();
            DownData();
        }
        #endregion

        #region button事件
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            MessageBox.Show(openFileDialog1.FileName);
        }

        private void CloseBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MinimizeBox_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
        }

        private void ShowHelp_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }
        SerialPort port3 = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        /// <summary>
        /// 注册表读取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetRegistData(string name)
        {
            string registData;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            RegistryKey aimdir = software.OpenSubKey("Lottry", true);
            registData = aimdir.GetValue(name).ToString();
            return registData;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists("XieYi.txt"))
            {
                if (!timer_ShouDong.Enabled)
                {
                    string[] allNowPort = SerialPort.GetPortNames();
                    if (allNowPort.Length > 0)
                    {
                        timer_port.Start();
                        timer_Lottery.Start();
                    }
                }
                else
                {
                    MessageBox.Show("请关闭手动打票在运行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                XieYi xieYi = new XieYi();
                xieYi.ShowDialog();
            }

        }
        public void ChuPiao()
        {
            ILotteryHandling iLotteryHandling = null;
            string[] strArray = null;
            string strNumber = string.Empty;
            string strID = string.Empty;
            string strBeiShu = string.Empty;
            strNumber = dataGridView1.Rows[0].Cells[7].Value.ToString();
            strID = dataGridView1.Rows[0].Cells[9].Value.ToString().Trim();
            strBeiShu = dataGridView1.Rows[0].Cells[6].Value.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(strID))
            {
                if (strID.Contains("900") || strID.Contains("910"))//足球,篮球
                {
                    strArray = strNumber.Split(',');
                    if (strID.Contains("900"))
                    {
                        iLotteryHandling = new JingCaiFootball(port3);
                    }
                    else
                    {
                        iLotteryHandling = new JingCaiBasketball(port3);
                    }
                    iLotteryHandling.PrinterNumber("51");
                    iLotteryHandling.PanDuanStrID(strID);
                    iLotteryHandling.ChuLi(strID, strArray);
                    iLotteryHandling.BeiShu(strBeiShu);
                    iLotteryHandling.GuoGuan(strNumber);
                    iLotteryHandling.TeDingKey("ENTER");
                }
                else
                {
                    strArray = strNumber.Split(';');
                    if (strID.Contains("390"))//大乐透
                    {
                        iLotteryHandling = new DaLeTou(port3);
                        iLotteryHandling.PrinterNumber("07");
                    }
                    else if (strID.Contains("630"))//排三
                    {
                        iLotteryHandling = new PaiSan(port3);
                        iLotteryHandling.PrinterNumber("02");
                    }
                    else if (strID.Contains("640"))//排五
                    {
                        iLotteryHandling = new PaiWu(port3);
                        iLotteryHandling.PrinterNumber("09");
                    }
                    iLotteryHandling.ChuLi(strID, strArray);
                    iLotteryHandling.BeiShu(strBeiShu);
                    iLotteryHandling.TeDingKey("ENTER");
                }
            }
            Thread.Sleep(2000);
        }
        #endregion

        #region 主窗体结束的事件
        private void MainUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region 使dataGridView1初始时默认不选中任何行
        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                dataGridView1.Rows[0].Selected = false;
            }
        }
        #endregion

        #region dataGridView1的dataGridView1_CellContentClick事件
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        //bool bbcd = true;
        int countSCV = 0;
        #region 使用Timer控件对打印成功的彩票解析并且保存到数据库
        private void timer_Lottery_Tick(object sender, EventArgs e)
        {
            //List<string> ret_number = new List<string>();
            ServiceReference1.LotterData sss = myWebServer.GetDataMethod("all");
            if (sss == null)
            {
                return;
            }
            MyLotteryData mydata = new MyLotteryData();
            List<MyLotteryData> mydataList = new List<MyLotteryData>();
            mydata.Identifiers = sss.Identifiers;
            mydata.HandleResult = sss.HandleResult == -8 ? "已投注" : "未投注";
            mydata.PrintResult = sss.PrintResult;
            mydata.Id = sss.ID;
            mydata.ZhuShu = sss.ZhuShu;
            mydata.Money = sss.Money;
            mydata.Multiple = sss.Multiple;
            mydata.Number = sss.Number;
            mydata.LotteryId = sss.LotteryId;
            mydata.PlayTypeID = sss.PlayTypeID;
            mydataList.Add(mydata);
            dataGridView1.DataSource = mydataList;
            InsertACData(mydata);
            ChuPiao();//出票
            if (myWebServer.upDate(dataGridView1.Rows[0].Cells[3].Value.ToString()) > 0)
            {
                HuiGunJieMian();//返回主界面
            }
            else
            {
                return;
            }
            //RunLottery();//sp不存在则插入数据
        }
        /// <summary>
        /// 手动出票
        /// </summary>
        private void ShouDongTitck()
        {
            ServiceReference1.LotterData sss = myWebServer.GetDataMethod("all");
            if (sss == null)
            {
                return;
            }
            MyLotteryData mydata = new MyLotteryData();
            List<MyLotteryData> mydataList = new List<MyLotteryData>();
            List<string> listStr = new List<string>();
            mydata.HandleResult = sss.HandleResult == -8 ? "已投注" : "未投注";
            mydata.PrintResult = sss.PrintResult;
            mydata.Id = sss.ID;
            mydata.ZhuShu = sss.ZhuShu;
            mydata.Money = sss.Money;
            mydata.Multiple = sss.Multiple;
            mydata.Number = sss.Number;
            mydata.LotteryId = sss.LotteryId;
            mydata.PlayTypeID = sss.PlayTypeID;
            mydataList.Add(mydata);
            dataGridView1.DataSource = mydataList;
            string nojincai = mydata.PlayTypeID.ToString();
            myWebServer.upDate(mydata.Identifiers);
            listStr = sss.TouZhuSPValues.Split(',').ToList();
            if (listStr.Count > 0)
            {
                if ((nojincai.Contains("390") || nojincai.Contains("630")) || nojincai.Contains("640"))
                {
                    return;
                }
                else if (nojincai == "9005" || nojincai == "9105")
                {
                    if (nojincai == "9005")
                    {
                        //RSP>160326001=0(1.59)

                        for (int i = 0; i < listStr.Count; i++)
                        {
                            MatchCollection matchStr = Regex.Matches(@"(\d+){6}(\d+)=(.*)", listStr[i]);
                            foreach (Match item in matchStr)
                            {
                                if (item.Success)
                                {

                                }
                            }
                        }

                    }
                }
                else
                {

                    if (nojincai == "9001")
                    {
                        shoudongRegex(@"(\d+)\((\d+.\d+)\)", listStr, mydata);
                    }
                    else if (nojincai == "9002")
                    {
                        shoudongRegex(@"(\d+)\((\d+.\d+)\)", listStr, mydata);
                    }
                    else if (nojincai == "9003")
                    {
                        shoudongRegex(@"(\d+:\d+|\d+)\((\d+.\d+)\)", listStr, mydata);
                    }
                    else if (nojincai == "9004")
                    {
                        shoudongRegex(@"(\d+\-\d+)\((\d+.\d+)\)", listStr, mydata);
                    }
                    else if (nojincai == "9006")
                    {
                        shoudongRegex(@"(\d+)\((\d+.\d+)\)", listStr, mydata);
                    }
                    else if (nojincai == "9101")
                    {
                        shoudongRegex(@"(\d+)\((\d+.\d+)\)", listStr, mydata);
                    }
                    else if (nojincai == "9102")
                    {
                        shoudongRegex(@"(\d+)\((\d+.\d+)\)", listStr, mydata);
                    }
                    else if (nojincai == "9103")
                    {
                        shoudongRegex(@"(\d+)\((\d+.\d+)\)", listStr, mydata);
                    }
                    else//9004
                    {
                        shoudongRegex(@"(\d+)\((\d+.\d+)\)", listStr, mydata);
                    }
                }
            }
        }
        public void shoudongRegex(string regexStr, List<string> listStr, MyLotteryData mydata)
        {
            for (int i = 0; i < listStr.Count; i++)
            {
                MatchCollection matchStr = Regex.Matches(@"(\d+){6}(\d+)=(.*)", listStr[i]);
                foreach (Match item in matchStr)
                {
                    if (item.Success)
                    {
                        List<string> listStr2 = item.Groups[3].Value.Split('/').ToList();
                        for (int j = 0; j < listStr2.Count; j++)
                        {
                            MatchCollection matchStr2 = Regex.Matches(regexStr, listStr2[i]);
                            foreach (Match item2 in matchStr2)
                            {
                                if (item2.Success)
                                {
                                    string svc = mydata.Identifiers + "&" + item.Groups[1].Value + item.Groups[2].Value + "&" + pdPlayName(mydata.PlayTypeID) + "&" + item2.Groups[1].Value + "&" + item2.Groups[2].Value + "&" + item.Groups[2].Value + "&" + item.Groups[1].Value;
                                    myWebServer.SetDataMethod(svc);
                                }
                            }
                        }
                    }
                }
            }
        }

        private string pdPlayName(int p)
        {
            string ss = string.Empty;
            switch (p)
            {
                case 9001: ss = "RSP"; break;
                case 9002: ss = "JQS"; break;
                case 9003: ss = "CBF"; break;
                case 9004: ss = "BQC"; break;
                case 9006: ss = "SPF"; break;
                case 9101: ss = "RSF"; break;
                case 9102: ss = "SFC"; break;
                case 9103: ss = "SFD"; break;
                case 9104: ss = "DXF"; break;
                default: break;
            }
            return ss;
        }

        private void HuiGunJieMian()
        {
            DaLeTou lt = new DaLeTou(port3);
            Thread.Sleep(20);
            lt.TeDingKey("ESC");
            Thread.Sleep(20);
            lt.TeDingKey("ESC");
            Thread.Sleep(20);
            lt.TeDingKey("ESC");
        }
        #endregion

        #region 读取串口数据
        string receive_txt = ""; int txtDataLength = 0;
        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            txtDataLength = port.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，
            byte[] buf = new byte[txtDataLength];//声明一个临时数组存储当前来的串口数据  
            port.Read(buf, 0, txtDataLength);//读取缓冲数据
            for (int i = 0; i < buf.Length; i++)
            {
                receive_txt += buf[i].ToString("X2") + " ";//以16进制进行接收
            }
        }
        #endregion

        #region RunLottery
        lotteryAction lotteryMethod = new lotteryAction();
        string scc = string.Empty;
        public void RunLottery()
        {
            string strConvert = string.Empty;
            if (string.IsNullOrWhiteSpace(receive_txt) || receive_txt.Length < 300)
            {
                return;
            }
            strConvert = lotteryMethod.returnLottery_txt(receive_txt);
            weekendArray.Clear(); sumStr.Clear(); typess.Clear(); Is_F.Clear();
            allType.Clear(); allDataText.Clear(); sumStr.Clear(); myInt = 0; herInt = 0;
            receive_txt = string.Empty; string outStrConvert = string.Empty; fValue.Clear();
            lotteryType = lotteryMethod.IsLottery(strConvert); //截取福彩还是体彩
            strPlayFootball = lotteryMethod.GetPlayFootball(strConvert);//截取玩法
            if (string.IsNullOrWhiteSpace(strPlayFootball))
            {
                return;
            }
            if ((strPlayFootball == "大乐透" || strPlayFootball == "排三") || strPlayFootball == "排五")
            {
                myWebServer.upDate(dataGridView1.Rows[0].Cells[3].Value.ToString());
            }
            else
            {
                strPlay = lotteryMethod.GetPlayMetoh(strConvert);//截取过关方式
                numberNo = lotteryMethod.GetNumberNo(strConvert);//截取票号
                numberSafetyCode = lotteryMethod.GetNumberSafetyCode(strConvert);//截取安全码
                numberPassword = lotteryMethod.GetNumberPassword(strConvert);//截取密码
                outStrConvert = lotteryMethod.StrSubstring(strConvert);//截取有用的部分信息
                passArray = lotteryMethod.passArrayMetoh(outStrConvert);//截取第几场
                weekendArray = lotteryMethod.weekendMetoh(outStrConvert);//截取周几?
                hostAndVisitorArray = lotteryMethod.hostAndVisitorMetoh(outStrConvert);//截取主客对
                victoryOrDefeatArray = lotteryMethod.victoryOrDefeatArrayMetoh(outStrConvert, strPlayFootball, ref Is_F, ref allDataText, ref allType);//截取胜负/比分/。。。
                multiple = lotteryMethod.GetMyMultiple(strConvert);//截取倍数
                sum = lotteryMethod.GetMySum(strConvert);//截取合计
                date = lotteryMethod.GetMyDate(strConvert); //截取日期
                time = lotteryMethod.GetMyTime(strConvert);//截取时间
                if (strPlayFootball.Contains("900"))
                {
                    saveDataLotteryF(IsFootabll(strConvert));
                }
                else if (strPlayFootball.Contains("910"))
                {
                    fValue = lotteryMethod.FValue(outStrConvert);
                    Is_F = lotteryMethod.Is_F(outStrConvert);
                    saveDataLotteryB(IsBasketball(strConvert));
                }
                receive_txt = string.Empty;
            }
        }
        #endregion
        public bool DownData()
        {
            string touzhu = string.Empty;
            ServiceReference1.LotterData sss = myWebServer.GetDataMethod("all");
            if (sss == null)
            {
                return false;
            }
            MyLotteryData mydata = new MyLotteryData();
            List<MyLotteryData> mydataList = new List<MyLotteryData>();
            mydata.Identifiers = sss.Identifiers;
            mydata.HandleResult = sss.HandleResult == -8 ? "已投注" : "未投注";
            mydata.PrintResult = sss.PrintResult;
            mydata.Id = sss.ID;
            mydata.ZhuShu = sss.ZhuShu;
            mydata.Money = sss.Money;
            mydata.Multiple = sss.Multiple;
            mydata.Number = sss.Number;
            mydata.LotteryId = sss.LotteryId;
            mydata.PlayTypeID = sss.PlayTypeID;
            mydataList.Add(mydata);
            dataGridView1.DataSource = mydataList;
            return true;
        }

        //public List<string> DownData_ShouGong()
        //{


        //    //string touzhuString = new SubstringWbeFootball().SelectUrl(sss.PlayTypeID.ToString(), sss.TouZhuSPValues);
        //    //if (!sss.TouZhuSPValues.Equals(touzhuString))
        //    //{
        //    //    ret_number.Add(touzhuString);
        //    //}
        //    //else
        //    //{

        //    //}
        //}
        private void button5_Click(object sender, EventArgs e)
        {
            this.timer_Lottery.Stop();
        }

        private void timer_port_Tick(object sender, EventArgs e)
        {
            port.DataReceived += port_DataReceived;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            DownData();//下载数据
            ChuPiao();//出票
            RunLottery();//上传+更新数据
            HuiGunJieMian();//返回主界面
        }
        private void timer_ShouDong_Tick(object sender, EventArgs e)
        {
            /*分析需求
             *1.下载当前彩票的数据:
             * 
             *  DAL.OLEDBHelp.GetExecute(string.Format("INSERT INTO TouZhuTitck ([BuyTitckID],[Multiple],[PlayType],[TouZhu],[CreateTime],[TouZhuValue]) values ('{0}','{1}','{2}','{3}','{4}','{5}')", thisData.Identifiers, thisData.Multiple.ToString(), thisData.PlayTypeID.ToString(), thisData.Number, thisData.Dtm.ToString(), thisData.TouZhuValue));
             * 
             * 
             */
            //toolStripLabel2.Visible = true;
            //if (!timer_Lottery.Enabled)
            //{
            //List<string> ret_number = new List<string>();
            ServiceReference1.LotterData sss = myWebServer.GetDataMethod("all");
            if (sss == null)
            {
                return;
            }
            if (myWebServer.upDate(sss.ID.ToString()) > 0)
            {
                ObtainData Accessdb = new ObtainData();
                MyLotteryData mydata = new MyLotteryData();
                mydata.Identifiers = sss.Identifiers;
                mydata.HandleResult = sss.HandleResult == -8 ? "已投注" : "未投注";
                mydata.PrintResult = sss.PrintResult;
                mydata.Id = sss.ID;
                mydata.ZhuShu = sss.ZhuShu;
                mydata.Money = sss.Money;
                mydata.Multiple = sss.Multiple;
                mydata.Number = sss.Number;
                mydata.LotteryId = sss.LotteryId;
                mydata.PlayTypeID = sss.PlayTypeID;
                mydata.Dtm = sss.MyDateTime;
                mydata.TouZhuValue = sss.TouZhuSPValues;
                Accessdb.InsertThisData(mydata);
                countSCV++;
                label_CountTitck.Text = countSCV.ToString();
            }
            //toolStripLabel2.Value = 100;
            //MyLotteryData mydata = new MyLotteryData();
            //List<MyLotteryData> mydataList = new List<MyLotteryData>();
            //mydata.Identifiers = sss.Identifiers;
            //mydata.HandleResult = sss.HandleResult == -8 ? "已投注" : "未投注";
            //mydata.PrintResult = sss.PrintResult;
            //mydata.Id = sss.ID;
            //mydata.ZhuShu = sss.ZhuShu;
            //mydata.Money = sss.Money;
            //mydata.Multiple = sss.Multiple;
            //mydata.Number = sss.Number;
            //mydata.LotteryId = sss.LotteryId;
            //mydata.PlayTypeID = sss.PlayTypeID;
            //mydata.Dtm = sss.MyDateTime;
            //mydata.TouZhuValue = sss.TouZhuSPValues;
            //ret_number.Add(mydata.Identifiers);
            //ret_number.Add(mydata.PlayTypeID.ToString());
            //ret_number.Add(sss.TouZhuSPValues);
            //mydataList.Add(mydata);
            //dataGridView1.DataSource = mydataList;
            //List<string> ret_List = new List<string>();
            //InsertACData(mydata);
            //if (ret_number != null)
            //{
            //    if (ret_number[1].Contains("900"))
            //    {
            //        //new SubstringWbeFootball().SelectUrl();
            //        SD_Football subWeb = new SD_Football();
            //        ret_List = subWeb.SelectUrl(ret_number[0], ret_number[1], ret_number[2], "Yes");
            //    }
            //    if (ret_number[1].Contains("910"))
            //    {
            //        SD_Basketball subWeb = new SD_Basketball();
            //        ret_List = subWeb.SelectUrl(ret_number[0], ret_number[1], ret_number[2]);
            //    }
            //}
            //if (ret_List != null)
            //{
            //    if (myWebServer.SelectIDDataMethod(ret_number[0]) <= 0)
            //    {
            //        for (int i = 0; i < ret_List.Count; i++)
            //        {
            //            myWebServer.SetDataMethod(ret_List[i].ToString());
            //        }
            //    }
            //if (myWebServer.upDate(dataGridView1.Rows[0].Cells[3].Value.ToString()) > 0)
            //{
            //    countSCV++;
            //    label_CountTitck.Text = countSCV.ToString();
            //}
            //}
            //}
        }

        private void InsertACData(MyLotteryData mydata)
        {
            ObtainData Ob = new ObtainData();
            if (mydata != null)
            {
                Ob.InsertThisData(mydata);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (timer_Lottery.Enabled)
            {
                if (MessageBox.Show("正在自动出票是否退出", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).ToString().Equals("Yes"))
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }

        }
        QueryTitck qut = null;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if (qut == null || qut.IsDisposed)
            //{
            //    qut = new QueryTitck();
            //    qut.Show();
            //}
            qut = new QueryTitck();//this.Cursor
            qut.ShowDialog();
        }

        private void contextSelect_Opening(object sender, CancelEventArgs e)
        {

        }

        private void MaxShou_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;

        }
        int each_btn = 0;
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (each_btn == 0)
            {
                toolStripButton5.Text = "...正在出票";
                toolStripButton5.Image = Image.FromFile(@"Image\Png\asdd.gif");
                toolStripButton5.ImageAlign = ContentAlignment.MiddleLeft;
                toolStripButton5.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                each_btn = 1;
                timer_ShouDong.Start();
            }
            else
            {
                toolStripButton5.Text = "启动手动出票";
                toolStripButton5.Image = Image.FromFile(@"Image\OtherImage\refresh.png");
                each_btn = 0;
                timer_ShouDong.Stop();
            }


        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CountAward ca = new CountAward();
            ca.ShowDialog();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            toolStrip1.BackColor = colorDialog1.Color;
        }
    }
}
