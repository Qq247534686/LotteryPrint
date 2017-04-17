using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
namespace WebApplication1
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        //Data Source=120.25.153.127;Initial Catalog=sls_mhb_bak;User ID=jdd;pwd=jdd20130512
        //private string conStr = ConfigurationManager.AppSettings["pubsConnectionStringSQL"].ToString();//远程数据库连接字符串
        private string conStr = "Data Source=101.69.207.4;Initial Catalog=sls_mhb_bak;User ID=jdd;pwd=jdd20130512";
        [WebMethod]
        public LotterData GetDataMethod(string InputID)
        {

            //List<LotterData> ss = new List<LotterData>();
            LotterData em = null;
            //定义“SqlConnnection”类实例
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                //定义“SqlCommand”实例，从表中取数据
                using (SqlDataAdapter command = new SqlDataAdapter())
                {
                    conn.Open(); //打开连接
                    if (InputID == "all")
                    {
                        command.SelectCommand = new SqlCommand("select top 1 * from T_SchemesTicket where PrintResult is NULL and DateTime>'2016-06-26 22:14:35.457'  and (HandleResult = -25 or HandleResult = -8) order by ID desc", conn);
                    }
                    else if (InputID == "last")
                    {
                        command.SelectCommand = new SqlCommand("select top 1 * from T_SchemesTicket order by ID desc", conn);
                    }
                    else
                    {
                        command.SelectCommand = new SqlCommand("select * from T_SchemesTicket where ID=" + int.Parse(InputID), conn);
                    }
                    DataSet ds = new DataSet();                 //定义“DataSet”类实例
                    command.Fill(ds, "tables");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)   //将全部数据存储于al变量中
                    {
                        em = new LotterData();
                        //定义类实例
                        em.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        em.MyDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["DateTime"] == System.DBNull.Value ? "2000-01-01" : ds.Tables[0].Rows[i]["DateTime"]);
                        em.AgentID = Convert.ToInt32(ds.Tables[0].Rows[i]["AgentID"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["AgentID"]);
                        em.Money = Convert.ToDecimal(ds.Tables[0].Rows[i]["Money"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["Money"]);
                        em.ZhuShu = Convert.ToInt32(ds.Tables[0].Rows[i]["ZhuShu"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["ZhuShu"]);
                        em.Multiple = Convert.ToInt32(ds.Tables[0].Rows[i]["Multiple"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["Multiple"]);
                        em.Number = Convert.ToString(ds.Tables[0].Rows[i]["Number"] == System.DBNull.Value ? "NULL" : ds.Tables[0].Rows[i]["Number"]);
                        em.Identifiers = Convert.ToString(ds.Tables[0].Rows[i]["Identifiers"] == System.DBNull.Value ? "NULL" : ds.Tables[0].Rows[i]["Identifiers"]);
                        em.Sends = Convert.ToInt32(ds.Tables[0].Rows[i]["Sends"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["Sends"]);
                        em.HandleDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["HandleDateTime"] == System.DBNull.Value ? "2000-01-01" : ds.Tables[0].Rows[i]["HandleDateTime"]);
                        em.HandleResult = Convert.ToInt32(ds.Tables[0].Rows[i]["HandleResult"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["HandleResult"]);
                        em.HandleDescription = Convert.ToString(ds.Tables[0].Rows[i]["HandleDescription"]);
                        em.EndTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["EndTime"]);
                        em.IsOpened = Convert.ToInt32(ds.Tables[0].Rows[i]["IsOpened"]);
                        em.WinMoney = Convert.ToDecimal(ds.Tables[0].Rows[i]["WinMoney"]);
                        em.WinMoneyNoWithTax = Convert.ToDecimal(ds.Tables[0].Rows[i]["WinMoneyNoWithTax"]);
                        em.WinDescription = Convert.ToString(ds.Tables[0].Rows[i]["WinDescription"] == System.DBNull.Value ? "NULL" : ds.Tables[0].Rows[i]["WinDescription"]);
                        em.QuashStatus = Convert.ToInt32(ds.Tables[0].Rows[i]["QuashStatus"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["QuashStatus"]);
                        em.LotteryId = Convert.ToInt32(ds.Tables[0].Rows[i]["LotteryId"]);
                        em.StrikeDate = Convert.ToString(ds.Tables[0].Rows[i]["StrikeDate"]);
                        em.PrintResult = Convert.ToString(ds.Tables[0].Rows[i]["PrintResult"] == System.DBNull.Value ? "×" : ds.Tables[0].Rows[i]["PrintResult"]);
                        em.PrintDescription = Convert.ToString(ds.Tables[0].Rows[i]["PrintDescription"] == System.DBNull.Value ? "NULL" : ds.Tables[0].Rows[i]["PrintDescription"]);
                        em.PrintDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["PrintDateTime"] == System.DBNull.Value ? "2000-01-01" : ds.Tables[0].Rows[i]["PrintDateTime"]);
                        em.PringHandleDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["PringHandleDateTime"] == System.DBNull.Value ? "2000-01-01" : ds.Tables[0].Rows[i]["PringHandleDateTime"]);
                        em.PrintOutTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["PrintOutTypeID"]);
                        em.TouZhuSPValues = Convert.ToString(ds.Tables[0].Rows[i]["TouZhuSPValues"]);
                        em.IssueName = Convert.ToString(ds.Tables[0].Rows[i]["IssueName"]);
                        em.TotalStatus = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalStatus"]);
                        em.OpenedStatus = Convert.ToInt32(ds.Tables[0].Rows[i]["OpenedStatus"]);
                        em.IsBackMoney = Convert.ToInt32(ds.Tables[0].Rows[i]["IsBackMoney"]);
                        em.IsJiaJiang = Convert.ToInt32(ds.Tables[0].Rows[i]["IsJiaJiang"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["IsJiaJiang"]);
                        em.TicketNo = Convert.ToString(ds.Tables[0].Rows[i]["TicketNo"]);
                        em.PlayTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["PlayTypeID"]);
                        em.Tdesc = Convert.ToInt32(ds.Tables[0].Rows[i]["Tdesc"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["Tdesc"]);
                        em.WinnerStatus = Convert.ToInt32(ds.Tables[0].Rows[i]["WinnerStatus"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["WinnerStatus"]);
                        em.PrizeStatus = Convert.ToInt32(ds.Tables[0].Rows[i]["PrizeStatus"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["PrizeStatus"]);
                        em.Remark = Convert.ToString(ds.Tables[0].Rows[i]["Remark"]);
                        em.Recvs = Convert.ToInt32(ds.Tables[0].Rows[i]["Recvs"]);
                        em.Splited = Convert.ToInt32(ds.Tables[0].Rows[i]["Splited"]);
                        em.SplitedParentID = Convert.ToString(ds.Tables[0].Rows[i]["SplitedParentID"]);
                        em.ParentIdentifier = Convert.ToString(ds.Tables[0].Rows[i]["ParentIdentifier"]);
                        em.OpenTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["OpenTime"]);
                        em.SchemeNo = Convert.ToString(ds.Tables[0].Rows[i]["SchemeNo"] == System.DBNull.Value ? "NULL" : ds.Tables[0].Rows[i]["SchemeNo"]);
                        em.FromSource = Convert.ToString(ds.Tables[0].Rows[i]["FromSource"] == System.DBNull.Value ? "NULL" : ds.Tables[0].Rows[i]["FromSource"]);
                        em.PushStatus = Convert.ToInt32(ds.Tables[0].Rows[i]["PushStatus"] == System.DBNull.Value ? -888 : ds.Tables[0].Rows[i]["PushStatus"]);
                        //ss.Add(em);
                    }
                }
                conn.Close();//关闭连接
            }
            return em;
        }
        [WebMethod]
        public int DelectDataMethod(string str)
        {
            int i = -1;
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            SqlCommand command = new SqlCommand("delete T_SchemesOddsPrintDetails where Identifiers='" + str + "'", conn);
            i = command.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        [WebMethod]
        public int SelectIDDataMethod(string str)
        {
            //delete  T_SchemesOddsPrintDetails where Identifiers=str
            int i = -1;
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            SqlCommand command = new SqlCommand("select * from  T_SchemesOddsPrintDetails where Identifiers='" + str + "'", conn);
            i = command.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        [WebMethod]
        public int SetDataMethod(List<string> str)
        {

            int i = -1; string messg = string.Empty;
            messg = str[0];
            try
            {
                SqlParameter[] ps = new SqlParameter[] { 
                        new SqlParameter("@identifiers",Convert.ToString(str[0])),
                        new SqlParameter("@match",Convert.ToString(str[1])),
                        new SqlParameter("@lotteryType",Convert.ToString(str[2])),
                        new SqlParameter("@matchResult",Convert.ToString(str[3])),
                        new SqlParameter("@matchSP",Convert.ToString(str[4])),
                        new SqlParameter("@UpdateTime",Convert.ToDateTime(DateTime.Now)),
                        new SqlParameter("@IndexNo",Convert.ToString(str[5])),
                        new SqlParameter("@IssueNo",Convert.ToString(str[6]))
                        };
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    using (SqlCommand command = new SqlCommand("update T_SchemesOddsPrintDetails set matchSP=@matchSP where identifiers=@identifiers and match=@match and lotteryType=@lotteryType and matchResult=@matchResult", conn))
                    {
                        conn.Open();
                        command.Parameters.AddRange(ps);
                        i = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                str.Clear();
            }
            catch
            {

            }
            finally
            { 
            
            }
            return i;
        }
        [WebMethod]
        public int SetDataMethod_2(string str)
        {
            int i = -1; 
            string[] strcc = str.Split('&');
            string messg = strcc[0];
            try
            {
               
                if (strcc != null)
                {
                    SqlParameter[] ps = new SqlParameter[] { 
                        new SqlParameter("@identifiers",Convert.ToString(strcc[0])),
                        new SqlParameter("@match",Convert.ToString(strcc[1])),
                        new SqlParameter("@lotteryType",Convert.ToString(strcc[2])),
                        new SqlParameter("@matchResult",Convert.ToString(strcc[3])),
                        new SqlParameter("@matchSP",Convert.ToString(strcc[4])),
                        new SqlParameter("@UpdateTime",Convert.ToDateTime(DateTime.Now)),
                        new SqlParameter("@IndexNo",Convert.ToString(strcc[5])),
                        new SqlParameter("@IssueNo",Convert.ToString(strcc[6]))
                        };
                    using (SqlConnection conn = new SqlConnection(conStr))
                    {
                        using (SqlCommand command = new SqlCommand("update T_SchemesOddsPrintDetails set matchSP=@matchSP where identifiers=@identifiers and match=@match and lotteryType=@lotteryType and matchResult=@matchResult", conn))
                        {
                            conn.Open();
                            command.Parameters.AddRange(ps);
                            i = command.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
                messg=DateTime.Now.ToString()+">"+strcc[0]+"已成功";
            }
            catch
            {
                messg = DateTime.Now.ToString() + ">" + strcc[0] + "警告,失败";
            }
            finally
            {
                if (!File.Exists("../Log/SetDataMethod_2.txt"))
                {
                    File.Create("../Log/SetDataMethod_2.txt");
                }
                using(StreamWriter sw=new StreamWriter("../Log/SetDataMethod_2.txt",true))
                {
                    sw.WriteLine(messg);
                }
            }
            return i;
        }
        [WebMethod]
        public int upDate(string str)
        {
            int i = -1; string messg = str;
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    using (SqlCommand command = new SqlCommand("update T_SchemesTicket set PrintResult=1,HandleResult=0,TotalStatus=1 where ID=@ID", conn))
                    {
                        conn.Open();
                        SqlParameter ps = new SqlParameter("ID", str);
                        command.Parameters.Add(ps);
                        i = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                messg = DateTime.Now.ToString() + ">" + messg + "已成功";
            }
            catch
            {
                messg = DateTime.Now.ToString() + ">" + messg + "警告,失败";
            }
            finally
            {
                if (!File.Exists("../Log/upDate.txt"))
                {
                    File.Create("../Log/upDate.txt");
                }
                using (StreamWriter sw = new StreamWriter("../Log/upDate.txt", true))
                {
                    sw.WriteLine(messg);
                }
            }
            return i;
        }
        public class LotterData
        {

            public int ID;

            public DateTime MyDateTime;

            public int AgentID;

            public decimal Money;

            public int ZhuShu;

            public int Multiple;

            public string Number;

            public string Identifiers;

            public int Sends;

            public DateTime HandleDateTime;

            public int HandleResult;

            public string HandleDescription;

            public DateTime EndTime;

            public int IsOpened;

            public decimal WinMoney;

            public decimal WinMoneyNoWithTax;

            public string WinDescription;

            public int QuashStatus;

            public int LotteryId;

            public string StrikeDate;

            public string PrintResult;

            public string PrintDescription;

            public DateTime PrintDateTime;

            public DateTime PringHandleDateTime;

            public int PrintOutTypeID;

            public string TouZhuSPValues;

            public string IssueName;

            public int TotalStatus;

            public int OpenedStatus;

            public int IsBackMoney;

            public int IsJiaJiang;

            public string TicketNo;

            public int PlayTypeID;

            public int Tdesc;

            public int WinnerStatus;

            public int PrizeStatus;

            public string Remark;

            public int Recvs;

            public int Splited;

            public string SplitedParentID;

            public string ParentIdentifier;

            public DateTime OpenTime;

            public string SchemeNo;

            public string FromSource;

            public int PushStatus;
        }
    }
}
