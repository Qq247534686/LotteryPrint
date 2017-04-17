using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.BLL
{
    public class PaiWu : LotteryBaseDal
    {
        public PaiWu(SerialPort port3)
        {
            this.port3 = port3;
        }
        public override void ChuLi(string strID, string[] chuLi)
        {
            switch (strID)
            {
                case "6401":
                    ExecuteNumber_6401(chuLi);
                    break;
                case "6402":
                    ExecuteNumber_6402(chuLi);
                    break;
                default: break;
            }
        }
        public void ExecuteNumber_6401(string[] chuLi)
        {
            //"1,2,3,4,5;1,2,3,4,6"
            string[] LinShiCun = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split(',');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    PrinterNumber(LinShiCun[j]);
                }
            }
        }
        public void ExecuteNumber_6402(string[] chuLi)
        {
            //"9,1,2,3,3456"
            TeDingKey("F1");
            string[] LinShiCun = null;
            for (int i = 0; i < chuLi.Length; i++)
            {
                LinShiCun = chuLi[i].Split(',');
                for (int j = 0; j < LinShiCun.Length; j++)
                {
                    PrinterNumber(LinShiCun[j]);
                    TeDingKey("D ARROW");
                }
            }
        }
    }
}
