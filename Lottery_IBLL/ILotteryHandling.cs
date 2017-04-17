using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_IBLL
{
    public interface ILotteryHandling
    {
        SerialPort port3{get;set;}
        void PrinterNumber(string matchNumber);
        void TeDingKey(string TheKey);
        void PanDuanStrID(string strID);
        void BeiShu(string beiShu);
        void ChuLi(string strID,string[] chuLi);
        void GuoGuan(string guoGuan);
    }
}
