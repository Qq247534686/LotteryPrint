using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.BLL
{
    public class DaLeTou : LotteryBaseDal
    {
        public DaLeTou(SerialPort port3)
        {
            this.port3 = port3;
        }
        public override void ChuLi(string strID, string[] chuLi)
        {
            switch (strID)
            {
                case "3901":
                    ExecuteNumber_3901(chuLi);
                    break;
                case "3902":
                    ExecuteNumber_3902(chuLi);
                    break;
                case "3903":
                    ExecuteNumber_3903(chuLi);
                    break;
                case "3904":
                    ExecuteNumber_3904(chuLi);
                    break;
                case "3907":
                    ExecuteNumber_3907(chuLi);
                    break;
                case "3908":
                    ExecuteNumber_3908(chuLi);
                    break;
                default: break;
            }
        }
        public void ExecuteNumber_3901(string[] chuLi)
        {
            //04 11 14 28 33-05 08;04 09 28 32 34-08 11
            string[] LinShiCun = null; string[] LinShiFan = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split('-');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    LinShiFan = LinShiCun[j].Split(' ');
                    for (int k = 0; k < LinShiFan.Length; k++)
                    {
                        PrinterNumber(LinShiFan[k]);
                    }
                }
            }
        }
        public void ExecuteNumber_3902(string[] chuLi)
        {
            //"03 08 12 15 19 28 35-02 07 08"
            string[] LinShiCun = null; string[] LinShiFan = null;
            TeDingKey("F1");
            TeDingKey("D ARROW");
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split('-');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    LinShiFan = LinShiCun[j].Split(' ');
                    for (int k = 0; k < LinShiFan.Length; k++)
                    {
                        PrinterNumber(LinShiFan[k]);
                    }
                    TeDingKey("D ARROW");
                    TeDingKey("D ARROW");
                }
                
            }
        }
        public void ExecuteNumber_3903(string[] chuLi)
        {
            //"08 12 15 19 35-02 07;08 12 15 19 28-02 07;08 12 15 19 28-02 07"
            string[] LinShiCun = null; string[] LinShiFan = null;
            TeDingKey("KP +");
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split('-');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    LinShiFan = LinShiCun[j].Split(' ');
                    for (int k = 0; k < LinShiFan.Length; k++)
                    {
                        PrinterNumber(LinShiFan[k]);
                    }
                }
            }
        }
        public void ExecuteNumber_3904(string[] chuLi)
        {
            string[] LinShiCun = null; string[] LinShiFan = null;
            TeDingKey("F1");
            TeDingKey("KP +");
            TeDingKey("D ARROW");
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split('-');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    LinShiFan = LinShiCun[j].Split(' ');
                    for (int k = 0; k < LinShiFan.Length; k++)
                    {
                        PrinterNumber(LinShiFan[k]);
                    }
                    TeDingKey("D ARROW");
                    TeDingKey("D ARROW");
                }
            }
        }
        public void ExecuteNumber_3907(string[] chuLi)
        {
            //"06 18$08 12 15 19 35-02$07 08 11"
            string[] LinShiCun = null; string[] LinShiFan = null; string[] LinShiShow = null;
            TeDingKey("F1");
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split('-');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    LinShiFan = LinShiCun[j].Split('$');
                    for (int k = 0; k < LinShiFan.Length; k++)
                    {
                        LinShiShow = LinShiFan[k].Split(' ');
                        for (int p = 0; p < LinShiShow.Length; p++)
                        {
                            PrinterNumber(LinShiShow[p]);
                        }
                        TeDingKey("D ARROW");
                    }
                }
            }
        }
        public void ExecuteNumber_3908(string[] chuLi)
        {
            //"06 18$08 12 15 19 35-02$07 08 11"
            string[] LinShiCun = null; string[] LinShiFan = null; string[] LinShiShow = null;
            TeDingKey("F1");
            TeDingKey("KP +");
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split('-');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    LinShiFan = LinShiCun[j].Split('$');
                    for (int k = 0; k < LinShiFan.Length; k++)
                    {
                        LinShiShow = LinShiFan[k].Split(' ');
                        for (int p = 0; p < LinShiShow.Length; p++)
                        {
                            PrinterNumber(LinShiShow[p]);
                        }
                        TeDingKey("D ARROW");
                    }
                }
            }
        }
    }
}
