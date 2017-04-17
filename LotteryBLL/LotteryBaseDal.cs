using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Lottery.DAL;
using Lottery_IBLL;

namespace Lottery.BLL
{
    public class LotteryBaseDal : ILotteryHandling
    {

        public LotteryBaseDal()
        { 
        
        }
        public SerialPort port3
        {
            get;
            set;
        }
        public void PrinterNumber(string matchNumber)
        {
            byte[] bty = null;
            for (int i = 0; i < matchNumber.Length; i++)
            {
                Thread.Sleep(20);
                bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(matchNumber[i].ToString());
                port3.Write(bty, 0, bty.Length);
            }
        }
        public void TeDingKey(string TheKey)
        {
            byte[] btyEnt = null;
            btyEnt = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(TheKey);
            port3.Write(btyEnt, 0, btyEnt.Length);
        }


        public virtual void PanDuanStrID(string strID)
        {
            string str1 = string.Empty; string str2 = string.Empty;
            switch (strID)
            {
                //足球
                case "9001": str1 = "5"; str2 = "6"; break;
                case "9002": str1 = "5"; str2 = "3"; break;
                case "9003": str1 = "5"; str2 = "2"; break;
                case "9004": str1 = "5"; str2 = "4"; break;
                case "9005": str1 = "5"; str2 = "9"; break;
                case "9006": str1 = "5"; str2 = "1"; break;
                //篮球
                case "9101": str1 = "6"; str2 = "1"; break;
                case "9102": str1 = "6"; str2 = "2"; break;
                case "9103": str1 = "6"; str2 = "3"; break;
                case "9104": str1 = "6"; str2 = "4"; break;
                case "9105": str1 = "6"; str2 = "9"; break;
                ////大乐透
                //case "3901": str1 = "0"; str2 = "1"; break;
                //case "3902": str1 = "0"; str2 = "1"; break;
                //case "3903": str1 = "0"; str2 = "1"; break;
                //case "3904": str1 = "0"; str2 = "1"; break;
                //case "3907": str1 = "0"; str2 = "1"; break;
                //case "3908": str1 = "0"; str2 = "1"; break;
                ////排三
                //case "6301": str1 = "0"; str2 = "2"; break;
                //case "6302": str1 = "0"; str2 = "2"; break;
                //case "6303": str1 = "0"; str2 = "2"; break;
                //case "6304": str1 = "0"; str2 = "2"; break;
                //case "6305": str1 = "0"; str2 = "2"; break;
                ////排五
                //case "6401": str1 = "0"; str2 = "3"; break;
                //case "6402": str1 = "0"; str2 = "3"; break;
                default: break;
            }
            byte[] bty1 = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(str1);
            port3.Write(bty1, 0, bty1.Length);
            byte[] bty2 = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(str2);
            port3.Write(bty2, 0, bty2.Length);
        }
        public void BeiShu(string beiShu)
        {
            if (beiShu == "1")
            {
                return;
            }
            TeDingKey("F5");
            PrinterNumber(beiShu);
            TeDingKey("ENTER");
        }
        public virtual void ChuLi(string strID,string[] chuLi)
        {
            
        }
        public string ReWeek( string str)
        {
            DateTime ss = new DateTime(int.Parse(("20"+str.Substring(0, 2))), int.Parse(str.Substring(2, 2)), int.Parse(str.Substring(4, 2)));
            string strWeek = string.Empty;
            switch (ss.DayOfWeek.ToString())
            {
                case "Monday": strWeek = "1"; break;
                case "Tuesday": strWeek = "2"; break;
                case "Wednesday": strWeek = "3"; break;
                case "Thursday": strWeek = "4"; break;
                case "Friday": strWeek = "5"; break;
                case "Saturday": strWeek = "6"; break;
                case "Sunday": strWeek = "7"; break;
                default: break;
            }
            return strWeek;
        }
        public void GuoGuan(string guoGuan)
        {
            string strdis = string.Empty;
            Match mc = Regex.Match(guoGuan, @"\|(\d+\*\d+)");
            strdis=new retrieveData().transmissionDicsPlay(mc.Groups[1].Value);
            if (string.IsNullOrWhiteSpace(strdis))
            {
                return;
            }
            TeDingKey("F2");
            PrinterNumber(strdis);
        }
    }
}
