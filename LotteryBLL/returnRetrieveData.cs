using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lottery.DAL;

namespace Lottery.BLL
{
    public class returnRetrieveData
    {
        SerialPort port3 = null;
        public returnRetrieveData(SerialPort port3)
        {
            this.port3 = port3;
        }

        #region 进入选择界面
        public void printDataByte()
        {
            byte[] bty1 = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("5");
            byte[] bty2 = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("1");
            port3.Write(bty1, 0, bty1.Length); port3.Write(bty2, 0, bty2.Length);
        }
        public void printDataByte(int lotteryNumber)
        {
            string str1 = string.Empty; string str2 = string.Empty;
            switch (lotteryNumber)
            {
                case 9001: str1 = "5"; str2 = "6"; break;
                case 9002: str1 = "5"; str2 = "3"; break;
                case 9003: str1 = "5"; str2 = "2"; break;
                case 9004: str1 = "5"; str2 = "4"; break;
                case 9005: str1 = "5"; str2 = "9"; break;
                case 9006: str1 = "5"; str2 = "1"; break;
                //....
                case 9101: str1 = "6"; str2 = "1"; break;
                case 9102: str1 = "6"; str2 = "2"; break;
                case 9103: str1 = "6"; str2 = "3"; break;
                case 9104: str1 = "6"; str2 = "4"; break;
                case 9106: str1 = "6"; str2 = "5"; break;
                default: break;
            }
            byte[] bty1 = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(str1);
            byte[] bty2 = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(str2);
            port3.Write(bty1, 0, bty1.Length); port3.Write(bty2, 0, bty2.Length);
        }
        #endregion

        #region 处理赛事选择
        public void MatchNumber(string matchNumber)
        {
            byte[] bty = null;
            for (int i = 0; i < matchNumber.Length; i++)
            {
                bty =Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(matchNumber[i].ToString());
                port3.Write(bty, 0, bty.Length);
            }
            
        }
        #endregion

        #region 处理倍数选择
        public void MultiplesNumber(string multiplesNumber)
        {
            byte[] btyMultiples = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F5");
            port3.Write(btyMultiples, 0, btyMultiples.Length);
            byte[] bty = null;
            for (int i = 0; i < multiplesNumber.Length; i++)
            {
                bty = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(multiplesNumber[i].ToString());
                port3.Write(bty, 0, bty.Length);
            }
            SelectIsOk();
        }

        #endregion

        #region 处理过关选择
        public void Pass_A_Barrier(string pass_A_Barrier)
        {
            if (pass_A_Barrier != null && pass_A_Barrier != "")
            {
                byte[] btyMultiples = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F2");
                port3.Write(btyMultiples, 0, btyMultiples.Length);
                selectData selectDataPass = new selectData();
                string str_Barrier = selectDataPass.SelectStr(pass_A_Barrier);
                byte[] btyBarrier = null;
                for (int i = 0; i < str_Barrier.Length; i++)
                {
                    btyBarrier = SerialPortKeyDictionary.ReturnSerialPortKey(str_Barrier[i].ToString());
                    port3.Write(btyBarrier, 0, btyBarrier.Length);
                }
            }
        }
        #endregion

        #region 确认选择
        public void SelectIsOk()
        {
            byte[] btyEnt = null;
            btyEnt = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("ENTER");
            port3.Write(btyEnt, 0, btyEnt.Length);
        }
        #endregion

        #region 处理竞彩选择
        public void Guess(string guess,int playNumber)
        {
            byte[] bty = null;
            if ((playNumber == 9001 || playNumber == 9002) || (playNumber == 9006))
            {
                for (int i = 0; i < guess.Length; i++)
                {
                    bty = SerialPortKeyDictionary.ReturnSerialPortKey(guess[i].ToString());
                    port3.Write(bty, 0, bty.Length);
                }
            }
            else if (playNumber == 9003)
            {
                switch (guess)
                {
                    case "胜其他": guess = "90"; break;
                    case "平其他": guess = "99"; break;
                    case "负其他": guess = "09"; break;
                    default: guess = guess.Replace(":", ""); break;
                }
                for (int i = 0; i < guess.Length; i++)
                {
                    bty = SerialPortKeyDictionary.ReturnSerialPortKey(guess[i].ToString());
                    port3.Write(bty, 0, bty.Length);
                }
            }
            else if (playNumber == 9004)
            {
                guess = guess.Replace("-", "");
                for (int i = 0; i < guess.Length; i++)
                {
                    bty = SerialPortKeyDictionary.ReturnSerialPortKey(guess[i].ToString());
                    port3.Write(bty, 0, bty.Length);
                }
            }
            else if ((playNumber == 9101 || playNumber == 9102) || (playNumber == 9104 || playNumber == 9103))
            {
                for (int i = 0; i < guess.Length; i++)
                {
                    bty = SerialPortKeyDictionary.ReturnSerialPortKey(guess[i].ToString());
                    port3.Write(bty, 0, bty.Length);
                }
            }
            SelectIsOk();
            byte[] btyMultiples = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey("F1");
            port3.Write(btyMultiples, 0, btyMultiples.Length);

        }
        #endregion

