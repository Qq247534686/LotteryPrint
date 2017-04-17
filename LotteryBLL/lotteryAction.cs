using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lottery.DAL;

namespace Lottery.BLL
{
    public class lotteryAction
    {
        retrieveData returnDataInfo = new retrieveData();

        #region lotteryAction的构造方法
        /// <summary>
        /// lotteryAction的构造方法
        /// </summary>
        public lotteryAction()
        { 
        
        }
        #endregion

        public List<string> ContextPlay(string str)
        { 
             string strOut = string.Empty;
             strOut=returnDataInfo.transmissionDiscPlayText(str);
             return strOut.Split(',').ToList();
        }

        #region 返回文本信息
        /// <summary>
        /// 返回16进制转文本信息
        /// </summary>
        /// <param name="receive_txt">16进制彩票数据</param>
        /// <returns>10进制彩票数据</returns>
        public string returnLottery_txt(string receive_txt)
        {
            string txt_Information = string.Empty;
            if (receive_txt.Length > 0)
            {
                receive_txt=receive_txt.Replace(" 00","").TrimEnd(' ');
                string[] xx = receive_txt.Split(' '); 
                if (xx.Length > 0)
                {
                    byte[] vv = new byte[xx.Length];
                    for (int i = 0; i < vv.Length; i++)
                    {
                        vv[i] = Convert.ToByte(xx[i].ToString(), 16);
                    }
                    txt_Information=Encoding.GetEncoding("GB2312").GetString(vv, 0, vv.Length);
                }
            }
            return txt_Information;
        }
        #endregion

        #region 是福利还是体育彩票
        /// <summary>
        /// 是福利还是体育彩票
        /// </summary>
        /// <param name="strConvert">彩票信息String</param>
        /// <returns></returns>
        public string IsLottery(string strConvert)
        {
            string strReceive = string.Empty;
            if (strConvert.Contains("中国福利彩票"))
            {
                strReceive = "F";
            }
            else if (strConvert.Contains("中国体育彩票"))
            {
                strReceive = "T";
            }
            strReceive = returnDataInfo.transmissionDicsLotteryData(strReceive);
            return strReceive;
        }
        #endregion

        #region 竞彩对应编码
        /// <summary>
        /// 竞彩足球/篮球类型所对应编码
        /// </summary>
        /// <param name="strConvert"></param>
        /// <returns></returns>
        public string GetPlayFootball(string strConvert)
        {
            string strReturn = string.Empty; string InputStr = string.Empty;
            strReturn = PlayFoot(strConvert).TrimStart('<').TrimEnd('>');
            switch (strReturn.Trim())
            {
                //足球
                case "竞彩足球胜平负": InputStr = "JC_A"; break;
                case "竞彩足球比分": InputStr = "JC_B"; break;
                case "竞彩足球总进球数": InputStr = "JC_C"; break;
                case "竞彩足球半全场胜平负": InputStr = "JC_D"; break;
                case "竞彩足球混合过关": InputStr = "JC_E"; break;
                case "竞彩足球让球胜平负": InputStr = "JC_F"; break;
                //篮球
                case "竞彩篮球让分胜负": InputStr = "JC_G"; break;
                case "竞彩篮球胜负": InputStr = "JC_H"; break;
                case "竞彩篮球胜分差": InputStr = "JC_I"; break;
                case "竞彩篮球大小分": InputStr = "JC_J"; break;
                case "竞彩篮球混合过关": InputStr = "JC_K"; break;
                //竞彩
                case "超级大乐透": InputStr = "JC_L"; break;
                //排三
                case "排列3": InputStr = "JC_M"; break;
                //排五
                case "排列5": InputStr = "JC_N"; break;
                default: break;
            }
            strReturn=returnDataInfo.transmissionDicsPlayTypeData(InputStr);
            return strReturn;
        }
        /// <summary>
        /// 返回关键的竞彩类型玩法
        /// </summary>
        /// <param name="strConvert"></param>
        /// <returns></returns>
        public string PlayFoot(string strConvert)
        {
            string strReturn = string.Empty;
            string str1 = @"(竞彩足球[\u4e00-\u9fa5]+)";//足球
            string str2 = @"(竞彩篮球[\u4e00-\u9fa5]+)";//篮球
            string str3 = @"(\<[\u4e00-\u9fa5]+\d\>)";//大乐透,排三,排五
            if (Regex.IsMatch(strConvert, str1))
            {
                strReturn = Regex.Match(strConvert, str1).Value;
            }
            else if (Regex.IsMatch(strConvert, str2))
            {
                strReturn = Regex.Match(strConvert, str2).Value; ;
            }
            else if (Regex.IsMatch(strConvert, str3))
            {
                strReturn = Regex.Match(strConvert, str3).Value; ;
            }
            return strReturn;
        }
        #endregion

