using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lottery.DAL;
using Lottery.Model;
using System.Configuration;
namespace Lottery.BLL
{
    public class SuanJiangTxt
    {
        private static readonly string theTxtPath = ConfigurationManager.AppSettings["txtPath"].ToString().Trim();
        private static readonly string theDateStr = ConfigurationManager.AppSettings["dateStr"].ToString().Trim();


        public DataTable GetTableTitck(DateTime DateTimeTitck_S, DateTime DateTimeTitck_E)
        {
            OleDbParameter[] ps = new OleDbParameter[] {
            new OleDbParameter("@DateTimeTitck_S",DateTimeTitck_S.ToShortDateString()),
            new OleDbParameter("@DateTimeTitck_E",DateTimeTitck_E.ToShortDateString())
            };
            DataTable dataTable = OLEDBHelp.GetTable("select [ZhongJiangID] as 订单号,[ZhongJiangTitck] as 奖票,[ChuangGuan] as 串关,[ZhongJiangMoney] as 奖金,[ZhongJianTime] as 创建时间,[PlayGame] as 玩法,[BeiShu] as 倍数,[RongCuo] as 已容错 from ZhongJiangTable where [ZhongJianTime]>=@DateTimeTitck_S and [ZhongJianTime]<=@DateTimeTitck_E order by [ZhongJianTime] desc", ps);
            return dataTable;

        }
        public void SelectUrl(DateTime DateTimeTitck_S, DateTime DateTimeTitck_E)
        {
            string theStr = string.Empty; string theDate = string.Empty;
            while (DateTimeTitck_S <= DateTimeTitck_E)
            {
                theDate = DateTimeTitck_S.ToString("yyyy-MM-dd");
                theStr = "http://zx.500.com/jczq/kaijiang.php?d=" + theDate;//中奖
                GetUrltoHtml(theStr, "GB2312", DateTimeTitck_S);
                DateTimeTitck_S = DateTimeTitck_S.AddDays(1);
            }
        }
        public void GetUrltoHtml(string Url, string type, DateTime dateTxt)
        {
            try
            {
                string dateName = dateTxt.ToString("yyyy-MM-dd").Replace("-", "").Substring(2);
                string RetuList = string.Empty;
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream)
                using (StreamReader reader = new StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    string txt_HTML = reader.ReadToEnd();
                    if (!Directory.Exists(theTxtPath))
                    {
                        Directory.CreateDirectory(theTxtPath);
                    }
                        using (FileStream fs = new FileStream(theTxtPath + "/" + dateName + ".txt", FileMode.Create))
                        {
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                sw.WriteLine(txt_HTML);
                            }
                        }
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="DateTimeTitck_S"></param>
        /// <param name="DateTimeTitck_E"></param>
        /// <returns></returns>
        public void SelectDate(DateTime DateTimeTitck_S, DateTime DateTimeTitck_E)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OleDbParameter[] ps = new OleDbParameter[] {
            new OleDbParameter("@DateTimeTitck_S",DateTimeTitck_S.ToShortDateString()),
            new OleDbParameter("@DateTimeTitck_E",DateTimeTitck_E.ToShortDateString())
            };
                dataTable = OLEDBHelp.GetTable("select [BuyTitckID] as 订单号,[PlayType] as 玩法,[Multiple] as 倍数,[TouZhu] as 投注内容,[TouZhuValue] as 投注赔率,[CreateTime] as 创建时间 from TouZhuTitck where [CreateTime]>=@DateTimeTitck_S and [CreateTime]<=@DateTimeTitck_E order by [CreateTime] desc", ps);
                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        SuanJiangClass suanJiangClass = new SuanJiangClass();
                        suanJiangClass.BuyTitckID = dataTable.Rows[i]["订单号"].ToString();
                        suanJiangClass.PlayType = dataTable.Rows[i]["玩法"].ToString();
                        suanJiangClass.Multiple = dataTable.Rows[i]["倍数"].ToString();
                        suanJiangClass.TouZhu = dataTable.Rows[i]["投注内容"].ToString();
                        suanJiangClass.TouZhuValue = dataTable.Rows[i]["投注赔率"].ToString();
                        suanJiangClass.CreateTime = DateTime.Parse(dataTable.Rows[i]["创建时间"].ToString());
                        switch (suanJiangClass.PlayType)
                        {
                            case "9001":ChuLiValue9001(suanJiangClass); break;
                            case "9002":ChuLiValue9002(suanJiangClass); break;
                            case "9003":ChuLiValue9003(suanJiangClass);break;
                            case "9004":ChuLiValue9004(suanJiangClass); break;
                            case "9005":ChuLiValue9005(suanJiangClass); break;
                            case "9006":ChuLiValue9006(suanJiangClass); break;
                        }
                       
                    }
                }
            }
            catch (Exception error)
            {
                Log.WriteLog(error, "Error");
            }
        }

        private void ChuLiValue9006(SuanJiangClass suanJiangClass)
        {
            List<Dictionary<string, string>> lis = new List<Dictionary<string, string>>();
            string chuanGuanShu = string.Empty;
            try
            {
                chuanGuanShu = Regex.Match(suanJiangClass.TouZhu, @".+?\|(\d+\*\d+)").Groups[1].Value;
                if (string.IsNullOrWhiteSpace(chuanGuanShu))
                {
                    Log.WriteLog(suanJiangClass.BuyTitckID + "的串关未截取到", "Error");
                    return;
                }
                List<string> listValue = suanJiangClass.TouZhuValue.Split(',').ToList();
                for (int i = 0; i < listValue.Count; i++)
                {
                    MatchCollection matchTitck = Regex.Matches(listValue[i], @"(\d{6})(\d+)=(.+)");
                    foreach (Match item in matchTitck)
                    {
                        string redy = string.Empty;
                        if (item.Success)
                        {
                            Titck_GeShi titck_GeShi = new Titck_GeShi();
                            titck_GeShi.Titck_PlayEnglish = "SPF";
                            titck_GeShi.Titck_DateTime = item.Groups[1].Value;
                            titck_GeShi.Titck_SaiShi = item.Groups[2].Value;
                            titck_GeShi.Titck_ContentString = item.Groups[3].Value;
                            //string dataTxt = ParesDate(item.Groups[2].Value); //suanJiangClass.CreateTime.AddDays(1).ToString("yyyy-MM-dd");
                            using (StreamReader streamRead = new StreamReader(theTxtPath + "/" + item.Groups[1].Value + ".txt", Encoding.GetEncoding("utf-8")))
                            {
                                redy = streamRead.ReadToEnd();//缓存内容
                            }
                            lis.Add(SplitTitckValue(titck_GeShi, redy));
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.WriteLog(suanJiangClass.BuyTitckID + "投注赔率匹配失败:" + error, "Error");
            }
            //SPF>160625102=1(4.10),RSP>160625103=1(4.05)/0(1.42),RSP>160625104=1(3.96),RSP>160625106=1(3.45)/0(2.40),RSP>160625108=1(3.50)
            GetZhongJianTitck(lis, chuanGuanShu, suanJiangClass);
            lis.Clear();
        }

        private void ChuLiValue9004(SuanJiangClass suanJiangClass)
        {
            List<Dictionary<string, string>> lis = new List<Dictionary<string, string>>();
            string chuanGuanShu = string.Empty;
            try
            {
                chuanGuanShu = Regex.Match(suanJiangClass.TouZhu, @".+?\|(\d+\*\d+)").Groups[1].Value;
                if (string.IsNullOrWhiteSpace(chuanGuanShu))
                {
                    Log.WriteLog(suanJiangClass.BuyTitckID + "的串关未截取到", "Error");
                    return;
                }
                List<string> listValue = suanJiangClass.TouZhuValue.Split(',').ToList();
                for (int i = 0; i < listValue.Count; i++)
                {
                    MatchCollection matchTitck = Regex.Matches(listValue[i], @"(\d{6})(\d+)=(.+)");
                    foreach (Match item in matchTitck)
                    {
                        string redy = string.Empty;
                        if (item.Success)
                        {
                            Titck_GeShi titck_GeShi = new Titck_GeShi();
                            titck_GeShi.Titck_PlayEnglish = "BQC";
                            titck_GeShi.Titck_DateTime = item.Groups[1].Value;
                            titck_GeShi.Titck_SaiShi = item.Groups[2].Value;
                            titck_GeShi.Titck_ContentString = item.Groups[3].Value;
                            //string dataTxt = ParesDate(item.Groups[2].Value); //suanJiangClass.CreateTime.AddDays(1).ToString("yyyy-MM-dd");
                            using (StreamReader streamRead = new StreamReader(theTxtPath + "/" + item.Groups[1].Value + ".txt", Encoding.GetEncoding("utf-8")))
                            {
                                redy = streamRead.ReadToEnd();//缓存内容
                            }
                            lis.Add(SplitTitckValue(titck_GeShi, redy));
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.WriteLog(suanJiangClass.BuyTitckID + "投注赔率匹配失败:" + error, "Error");
            }
            //SPF>160625102=1(4.10),RSP>160625103=1(4.05)/0(1.42),RSP>160625104=1(3.96),RSP>160625106=1(3.45)/0(2.40),RSP>160625108=1(3.50)
            GetZhongJianTitck(lis, chuanGuanShu, suanJiangClass);
            lis.Clear();
        }

        private void ChuLiValue9003(SuanJiangClass suanJiangClass)
        {
            List<Dictionary<string, string>> lis = new List<Dictionary<string, string>>();
            string chuanGuanShu = string.Empty;
            try
            {
                chuanGuanShu = Regex.Match(suanJiangClass.TouZhu, @".+?\|(\d+\*\d+)").Groups[1].Value;
                if (string.IsNullOrWhiteSpace(chuanGuanShu))
                {
                    Log.WriteLog(suanJiangClass.BuyTitckID + "的串关未截取到", "Error");
                    return;
                }
                List<string> listValue = suanJiangClass.TouZhuValue.Split(',').ToList();
                for (int i = 0; i < listValue.Count; i++)
                {
                    MatchCollection matchTitck = Regex.Matches(listValue[i], @"(\d{6})(\d+)=(.+)");
                    foreach (Match item in matchTitck)
                    {
                        string redy = string.Empty;
                        if (item.Success)
                        {
                            Titck_GeShi titck_GeShi = new Titck_GeShi();
                            titck_GeShi.Titck_PlayEnglish = "CBF";
                            titck_GeShi.Titck_DateTime = item.Groups[1].Value;
                            titck_GeShi.Titck_SaiShi = item.Groups[2].Value;
                            titck_GeShi.Titck_ContentString = item.Groups[3].Value;
                            //string dataTxt = ParesDate(item.Groups[2].Value); //suanJiangClass.CreateTime.AddDays(1).ToString("yyyy-MM-dd");
                            using (StreamReader streamRead = new StreamReader(theTxtPath + "/" + item.Groups[1].Value + ".txt", Encoding.GetEncoding("utf-8")))
                            {
                                redy = streamRead.ReadToEnd();//缓存内容
                            }
                            lis.Add(SplitTitckValue(titck_GeShi, redy));
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.WriteLog(suanJiangClass.BuyTitckID + "投注赔率匹配失败:" + error, "Error");
            }
            //SPF>160625102=1(4.10),RSP>160625103=1(4.05)/0(1.42),RSP>160625104=1(3.96),RSP>160625106=1(3.45)/0(2.40),RSP>160625108=1(3.50)
            GetZhongJianTitck(lis, chuanGuanShu, suanJiangClass);
            lis.Clear();
        }

        private void ChuLiValue9002(SuanJiangClass suanJiangClass)
        {
            List<Dictionary<string, string>> lis = new List<Dictionary<string, string>>();
            string chuanGuanShu = string.Empty;
            try
            {
                chuanGuanShu = Regex.Match(suanJiangClass.TouZhu, @".+?\|(\d+\*\d+)").Groups[1].Value;
                if (string.IsNullOrWhiteSpace(chuanGuanShu))
                {
                    Log.WriteLog(suanJiangClass.BuyTitckID + "的串关未截取到", "Error");
                    return;
                }
                List<string> listValue = suanJiangClass.TouZhuValue.Split(',').ToList();
                for (int i = 0; i < listValue.Count; i++)
                {
                    MatchCollection matchTitck = Regex.Matches(listValue[i], @"(\d{6})(\d+)=(.+)");
                    foreach (Match item in matchTitck)
                    {
                        string redy = string.Empty;
                        if (item.Success)
                        {
                            Titck_GeShi titck_GeShi = new Titck_GeShi();
                            titck_GeShi.Titck_PlayEnglish = "JQS";
                            titck_GeShi.Titck_DateTime = item.Groups[1].Value;
                            titck_GeShi.Titck_SaiShi = item.Groups[2].Value;
                            titck_GeShi.Titck_ContentString = item.Groups[3].Value;
                            //string dataTxt = ParesDate(item.Groups[2].Value); //suanJiangClass.CreateTime.AddDays(1).ToString("yyyy-MM-dd");
                            using (StreamReader streamRead = new StreamReader(theTxtPath + "/" + item.Groups[1].Value + ".txt", Encoding.GetEncoding("utf-8")))
                            {
                                redy = streamRead.ReadToEnd();//缓存内容
                            }
                            lis.Add(SplitTitckValue(titck_GeShi, redy));
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.WriteLog(suanJiangClass.BuyTitckID + "投注赔率匹配失败:" + error, "Error");
            }
            //SPF>160625102=1(4.10),RSP>160625103=1(4.05)/0(1.42),RSP>160625104=1(3.96),RSP>160625106=1(3.45)/0(2.40),RSP>160625108=1(3.50)
            GetZhongJianTitck(lis, chuanGuanShu, suanJiangClass);
            lis.Clear();
        }

        private void ChuLiValue9001(SuanJiangClass suanJiangClass)
        {
            List<Dictionary<string, string>> lis = new List<Dictionary<string, string>>();
            string chuanGuanShu = string.Empty;
            try
            {
                chuanGuanShu = Regex.Match(suanJiangClass.TouZhu, @".+?\|(\d+\*\d+)").Groups[1].Value;
                if (string.IsNullOrWhiteSpace(chuanGuanShu))
                {
                    Log.WriteLog(suanJiangClass.BuyTitckID + "的串关未截取到", "Error");
                    return;
                }
                List<string> listValue = suanJiangClass.TouZhuValue.Split(',').ToList();
                for (int i = 0; i < listValue.Count; i++)
                {
                    MatchCollection matchTitck = Regex.Matches(listValue[i], @"(\d{6})(\d+)=(.+)");
                    foreach (Match item in matchTitck)
                    {
                        string redy = string.Empty;
                        if (item.Success)
                        {
                            Titck_GeShi titck_GeShi = new Titck_GeShi();
                            titck_GeShi.Titck_PlayEnglish = "RSP";
                            titck_GeShi.Titck_DateTime = item.Groups[1].Value;
                            titck_GeShi.Titck_SaiShi = item.Groups[2].Value;
                            titck_GeShi.Titck_ContentString = item.Groups[3].Value;
                            //string dataTxt = ParesDate(item.Groups[2].Value); //suanJiangClass.CreateTime.AddDays(1).ToString("yyyy-MM-dd");
                            using (StreamReader streamRead = new StreamReader(theTxtPath + "/" + item.Groups[1].Value + ".txt", Encoding.GetEncoding("utf-8")))
                            {
                                redy = streamRead.ReadToEnd();//缓存内容
                            }
                            lis.Add(SplitTitckValue(titck_GeShi, redy));
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.WriteLog(suanJiangClass.BuyTitckID + "投注赔率匹配失败:" + error, "Error");
            }
            //SPF>160625102=1(4.10),RSP>160625103=1(4.05)/0(1.42),RSP>160625104=1(3.96),RSP>160625106=1(3.45)/0(2.40),RSP>160625108=1(3.50)
            GetZhongJianTitck(lis, chuanGuanShu, suanJiangClass);
            lis.Clear();
        }
        /// <summary>
        /// 处理票_001
        /// </summary>
        /// <param name="suanJiangClass"></param>
        private void ChuLiValue9005(SuanJiangClass suanJiangClass)
        {
            //2016/6/25 17:44:55
            //RSP|160625103=3,160625110=1|2*1
            //HH|SPF>160625113=3,SPF>160625119=3,RSP>160625101=0,RSP>160625104=3,RSP>160625112=0,SPF>160625116=3,SPF>160625122=3|7*1
            //SPF>160625102=1(4.10),RSP>160625103=1(4.05)/0(1.42),RSP>160625104=1(3.96),RSP>160625106=1(3.45)/0(2.40),RSP>160625108=1(3.50)
            //SPF>160625102=3(1.55),SPF>160625107=1(3.20),RSP>160625101=0(1.63),RSP>160625104=3(1.64),RSP>160625112=0(1.63),SPF>160625116=3(1.94),SPF>160625122=3(1.50)
            List<Dictionary<string, string>> lis = new List<Dictionary<string, string>>();
            string chuanGuanShu = string.Empty;
            try
            {

                chuanGuanShu = Regex.Match(suanJiangClass.TouZhu, @".+?\|(\d+\*\d+)").Groups[1].Value;
                if (string.IsNullOrWhiteSpace(chuanGuanShu))
                {
                    Log.WriteLog(suanJiangClass.BuyTitckID + "的串关未截取到", "Error");
                }

                List<string> listValue = suanJiangClass.TouZhuValue.Split(',').ToList();
                for (int i = 0; i < listValue.Count; i++)
                {
                    MatchCollection matchTitck = Regex.Matches(listValue[i], @"([A-Z]+)>(\d{6})(\d+)=(.+)");
                    foreach (Match item in matchTitck)
                    {
                        string redy = string.Empty;
                        if (item.Success)
                        {
                            Titck_GeShi titck_GeShi = new Titck_GeShi();
                            titck_GeShi.Titck_PlayEnglish = item.Groups[1].Value;
                            titck_GeShi.Titck_DateTime = item.Groups[2].Value;
                            titck_GeShi.Titck_SaiShi = item.Groups[3].Value;
                            titck_GeShi.Titck_ContentString = item.Groups[4].Value;
                            //string dataTxt = ParesDate(item.Groups[2].Value); //suanJiangClass.CreateTime.AddDays(1).ToString("yyyy-MM-dd");
                            using (StreamReader streamRead = new StreamReader(theTxtPath + "/" + item.Groups[2].Value + ".txt", Encoding.GetEncoding("utf-8")))
                            {
                                redy = streamRead.ReadToEnd();//缓存内容
                            }
                            lis.Add(SplitTitckValue(titck_GeShi, redy));
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.WriteLog(suanJiangClass.BuyTitckID + "投注赔率匹配失败:" + error, "Error");
            }
            //SPF>160625102=1(4.10),RSP>160625103=1(4.05)/0(1.42),RSP>160625104=1(3.96),RSP>160625106=1(3.45)/0(2.40),RSP>160625108=1(3.50)
            GetZhongJianTitck(lis, chuanGuanShu, suanJiangClass);
            lis.Clear();
        }

        private void GetZhongJianTitck(List<Dictionary<string, string>> lis, string chuanGuanShu, SuanJiangClass suanJiangClass)
        {
            double titckMoney = 1; int zj = 0; int mzj = 0;
            for (int i = 0; i < lis.Count; i++)
            {
                foreach (var item in lis[i].Keys)
                {
                    string ssc = string.Empty;
                    lis[i].TryGetValue(item.ToString(), out ssc);
                    if (ssc != "No")
                    {
                        double xxv = double.Parse(ssc);
                        titckMoney =titckMoney * xxv;
                        zj++;
                    }
                    else
                    {
                        mzj++;
                    }
                }
            }
            int rccs = -1;
            rccs = int.Parse(new selectData().SelectStrZJ(chuanGuanShu));//得到容错次数
            if (mzj <= rccs)
            {
                if (!(OLEDBHelp.GetReader("select TOP 1 * from ZhongJiangTable where [ZhongJiangID]='" + suanJiangClass.BuyTitckID + "'").HasRows))
                {
                    double bs = titckMoney * int.Parse(suanJiangClass.Multiple)*2.00;
                    OleDbParameter[] ps = new OleDbParameter[] {
            new OleDbParameter("@ZhongJiangID",suanJiangClass.BuyTitckID),
            new OleDbParameter("@ZhongJiangTitck",suanJiangClass.TouZhuValue),
            new OleDbParameter("@ChuangGuan",chuanGuanShu),
            new OleDbParameter("@ZhongJiangMoney",bs.ToString()),
            new OleDbParameter("@ZhongJianTime",suanJiangClass.CreateTime.ToString()),
            new OleDbParameter("@BeiShu",suanJiangClass.Multiple),
            new OleDbParameter("@PlayGame",suanJiangClass.PlayType),
            new OleDbParameter("@RongCuo",mzj.ToString())
             };
                    int sb = OLEDBHelp.GetExecute("insert into ZhongJiangTable([ZhongJiangID],[ZhongJiangTitck],[ChuangGuan],[ZhongJiangMoney],[ZhongJianTime],[BeiShu],[PlayGame],[RongCuo]) values(@ZhongJiangID,@ZhongJiangTitck,@ChuangGuan,@ZhongJiangMoney,@ZhongJianTime,@BeiShu,@PlayGame,@RongCuo)", ps);
                }
            }
            //string ssx = zj + "|" + mzj;
        }
        /// <summary>
        /// 格修复
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string ParesDate(string p)
        {
            p = theDateStr + p.Substring(0, 2) + "/" + p.Substring(2, 2) + "/" + p.Substring(4, 2);
            p = DateTime.Parse(p).AddDays(1).ToString("yyyy-MM-dd").Replace("-", "").Substring(2);
            return p;
        }
        /// <summary>
        /// 处理票_002
        /// </summary>
        /// <param name="titck_GeShi"></param>
        /// <param name="redy"></param>
        private Dictionary<string, string> SplitTitckValue(Titck_GeShi titck_GeShi, string redy)
        {
            //2016/6/25 17:44:55
            //RSP|160625103=3,160625110=1|2*1
            //HH|SPF>160625113=3,SPF>160625119=3,RSP>160625101=0,RSP>160625104=3,RSP>160625112=0,SPF>160625116=3,SPF>160625122=3|7*1
            //SPF>160625102=1(4.10),RSP>160625103=1(4.05)/0(1.42),RSP>160625104=1(3.96),RSP>160625106=1(3.45)/0(2.40),RSP>160625108=1(3.50)
            //SPF>160625102=3(1.55),SPF>160625107=1(3.20),RSP>160625101=0(1.63),RSP>160625104=3(1.64),RSP>160625112=0(1.63),SPF>160625116=3(1.94),SPF>160625122=3(1.50)
            string joinDate = titck_GeShi.Titck_DateTime.Substring(0, 2) + "/" + titck_GeShi.Titck_DateTime.Substring(2, 2) + "/" + titck_GeShi.Titck_DateTime.Substring(4, 2);
            string dayOfWeek = theDateStr + joinDate;
            switch (DateTime.Parse(dayOfWeek).DayOfWeek.ToString())
            {
                case "Monday": dayOfWeek = "周一"; break;
                case "Tuesday": dayOfWeek = "周二"; break;
                case "Wednesday": dayOfWeek = "周三"; break;
                case "Thursday": dayOfWeek = "周四"; break;
                case "Friday": dayOfWeek = "周五"; break;
                case "Saturday": dayOfWeek = "周六"; break;
                case "Sunday": dayOfWeek = "周日"; break;
                default: break;
            }
            dayOfWeek += titck_GeShi.Titck_SaiShi;
            string partValue = Regex.Match(redy.Replace("\r\n", ""), "<td>" + dayOfWeek + "</td>(.+?)</tr>").Groups[1].Value;

            string bf = string.Empty;
            string spf = string.Empty;
            string rqspf = string.Empty;
            string zjqs = string.Empty;
            string bqc = string.Empty;

            if (!string.IsNullOrWhiteSpace(partValue))
            {
                //比分
                bf = GetBF(Regex.Match(partValue, "<td class=\"[a-zA-z]+\">\\((.+?)\\) (.+?)</td>").Groups[2].Value);
                //让球胜平负
                rqspf = GetSPF(Regex.Match(partValue, "<td>([\u4e00-\u9fa5]{1})</td>").Groups[1].Value);
                //胜平负
                spf = GetSPF(Regex.Match(partValue, "<td class=\"[a-zA-z]+\">([\u4e00-\u9fa5]{1})</td>").Groups[1].Value);
                //总进球数
                zjqs = Regex.Match(partValue, "<td class=\"[a-zA-z]+\">(\\d+(\\+)*)</td>").Groups[1].Value;
                //半全场
                bqc = GetBQC(Regex.Match(partValue, "<td class=\"[a-zA-z]+\">([\u4e00-\u9fa5]{2})</td>").Groups[1].Value);
            }
            Dictionary<string, string> dir = new Dictionary<string, string>();
            switch (titck_GeShi.Titck_PlayEnglish)
            {
                case "SPF":
                    List<string> titckString_spf = titck_GeShi.Titck_ContentString.Split('/').ToList();
                    for (int i = 0; i < titckString_spf.Count; i++)
                    {
                        string str1 = Regex.Match(titckString_spf[i], @"(\d)\(\d+.\d+\)").Groups[1].Value;
                        string str2 = Regex.Match(titckString_spf[i], @"(\d)\((\d+.\d+)\)").Groups[2].Value;
                        if (spf == str1)
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + spf, str2);
                        }
                        else
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + str1, "No");
                        }
                    }
                    break;
                case "RSP":
                    List<string> titckString_rsf = titck_GeShi.Titck_ContentString.Split('/').ToList();
                    for (int i = 0; i < titckString_rsf.Count; i++)
                    {
                        string str1 = Regex.Match(titckString_rsf[i], @"(\d)\(\d+.\d+\)").Groups[1].Value;
                        string str2 = Regex.Match(titckString_rsf[i], @"(\d)\((\d+.\d+)\)").Groups[2].Value;
                        if (rqspf == str1)
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + rqspf, str2);
                        }
                        else
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + str1, "No");
                        }
                    }
                    break;
                case "JQS":
                    List<string> titckString_jqs = titck_GeShi.Titck_ContentString.Split('/').ToList();
                    for (int i = 0; i < titckString_jqs.Count; i++)
                    {
                        string str1 = Regex.Match(titckString_jqs[i], @"(\d+)\(\d+.\d+\)").Groups[1].Value;
                        string str2 = Regex.Match(titckString_jqs[i], @"(\d+)\((\d+.\d+)\)").Groups[2].Value;
                        if (zjqs == str1)
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + zjqs, str2);
                        }
                        else
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + str1, "No");
                        }
                    }
                    break;
                case "BQC":
                    List<string> titckString_bqc = titck_GeShi.Titck_ContentString.Split('/').ToList();
                    for (int i = 0; i < titckString_bqc.Count; i++)
                    {
                        string str1 = Regex.Match(titckString_bqc[i], @"(\d+\-\d+)\(\d+.\d+\)").Groups[1].Value;
                        string str2 = Regex.Match(titckString_bqc[i], @"(\d+\-\d+)\((\d+.\d+)\)").Groups[2].Value;
                        if (bqc == str1)
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + bqc, str2);
                        }
                        else
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + str1, "No");
                        }
                    }
                    break;
                case "CBF":
                    List<string> titckString_bf = titck_GeShi.Titck_ContentString.Split('/').ToList();
                    for (int i = 0; i < titckString_bf.Count; i++)
                    {
                        string str1 = Regex.Match(titckString_bf[i], @"(\d+:\d+|[\u4e00-\u9fa5]+)\(\d+.\d+\)").Groups[1].Value;
                        string str2 = Regex.Match(titckString_bf[i], @"(\d+:\d+|[\u4e00-\u9fa5]+)\((\d+.\d+)\)").Groups[2].Value;
                        if (bf == str1)
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + bf, str2);
                        }
                        else
                        {
                            dir.Add(titck_GeShi.Titck_PlayEnglish + ">" + titck_GeShi.Titck_DateTime + titck_GeShi.Titck_SaiShi + "F" + i + "=" + str1, "No");
                        }
                    }
                    break;
                default: break;
            }
            //SPF>160625102=1(4.10),RSP>160625103=1(4.05)/0(1.42),RSP>160625104=1(3.96),RSP>160625106=1(3.45)/0(2.40),RSP>160625108=1(3.50)
            return dir;
        }

        private string GetBF(string p)
        {
            switch (p)
            {
                case "9:0": p = "胜其他"; break;
                case "9:9": p = "平其他"; break;
                case "0:9": p = "负其他"; break;
            }

            return p;
        }

        private string GetBQC(string p)
        {
            string str1 = p.Substring(0, 1);
            string str2 = p.Substring(1, 1);
            switch (str1)
            {
                case "胜": str1 = "3"; break;
                case "平": str1 = "1"; break;
                case "负": str1 = "0"; break;
                default: break;
            }
            switch (str2)
            {
                case "胜": str2 = "3"; break;
                case "平": str2 = "1"; break;
                case "负": str2 = "0"; break;
                default: break;
            }
            return str1 + "-" + str2;
        }

        private string GetSPF(string p)
        {
            switch (p)
            {
                case "胜": p = "3"; break;
                case "平": p = "1"; break;
                case "负": p = "0"; break;
                default: break;
            }
            return p;
        }




















    }
}
