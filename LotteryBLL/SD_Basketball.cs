using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lottery.BLL
{
    public class SD_Basketball
    {
        public SD_Basketball()
        {

        }
        public List<string> SelectUrl(string ident, string playID, string numberStr)
        {
            List<string> spValue = new List<string>();
            string theStr = string.Empty;
            switch (playID)
            {
                case "9101": spValue = Get_RSF(ident, numberStr); break;
                case "9102": spValue = Get_SFC(ident, numberStr); break;
                case "9103": spValue = Get_SFD(ident, numberStr); break;
                case "9104": spValue = Get_DXF(ident, numberStr); break;
                case "9105": spValue = Get_HH(ident, numberStr); break;
                default: break;
            }
            return spValue;
        }

        private List<string> Get_HH(string ident, string numberStr)
        {
            List<string> strRet = new List<string>();
            List<List<string>> spValue = new List<List<string>>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"([A-Z]+)>(.+)");
                foreach (Match item in strMatch)
                {
                    if (item.Success)
                    {
                        switch (item.Groups[1].Value)
                        {
                            case "RSF": spValue.Add(Get_RSF(ident, item.Groups[2].Value)); break;
                            case "SFC": spValue.Add(Get_SFC(ident, item.Groups[2].Value)); break;
                            case "SFD": spValue.Add(Get_SFD(ident, item.Groups[2].Value)); break;
                            case "DXF": spValue.Add(Get_DXF(ident, item.Groups[2].Value)); break;
                        }
                    }
                }
            }
            for (int i = 0; i < spValue.Count; i++)
            {
                for (int j = 0; j < spValue[i].Count; j++)
                {
                    strRet.Add(spValue[i][j]);
                }
            }
            return strRet;
        }

        private List<string> Get_DXF(string ident, string numberStr)
        {
            List<string> strRet = new List<string>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"(DXF\|)*((\d{6})(\d{3}))=(.+)");
                foreach (Match item in strMatch)
                {
                    if (item.Success)
                    {
                        MatchCollection strMatch_SP = Regex.Matches(item.Groups[5].Value, @"(\d+)\((\d+.\d+)\)");
                        foreach (Match item_sp in strMatch_SP)
                        {
                            if (item_sp.Success)
                            {
                                strRet.Add(ident + "&" + item.Groups[2].Value + "&" + "DXF" + "&" + item_sp.Groups[1].Value + "&" + item_sp.Groups[2].Value + "&" + item.Groups[4].Value + "&" + item.Groups[3].Value);
                            }
                        }

                    }
                }
            }
            return strRet;
        }

        private List<string> Get_SFD(string ident, string numberStr)
        {
            List<string> strRet = new List<string>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"(SFD\|)*((\d{6})(\d{3}))=(.+)");
                foreach (Match item in strMatch)
                {
                    if (item.Success)
                    {
                        MatchCollection strMatch_SP = Regex.Matches(item.Groups[5].Value, @"(\d+)\((\d+.\d+)\)");
                        foreach (Match item_sp in strMatch_SP)
                        {
                            if (item_sp.Success)
                            {
                                strRet.Add(ident + "&" + item.Groups[2].Value + "&" + "SFD" + "&" + item_sp.Groups[1].Value + "&" + item_sp.Groups[2].Value + "&" + item.Groups[4].Value + "&" + item.Groups[3].Value);
                            }
                        }

                    }
                }
            }
            return strRet;
        }

        private List<string> Get_SFC(string ident, string numberStr)
        {
            List<string> strRet = new List<string>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"(SFC\|)*((\d{6})(\d{3}))=(.+)");
                foreach (Match item in strMatch)
                {
                    if (item.Success)
                    {
                        MatchCollection strMatch_SP = Regex.Matches(item.Groups[5].Value, @"(\d+)\((\d+.\d+)\)");
                        foreach (Match item_sp in strMatch_SP)
                        {
                            if (item_sp.Success)
                            {
                                strRet.Add(ident + "&" + item.Groups[2].Value + "&" + "SFC" + "&" + item_sp.Groups[1].Value + "&" + item_sp.Groups[2].Value + "&" + item.Groups[4].Value + "&" + item.Groups[3].Value);
                            }
                        }

                    }
                }
            }
            return strRet;
        }

        private List<string> Get_RSF(string ident, string numberStr)
        {
            List<string> strRet = new List<string>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"(RSF\|)*((\d{6})(\d{3}))=(.+)");
                foreach (Match item in strMatch)
                {
                    if (item.Success)
                    {
                        MatchCollection strMatch_SP = Regex.Matches(item.Groups[5].Value, @"(\d+)\((\d+.\d+)\)");
                        foreach (Match item_sp in strMatch_SP)
                        {
                            if (item_sp.Success)
                            {
                                strRet.Add(ident + "&" + item.Groups[2].Value + "&" + "RSF" + "&" + item_sp.Groups[1].Value + "&" + item_sp.Groups[2].Value + "&" + item.Groups[4].Value + "&" + item.Groups[3].Value);
                            }
                        }

                    }
                }
            }
            return strRet;
        }
    }
}