        #region 玩法对应串关
        /// <summary>
        /// 玩法对应串关
        /// </summary>
        /// <param name="strConvert"></param>
        /// <returns></returns>
        public string GetPlayMetoh(string strConvert)
        {
            string strReturn = "单场固定";
            string str = @"([1-9])x(([1-9][0-9][0-9])|([1-9][0-9])|([1-9]))";
            MatchCollection matches = Regex.Matches(strConvert, str);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    GroupCollection groups = match.Groups;
                    string strGroup = match.Groups[1].Value + "*" + match.Groups[2].Value;
                    strReturn = "|" + strGroup;
                }
            }
            return strReturn;
        }
        #endregion

        #region 截取票号
        /// <summary>
        /// 截取票号
        /// </summary>
        /// <param name="strConvert"></param>
        /// <returns></returns>
        public string GetNumberNo(string strConvert)
        {
            string strReturn = string.Empty;
            string str = @"([0-9]{15,})";
            MatchCollection matches = Regex.Matches(strConvert, str);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    strReturn = match.Groups[0].Value;
                }
            }
            return strReturn.Trim();
        }
        #endregion

        #region 截取安全码
        /// <summary>
        /// 截取安全码
        /// </summary>
        /// <param name="strConvert"></param>
        /// <returns></returns>
        public string GetNumberSafetyCode(string strConvert)
        {
            string strReturn = string.Empty;
            string str = @"( [0-9A-Z]{4,} )";
            MatchCollection matches = Regex.Matches(strConvert, str);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    strReturn = match.Groups[0].Value;
                }
            }
            return strReturn.Trim();
        }
        #endregion

        #region 截取密码
        /// <summary>
        /// 截取密码
        /// </summary>
        /// <param name="strConvert"></param>
        /// <returns></returns>
        public string GetNumberPassword(string strConvert)
        {
            string strReturn = string.Empty;
            string str = @" [0-9]{7,}";
            MatchCollection matches = Regex.Matches(strConvert, str);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    strReturn = match.Value;
                }
            }
            return strReturn.Trim();
        }
        #endregion

        #region 截取有用的部分信息
        /// <summary>
        /// 截取有用的部分信息
        /// </summary>
        /// <param name="strConvert"></param>
        /// <returns></returns>
        public string StrSubstring(string strConvert)
        {
            string strReturn = string.Empty;
            int fristPosstion = strConvert.IndexOf('─');
            int lastPosstion = strConvert.LastIndexOf('─');
            if (fristPosstion >= 0 && lastPosstion >= 0)
            {
                strReturn = strConvert.Substring(fristPosstion, (lastPosstion - fristPosstion)).Replace("─", "");
            }
            return strReturn;
        }
        #endregion

        #region 截取内容-第几场
        /// <summary>
        /// 截取第几场
        /// </summary>
        /// <param name="strpass"></param>
        /// <returns></returns>
        public List<string> passArrayMetoh(string strConvert)
        {
            List<string> passD = new List<string>();
            string str1 = @"[\u4e00-\u9fa5]([0-9]|[0-9][0-9])关";
            MatchCollection matchs1 = Regex.Matches(strConvert, str1);
            foreach (Match match in matchs1)
            {
                if (match.Success)
                {
                    passD.Add(match.Value);
                }
            }
            return passD;
        }
        #endregion

        #region 截取内容-周几00?
        /// <summary>
        /// 周几00?
        /// </summary>
        /// <param name="strpass"></param>
        /// <returns></returns>
        public List<string> weekendMetoh(string strpass)
        {
            List<string> passW = new List<string>();
            string str1 = @"周[\u4e00-\u9fa5]([0-9]+)";
            MatchCollection matchs1 = Regex.Matches(strpass, str1);
            foreach (Match match in matchs1)
            {
                if (match.Success)
                {
                    passW.Add(match.Groups[1].Value);
                }
            }
            return passW;
        }
        #endregion

        #region 截取内容-主客对
        /// <summary>
        /// 截取内容-主客对
        /// </summary>
        /// <param name="strpass"></param>
        /// <returns></returns>
        public List<string> hostAndVisitorMetoh(string strpass)
        {
            List<string> passZK = new List<string>();
            string str1 = @"(([\u4e00-\u9fa5]{2}):([\u4e00-\u9fa5]+) Vs ([\u4e00-\u9fa5]{2}):([\u4e00-\u9fa5]+))";
            MatchCollection matchs1 = Regex.Matches(strpass, str1);
            foreach (Match match in matchs1)
            {
                if (match.Success)
                {
                    passZK.Add(match.Groups[1].Value);
                }
            }
            return passZK;
        }
        #endregion

        #region 彩票匹配方法
        /// <summary>
        /// 彩票匹配方法
        /// </summary>
        /// <param name="strpass"></param>
        /// <returns></returns>
        public List<string> victoryOrDefeatArrayMetoh(string strpass, string strPlayFootball, ref List<string> Is_F, ref List<string> allDataText, ref List<string> allType)
        {

            string str1 = string.Empty; string stv = string.Empty;
            List<string> passAll = new List<string>();
            switch (strPlayFootball)
            {
                //足球
                case "9001": 
                    stv = @"(([\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])(\+[\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])*)";
                    //匹配让球数---------
                    //strRegex2 = @"[\u4e00-\u9fa5]-(\d+)";//F
                    break;
                case "9002": stv = @"((\((\d+|\d+\+?)\)@\d+.\d+[\u4e00-\u9fa5])(\+\((\d+|\d+\+?)\)@\d+.\d+[\u4e00-\u9fa5])*)";
                    break;
                case "9003": stv = @"((\(((\d+:\d+)|([\u4e00-\u9fa5]+))\))@\d+.\d+[\u4e00-\u9fa5](\+(\(((\d+:\d+)|([\u4e00-\u9fa5]+))\))@\d+.\d+[\u4e00-\u9fa5])*)";
                    break;
                case "9004": stv = @"(([\u4e00-\u9fa5][\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])(\+[\u4e00-\u9fa5][\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])*)";
                    break;
                case "9005": str1 = @"\d ([\u4e00-\u9fa5]+) ";
                    MatchCollection XXX = Regex.Matches(strpass, @"(((\([\u4e00-\u9fa5]+\)|\((\d+|\d+\+?)\))|\(\d+:\d+\)|[\u4e00-\u9fa5]+)@\d+.\d+[\u4e00-\u9fa5])(\+((\([\u4e00-\u9fa5]+\)|\((\d+|\d+\+?)\))|\(\d+:\d+\)|[\u4e00-\u9fa5]+)@\d+.\d+[\u4e00-\u9fa5])*");
                    foreach (Match item in XXX)
                    {
                        if (item.Success)
                        {
                            allDataText.Add(item.Groups[0].Value);
                        }
                    }
                    MatchCollection matchs1 = Regex.Matches(strpass, str1);
                    foreach (Match match in matchs1)
                    {
                        if (match.Success)
                        {
                            allType.Add(match.Groups[1].Value);//竞猜类型
                        }
                    }
                    break;
                case "9006":
                    stv = @"(([\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])(\+[\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])*)";
                    break;
                //篮球
                case "9101":
                    //strRegex2 = @"[\u4e00-\u9fa5]((\+|\-)(\d+(.\d+)*))";//F
                    stv = @"(([\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])(\+[\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])*)";
                    break;
                case "9102": stv = @"(([\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])(\+[\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])*)";
                    break;
                case "9103":
                    stv = @"((\(([\u4e00-\u9fa5](\d+\+|\d+\-\d+))\)@\d+.\d+[\u4e00-\u9fa5])(\+(\(([\u4e00-\u9fa5](\d+\+|\d+\-\d+))\)@\d+.\d+[\u4e00-\u9fa5]))*)";
                    break;
                case "9104":
                    //strRegex2 = @"总分:(\d+(.\d+)*)";//F
                    stv = @"(([\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])(\+[\u4e00-\u9fa5]@\d+.\d+[\u4e00-\u9fa5])*)"; 
                    break;
                case "9105":
                    //strRegex2 = @"(总分|让分):([\u4e00-\u9fa5]+\+)*(\d+(.\d+)*)";//F
                    str1 = @"\d ([\u4e00-\u9fa5]+) ";
                    MatchCollection LLL = Regex.Matches(strpass, @"((\(([\u4e00-\u9fa5](\d+\+|\d+\-\d+))\)|[\u4e00-\u9fa5]+)@\d+.\d+[\u4e00-\u9fa5](\+(\((([\u4e00-\u9fa5](\d+\+|\d+\-\d+))\))|[\u4e00-\u9fa5]+)@\d+.\d+[\u4e00-\u9fa5])*)");
                    foreach (Match item in LLL)
                    {
                        if (item.Success)
                        {
                            allDataText.Add(item.Groups[0].Value);
                        }
                    }
                    MatchCollection maxths = Regex.Matches(strpass, str1);
                    foreach (Match match in maxths)
                    {
                        if (match.Success)
                        {
                            allType.Add(match.Groups[1].Value);//竞猜类型
                        }
                    }
                    break;
                default: break;
            }
            if (strPlayFootball != "9005" && strPlayFootball != "9105")
            {
                passAll = GOList(strpass, stv);
            }
            return passAll;
        }
        /// <summary>
        /// 截取对应场数的内容集合
        /// </summary>
        /// <param name="strpass">进行匹配的字符串</param>
        /// <param name="stv">正则表达式</param>
        public List<string> GOList(string strpass, string stv)
        {
            List<string> passStr = new List<string>();
            MatchCollection XXX = Regex.Matches(strpass, stv);
            foreach (Match item in XXX)
            {
                if (item.Success)
                {
                    passStr.Add(item.Groups[0].Value);
                }
            }
            return passStr;
        }
        #endregion

        #region 截取倍数
        /// <summary>
        /// 截取倍数
        /// </summary>
        /// <param name="strMultiple"></param>
        /// <returns></returns>
        public string GetMyMultiple(string strMultiple)
        {
            string multiple = "";
            string strRegex = @"倍数:([0-9]+)";
            MatchCollection matches = Regex.Matches(strMultiple, strRegex);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    multiple = match.Groups[1].Value;
                }
            }
            return multiple;
        }
        #endregion

        #region 截取合计
        /// <summary>
        /// 截取合计
        /// </summary>
        /// <param name="strSum"></param>
        /// <returns></returns>
        public string GetMySum(string strSum)
        {
            string sum = "";
            string strRegex = @"合计:\s+([0-9]+)[\u4e00-\u9fa5]";
            MatchCollection matches = Regex.Matches(strSum, strRegex);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    sum = match.Groups[1].Value;
                }
            }
            return sum;
        }
        #endregion

        #region 截取日期
        /// <summary>
        /// 截取日期
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public string GetMyDate(string strDate)
        {
            string date = "";
            string strRegex = @"([0-9]+-[0-9]+-[0-9]+)";
            MatchCollection matches = Regex.Matches(strDate, strRegex);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    date = match.Groups[1].Value;
                }
            }
            return date.Replace("-", "");
        }
        #endregion

        #region 截取时间
        /// <summary>
        /// 截取时间
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        public string GetMyTime(string strTime)
        {
            string time = "";
            string strRegex = @"([0-9]+:[0-9]+:[0-9]+)";
            MatchCollection matches = Regex.Matches(strTime, strRegex);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    time = match.Groups[1].Value;
                }
            }
            return time;
        }
        #endregion

        public List<string> FValue(string strResult)
        {
            List<string> fValue = new List<string>();
            string strFValue = @"[\u4e00-\u9fa5]((\+|\-)(\d+(.\d+)*))";//F
            MatchCollection mct = Regex.Matches(strResult, strFValue);
            foreach (Match match in mct)
            {
                if (match.Success)
                {
                    fValue.Add("F[" + match.Groups[1].Value + "]");
                }
            }
            return fValue;
        }

        public List<string> Is_F(string outStrConvert)
        {
            List<string> Is_F = new List<string>();
            string strFValue = @"[\u4e00-\u9fa5]+总分:((\d+(.\d+)*))";//F
            MatchCollection mct = Regex.Matches(outStrConvert, strFValue);
            foreach (Match match in mct)
            {
                if (match.Success)
                {
                    Is_F.Add("F[" + match.Groups[1].Value + "]");
                }
            }
            return Is_F;
        }
    }
}
