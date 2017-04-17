using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lottery.BLL
{
    public class JingCaiBasketball : LotteryBaseDal
    {
        public JingCaiBasketball(SerialPort port3)
        {
            this.port3 = port3;
        }
        public override void ChuLi(string strID, string[] chuLi)
        { 
         string RegexStr=string.Empty;
         switch (strID)
         {
             case "9101":
                 RegexStr = @"(\d+)(\d{3})=((\d)(/\d)*)";//
                 ExecuteNumber_RSF(chuLi, RegexStr,"No");
                 break;
             case "9102":
                 RegexStr = @"(\d+)(\d{3})=((\d+)(/\d+)*)";//
                 ExecuteNumber_SFC(chuLi, RegexStr, "No");
                 break;
             case "9103": 
                 RegexStr = @"(\d+)(\d{3})=((\d+)(/\d+)*)";//
                 ExecuteNumber_SFD(chuLi, RegexStr, "No");
                 break;
             case "9104":
                 RegexStr = @"(\d+)(\d{3})=((\d)(/\d)*)";//
                 ExecuteNumber_DXF(chuLi, RegexStr, "No");
                 break;
             case "9105":
                 RegexStr = @"(HH\|)*([A-Z]+)\>\d+(\d{3})=(.+)";//HH|RSP>151010301=1,SFC>151010302=02,DXF>151010303=2|3*1
                 ExecuteNumber_HHB(chuLi, RegexStr);
                 break;
             default: break;
         }
        }
       
        public void ExecuteNumber_RSF(string[] chuLi, string RegexStr,string isHHStr)
        {
            string[] strSplit = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                MatchCollection mc = Regex.Matches(chuLi[i], RegexStr);
                foreach (Match item in mc)
                {
                    if (item.Success)
                    {
                        PrinterNumber(this.ReWeek(item.Groups[1].Value) + item.Groups[2].Value);
                        if (isHHStr == "Yes")
                        {
                            TeDingKey("1");
                        }
                        if (item.Groups[3].Value.Contains("/"))
                        {
                            strSplit = item.Groups[3].Value.Split('/');
                            for (int j = 0; j < strSplit.Length; j++)
                            {
                                TeDingKey(strSplit[j]);
                            }
                        }
                        else
                        {
                            TeDingKey(item.Groups[3].Value);
                        }
                    }
                }
                TeDingKey("F1");
            }
        }
        public void ExecuteNumber_SFC(string[] chuLi, string RegexStr, string isHHStr)
        {
            string[] strSplit = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                MatchCollection mc = Regex.Matches(chuLi[i], RegexStr);
                foreach (Match item in mc)
                {
                    if (item.Success)
                    {
                        PrinterNumber(this.ReWeek(item.Groups[1].Value) + item.Groups[2].Value);
                        if (isHHStr == "Yes")
                        {
                            TeDingKey("2");
                        }
                        if (item.Groups[3].Value.Contains("/"))
                        {
                            strSplit = item.Groups[3].Value.Split('/');
                            for (int j = 0; j < strSplit.Length; j++)
                            {
                                PrinterNumber(strSplit[j]);
                            }
                        }
                        else
                        {
                            PrinterNumber(item.Groups[3].Value);
                        }
                    }
                }
                TeDingKey("F1");
            }
        }
        public void ExecuteNumber_SFD(string[] chuLi, string RegexStr, string isHHStr)
        {
            string[] strSplit = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                MatchCollection mc = Regex.Matches(chuLi[i], RegexStr);
                foreach (Match item in mc)
                {
                    if (item.Success)
                    {
                        PrinterNumber(this.ReWeek(item.Groups[1].Value) + item.Groups[2].Value);
                        if (isHHStr == "Yes")
                        {
                            TeDingKey("3");
                        }
                        if (item.Groups[3].Value.Contains("/"))
                        {
                            strSplit = item.Groups[3].Value.Split('/');
                            for (int j = 0; j < strSplit.Length; j++)
                            {
                                PrinterNumber(strSplit[j]);
                            }
                        }
                        else
                        {
                            PrinterNumber(item.Groups[3].Value);
                        }
                    }
                }
                TeDingKey("F1");
            }
        }
        public void ExecuteNumber_DXF(string[] chuLi, string RegexStr, string isHHStr)
        {
            string[] strSplit = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                MatchCollection mc = Regex.Matches(chuLi[i], RegexStr);
                foreach (Match item in mc)
                {
                    if (item.Success)
                    {
                        PrinterNumber(this.ReWeek(item.Groups[1].Value) + item.Groups[2].Value);
                        if (isHHStr == "Yes")
                        {
                            TeDingKey("4");
                        }
                        if (item.Groups[3].Value.Contains("/"))
                        {
                            strSplit = item.Groups[3].Value.Split('/');
                            for (int j = 0; j < strSplit.Length; j++)
                            {
                                TeDingKey(strSplit[j]);
                            }
                        }
                        else
                        {
                            TeDingKey(item.Groups[3].Value);
                        }
                    }
                }
                TeDingKey("F1");
            }
        }
        public void ExecuteNumber_HHB(string[] chuLi, string RegexStr)
        {
            string strSplit = string.Empty;
            string strTo = string.Empty;
            string[] strAll = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                MatchCollection mc = Regex.Matches(chuLi[i], RegexStr);
                foreach (Match item in mc)
                {
                    if (item.Success)
                    {
                        strAll = item.Groups[0].Value.Trim().Split(',');
                        strTo = item.Groups[2].Value;
                        switch (strTo)
                        {
                            case "RSF": ExecuteNumber_RSF(strAll, @"(\d+)(\d{3})=((\d)(/\d)*)", "Yes"); break;
                            case "SFC": ExecuteNumber_SFC(strAll, @"(\d+)(\d{3})=((\d+)(/\d+)*)", "Yes"); break;
                            case "SFD": ExecuteNumber_SFD(strAll, @"(\d+)(\d{3})=((\d+)(/\d+)*)", "Yes"); break;
                            case "DXF": ExecuteNumber_DXF(strAll, @"(\d+)(\d{3})=((\d)(/\d)*)", "Yes"); break;
                            default: break;
                        }
                    }
                }
            }
        }



    }
}
