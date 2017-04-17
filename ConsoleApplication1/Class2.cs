using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Class2
    {
        //Dictionary<string, string> theSP = new Dictionary<string, string>();
        private List<string> ChaiFeng(List<string> strnumber)
        {
            List<string> str = new List<string>();
            for (int i = 0; i < strnumber.Count; i++)
            {
                MatchCollection matchStr = Regex.Matches(strnumber[i].Trim(), @"([A_Z]+\|)*(\d{6})(\d{3})=(.+?)(\|\d+\*\d+)*");
                foreach (Match item in matchStr)
                {
                    if (item.Success)
                    {
                        string ss = pushDate(item.Groups[2].Value) + item.Groups[3].Value + "," + GethDate(item.Groups[2].Value) + "," + item.Groups[4].Value + "," + item.Groups[2].Value + item.Groups[3].Value;
                        str.Add(ss);
                    }
                }
            }
            return str;
        }

        private string GethDate(string p)
        {
            string detDate = string.Empty;
            detDate = DateTime.Now.Year.ToString() + "-" + p.Substring(2, 2) + "-" + p.Substring(4, 2);
            return detDate;
        }

        private string pushDate(string ssc)
        {
            string strWeek = string.Empty;
            DateTime dtime = new DateTime(DateTime.Now.Year, int.Parse(ssc.Substring(2, 2)), int.Parse(ssc.Substring(4, 2)));
            switch (dtime.DayOfWeek.ToString())
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
        public string SubstringHTML_Div(string string_html, List<string> strnumber, string PlayID)
        {
            string dateStr = string.Empty;
            string str_html = string.Empty;
            string strList = string.Empty;
            strnumber = ChaiFeng(strnumber);
            for (int i = 0; i < strnumber.Count; i++)
            {
                string[] strArray = strnumber[i].Split(',');
                str_html = Regex.Match(string_html, string.Format("<tr class=\"\"  zid=\"\\d+\" mid=\"\\d+\" pname=\"{0}\" pdate=\"{1}\"(.+?)</tbody>",strArray[0].ToString() ,strArray[0].ToString())).Value;
                string ss = PlayGameSP(PlayID, strArray, str_html);
                strList += ss + ",";
            }
            return strList.TrimEnd(',');
        }

        private string PlayGameSP(string PlayID, string[] PlayType, string str_html)
        {
            //测试
            //Get_Test(str_html);
            //
            string ss = string.Empty;
            switch (PlayID)
            {
                case "9101": ss = Get_9101(PlayType, str_html); break;
                case "9202": ss = Get_9002(PlayType, str_html); break;
                case "9303": ss = Get_9003(PlayType, str_html); break;
                case "9404": ss = Get_9004(PlayType, str_html); break;
                //case "9005": ss = Get_9005(PlayType, str_html); break
                default: break;
            }
            return ss;
        }

        private void Get_Test(string str_html)
        {
            List<string> str = new List<string>();
            string playStr = Regex.Match(str_html, "<!--半全场-->(.+)</span><s class=\"arrow\"></s>").Value;
            MatchCollection matchs = Regex.Matches(playStr, "<span class=\"odds_item more_item\" data-sp=\"(.+)\" data-type=\"bqc\"");
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    string s = item.Groups[1].Value;
                    List<string> listStr = item.Groups[1].Value.Split(',').ToList();
                    //str.Add(ChainToNumber(item.Groups[1].Value) + "(" + item.Groups[2].Value + ")");
                }
            }
        }

        private string ChainToNumber(string p)
        {
            string strToNumber = string.Empty;
            switch (p)
            {
                case "胜": strToNumber = "3"; break;
                case "平": strToNumber = "1"; break;
                case "负": strToNumber = "0"; break;
                default: break;
            }
            return strToNumber;
        }

        private string Get_9101(string[] PlayType, string str_html)
        {
            string chuanGuab = string.Empty;
            string strInput = string.Empty;
            string strRet = string.Empty;
            List<string> rfList = new List<string>();

            Dictionary<string, string> dicStr = new Dictionary<string, string>();
            MatchCollection clg = Regex.Matches(str_html, "value=\"(\\d)\"(.+?)data-sp=\"(\\d+.\\d+)\"  data-rf=\"(.+?)\"/>");
            foreach (Match item in clg)
            {
                if (item.Success)
                {
                    string grop_1 = item.Groups[1].Value == "" ? "None" : item.Groups[1].Value;
                    string grop_3 = item.Groups[3].Value == "" ? "1.00" : item.Groups[3].Value;
                    string grop_4 = item.Groups[4].Value == "" ? "1.00" : item.Groups[4].Value;
                    dicStr.Add(grop_1, grop_3); rfList.Add("[" + grop_4 + "]");
                }
            }
            rfList.Reverse();
            if (PlayType[2].Contains("|"))
            {
                //chuanGuab = PlayType[2].Substring(PlayType[2].IndexOf("|") + 1);
                PlayType[2] = PlayType[2].Remove(PlayType[2].IndexOf("|"));
            }
            List<string> listStr = PlayType[2].Split('/').ToList();
            for (int i = 0; i < listStr.Count; i++)
            {
                if (dicStr.TryGetValue(listStr[i], out strInput))
                {
                    strRet += listStr[i] + "(" + strInput + ")" + rfList[i] + "/" + chuanGuab;
                }
            }
            return strRet.TrimEnd('/');
        }

        private string Get_9002(string[] PlayType, string str_html)
        {
            List<string> str = new List<string>();
            Dictionary<string, string> strDic = new Dictionary<string, string>();
            string strplay = PlayType[1].Replace("-", "").Substring(2) + PlayType[0].Substring(1);
            string playStr = Regex.Match(str_html, "<!--进球数-->(.+)<!--半全场-->").Value;
            MatchCollection matchs = Regex.Matches(playStr, "<span class=\"odds_item more_item\" data-sp=\"(.+)\" data-type=\"jqs\"");
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    //string s = item.Groups[1].Value;
                    List<string> listStr = item.Groups[1].Value.Split(',').ToList();
                    for (int i = 0; i < listStr.Count; i++)
                    {
                        MatchCollection matchs_2 = Regex.Matches(listStr[i], @"(\d)\|(\d+.\d+)");
                        foreach (Match item_2 in matchs_2)
                        {
                            if (item_2.Success)
                            {
                                strDic.Add(item_2.Groups[1].Value, "(" + item_2.Groups[2].Value + ")");
                                //str.Add(strplay + "=" + item_2.Groups[1].Value + "(" + item_2.Groups[2].Value + ")");
                            }
                        }
                    }
                }
            }
            string b = ZuHeString(strDic, PlayType);
            return b;
        }

        private string Get_9003(string[] PlayType, string str_html)
        {
            List<string> str = new List<string>();
            Dictionary<string, string> strDic = new Dictionary<string, string>();
            string strplay = PlayType[1].Replace("-", "").Substring(2) + PlayType[0].Substring(1);
            string playStr = Regex.Match(str_html, "<!--比分-->(.+)").Value;
            MatchCollection matchs = Regex.Matches(playStr, "<span class=\"odds_item more_item\" data-sp=\"(.+)\" data-type=\"bf\"");
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    string s = item.Groups[1].Value;
                    List<string> listStr = item.Groups[1].Value.Split(',').ToList();
                    for (int i = 0; i < listStr.Count; i++)
                    {
                        MatchCollection matchs_2 = Regex.Matches(listStr[i], @"(\d{1})(A|\d)\|(\d+.\d+)");
                        foreach (Match item_2 in matchs_2)
                        {
                            if (item_2.Success)
                            {
                                if (item_2.Groups[1].Value == "0" && item_2.Groups[2].Value == "A")
                                {
                                    strDic.Add("负其他", "(" + item_2.Groups[3].Value + ")");
                                }
                                else if (item_2.Groups[1].Value == "1" && item_2.Groups[2].Value == "A")
                                {
                                    strDic.Add("平其他", "(" + item_2.Groups[3].Value + ")");
                                }
                                else if (item_2.Groups[1].Value == "3" && item_2.Groups[2].Value == "A")
                                {
                                    strDic.Add("胜其他", "(" + item_2.Groups[3].Value + ")");
                                }
                                else
                                {
                                    strDic.Add(item_2.Groups[1].Value + ":" + item_2.Groups[2].Value, "(" + item_2.Groups[3].Value + ")");
                                }
                            }
                        }
                    }

                }
            }
            string b = ZuHeString(strDic, PlayType);
            return b;
        }

        private string Get_9004(string[] PlayType, string str_html)
        {
            List<string> str = new List<string>();
            Dictionary<string, string> strDic = new Dictionary<string, string>();
            string strplay = PlayType[1].Replace("-", "").Substring(2) + PlayType[0].Substring(1);
            string playStr = Regex.Match(str_html, "<!--半全场-->(.+)</span><s class=\"arrow\"></s>").Value;
            MatchCollection matchs = Regex.Matches(playStr, "<span class=\"odds_item more_item\" data-sp=\"(.+)\" data-type=\"bqc\"");
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    string s = item.Groups[1].Value;
                    List<string> listStr = item.Groups[1].Value.Split(',').ToList();
                    for (int i = 0; i < listStr.Count; i++)
                    {
                        MatchCollection matchs_2 = Regex.Matches(listStr[i], @"(\d{1})(\d{1})\|(\d+.\d+)");
                        foreach (Match item_2 in matchs_2)
                        {
                            if (item_2.Success)
                            {
                                strDic.Add(item_2.Groups[1].Value + "-" + item_2.Groups[2].Value, "(" + item_2.Groups[3].Value + ")");

                            }
                        }
                    }
                }
            }
            string b = ZuHeString(strDic, PlayType);
            return b;
        }

        private string Get_9005(string[] PlayType, string str_html)
        {
            string strHH = string.Empty;


            return strHH;
        }

        private string Get_9006(string[] PlayType, string str_html)
        {
            List<string> str = new List<string>();
            Dictionary<string, string> strDic = new Dictionary<string, string>();
            string strplay = PlayType[1].Replace("-", "").Substring(2) + PlayType[0].Substring(1);
            string playStr = Regex.Match(str_html, "<!--胜平负-->(.+)<!--让球胜平负-->").Value;
            MatchCollection matchs = Regex.Matches(playStr, "<span class=\"item_left\">(.)</span>(\\d+.\\d+)</span>");
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    strDic.Add(ChainToNumber(item.Groups[1].Value), "(" + item.Groups[2].Value + ")");
                }
            }
            string b = ZuHeString(strDic, PlayType);
            return b;
        }
        public string SelectUrl(string PlayID, string Number_Str)
        {
            string theStr = string.Empty;
            //theStr = "http://www.310win.com/buy/JingCaiHunhe.aspx";
            theStr = "http://trade.500.com/jclq/index.php?playid=313";//500万彩票网
            theStr = GetUrltoHtml(theStr, "GB2312", Number_Str, PlayID);
            return theStr;
        }
        public string GetUrltoHtml(string Url, string type, string numberStr, string PlayID)
        {
            try
            {
                string RetuList = string.Empty;
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance.

                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream)
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    string txt_HTML = reader.ReadToEnd().Replace("\n", "");
                    if (PlayID != "9105")
                    {
                        RetuList = SubstringHTML_Div(txt_HTML, numberStr.Split(',').ToList<string>(), PlayID);
                    }
                    else
                    {
                        List<string> splitNumber = numberStr.Split(',').ToList();
                        for (int i = 0; i < splitNumber.Count; i++)
                        {
                            MatchCollection matchs = Regex.Matches(splitNumber[i], @"(HH\|)*([A-Z]+)>(\d+)=(.+?)(\|\d+\*\d+)*");
                            foreach (Match item in matchs)
                            {
                                if (item.Success)
                                {
                                    string svc = item.Groups[4].Value;
                                    PlayID = SetPlayID(item.Groups[2].Value);
                                    RetuList += item.Groups[2].Value + ">" + SubstringHTML_Div(txt_HTML, splitNumber[i].Split(',').ToList(), PlayID) + ",";
                                }
                            }
                        }
                    }
                }
                return RetuList.TrimEnd(',');
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        private string SetPlayID(string p)
        {
            string playID = string.Empty;
            switch (p)
            {
                case "RSP": playID = "9001"; break;
                case "JQS": playID = "9002"; break;
                case "CBF": playID = "9003"; break;
                case "BQC": playID = "9004"; break;
                case "SPF": playID = "9006"; break;
                default: break;

            }
            return playID;
        }
        public string ZuHeString(Dictionary<string, string> strDic, string[] PlayType)
        {
            List<string> theFootball = new List<string>();
            theFootball = PlayType[2].Split('/').ToList();
            string outStr = string.Empty; string retStr = string.Empty;
            for (int i = 0; i < theFootball.Count; i++)
            {
                if (strDic.TryGetValue(theFootball[i], out outStr))
                {
                    retStr += theFootball[i] + outStr + "/";
                }
            }
            retStr = PlayType[3] + "=" + retStr.TrimEnd('/');
            return retStr;
        }

    }
}
