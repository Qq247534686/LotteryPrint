using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.DAL;
using Lottery.Model;
namespace Lottery.BLL
{
    public static class ControlSerialPortKey
    {
        public static byte[] GetSerialPortKey(string str)
        {
            byte[] bty=null;
            bty = DAL.SerialPortKeyDictionary.ReturnSerialPortKey(str);
            return bty;
        }
    }
}
