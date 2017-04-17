using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lottery.Model;
using Lottery.BLL;
using System.IO.Ports;
using LotteryPrinter.ServiceReference1;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using System.Threading;
namespace LotteryPrinter
{
    public partial class Setting : Form
    {
        PrintingSetting pscn = null;
        SportsLotterySetting sls = null;
        WelfareLottery wl = null;
        ObtainData bll = new ObtainData();
        SerialPort port3 = null;
        public Setting(SerialPort port3)
        {
            InitializeComponent();
            this.port3 = port3;
        }
        public Setting()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //SerialPort port3 = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        private void Setting_Load(object sender, EventArgs e)
        {
            Thread th = new Thread(StartGo);
            th.Name = "TH_Setting";
            th.IsBackground = true;
            th.Start();
           

        }
        public void StartGo()
        {
            string[] allNowPort = SerialPort.GetPortNames();
            if (allNowPort.Length > 0)
            {
                com_PortName.DataSource = allNowPort;
            }
            GivePrintingSetting();
            GiveSportsLotterySetting();
            GiveWelfareLotteryTable();
        }
        /// <summary>
        /// 打印设置
        /// </summary>
        public void GivePrintingSetting()
        {
            if (bll.IsTypePrintingSetting() == true)
            {
                //载入数据打印设置
                pscn = bll.GetPrintingSetting(1);
                SetPrintingSetting(pscn);
            }
            else
            {
                //默认打印设置数据
                pscn = bll.GetPrintingSetting(0);
                SetPrintingSetting(pscn);
            }
        }
        /// <summary>
        /// 体彩设置
        /// </summary>
        public void GiveSportsLotterySetting()
        {
            if (bll.IsTypeSportsLotterySetting() == true)
            {
                //载入数据体彩设置
                sls = bll.GetSportsLotterySetting(1);
                SetSportsLotterySetting(sls);
            }
            else
            {
                //默认体彩设置数据
                sls = bll.GetSportsLotterySetting(0);
                SetSportsLotterySetting(sls);
            }
        }
        /// <summary>
        /// 福彩设置
        /// </summary>
        public void GiveWelfareLotteryTable()
        {
            if (bll.IsTypeWelfareLottery() == true)
            {
                //载入数据福彩设置
                wl = bll.GetWelfareLottery(1);
                SetWelfareLottery(wl);
            }
            else
            {
                //默认福彩设置数据
                wl = bll.GetWelfareLottery(0);
                SetWelfareLottery(wl);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //传入对象用于保存
            int i = bll.IsSavePrintingSetting(SavePrintingSettingObject());
            int j = bll.IsSaveSportsLotterySetting(SaveSportsLotterySettingObject());
            int k = bll.IsSaveWelfareLottery(SaveWelfareLotteryObject());
            if ((i + j + k) > 0)
            {
                MessageBox.Show("保存成功,请重启程序运行保存的信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 恢复打印设置
        /// </summary>
        /// <param name="prs"></param>
        public void SetPrintingSetting(PrintingSetting prs)
        {
            com_Printer.SelectedIndex = prs.Printer;
            com_PortName.SelectedIndex = prs.PortName;
            com_PrinterSettingP.SelectedIndex = prs.PrinterSettingP;
            com_PrinterPrompt.SelectedIndex = prs.PrinterPrompt;
            che_PromptType.Checked = prs.PromptType == 1 ? true : false;
            num_DelayF.Value = prs.DelayF;
            num_IssueWaitF.Value = prs.IssueWaitF;
            num_EveryDelayD.Value = prs.EveryDelay;
            num_IssueWaitD.Value = prs.IssueWaitD;
        }
        /// <summary>
        /// 恢复体彩设置
        /// </summary>
        /// <param name="prs"></param>
        public void SetSportsLotterySetting(SportsLotterySetting prs)
        {
            com_IssueT.SelectedIndex = prs.IssueT;
            com_FootballLottery.SelectedIndex = prs.FootballLottery;
            com_Number.SelectedIndex = prs.Number;
            com_Lotto.SelectedIndex = prs.Lotto;
            com_MultiplyPrintingOne.SelectedIndex = prs.MultiplyPrintingOne;
            com_MultiplyPrintingTow.SelectedIndex = prs.MultiplyPrintingTow;
            com_MultiplyPrintingThree.SelectedIndex = prs.MultiplyPrintingThree;
            che_RowOfThree.Checked = prs.RowOfThree == 1 ? true : false;
            che_Basketball.Checked = prs.Basketball == 1 ? true : false;
            che_Other.Checked = prs.Other == 1 ? true : false;
            che_OtherFootballLottery.Checked = prs.OtherFootballLottery == 1 ? true : false;
            che_PagePrint.Checked = prs.PagePrint == 1 ? true : false;
            com_PagePrintSetting.SelectedIndex = prs.PagePrintSetting;
            che_PagePrintDouble.Checked = prs.PagePrintDouble == 1 ? true : false;
            com_SwitchDouble.SelectedIndex = prs.SwitchDouble;
        }
        /// <summary>
        /// 恢复福彩设置
        /// </summary>
        /// <param name="prs"></param>
        public void SetWelfareLottery(WelfareLottery prs)
        {
            com_IssueF.SelectedIndex = prs.IssueF;
            che_Delay.Checked = prs.Delay == 1 ? true : false;
            num_CompoundKey.Value = prs.CompoundTime;
            com_Compound.SelectedIndex = prs.Compound;
            com_UnionLotto.SelectedIndex = prs.UnionLotto;
            che_BeforePage.Checked = prs.BeforePage == 1 ? true : false;
            com_BPageExportOne.SelectedIndex = prs.BPageExportOne;
            com_BPageExportTow.SelectedIndex = prs.BPageExportTow;
            com_BPageExportThree.SelectedIndex = prs.BPageExportThree;
            che_StakesPage.Checked = prs.StakesPage == 1 ? true : false;
            com_SEachnoteOne.SelectedIndex = prs.SEachnoteOne;
            com_SEachnoteTow.SelectedIndex = prs.SEachnoteTow;
            com_SEachnoteThree.SelectedIndex = prs.SEachnoteThree;
            che_TicketAfter.Checked = prs.TicketAfter == 1 ? true : false;
            com_TicketOne.SelectedIndex = prs.TicketOne;
            com_TicketTow.SelectedIndex = prs.TicketTow;
            com_TicketThree.SelectedIndex = prs.TicketThree;
            che_PrintingState.Checked = prs.PrintingState == 1 ? true : false;
            com_PrintingStateCom.SelectedIndex = prs.PrintingStateCom;
            che_Bet.Checked = prs.Bet == 1 ? true : false;
        }
        /// <summary>
        /// 将打印设置的信息存入对象
        /// </summary>
        /// <returns></returns>
        public PrintingSetting SavePrintingSettingObject()
        {
            pscn = new PrintingSetting();
            pscn.Printer = com_Printer.SelectedIndex;
            pscn.PortName = com_PortName.SelectedIndex;
            pscn.PrinterSettingP = com_PrinterSettingP.SelectedIndex;
            pscn.PrinterPrompt = com_PrinterPrompt.SelectedIndex;
            pscn.PromptType = che_PromptType.Checked == true ? 1 : 0;
            pscn.DelayF = Convert.ToInt32(num_DelayF.Value);
            pscn.IssueWaitF = Convert.ToInt32(num_IssueWaitF.Value);
            pscn.EveryDelay = Convert.ToInt32(num_EveryDelayD.Value);
            pscn.IssueWaitD = Convert.ToInt32(num_IssueWaitD.Value);
            return pscn;
        }
        /// <summary>
        /// 将体彩设置的信息存入对象
        /// </summary>
        /// <returns></returns>
        public SportsLotterySetting SaveSportsLotterySettingObject()
        {
            sls = new SportsLotterySetting();
            sls.IssueT = com_IssueT.SelectedIndex;
            sls.FootballLottery = com_FootballLottery.SelectedIndex;
            sls.Number = com_Number.SelectedIndex;
            sls.Lotto = com_Lotto.SelectedIndex;
            sls.MultiplyPrintingOne = com_MultiplyPrintingOne.SelectedIndex;
            sls.MultiplyPrintingTow = com_MultiplyPrintingTow.SelectedIndex;
            sls.MultiplyPrintingThree = com_MultiplyPrintingThree.SelectedIndex;
            sls.RowOfThree = che_RowOfThree.Checked == true ? 1 : 0;
            sls.Basketball = che_Basketball.Checked == true ? 1 : 0;
            sls.Other = che_Other.Checked == true ? 1 : 0;
            sls.OtherFootballLottery = che_OtherFootballLottery.Checked == true ? 1 : 0;
            sls.PagePrint = che_PagePrint.Checked == true ? 1 : 0;
            sls.PagePrintSetting = com_PagePrintSetting.SelectedIndex;
            sls.PagePrintDouble = che_PagePrintDouble.Checked == true ? 1 : 0;
            sls.SwitchDouble = com_SwitchDouble.SelectedIndex;
            return sls;
        }
        /// <summary>
        /// 将福彩设置的信息存入对象
        /// </summary>
        /// <returns></returns>
        public WelfareLottery SaveWelfareLotteryObject()
        {
            wl.IssueF = com_IssueF.SelectedIndex;
            wl.Delay = che_Delay.Checked == true ? 1 : 0;
            wl.CompoundTime = Convert.ToInt32(num_CompoundKey.Value);
            wl.Compound = com_Compound.SelectedIndex;
            wl.UnionLotto = com_UnionLotto.SelectedIndex;
            wl.BeforePage = che_BeforePage.Checked == true ? 1 : 0;
            wl.BPageExportOne = com_BPageExportOne.SelectedIndex;
            wl.BPageExportTow = com_BPageExportTow.SelectedIndex;
            wl.BPageExportThree = com_BPageExportThree.SelectedIndex;
            wl.StakesPage = che_StakesPage.Checked == true ? 1 : 0;
            wl.SEachnoteOne = com_SEachnoteOne.SelectedIndex;
            wl.SEachnoteTow = com_SEachnoteTow.SelectedIndex;
            wl.SEachnoteThree = com_SEachnoteThree.SelectedIndex;
            wl.TicketAfter = che_TicketAfter.Checked == true ? 1 : 0;
            wl.TicketOne = com_TicketOne.SelectedIndex;
            wl.TicketTow = com_TicketTow.SelectedIndex;
            wl.TicketThree = com_TicketThree.SelectedIndex;
            wl.PrintingState = che_PrintingState.Checked == true ? 1 : 0;
            wl.PrintingStateCom = com_PrintingStateCom.SelectedIndex;
            wl.Bet = che_Bet.Checked == true ? 1 : 0;
            return wl;
        }
        private void btn_One_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_One.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Tow_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_Tow.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Three_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_Three.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Four_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_Four.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_five_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_five.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Six_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_Six.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Seven_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_Seven.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Eight_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_Eight.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Nine_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_Nine.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Zero_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(btn_Zero.Text);
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Upper_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("U ARROW");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Down_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("D ARROW");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Left_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("L ARROW");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Right_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("R ARROW");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("DELETE");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Backspace_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("BKSP");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Printing_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("PRINT");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }


        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("ENTER");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }



        }

        private void btn_BlankSpaces_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("SPACE");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            //port3.Close();
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("ESC");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_F1_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F1");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_F2_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F2");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }
        }

        private void btn_F3_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F3");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_F4_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F4");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_F5_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F5");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_F6_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F6");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_F7_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F7");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("KP +");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Jian_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("KP -");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Cheng_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("KP *");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_Chu_Click(object sender, EventArgs e)
        {
            if (port3.IsOpen)
            {
                byte[] bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("KP //");
                port3.Write(bty, 0, bty.Length);
            }
            else
            {
                return;
            }

        }

        private void btn_huanYuan_Click(object sender, EventArgs e)
        {
            pscn = bll.GetPrintingSetting(0);
            SetPrintingSetting(pscn);
        }

    }
}
