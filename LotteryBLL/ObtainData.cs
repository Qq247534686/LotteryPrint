using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Model;
using Lottery.DAL;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using System.Data;
namespace Lottery.BLL
{
    public class ObtainData
    {
        public ObtainData()
        {

        }
        public DataTable Get_TableData(int sc)
        {
            DataTable rs = DAL.OLEDBHelp.GetTable("select top "+sc+" * from TouZhuTitck where [IsTitck]='0' and [BiaoShi]=0 order by [CreateTime] ASC");
            return rs;
        }
        public int Set_Data(string id,string sp)
        {
            int i = -1;
            if (!string.IsNullOrWhiteSpace(id))
            {
                i = DAL.OLEDBHelp.GetExecute("update TouZhuTitck set [IsTitck]='1',[BiaoShi]=1,[UpTime]='" + DateTime.Now.ToString() + "',MaxBonus='"+sp+"' where [BuyTitckID]='" + id + "'");
            }
            return i;
        }
        public void mR()
        {
            DAL.OLEDBHelp.GetExecute("update TouZhuTitck set [IsTitck]='0',[BiaoShi]=0");
        }
        public int InsertThisData(MyLotteryData thisData)
        {
            int i=-1;
            DAL.OLEDBHelp.GetExecute(string.Format("INSERT INTO TouZhuTitck ([BuyTitckID],[Multiple],[PlayType],[TouZhu],[CreateTime],[TouZhuValue]) values ('{0}','{1}','{2}','{3}','{4}','{5}')", thisData.Identifiers, thisData.Multiple.ToString(), thisData.PlayTypeID.ToString(), thisData.Number, thisData.Dtm.ToString(), thisData.TouZhuValue));
            return i;
        }
        /// <summary>
        /// 获得标识是否修改过打印设置的方法
        /// </summary>
        /// <returns></returns>
        public bool IsTypePrintingSetting()
        {
            bool falg = false;
            if (DAL.OLEDBHelp.GetSingle("select Logo from PrintingSettingTable where ID=2") == "1")
            {
                falg = true;
            }
            return falg;
        }
        /// <summary>
        /// 获得标识是否修改过体彩设置的方法
        /// </summary>
        /// <returns></returns>
        public bool IsTypeSportsLotterySetting()
        {
            bool falg = false;
            if (DAL.OLEDBHelp.GetSingle("select Logo from SportsLotterySettingTable where ID=2") == "1")
            {
                falg = true;
            }
            return falg;
        }
        /// <summary>
        /// 获得标识是否修改过福彩设置的方法
        /// </summary>
        /// <returns></returns>
        public bool IsTypeWelfareLottery()
        {
            bool falg = false;
            if (DAL.OLEDBHelp.GetSingle("select Logo from WelfareLotteryTable where ID=2") == "1")
            {
                falg = true;
            }
            return falg;
        }
        /// <summary>
        /// 读取打印设置
        /// </summary>
        /// <param name="i">0:默认设置,1:还原上次保存的设置</param>
        /// <returns></returns>
        public PrintingSetting GetPrintingSetting(int i)
        {
            PrintingSetting ps = new PrintingSetting();
            OleDbDataReader oleRead = null;
            if (i == 0)
            {
                oleRead = DAL.OLEDBHelp.GetReader("select * from PrintingSettingTable where ID=1");
            }
            else if (i == 1)
            {
                oleRead = DAL.OLEDBHelp.GetReader("select * from PrintingSettingTable where ID=2");
            }
            while (oleRead.Read())
            {
                ps.Printer = Convert.ToInt32(oleRead["Printer"]);
                ps.PortName = Convert.ToInt32(oleRead["PortName"]);
                ps.PrinterSettingP = Convert.ToInt32(oleRead["PrinterSettingP"]);
                ps.PrinterPrompt = Convert.ToInt32(oleRead["PrinterPrompt"]);
                ps.PromptType = Convert.ToInt32(oleRead["PromptType"]);
                ps.DelayF = Convert.ToInt32(oleRead["DelayF"]);
                ps.IssueWaitF = Convert.ToInt32(oleRead["IssueWaitF"]);
                ps.EveryDelay = Convert.ToInt32(oleRead["EveryDelay"]);
                ps.IssueWaitD = Convert.ToInt32(oleRead["IssueWaitD"]);
            }
            return ps;
        }
        /// <summary>
        /// 读取体彩设置
        /// </summary>
        /// <param name="i">0:默认设置,1:还原上次保存的设置</param>
        /// <returns></returns>
        public SportsLotterySetting GetSportsLotterySetting(int i)
        {
            SportsLotterySetting ps = new SportsLotterySetting();
            OleDbDataReader oleRead = null;
            if (i == 0)
            {
                oleRead = DAL.OLEDBHelp.GetReader("select * from SportsLotterySettingTable where ID=1");
            }
            else if (i == 1)
            {
                oleRead = DAL.OLEDBHelp.GetReader("select * from SportsLotterySettingTable where ID=2");
            }
            while (oleRead.Read())
            {
                ps.IssueT = Convert.ToInt32(oleRead["IssueT"]);
                ps.FootballLottery = Convert.ToInt32(oleRead["FootballLottery"]);
                ps.Number = Convert.ToInt32(oleRead["Number"]);
                ps.Lotto = Convert.ToInt32(oleRead["Lotto"]);
                ps.MultiplyPrintingOne = Convert.ToInt32(oleRead["MultiplyPrintingOne"]);
                ps.MultiplyPrintingTow = Convert.ToInt32(oleRead["MultiplyPrintingTow"]);
                ps.MultiplyPrintingThree = Convert.ToInt32(oleRead["MultiplyPrintingThree"]);
                ps.RowOfThree = Convert.ToInt32(oleRead["RowOfThree"]);
                ps.Basketball = Convert.ToInt32(oleRead["Basketball"]);
                ps.Other = Convert.ToInt32(oleRead["Other"]);
                ps.OtherFootballLottery = Convert.ToInt32(oleRead["OtherFootballLottery"]);
                ps.PagePrint = Convert.ToInt32(oleRead["PagePrint"]);
                ps.PagePrintSetting = Convert.ToInt32(oleRead["PagePrintSetting"]);
                ps.PagePrintDouble = Convert.ToInt32(oleRead["PagePrintDouble"]);
                ps.SwitchDouble = Convert.ToInt32(oleRead["SwitchDouble"]);
            }
            return ps;
        }
        /// <summary>
        /// 读取福彩设置
        /// </summary>
        /// <param name="i">0:默认设置,1:还原上次保存的设置</param>
        /// <returns></returns>
        public WelfareLottery GetWelfareLottery(int i)
        {
            WelfareLottery ps = new WelfareLottery();
            OleDbDataReader oleRead = null;
            if (i == 0)
            {
                oleRead = DAL.OLEDBHelp.GetReader("select * from WelfareLotteryTable where ID=1");
            }
            else if (i == 1)
            {
                oleRead = DAL.OLEDBHelp.GetReader("select * from WelfareLotteryTable where ID=2");
            }
            while (oleRead.Read())
            {
                ps.IssueF = Convert.ToInt32(oleRead["IssueF"]);
                ps.Delay = Convert.ToInt32(oleRead["Delay"]);
                ps.CompoundTime = Convert.ToInt32(oleRead["CompoundTime"]);
                ps.Compound = Convert.ToInt32(oleRead["Compound"]);
                ps.UnionLotto = Convert.ToInt32(oleRead["UnionLotto"]);
                ps.BeforePage = Convert.ToInt32(oleRead["BeforePage"]);
                ps.BPageExportOne = Convert.ToInt32(oleRead["BPageExportOne"]);
                ps.BPageExportTow = Convert.ToInt32(oleRead["BPageExportTow"]);
                ps.BPageExportThree = Convert.ToInt32(oleRead["BPageExportThree"]);
                ps.StakesPage = Convert.ToInt32(oleRead["StakesPage"]);
                ps.SEachnoteOne = Convert.ToInt32(oleRead["SEachnoteOne"]);
                ps.SEachnoteTow = Convert.ToInt32(oleRead["SEachnoteTow"]);
                ps.SEachnoteThree = Convert.ToInt32(oleRead["SEachnoteThree"]);
                ps.TicketAfter = Convert.ToInt32(oleRead["TicketAfter"]);
                ps.TicketOne = Convert.ToInt32(oleRead["TicketOne"]);
                ps.TicketTow = Convert.ToInt32(oleRead["TicketTow"]);
                ps.TicketThree = Convert.ToInt32(oleRead["TicketThree"]);
                ps.PrintingState = Convert.ToInt32(oleRead["PrintingState"]);
                ps.PrintingStateCom = Convert.ToInt32(oleRead["PrintingStateCom"]);
                ps.Bet = Convert.ToInt32(oleRead["Bet"]);
            }
            return ps;
        }
        /// <summary>
        /// 是否成功保存打印设置的方法
        /// </summary>
        /// <param name="prs">打印设置对象</param>
        /// <returns></returns>
        public int IsSavePrintingSetting(PrintingSetting prs)
        {
            int i = -1;
            if (SavePrintingSetting(prs) >= 0)
            {
                i = 1;
            }
            return i;
        }
        /// <summary>
        /// 是否成功保存体彩设置的方法
        /// </summary>
        /// <param name="prs">体彩设置对象</param>
        /// <returns></returns>
        public int IsSaveSportsLotterySetting(SportsLotterySetting prs)
        {
            int j = -1;
            if (SaveSportsLotterySetting(prs) >= 0)
            {
                j = 1;
            }
            return j;
        }
        /// <summary>
        /// 是否成功保存福彩的方法
        /// </summary>
        /// <param name="prs">福彩设置对象</param>
        /// <returns></returns>
        public int IsSaveWelfareLottery(WelfareLottery prs)
        {
            int k = -1;
            if (SaveWelfareLottery(prs) >= 0)
            {
                k = 1;
            }
            return k;
        }
        /// <summary>
        /// 保存打印设置
        /// </summary>
        /// <param name="prs">打印设置对象</param>
        /// <returns></returns>
        public int SavePrintingSetting(PrintingSetting prs)
        {
            int i = -1;
            string sqlPrintingSetting = "update PrintingSettingTable set [Printer]=@Printer,[PortName]=@PortName,[PrinterSettingP]=@PrinterSettingP,[PrinterPrompt]=@PrinterPrompt,[PromptType]=@PromptType,[DelayF]=@DelayF,[IssueWaitF]=@IssueWaitF,[EveryDelay]=@EveryDelay,[IssueWaitD]=@IssueWaitD,Logo=1 where ID=2";
            OleDbParameter[] oldeps = new OleDbParameter[] { 
            new OleDbParameter("@Printer",prs.Printer),
            new OleDbParameter("@PortName",prs.PortName),
            new OleDbParameter("@PrinterSettingP",prs.PrinterSettingP),
            new OleDbParameter("@PrinterPrompt",prs.PrinterPrompt),
            new OleDbParameter("@PromptType",prs.PromptType),
            new OleDbParameter("@DelayF",prs.DelayF),
            new OleDbParameter("@IssueWaitF",prs.IssueWaitF),
            new OleDbParameter("@EveryDelay",prs.EveryDelay),
            new OleDbParameter("@IssueWaitD",prs.IssueWaitD),
            };
            i = DAL.OLEDBHelp.GetExecute(sqlPrintingSetting, oldeps);
            return i;
        }
        /// <summary>
        /// 保存体彩设置
        /// </summary>
        /// <param name="prs">体彩设置对象</param>
        /// <returns></returns>
        public int SaveSportsLotterySetting(SportsLotterySetting prs)
        {
            int i = -1;
            string sqlSportsLotterySetting = "update SportsLotterySettingTable set [IssueT]=@IssueT,[FootballLottery]=@FootballLottery,[Number]=@Number,[Lotto]=@Lotto,[MultiplyPrintingOne]=@MultiplyPrintingOne,[MultiplyPrintingTow]=@MultiplyPrintingTow,[MultiplyPrintingThree]=@MultiplyPrintingThree,[RowOfThree]=@RowOfThree,[Basketball]=@Basketball,[Other]=@Other,[OtherFootballLottery]=@OtherFootballLottery,[PagePrint]=@PagePrint,[PagePrintSetting]=@PagePrintSetting,[PagePrintDouble]=@PagePrintDouble,[SwitchDouble]=@SwitchDouble,Logo=1 where ID=2";
            OleDbParameter[] oldepls = new OleDbParameter[] { 
            new OleDbParameter("@IssueT",prs.IssueT),
            new OleDbParameter("@FootballLottery",prs.FootballLottery),
            new OleDbParameter("@Number",prs.Number),
            new OleDbParameter("@Lotto",prs.Lotto),
            new OleDbParameter("@MultiplyPrintingOne",prs.MultiplyPrintingOne),
            new OleDbParameter("@MultiplyPrintingTow",prs.MultiplyPrintingTow),
            new OleDbParameter("@MultiplyPrintingThree",prs.MultiplyPrintingThree),
            new OleDbParameter("@RowOfThree",prs.RowOfThree),
            new OleDbParameter("@Basketball",prs.Basketball),
            new OleDbParameter("@Other",prs.Other),
            new OleDbParameter("@OtherFootballLottery",prs.OtherFootballLottery),
            new OleDbParameter("@PagePrint",prs.PagePrint),
            new OleDbParameter("@PagePrintSetting",prs.PagePrintSetting),
            new OleDbParameter("@PagePrintDouble",prs.PagePrintDouble),
            new OleDbParameter("@SwitchDouble",prs.SwitchDouble)
            };
            i = DAL.OLEDBHelp.GetExecute(sqlSportsLotterySetting, oldepls);
            return i;
        }
        /// <summary>
        /// 保存福彩设置
        /// </summary>
        /// <param name="prs">福彩设置对象</param>
        /// <returns></returns>
        public int SaveWelfareLottery(WelfareLottery prs)
        {
            int i = -1;
            string sqlWelfareLottery = "update WelfareLotteryTable set [IssueF]=@IssueF,[Delay]=@Delay,[CompoundTime]=@CompoundTime,[Compound]=@Compound,[UnionLotto]=@UnionLotto,[BeforePage]=@BeforePage,[BPageExportOne]=@BPageExportOne,[BPageExportTow]=@BPageExportTow,[BPageExportThree]=@BPageExportThree,[StakesPage]=@StakesPage,[SEachnoteOne]=@SEachnoteOne,[SEachnoteTow]=@SEachnoteTow,[SEachnoteThree]=@SEachnoteThree,[TicketAfter]=@TicketAfter,[TicketOne]=@TicketOne,[TicketTow]=@TicketTow,[TicketThree]=@TicketThree,[PrintingState]=@PrintingState,[PrintingStateCom]=@PrintingStateCom,[Bet]=@Bet,Logo=1 where ID=2";
            OleDbParameter[] oldepls = new OleDbParameter[] { 
            new OleDbParameter("@IssueF",prs.IssueF),
            new OleDbParameter("@Delay",prs.Delay),
            new OleDbParameter("@CompoundTime",prs.CompoundTime),
            new OleDbParameter("@Compound",prs.Compound),
            new OleDbParameter("@UnionLotto",prs.UnionLotto),
            new OleDbParameter("@BeforePage",prs.BeforePage),
            new OleDbParameter("@BPageExportOne",prs.BPageExportOne),
            new OleDbParameter("@BPageExportTow",prs.BPageExportTow),
            new OleDbParameter("@BPageExportThree",prs.BPageExportThree),
            new OleDbParameter("@StakesPage",prs.StakesPage),
            new OleDbParameter("@SEachnoteOne",prs.SEachnoteOne),
            new OleDbParameter("@SEachnoteTow",prs.SEachnoteTow),
            new OleDbParameter("@SEachnoteThree",prs.SEachnoteThree),
            new OleDbParameter("@TicketAfter",prs.TicketAfter),
            new OleDbParameter("@TicketOne",prs.TicketOne),
            new OleDbParameter("@TicketTow",prs.TicketTow),
            new OleDbParameter("@TicketThree",prs.TicketThree),
            new OleDbParameter("@PrintingState",prs.PrintingState),
            new OleDbParameter("@PrintingStateCom",prs.PrintingStateCom),
            new OleDbParameter("@Bet",prs.Bet)//20
            };
            i = DAL.OLEDBHelp.GetExecute(sqlWelfareLottery, oldepls);
            return i;
        }
    }
}
