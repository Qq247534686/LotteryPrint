using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DAL
{
    public class selectData
    {
        Dictionary<string, string> dicsPlay = null;
        Dictionary<string, string> dicsZJ = null;
        public selectData()
        {
            dicsPlay = new Dictionary<string, string>();
            dicsZJ = new Dictionary<string, string>();
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
            //dicsPlay.Add("7*127", "34");
            dicsPlay.Add("8*1", "02");
            dicsPlay.Add("8*8", "03");
            dicsPlay.Add("8*9", "04");
            dicsPlay.Add("8*28", "05");
            dicsPlay.Add("8*56", "06");
            dicsPlay.Add("8*70", "07");
            dicsPlay.Add("8*247", "08");
            //zj
            dicsZJ.Add("1*1", "0");
            dicsZJ.Add("2*1", "0");
            dicsZJ.Add("3*1", "0");
            dicsZJ.Add("3*3", "1");
            dicsZJ.Add("3*4", "1");
            dicsZJ.Add("4*1", "0");
            dicsZJ.Add("4*4", "1");
            dicsZJ.Add("4*5", "1");
            dicsZJ.Add("4*6", "2");
            dicsZJ.Add("4*11", "2");
            dicsZJ.Add("5*1", "0");
            dicsZJ.Add("5*5", "1");
            dicsZJ.Add("5*6", "1");
            dicsZJ.Add("5*10", "3");
            dicsZJ.Add("5*16", "2");
            dicsZJ.Add("5*20", "3");
            dicsZJ.Add("5*26", "3");
            dicsZJ.Add("6*1", "0");
            dicsZJ.Add("6*6", "1");
            dicsZJ.Add("6*7", "1");
            dicsZJ.Add("6*15", "4");
            dicsZJ.Add("6*20", "3");
            dicsZJ.Add("6*22", "2");
            dicsZJ.Add("6*35", "4");
            dicsZJ.Add("6*42", "3");
            dicsZJ.Add("6*50", "4");
            dicsZJ.Add("6*57", "4");
            dicsZJ.Add("7*1", "0");
            dicsZJ.Add("7*7", "1");
            dicsZJ.Add("7*8", "1");
            dicsZJ.Add("7*21", "2");
            dicsZJ.Add("7*35", "3");
            dicsZJ.Add("7*120", "5");
            dicsZJ.Add("8*1", "0");
            dicsZJ.Add("8*8", "1");
            dicsZJ.Add("8*9", "1");
            dicsZJ.Add("8*28", "2");
            dicsZJ.Add("8*56", "3");
            dicsZJ.Add("8*70", "4");
            dicsZJ.Add("8*247", "6");
        }
        public string SelectStr(string pass_A_Barrier)
        {
            string str = string.Empty;
            dicsPlay.TryGetValue(pass_A_Barrier, out str);
            return str;
        }
        public string SelectStrZJ(string pass_A_Barrier)
        {
            string str = string.Empty;
            dicsZJ.TryGetValue(pass_A_Barrier, out str);
            return str;
        }
    }
}
