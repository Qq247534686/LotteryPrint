using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Class1
    {
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
        private List<string> ChaiFeng(List<string> strnumber)
        {
            List<string> str = new List<string>();
            for (int i = 0; i < strnumber.Count; i++)
            {
                MatchCollection matchStr = Regex.Matches(strnumber[i].Trim(), @"([A_Z]+\|)*(\d{6})(\d{3})=(.+)(\|\d+\*\d+)*");
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
        private string SubstringHTML_Div(string string_html, List<string> strnumber, string PlayID)
        {
            string ss = string.Empty;
            string dateStr = string.Empty;
            string str_html = string.Empty;
            string strList = string.Empty;
            strnumber = ChaiFeng(strnumber);
            for (int i = 0; i < strnumber.Count; i++)
            {
                string[] strArray = strnumber[i].Split(',');//2301,2016-06-21,1,160621301
                MatchCollection clg = Regex.Matches(string_html, string.Format("<tr class=\"\"  zid=\"\\d+\" mid=\"\\d+\" pname=\"{0}\" pdate=\"{1}\"(.+?)</tbody>", strArray[0].ToString(), strArray[0].ToString()));
                foreach (Match item in clg)
                {
                    if (item.Success)
                    {
                        str_html = item.Groups[1].Value;
                        if (!string.IsNullOrWhiteSpace(str_html))
                        {
                            ss = PlayGameSP(PlayID, strArray, str_html);
                        }
                    }
                }
                strList += strArray[3] + "=" + ss + ",";
            }
            return strList.TrimEnd(',');
        }
        private string PlayGameSP(string PlayID, string[] PlayType, string str_html)
        {
            //测试
            //Get_Test(str_html, PlayType, PlayID);
            //
            string ss = string.Empty;
            switch (PlayID)
            {
                case "9101":ss = Get_9101(PlayType,str_html);
            break;
                case "9102": ss = ss = Get_9102(PlayType, str_html);
            break;
                case "9103": ss =Get_9103(PlayType, str_html); 
            break;
                case "9104": ss = Get_9104(PlayType, str_html);
            break;
                default: break;
            }
            return ss;
        }

        private void Get_Test(string str_html, string[] PlayType, string PlayID)
        {

            //return strRet;
        }

        private string Get_9104(string[] PlayType, string str_html)
        {
            string strInput = string.Empty;
            string strRet = string.Empty;
            List<string> lis = new List<string>();
            Dictionary<string, string> dicStr = new Dictionary<string, string>();
            MatchCollection clg = Regex.Matches(str_html, "data-sp=\"(\\d+.\\d+)\"/>(.+?)(\\d+.\\d+)</label>");
            foreach (Match item in clg)
            {
                if (item.Success)
                {
                    string grop_1 = item.Groups[1].Value == "" ? "1.00" : item.Groups[1].Value;
                    string grop_2 = item.Groups[1].Value == "" ? "None" : item.Groups[2].Value;
                    string grop_3 = item.Groups[3].Value == "" ? "1.00" : item.Groups[3].Value;
                    dicStr.Add(SVC(grop_2), grop_1); lis.Add("["+grop_3+"]");
                }
            }
            if (PlayType[2].Contains("|"))
            {
                //chuanGuab = PlayType[2].Substring(PlayType[2].IndexOf("|") + 1);
                PlayType[2] = PlayType[2].Remove(PlayType[2].IndexOf("|"));
            }
            List<string> listStr = PlayType[2].Split('/').ToList();
            for (int i = 0; i < listStr.Count; i++)
            {
                if (dicStr.TryGetValue(listStr[i], out strInput) )
                {
                    if (lis[i] != null)
                    {
                        strRet += listStr[i] + "(" + strInput + ")" +lis[i]+ "/";
                    }
                }
            }
            return strRet.TrimEnd('/');
        }

        private string SVC(string grop_2)
        {
            string sc=string.Empty;
            switch (grop_2)
            {
                case "大": sc = "1"; break;
                case "小": sc = "2"; break;
                default: break;
            }

            return sc;
        }

       

        private string Get_9103(string[] PlayType, string str_html)
        {
            string strRet = string.Empty;
            Dictionary<string, string> ZhuKe = new Dictionary<string, string>();
            string KeString = Regex.Match(str_html, "客胜：</td>(.+?)</tr>").Value;
            string ZhuString = Regex.Match(str_html, "主胜：</td>(.+?)</tr>").Value;
            MatchCollection mtcs_1 = Regex.Matches(KeString, "data-sp=\"(.+?)\">(.+?)分</label>");
            foreach (Match item in mtcs_1)
            {
                if (item.Success)
                {
                    ZhuKe.Add(GetRetStrValueKeZhu(item.Groups[2].Value, "ke"), item.Groups[1].Value);
                }
            }
            MatchCollection mtcs_2 = Regex.Matches(ZhuString, "data-sp=\"(.+?)\">(.+?)分</label>");
            foreach (Match item in mtcs_2)
            {
                if (item.Success)
                {
                    ZhuKe.Add(GetRetStrValueKeZhu(item.Groups[2].Value, "zhu"), item.Groups[1].Value);
                }
            }
            if (PlayType[2].Contains("|"))
            {
                //chuanGuab = PlayType[2].Substring(PlayType[2].IndexOf("|") + 1);
                PlayType[2] = PlayType[2].Remove(PlayType[2].IndexOf("|"));
            }
            string strInput=string.Empty;
            List<string> listStr = PlayType[2].Split('/').ToList();
            for (int i = 0; i < listStr.Count; i++)
            {
                if (ZhuKe.TryGetValue(listStr[i], out strInput))
                {
                    strRet += listStr[i]+"("+strInput +")"+ "/";
                }
            }
            return strRet.TrimEnd(',');
        }

        private string GetRetStrValueKeZhu(string p, string IsZhuKe)
        {
            string strRet = string.Empty;
            if (IsZhuKe == "ke")
            {
                switch (p)
                {
                    case "1-5": strRet = "11"; break;
                    case "6-10": strRet = "12"; break;
                    case "11-15": strRet = "13"; break;
                    case "16-20": strRet = "14"; break;
                    case "21-25": strRet = "15"; break;
                    case "26+": strRet = "16"; break;
                    default: break;
                }
            }
            else
            {
                switch (p)
                {
                    case "1-5": strRet = "01"; break;
                    case "6-10": strRet = "02"; break;
                    case "11-15": strRet = "03"; break;
                    case "16-20": strRet = "04"; break;
                    case "21-25": strRet = "05"; break;
                    case "26+": strRet = "06"; break;
                    default: break;
                }
            }
            return strRet;
        }

        private string Get_9102(string[] PlayType, string str_html)
        {
            //2301,2016-06-21,1,160621301
            string strInput = string.Empty;
            string strRet = string.Empty;
            Dictionary<string, string> dicStr = new Dictionary<string, string>();
            MatchCollection clg = Regex.Matches(str_html, "lost=\"(\\d+.\\d+)\" win=\"(\\d+.\\d+)\"");
            foreach (Match item in clg)
            {
                if (item.Success)
                {
                    string grop_1 = item.Groups[1].Value == "" ? "1.00" : item.Groups[1].Value;
                    string grop_2 = item.Groups[2].Value == "" ? "1.00" : item.Groups[2].Value;
                    dicStr.Add("2", grop_1); dicStr.Add("1", grop_2);
                }
            }
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
                    strRet += listStr[i] + "(" + strInput + ")" + "/";
                }
            }
            return strRet.TrimEnd('/');
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
        public string SelectUrl(string PlayID, string Number_Str)
        {
            string theStr = string.Empty;
            //theStr = "http://www.310win.com/buy/JingCaiHunhe.aspx";
            theStr = "http://trade.500.com/jclq/index.php?playid=313";//500万彩票网
            theStr = GetUrltoHtml(theStr, "GB2312", Number_Str, PlayID);
            return theStr;
        }
        private string GetUrltoHtml(string Url, string type, string numberStr, string PlayID)
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
                        string isc = string.Empty;
                        switch (PlayID)
                        {
                            case "9101": isc = "RSF"; break;
                            case "9102": isc = "SFC"; break;
                            case "9103": isc = "SFD"; break;
                            case "9104": isc = "DXF"; break;
                            default: break;
                            
                        }
                        RetuList =isc+"|"+ SubstringHTML_Div(txt_HTML, numberStr.Split(',').ToList<string>(), PlayID);
                    }
                    else
                    {
                        List<string> splitNumber = numberStr.Split(',').ToList();
                        for (int i = 0; i < splitNumber.Count; i++)
                        {
                            MatchCollection matchs = Regex.Matches(splitNumber[i], @"(HH\|)*([A-Z]+)>(\d+)=(.+)(\|\d+\*\d+)*");
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
                case "RSF": playID = "9101"; break;
                case "SFC": playID = "9102"; break;
                case "SFD": playID = "9103"; break;
                case "DXF": playID = "9104"; break;
                default: break;
            }
            return playID;
        }
    }
}
