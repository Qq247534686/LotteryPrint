using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lottery.BLL
{
    public class SD_Football
    {
        public SD_Football()
        {

        }
        public List<string> SelectUrl(string ident, string playID, string numberStr,string YesOrNo)
        {
            List<string> spValue = new List<string>();
            string theStr = string.Empty;
            switch (playID)
            {
                case "9001": spValue = Get_RSP(ident, numberStr, YesOrNo); break;
                case "9002": spValue = Get_JQS(ident, numberStr, YesOrNo); break;
                case "9003": spValue = Get_CBF(ident, numberStr, YesOrNo); break;
                case "9004": spValue = Get_BQC(ident, numberStr, YesOrNo); break;
                case "9005": spValue = Get_HH(ident, numberStr, YesOrNo); break;
                case "9006": spValue = Get_SPF(ident, numberStr, YesOrNo); break;
                default: break;
            }
            return spValue;
        }

        private List<string> Get_HH(string ident, string numberStr, string YesOrNo)
        {
            float respValue = 1;
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
                            case "RSP": spValue.Add(Get_RSP(ident, item.Groups[2].Value, YesOrNo)); break;
                            case "JQS": spValue.Add(Get_JQS(ident, item.Groups[2].Value, YesOrNo)); break;
                            case "CBF": spValue.Add(Get_CBF(ident, item.Groups[2].Value, YesOrNo)); break;
                            case "BQC": spValue.Add(Get_BQC(ident, item.Groups[2].Value, YesOrNo)); break;
                            case "SPF": spValue.Add(Get_SPF(ident, item.Groups[2].Value, YesOrNo)); break;
                        }
                    }
                }
            }
            for (int i = 0; i < spValue.Count; i++)
            {
                for (int j = 0; j < spValue[i].Count; j++)
                {
                    if (YesOrNo.Equals("Yes"))
                    {
                        strRet.Add(spValue[i][j]);
                    }
                    else
                    {
                        respValue= respValue*float.Parse(spValue[i][j]);
                    }
                }
            }
            if (!YesOrNo.Equals("Yes"))
            {
                strRet.Add(respValue.ToString());
            }
            return strRet;
        }

        private List<string> Get_BQC(string ident, string numberStr, string YesOrNo)
        {
            List<string> strRet = new List<string>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                List<float> StrSumSp = new List<float>();
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"(BQC\|)*((\d{6})(\d{3}))=(.+)");
                foreach (Match item in strMatch)
                {
                    if (item.Success)
                    {
                        MatchCollection strMatch_SP = Regex.Matches(item.Groups[5].Value, @"(\d+\-\d+)\((\d+.\d+)\)");
                        foreach (Match item_sp in strMatch_SP)
                        {
                            if (item_sp.Success)
                            {
                                if (YesOrNo.Equals("Yes"))
                                {
                                    strRet.Add(ident + "&" + item.Groups[2].Value + "&" + "BQC" + "&" + item_sp.Groups[1].Value + "&" + item_sp.Groups[2].Value + "&" + item.Groups[4].Value + "&" + item.Groups[3].Value);
                                }
                                else
                                {
                                    StrSumSp.Add(float.Parse(item_sp.Groups[2].Value));
                                }
                            }
                        }

                    }
                }
                if (!YesOrNo.Equals("Yes"))
                {
                    strRet.Add(StrSumSp.Max().ToString());
                }
            }
            return strRet;
        }

        private List<string> Get_CBF(string ident, string numberStr ,string YesOrNo)
        {
            List<string> strRet = new List<string>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                List<float> StrSumSp = new List<float>();
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"(CBF\|)*((\d{6})(\d{3}))=(.+)");
                foreach (Match item in strMatch)
                {
                    if (item.Success)
                    {
                        MatchCollection strMatch_SP = Regex.Matches(item.Groups[5].Value, @"(\d+:\d+)\((\d+.\d+)\)");
                        foreach (Match item_sp in strMatch_SP)
                        {
                            if (item_sp.Success)
                            {
                                if (YesOrNo.Equals("Yes"))
                                {
                                 strRet.Add(ident + "&" + item.Groups[2].Value + "&" + "CBF" + "&" + item_sp.Groups[1].Value + "&" + item_sp.Groups[2].Value + "&" + item.Groups[4].Value + "&" + item.Groups[3].Value);
                                }
                                else
                                {
                                    StrSumSp.Add(float.Parse(item_sp.Groups[2].Value));
                                }
                               
                            }
                        }

                    }
                }
                if (!YesOrNo.Equals("Yes"))
                {
                    strRet.Add(StrSumSp.Max().ToString());
                }
            }
            return strRet;
        }

        private List<string> Get_JQS(string ident, string numberStr,string YesOrNo)
        {
            List<string> strRet = new List<string>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"(JQS\|)*((\d{6})(\d{3}))=(.+)");
                List<float> StrSumSp = new List<float>();
                foreach (Match item in strMatch)
                {
                   
                    if (item.Success)
                    {
                        MatchCollection strMatch_SP = Regex.Matches(item.Groups[5].Value, @"(\d+)\((\d+.\d+)\)");
                        foreach (Match item_sp in strMatch_SP)
                        {
                            if (item_sp.Success)
                            {
                                if (YesOrNo.Equals("Yes"))
                                {
                                    strRet.Add(ident + "&" + item.Groups[2].Value + "&" + "JQS" + "&" + item_sp.Groups[1].Value + "&" + item_sp.Groups[2].Value + "&" + item.Groups[4].Value + "&" + item.Groups[3].Value);
                                }
                                else
                                {
                                    StrSumSp.Add(float.Parse(item_sp.Groups[2].Value));
                                }
                            }
                        }

                    }
                }
                if (!YesOrNo.Equals("Yes"))
                {
                    strRet.Add(StrSumSp.Max().ToString());
                }
            }
            return strRet;
        }

        private List<string> Get_RSP(string ident, string numberStr, string YesOrNo)
        {
            List<string> strRet = new List<string>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                List<float> StrSumSp = new List<float>();
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"(RSP\|)*((\d{6})(\d{3}))=(.+)");
                foreach (Match item in strMatch)
                {
                    if (item.Success)
                    {
                        MatchCollection strMatch_SP = Regex.Matches(item.Groups[5].Value, @"(\d+)\((\d+.\d+)\)");
                        foreach (Match item_sp in strMatch_SP)
                        {
                            if (item_sp.Success)
                            {
                                if (YesOrNo.Equals("Yes"))
                                {

                                    strRet.Add(ident + "&" + item.Groups[2].Value + "&" + "RSP" + "&" + item_sp.Groups[1].Value + "&" + item_sp.Groups[2].Value + "&" + item.Groups[4].Value + "&" + item.Groups[3].Value);
                                }
                                else
                                {
                                    StrSumSp.Add(float.Parse(item_sp.Groups[2].Value));
                                }
                            }
                        }

                    }
                }
                if (!YesOrNo.Equals("Yes"))
                {
                    strRet.Add(StrSumSp.Max().ToString());
                }
            }
            return strRet;
        }
        public List<string> Get_SPF(string ident, string numberStr,string YesOrNo)
        {
            List<string> strRet = new List<string>();
            List<string> splitNumber = new List<string>();
            splitNumber = numberStr.Split(',').ToList();
            for (int i = 0; i < splitNumber.Count; i++)
            {
                List<float> StrSumSp = new List<float>();
                MatchCollection strMatch = Regex.Matches(splitNumber[i], @"(SPF\|)*((\d{6})(\d{3}))=(.+)");
                foreach (Match item in strMatch)
                {
                    if (item.Success)
                    {
                        MatchCollection strMatch_SP = Regex.Matches(item.Groups[5].Value, @"(\d+)\((\d+.\d+)\)");
                        foreach (Match item_sp in strMatch_SP)
                        {
                            if (item_sp.Success)
                            {
                                if (YesOrNo.Equals("Yes"))
                                {
                                    strRet.Add(ident + "&" + item.Groups[2].Value + "&" + "SPF" + "&" + item_sp.Groups[1].Value + "&" + item_sp.Groups[2].Value + "&" + item.Groups[4].Value + "&" + item.Groups[3].Value);
                                }
                                else
                                {
                                    StrSumSp.Add(float.Parse(item_sp.Groups[2].Value));
                                }
                            }
                        }

                    }
                }
                if (!YesOrNo.Equals("Yes"))
                {
                    strRet.Add(StrSumSp.Max().ToString());
                }
            }
            return strRet;
        }
    }
}
