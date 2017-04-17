using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lottery_IBLL;

namespace Lottery.BLL
{
    public class JingCaiFootball : LotteryBaseDal
    {
        public JingCaiFootball(SerialPort port3)
        {
            this.port3 = port3;
        }
        public override void ChuLi(string strID,string[] chuLi)
        {
            string RegexStr=string.Empty;
            switch (strID)
            {
                case "9001":
                    RegexStr=@"(\d+)(\d{3})=((\d)(/\d)*)";//RSP|151010001=3/1,151010002=0/1,151010003=0/1|3*1
                    ExecuteNumber_RSP(chuLi, RegexStr,"No");
                    break;
                case "9002":
                    RegexStr = @"(\d+)(\d{3})=((\d+)(/\d+)*)";//JQS|160324001=26/2/3,160324002=3|2*1
                    ExecuteNumber_JQS(chuLi, RegexStr, "No");
                    break;
                case "9003":
                    RegexStr = @"(\d+)(\d{3})=((\d+:\d+|[\u4e00-\u9fa5]+)(/([\u4e00-\u9fa5]+|\d+:\d+))*)";//CBF>160324002=2:0|2*1
                    ExecuteNumber_CBF(chuLi, RegexStr, "No"); 
                    break;
                case "9004":
                    RegexStr = @"(\d+)(\d{3})=((\d+\-\d+)(/\d+\-\d+)*)";//BQC|160302001=3-3,160302002=3-0|2*1
                    ExecuteNumber_BQC(chuLi, RegexStr, "No"); 
                    break;
                case "9005":
                    #region JDD格式混合正则表达式解析
                    //RegexStr = @"(HH\|)*([A-Z]+)*(\||\>)*\d+(\d{4})=\d_((((\d+:\d+|[\u4e00-\u9fa5]+)(/(\d+:\d+)|[\u4e00-\u9fa5]+)*)|((\d+\-\d+)(/\d+\-\d+)*))|(\d)(/\d)*)";
                    //HH|RSP>160305001=0,RSP>160305002=0,RSP>160305004=0,SPF>160305006=3
                    #endregion
                    RegexStr = @"(HH\|)*([A-Z]+)\>\d+(\d{3})=(.+)";
                    //HH|RSP>151205010=0,SPF>151205013=0,RSP>151205038=3,RSP>151205048=3,RSP>151205049=3,RSP>151205051=3,RSP>151205062=0,SPF>151205063=3|8*1
                    ExecuteNumber_HHF(chuLi, RegexStr);
                    break;
                case "9006":
                    RegexStr=@"(\d+)(\d{3})=((\d)(/\d)*)";//SPF|151205017=3/1/0,151205021=3,151205031=3,151205035=3|4*1
                    ExecuteNumber_SPF(chuLi, RegexStr, "No");
                    break;
                default: break;
            }
        }
        public void ExecuteNumber_HHF(string[] chuLi, string RegexStr)
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
                            #region JDD格式
                            case "JQS": ExecuteNumber_JQS(strAll, @"(\d+)(\d{3})=((\d+)(/\d+)*)", "Yes"); break;
                            case "CBF": ExecuteNumber_CBF(strAll, @"(\d+)(\d{3})=((\d+:\d+|[\u4e00-\u9fa5]+)(/([\u4e00-\u9fa5]+|\d+:\d+))*)", "Yes"); break;
                            case "BQC": ExecuteNumber_BQC(strAll, @"(\d+)(\d{3})=((\d+\-\d+)(/\d+\-\d+)*)", "Yes"); break;
                            case "RSP": ExecuteNumber_RSP(strAll, @"(\d+)(\d{3})=((\d)(/\d)*)", "Yes"); break;
                            case "SPF": ExecuteNumber_SPF(strAll, @"(\d+)(\d{3})=((\d)(/\d)*)", "Yes"); break;
                            default: break;
                            #endregion

                            #region 当前格式
                            //case "1": ExecuteNumber_RSP(strAll, @"\d+(\d{3})=((\d)(/\d)*)", "Yes"); break;
                            //case "2": ExecuteNumber_JQS(strAll, @"\d+(\d{3})=((\d+)(/\d+)*)", "Yes"); break;
                            //case "3": ExecuteNumber_CBF(strAll, @"\d+(\d{3})=((\d+:\d+|[\u4e00-\u9fa5]+)(/([\u4e00-\u9fa5]+|\d+:\d+))*)", "Yes"); break;
                            //case "4": ExecuteNumber_BQC(strAll, @"\d+(\d{3})=((\d+\-\d+)(/\d+\-\d+)*)", "Yes"); break;
                            //case "6": ExecuteNumber_SPF(strAll, @"\d+(\d{3})=((\d)(/\d)*)", "Yes"); break;
                            //default: break;
                            #endregion
                        }
                    }
                }
            }
        }
        public void ExecuteNumber_JQS(string[] chuLi, string RegexStr, string isHHStr)
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
        public void ExecuteNumber_CBF(string[] chuLi, string RegexStr, string isHHStr)
        {
            string[] strSplit = null; string strRes = string.Empty;
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
                                switch (strSplit[j])
                                {
                                    case "胜其他": strSplit[j] = "90"; break;
                                    case "负其他": strSplit[j] = "09"; break;
                                    case "平其他": strSplit[j] = "99"; break;
                                    default: break;
                                }
                                strRes = strSplit[j].Replace(":", "");
                                PrinterNumber(strRes);
                            }
                        }
                        else
                        {
                            string gropStr = item.Groups[3].Value;
                            switch (gropStr)
                            {
                                case "胜其他": gropStr = "90"; break;
                                case "负其他": gropStr = "09"; break;
                                case "平其他": gropStr = "99"; break;
                                default: break;
                            }
                            strRes = gropStr.Replace(":", "");
                            PrinterNumber(strRes);
                        }
                    }
                }
                TeDingKey("F1");
            }
        }
        public void ExecuteNumber_BQC(string[] chuLi, string RegexStr, string isHHStr)
        {
            string[] strSplit = null; string strRes = string.Empty;
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
                                strRes = strSplit[j].Replace("-", "");
                                PrinterNumber(strRes);
                            }
                        }
                        else
                        {
                            strRes = item.Groups[3].Value.Replace("-", "");
                            PrinterNumber(strRes);
                        }
                    }
                }
                TeDingKey("F1");
            }
        }
        public void ExecuteNumber_RSP(string[] chuLi, string RegexStr, string isHHStr)
        {
            string[] strSplit = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                MatchCollection mc = Regex.Matches(chuLi[i], RegexStr);
                foreach (Match item in mc)
                {
                    if (item.Success)
                    {
                        //SPF|151205017=3/1/0,
                        PrinterNumber(this.ReWeek(item.Groups[1].Value) + item.Groups[2].Value);
                        if (isHHStr == "Yes")
                        {
                            TeDingKey("6");
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
        public void ExecuteNumber_SPF(string[] chuLi, string RegexStr, string isHHStr)
        {
            string[] strSplit = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                MatchCollection mc = Regex.Matches(chuLi[i], RegexStr);
                foreach (Match item in mc)
                {
                    if (item.Success)
                    {
                        //SPF|151205017=3/1/0,
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
       
       
    }
}
