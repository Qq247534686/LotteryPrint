using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DAL
{
    public class retrieveData
    {
        Dictionary<string, string> dicsPlay = null;
        Dictionary<string, string> dicsPlayType = null;
        Dictionary<string, string> dicsLottery = null;
        Dictionary<string, string> discPlayText = null;
        #region 构造方法
        public retrieveData()
        {
            //福利和体育的编码
            dicsLottery = new Dictionary<string, string>();
            dicsLottery.Add("F", "中国福利彩票");
            dicsLottery.Add("T", "中国体育彩票");
            //玩法对应编码
            dicsPlay = new Dictionary<string, string>();
            dicsPlay.Add("1*1", "01");
            dicsPlay.Add("2*1", "02");
            dicsPlay.Add("3*1", "02");
            dicsPlay.Add("3*3", "03");
            dicsPlay.Add("3*4", "04");
            dicsPlay.Add("4*1", "02");
            dicsPlay.Add("4*4", "03");
            dicsPlay.Add("4*5", "04");
            dicsPlay.Add("4*6", "05");
            dicsPlay.Add("4*11", "06");
            dicsPlay.Add("5*1", "02");
            dicsPlay.Add("5*5", "03");
            dicsPlay.Add("5*6", "04");
            dicsPlay.Add("5*10", "05");
            dicsPlay.Add("5*16", "06");
            dicsPlay.Add("5*20", "07");
            dicsPlay.Add("5*26", "08");
            dicsPlay.Add("6*1", "02");
            dicsPlay.Add("6*6", "03");
            dicsPlay.Add("6*7", "04");
            dicsPlay.Add("6*15", "05");
            dicsPlay.Add("6*20", "06");
            dicsPlay.Add("6*22", "07");
            dicsPlay.Add("6*35", "08");
            dicsPlay.Add("6*42", "09");
            dicsPlay.Add("6*50", "10");
            dicsPlay.Add("6*57", "11");
            dicsPlay.Add("7*1", "02");
            dicsPlay.Add("7*7", "03");
            dicsPlay.Add("7*8", "04");
            dicsPlay.Add("7*21", "05");
            dicsPlay.Add("7*35", "06");
            dicsPlay.Add("7*120", "07");
            dicsPlay.Add("8*1", "02");
            dicsPlay.Add("8*8", "03");
            dicsPlay.Add("8*9", "04");
            dicsPlay.Add("8*28", "05");
            dicsPlay.Add("8*56", "06");
            dicsPlay.Add("8*70", "07");
            dicsPlay.Add("8*247", "08");
            //竞彩足球玩法对应编码
            dicsPlayType = new Dictionary<string, string>();
            //足球
            dicsPlayType.Add("JC_A", "9006");
            dicsPlayType.Add("JC_B", "9003");
            dicsPlayType.Add("JC_C", "9002");
            dicsPlayType.Add("JC_D", "9004");
            dicsPlayType.Add("JC_E", "9005");
            dicsPlayType.Add("JC_F", "9001");
            //篮球
            dicsPlayType.Add("JC_G", "9101");
            dicsPlayType.Add("JC_H", "9102");
            dicsPlayType.Add("JC_I", "9103");
            dicsPlayType.Add("JC_J", "9104");
            dicsPlayType.Add("JC_K", "9105");
            dicsPlayType.Add("JC_L", "大乐透");
            dicsPlayType.Add("JC_M", "排三");
            dicsPlayType.Add("JC_N", "排五");
            discPlayText = new Dictionary<string, string>();
            discPlayText.Add("9006", "竞彩足球,胜平负");
            discPlayText.Add("9003", "竞彩足球,比分");
            discPlayText.Add("9002", "竞彩足球,总进球数");
            discPlayText.Add("9004", "竞彩足球,半全场胜平负");
            discPlayText.Add("9005", "竞彩足球,混合过关");
            discPlayText.Add("9001", "竞彩足球,让球胜平负");
            //篮球
            discPlayText.Add("9101", "竞彩篮球,让分胜负");
            discPlayText.Add("9102", "竞彩篮球,胜负");
            discPlayText.Add("9103", "竞彩篮球,胜分差");
            discPlayText.Add("9104", "竞彩篮球,大小分");
            discPlayText.Add("9105", "竞彩篮球,混合过关");
            discPlayText.Add("排三", "排三");
            discPlayText.Add("排五", "排五");



        }
        #endregion
        public string transmissionDiscPlayText(string strbut)
        {
            string strOut = string.Empty;
            discPlayText.TryGetValue(strbut.ToString(), out strOut);
            return strOut;
        }
        //
        public string transmissionDicsLotteryData(string strbut)
        {
            string strOut = string.Empty;
            dicsLottery.TryGetValue(strbut.ToString(), out strOut);
            return strOut;
        }
        //
        public string transmissionDicsPlayTypeData(string strbut)
        {
            string strOut = string.Empty;
            dicsPlayType.TryGetValue(strbut.ToString(), out strOut);
            return strOut;
        }
        public string transmissionDicsPlay(string strbut)
        {
            string strOut = string.Empty;
            dicsPlay.TryGetValue(strbut.ToString(), out strOut);
            return strOut;
        }
    }
}
