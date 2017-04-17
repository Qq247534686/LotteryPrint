using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.BLL
{
    public class PaiSan : LotteryBaseDal
    {
        public PaiSan(SerialPort port3)
        {
            this.port3 = port3;
        }
        public override void ChuLi(string strID, string[] chuLi)
        {
            switch (strID)
            {
                case "6301":
                    ExecuteNumber_6301(chuLi);
                    break;
                case "6302":
                    ExecuteNumber_6302(chuLi);
                    break;
                case "6303": 
                    ExecuteNumber_6303(chuLi);
                    break;
                case "6304": 
                    ExecuteNumber_6304(chuLi);
                    break;
                case "6305":
                    ExecuteNumber_6305(chuLi);
                    break;
                default: break;
            }
        
        }
        public void ExecuteNumber_6301(string[] chuLi)
        {
            //"1|5,1,8;1|5,1,8"
            string[] LinShiCun = null; 
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split(',');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    if (LinShiCun[j].Contains("|"))
                    {
                        LinShiCun[j] = LinShiCun[j].Substring(LinShiCun[j].IndexOf('|') + 1);
                    }
                    PrinterNumber(LinShiCun[j]);
                }
            }
        }
        public void ExecuteNumber_6302(string[] chuLi)
        {

        }
        public void ExecuteNumber_6303(string[] chuLi)
        {
            string[] LinShiCun = null;
            TeDingKey("F1");
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split(',');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    if (LinShiCun[j].Contains("|"))
                    {
                        LinShiCun[j] = LinShiCun[j].Substring(LinShiCun[j].IndexOf('|') + 1);
                    }
                    PrinterNumber(LinShiCun[j]);
                }
            }
        }
        public void ExecuteNumber_6304(string[] chuLi)
        {
            TeDingKey("F2");
            PrinterNumber("04");//F6|012345
            for (int i = 0; i < chuLi.Length; i++)
            {
                if (chuLi[i].Contains("|"))
                {
                    chuLi[i] = chuLi[i].Substring(chuLi[i].IndexOf('|') + 1);
                }
                PrinterNumber(chuLi[i]);
            }
        }
        public void ExecuteNumber_6305(string[] chuLi)
        {
            TeDingKey("F2");
            PrinterNumber("03");//F6|012345
            for (int i = 0; i < chuLi.Length; i++)
            {
                if (chuLi[i].Contains("|"))
                {
                    chuLi[i] = chuLi[i].Substring(chuLi[i].IndexOf('|') + 1);
                }
                PrinterNumber(chuLi[i]);
            }
        }
    }
}