        #region 分步解析Number
        public void ExecuteNumber(string[] str, int playNumber)
        {
            if ((playNumber == 9001 || playNumber == 9002) || playNumber == 9006)
            {
                #region 9001,9002,9006 普通格式
                string pass_A_Barrier = string.Empty;//201412162001=1_3/1/0|1*1
                //RSP|151214001=3/1,151214002=3/1,151214004=3/0,151214005=1/0,151214006=3/1|5*1
                string strRegex = @"\d+(\d{3})=(\d)(/(\d))*(\|(\d+\*\d+))*";
                for (int i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i], strRegex))
                    {
                        MatchCollection matchs = Regex.Matches(str[i], strRegex);
                        foreach (Match item in matchs)
                        {
                            if (item.Success)
                            {
                                string stc1 = item.Groups[1].Value;//编号
                                MatchNumber(NowWeek()+ stc1);
                                //string stc2 = item.Groups[2].Value;
                                string stc3 = item.Groups[2].Value;
                                Guess(stc3, playNumber);
                                if (item.Groups[4].Value != "" && item.Groups[4].Value != null)
                                {
                                    MatchNumber(NowWeek() + stc1); Guess(item.Groups[4].Value, playNumber);
                                }
                                pass_A_Barrier = item.Groups[6].Value;
                                if (pass_A_Barrier != "" && pass_A_Barrier != null)
                                {
                                    Pass_A_Barrier(pass_A_Barrier);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 9001,9002,9006 JDD格式
                //string pass_A_Barrier = string.Empty;
                ////201412165001=1_3|1*1 %% 201410163001=1_3,201410163001=1_1,201410163001=1_0|3*1
                //string strRegex = @"\d+(\d{4})=(\d)_(\d)(/(\d))*(\|(\d+\*\d+))*";
                //for (int i = 0; i < str.Length; i++)
                //{
                //    if (Regex.IsMatch(str[i], strRegex))
                //    {
                //        MatchCollection matchs = Regex.Matches(str[i], strRegex);
                //        foreach (Match item in matchs)
                //        {
                //            if (item.Success)
                //            {
                //                string stc1 = item.Groups[1].Value;//编号
                //                MatchNumber(stc1);
                //                string stc2 = item.Groups[2].Value;
                //                string stc3 = item.Groups[3].Value;
                //                Guess(stc3, playNumber);
                //                if (item.Groups[5].Value != "" && item.Groups[5].Value != null)
                //                {
                //                    MatchNumber(stc1); Guess(item.Groups[5].Value, playNumber);
                //                }
                //                pass_A_Barrier = item.Groups[7].Value;
                //                if (pass_A_Barrier != "" && pass_A_Barrier != null)
                //                {
                //                    Pass_A_Barrier(pass_A_Barrier);
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion
            }
            else if (playNumber == 9003)
            {
                #region 9003 普通格式
                string pass_A_Barrier = string.Empty;
                string strRegex = @"\d+(\d{3})=(\d+:\d+|[\u4e00-\u9fa5]+)(/(\d+:\d+|[\u4e00-\u9fa5]+))*(\|(\d+\*\d+))*";
                for (int i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i], strRegex))
                    {
                        MatchCollection matchs = Regex.Matches(str[i], strRegex);
                        foreach (Match item in matchs)
                        {
                            if (item.Success)
                            {
                                string stc1 = item.Groups[1].Value;//编号
                                MatchNumber(NowWeek() + stc1);
                                //string stc2 = item.Groups[2].Value;
                                string stc3 = item.Groups[2].Value;
                                Guess(stc3, playNumber);
                                if (item.Groups[4].Value != "" && item.Groups[4].Value != null)
                                {
                                    MatchNumber(NowWeek() + stc1); Guess(item.Groups[4].Value, playNumber);
                                }
                                pass_A_Barrier = item.Groups[6].Value;
                                if (pass_A_Barrier != "" && pass_A_Barrier != null)
                                {
                                    Pass_A_Barrier(pass_A_Barrier);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 9003 JDD
                //string pass_A_Barrier = string.Empty;
                //string strRegex = @"\d+(\d{4})=(\d)_(\d+:\d+|[\u4e00-\u9fa5]+)(/(\d+:\d+|[\u4e00-\u9fa5]+))*(\|(\d+\*\d+))*";
                //for (int i = 0; i < str.Length; i++)
                //{
                //    if (Regex.IsMatch(str[i], strRegex))
                //    {
                //        MatchCollection matchs = Regex.Matches(str[i], strRegex);
                //        foreach (Match item in matchs)
                //        {
                //            if (item.Success)
                //            {
                //                string stc1 = item.Groups[1].Value;//编号
                //                MatchNumber(stc1);
                //                string stc2 = item.Groups[2].Value;
                //                string stc3 = item.Groups[3].Value;
                //                Guess(stc3, playNumber);
                //                if (item.Groups[5].Value != "" && item.Groups[5].Value != null)
                //                {
                //                    MatchNumber(stc1); Guess(item.Groups[5].Value, playNumber);
                //                }
                //                pass_A_Barrier = item.Groups[7].Value;
                //                if (pass_A_Barrier != "" && pass_A_Barrier != null)
                //                {
                //                    Pass_A_Barrier(pass_A_Barrier);
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion
            }
            else if (playNumber == 9004)
            {
                #region 9004 普通格式
                string pass_A_Barrier = string.Empty;
                string strRegex = @"\d+(\d{3})=(\d+\-\d+)(/(\d+\-\d+))*(\|(\d+\*\d+))*";
                for (int i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i], strRegex))
                    {
                        MatchCollection matchs = Regex.Matches(str[i], strRegex);
                        foreach (Match item in matchs)
                        {
                            if (item.Success)
                            {
                                string stc1 = item.Groups[1].Value;//编号
                                MatchNumber(NowWeek() + stc1);
                                //string stc2 = item.Groups[2].Value;
                                string stc3 = item.Groups[2].Value;
                                Guess(stc3, playNumber);
                                if (item.Groups[4].Value != "" && item.Groups[4].Value != null)
                                {
                                    MatchNumber(NowWeek() + stc1); Guess(item.Groups[5].Value, playNumber);
                                }
                                pass_A_Barrier = item.Groups[6].Value;
                                if (pass_A_Barrier != "" && pass_A_Barrier != null)
                                {
                                    Pass_A_Barrier(pass_A_Barrier);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 9004 JDD
                //string pass_A_Barrier = string.Empty;
                //string strRegex = @"\d+(\d{4})=(\d)_(\d+\-\d+)(/(\d+\-\d+))*(\|(\d+\*\d+))*";
                //for (int i = 0; i < str.Length; i++)
                //{
                //    if (Regex.IsMatch(str[i], strRegex))
                //    {
                //        MatchCollection matchs = Regex.Matches(str[i], strRegex);
                //        foreach (Match item in matchs)
                //        {
                //            if (item.Success)
                //            {
                //                string stc1 = item.Groups[1].Value;//编号
                //                MatchNumber(stc1);
                //                string stc2 = item.Groups[2].Value;
                //                string stc3 = item.Groups[3].Value;
                //                Guess(stc3, playNumber);
                //                if (item.Groups[5].Value != "" && item.Groups[5].Value != null)
                //                {
                //                    MatchNumber(stc1); Guess(item.Groups[5].Value, playNumber);
                //                }
                //                pass_A_Barrier = item.Groups[7].Value;
                //                if (pass_A_Barrier != "" && pass_A_Barrier != null)
                //                {
                //                    Pass_A_Barrier(pass_A_Barrier);
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion
            }
            else if ((playNumber == 9101 || playNumber == 9102) || (playNumber == 9104 || playNumber == 9103))
            {
                #region 9101,9102,9104 普通格式
                string pass_A_Barrier = string.Empty;
                //201412162301=1_2/1,201412162302=1_2/1,201412162305=1_1,201412162306=1_2|4*11
                string strRegex = @"\d+(\d{2})=(\d+)(/(\d+))*(\|(\d+\*\d+))*";
                for (int i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i], strRegex))
                    {
                        MatchCollection matchs = Regex.Matches(str[i], strRegex);
                        foreach (Match item in matchs)
                        {
                            if (item.Success)
                            {
                                string stc1 = item.Groups[1].Value;//编号
                                MatchNumber(NowWeek() + stc1);
                                //string stc2 = item.Groups[2].Value;
                                string stc3 = item.Groups[2].Value;
                                Guess(stc3, playNumber);
                                if (item.Groups[4].Value != "" && item.Groups[4].Value != null)
                                {
                                    MatchNumber(NowWeek() + stc1); Guess(item.Groups[4].Value, playNumber);
                                }
                                pass_A_Barrier = item.Groups[6].Value;
                                if (pass_A_Barrier != "" && pass_A_Barrier != null)
                                {
                                    Pass_A_Barrier(pass_A_Barrier);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 9101,9102,9104 JDD
                // string pass_A_Barrier = string.Empty;
                ////201412162301=1_2/1,201412162302=1_2/1,201412162305=1_1,201412162306=1_2|4*11
                //string strRegex = @"\d+(\d{3})=(\d)_(\d+)(/(\d+))*(\|(\d+\*\d+))*";
                //for (int i = 0; i < str.Length; i++)
                //{
                //    if (Regex.IsMatch(str[i], strRegex))
                //    {
                //        MatchCollection matchs = Regex.Matches(str[i], strRegex);
                //        foreach (Match item in matchs)
                //        {
                //            if (item.Success)
                //            {
                //                string stc1 = item.Groups[1].Value;//编号
                //                MatchNumber(stc1);
                //                string stc2 = item.Groups[2].Value;
                //                string stc3 = item.Groups[3].Value;
                //                Guess(stc3, playNumber);
                //                if (item.Groups[5].Value != "" && item.Groups[5].Value != null)
                //                {
                //                    MatchNumber(stc1); Guess(item.Groups[5].Value, playNumber);
                //                }
                //                pass_A_Barrier = item.Groups[7].Value;
                //                if (pass_A_Barrier != "" && pass_A_Barrier != null)
                //                {
                //                    Pass_A_Barrier(pass_A_Barrier);
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion
            }
            //else if (playNumber == 9103)
            //{
            //    #region 9103
            //    string pass_A_Barrier = string.Empty;
            //    //201411053305=3_01,201411053306=3_02/03|2*1
            //    string strRegex = @"\d+(\d{3})=(\d)_(\d+)(/(\d+))*(\|(\d+\*\d+))*";
            //    for (int i = 0; i < str.Length; i++)
            //    {
            //        if (Regex.IsMatch(str[i], strRegex))
            //        {
            //            MatchCollection matchs = Regex.Matches(str[i], strRegex);
            //            foreach (Match item in matchs)
            //            {
            //                if (item.Success)
            //                {
            //                    string stc1 = item.Groups[1].Value;//编号
            //                    MatchNumber(stc1);
            //                    string stc2 = item.Groups[2].Value;
            //                    string stc3 = item.Groups[3].Value;
            //                    Guess(stc3, playNumber);
            //                    if (item.Groups[5].Value != "" && item.Groups[5].Value != null)
            //                    {
            //                        MatchNumber(stc1); Guess(item.Groups[5].Value, playNumber);
            //                    }
            //                    pass_A_Barrier = item.Groups[7].Value;
            //                    if (pass_A_Barrier != "" && pass_A_Barrier != null)
            //                    {
            //                        Pass_A_Barrier(pass_A_Barrier);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    #endregion
            //}
        }
        #endregion

        #region 混合足球玩法分类
        public int selectFootNumber(string strNumber)
        {
            int returnNumber = 0; string TestStr = string.Empty;
            //switch(strNumber)
            //{
            //    case "1": returnNumber = 9001; TestStr = "6"; break;
            //    case "2": returnNumber = 9002; TestStr = "3"; break;
            //    case "3": returnNumber = 9003; TestStr = "2"; break;
            //    case "4": returnNumber = 9004; TestStr = "4"; break;
            //    case "6": returnNumber = 9006; TestStr = "1"; break;
            //    default:break;
            //}
            //----------
            switch (strNumber)
            {
                case "RSP": returnNumber = 9001; TestStr = "6"; break;
                case "JQS": returnNumber = 9002; TestStr = "3"; break;
                case "CBF": returnNumber = 9003; TestStr = "2"; break;
                case "BQC": returnNumber = 9004; TestStr = "4"; break;
                case "SPF": returnNumber = 9006; TestStr = "1"; break;
                default: break;
            }
            byte[] btyNumber = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(TestStr);
            port3.Write(btyNumber, 0, btyNumber.Length);
            return returnNumber;
        }
        #endregion

        #region 混合篮球玩法分类
        public int selectBasktNumber(string strNumber)
        {
            int returnNumber = 0; string TestStr = string.Empty;
            //switch (strNumber)//JDD
            //{
            //    case "1": returnNumber = 9101; TestStr = "1"; break;
            //    case "2": returnNumber = 9102; TestStr = "2"; break;
            //    case "3": returnNumber = 9103; TestStr = "3"; break;
            //    case "4": returnNumber = 9104; TestStr = "4"; break;
            //    default: break;
            //}
            //--------------
            switch (strNumber)
            {
                case "RSP": returnNumber = 9101; TestStr = "1"; break;
                case "SFC": returnNumber = 9102; TestStr = "2"; break;
                case "SFD": returnNumber = 9103; TestStr = "3"; break;
                case "DXF": returnNumber = 9104; TestStr = "4"; break;
                default: break;
            }
            byte[] btyNumber = Lottery.BLL.ControlSerialPortKey.GetSerialPortKey(TestStr);
            port3.Write(btyNumber, 0, btyNumber.Length);
            return returnNumber;
        }
        #endregion

        #region 混合足球
        public void MixtureFootball(string str, string strRegex)
        {
            string pass_A_Barrier = string.Empty;
            MatchCollection matchs = Regex.Matches(str, strRegex);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    string stc1 = item.Groups[3].Value;//编号
                    MatchNumber(NowWeek() + stc1);
                    string stc2 = item.Groups[2].Value;
                    int strNumberFoot = selectFootNumber(stc2);
                    string stc3 = item.Groups[4].Value;
                    Guess(stc3, strNumberFoot);
                    if (item.Groups[6].Value != "" && item.Groups[6].Value != null)
                    {
                        MatchNumber(NowWeek() + stc1); Guess(item.Groups[6].Value, strNumberFoot);
                    }
                    pass_A_Barrier = item.Groups[8].Value;
                    if (pass_A_Barrier != "" && pass_A_Barrier != null)
                    {
                        Pass_A_Barrier(pass_A_Barrier);
                    }
                }
            }
        }
        #endregion

        #region 混合篮球
        public void MixtureBasketball(string str, string strRegex)
        {
            string pass_A_Barrier = string.Empty;
            MatchCollection matchs = Regex.Matches(str, strRegex);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    string stc1 = item.Groups[3].Value;//编号
                    MatchNumber(NowWeek() + stc1);
                    string stc2 = item.Groups[2].Value;
                    int strNumberFoot = selectBasktNumber(stc2);
                    string stc3 = item.Groups[4].Value;
                    Guess(stc3, strNumberFoot);
                    if (item.Groups[6].Value != "" && item.Groups[6].Value != null)
                    {
                        MatchNumber(NowWeek() + stc1); Guess(item.Groups[6].Value, strNumberFoot);
                    }
                    pass_A_Barrier = item.Groups[8].Value;
                    if (pass_A_Barrier != "" && pass_A_Barrier != null)
                    {
                        Pass_A_Barrier(pass_A_Barrier);
                    }
                }
            }
        }
        #endregion

        #region 处理解析Number
        public void OperationArray(int playNumber, string[] str)
        {
            if ((playNumber == 9001 || playNumber == 9002)|| playNumber == 9006)
            {
                //201412165001=1_3/1,201412165002=1_1|2*1
                ExecuteNumber(str, playNumber);
            }
            else if (playNumber == 9003)
            {
                //201410303001=3_胜其他,201410303002=3_平其他,201410303003=3_负其他|3*1
                //201410293001=3_3:0,201410293002=3_1:0,201410293003=3_0:0/1:0|3*1
                ExecuteNumber(str, playNumber);
            }
            else if (playNumber == 9004)
            {
                //201410163001=4_3-0,201410163002=4_1-0/0-0|2*1
                 ExecuteNumber(str, playNumber);
            }
            else if (playNumber == 9005)
            {
                #region 9005 普通格式
                string strRegexOneOrTwo = @"(HH\|)*([A-Z]+)\>\d+(\d{3})=(\d)(/(\d))*(\|(\d+\*\d+))*";
                string strRegexThree = @"(HH\|)*([A-Z]+)\>\d+(\d{3})=(\d+:\d+|[\u4e00-\u9fa5]+)(/(\d+:\d+|[\u4e00-\u9fa5]+))*(\|(\d+\*\d+))*";
                string strRegexFour = @"(HH\|)*([A-Z]+)\>\d+(\d{3})=(\d+\-\d+)(/(\d+\-\d+))*(\|(\d+\*\d+))*";
                for (int i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i], strRegexOneOrTwo))
                    {
                        MixtureFootball(str[i], strRegexOneOrTwo);

                    }
                    else if (Regex.IsMatch(str[i], strRegexThree))
                    {
                        MixtureFootball(str[i], strRegexThree);
                    }
                    else if (Regex.IsMatch(str[i], strRegexFour))
                    {
                        MixtureFootball(str[i], strRegexFour);
                    }
                }
                #endregion
                //201410113011=3_3:0,201410113012=4_1-0,201410113013=2_0|3*1
                #region 9005 JDD
                //string strRegexOneOrTwo = @"\d+(\d{4})=(\d)_(\d)(/(\d))*(\|(\d+\*\d+))*";
                //string strRegexThree = @"\d+(\d{4})=(\d)_(\d+:\d+|[\u4e00-\u9fa5]+)(/(\d+:\d+|[\u4e00-\u9fa5]+))*(\|(\d+\*\d+))*";
                //string strRegexFour = @"\d+(\d{4})=(\d)_(\d+\-\d+)(/(\d+\-\d+))*(\|(\d+\*\d+))*";
                //for (int i = 0; i < str.Length; i++)
                //{
                //    if (Regex.IsMatch(str[i], strRegexOneOrTwo))
                //    {
                //        MixtureFootball(str[i], strRegexOneOrTwo);
                        
                //    }
                //    else if (Regex.IsMatch(str[i], strRegexThree))
                //    {
                //        MixtureFootball(str[i], strRegexThree);
                //    }
                //    else if (Regex.IsMatch(str[i], strRegexFour))
                //    {
                //        MixtureFootball(str[i], strRegexFour);
                //    }
                //}
                #endregion
            }
            else if (playNumber == 9101)
            {
                ExecuteNumber(str, playNumber);
            }
            else if (playNumber == 9102)
            {
                ExecuteNumber(str, playNumber);
            }
            else if (playNumber == 9103)
            {
                ExecuteNumber(str, playNumber);
            }
            else if (playNumber == 9104)
            {
                ExecuteNumber(str, playNumber);
            }
            else if (playNumber == 9105)
            {
                #region 9105
                string strRegexOneOrTwo = @"(HH\|)*([A-Z]+)\>\d+(\d{2})=(\d+)(/(\d+))*(\|(\d+\*\d+))*";
                for (int i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i], strRegexOneOrTwo))
                    {
                        MixtureBasketball(str[i], strRegexOneOrTwo);

                    }
                }
                #endregion
            }
        }
        #endregion
        public string NowWeek()
        {
            string dayStr = string.Empty;
            switch (DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday": dayStr = "1"; break;
                case "Tuesday": dayStr = "2"; break;
                case "Wednesday": dayStr = "3"; break;
                case "Thursday": dayStr = "4"; break;
                case "Friday": dayStr = "5"; break;
                case "Saturday": dayStr = "6"; break;
                case "Sunday ": dayStr = "2"; break;
            }
            return dayStr;
        }
    }
}
