using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DAL
{
    public static class SerialPortKeyDictionary
    {
       private static Dictionary<string, string> KeyDictionary = new Dictionary<string, string>();
        public static void SaveSerialPortKeyDictionary()
        {
            KeyDictionary.Add("PRINT", "14-4D-F0-4D-F0-14");//打印码组合
            KeyDictionary.Add("A", "1C-F0-1C");//--
            KeyDictionary.Add("B", "32-F0-32");
            KeyDictionary.Add("C", "21-F0-21");
            KeyDictionary.Add("D", "23-F0-23");
            KeyDictionary.Add("E", "34-F0-34");
            KeyDictionary.Add("F", "2B-F0-2B");
            KeyDictionary.Add("G", "34-F0-34");
            KeyDictionary.Add("H", "33-F0-33");
            KeyDictionary.Add("I", "43-F0-43");
            KeyDictionary.Add("J", "3B-F0-3B");
            KeyDictionary.Add("K", "42-F0-42");
            KeyDictionary.Add("L", "4B-F0-4B");
            KeyDictionary.Add("M", "3A-F0-3A");
            KeyDictionary.Add("N", "31-F0-31");
            KeyDictionary.Add("O", "44-F0-44");
            KeyDictionary.Add("P", "4D-F0-4D");
            KeyDictionary.Add("Q", "15-F0-15");
            KeyDictionary.Add("R", "2D-F0-2D");
            KeyDictionary.Add("S", "1B-F0-1B");
            KeyDictionary.Add("T", "2C-F0-2C");
            KeyDictionary.Add("U", "3C-F0-3C");
            KeyDictionary.Add("V", "2A-F0-2A");
            KeyDictionary.Add("W", "1D-F0-1D");
            KeyDictionary.Add("X", "22-F0-22");
            KeyDictionary.Add("Y", "35-F0-35");
            KeyDictionary.Add("Z", "1A-F0-1A");
            KeyDictionary.Add("0", "45-F0-45");//--Number+
            KeyDictionary.Add("1", "16-F0-16");
            KeyDictionary.Add("2", "1E-F0-1E");
            KeyDictionary.Add("3", "26-F0-26");
            KeyDictionary.Add("4", "25-F0-25");
            KeyDictionary.Add("5", "2E-F0-2E");
            KeyDictionary.Add("6", "36-F0-36");
            KeyDictionary.Add("7", "3D-F0-3D");
            KeyDictionary.Add("8", "3E-F0-3E");
            KeyDictionary.Add("9", "46-F0-46");
            KeyDictionary.Add("`", "0E-F0-0E");
            KeyDictionary.Add("-", "4E-F0-4E");
            KeyDictionary.Add("=", "55-F0-55");
            KeyDictionary.Add("\\", "5D-F0-5D");
            KeyDictionary.Add("BKSP", "66-F0-66");//-----NOW
            KeyDictionary.Add("SPACE", "29-F0-29");
            KeyDictionary.Add("TAB", "0D-F0-0D");
            KeyDictionary.Add("CAPS", "58-F0-58");
            KeyDictionary.Add("L SHFT", "12-F0-12");
            KeyDictionary.Add("L CTRL", "14-F0-14");
            KeyDictionary.Add("L GUI", "10-1F-E0-F0-1F");
            KeyDictionary.Add("L ALT", "11-F0-11");
            KeyDictionary.Add("R SHFT", "59-F0-59");
            KeyDictionary.Add("R CTRL", "E0-14-E0-F0-14");
            KeyDictionary.Add("R GUI", "E0-27-E0-F0-27");
            KeyDictionary.Add("R ALT", "E0-11-E0-F0-11");
            KeyDictionary.Add("APPS", "E0-2F-E0-F0-2F");
            KeyDictionary.Add("ENTER", "5A-F0-5A");
            KeyDictionary.Add("ESC", "76-F0-76");
            KeyDictionary.Add("F1", "05-F0-05");//--F1~F12
            KeyDictionary.Add("F2", "06-F0-06");
            KeyDictionary.Add("F3", "04-F0-04");
            KeyDictionary.Add("F4", "0C-F0-0C");
            KeyDictionary.Add("F5", "03-F0-03");
            KeyDictionary.Add("F6", "0B-F0-0B");
            KeyDictionary.Add("F7", "83-F0-83");
            KeyDictionary.Add("F8", "0A-F0-0A");
            KeyDictionary.Add("F9", "01-F0-01");
            KeyDictionary.Add("F10", "09-F0-09");
            KeyDictionary.Add("F11", "78-F0-78");
            KeyDictionary.Add("F12", "07-F0-07");
            KeyDictionary.Add("PRNT", "E0-12-E0-F0");
            KeyDictionary.Add("SCRN", "E0-7C-7C-E0-F0-12");
            KeyDictionary.Add("SCROLL", "7E-F0-7E");
            KeyDictionary.Add("PAUSE", "E1-14-77-E1-F0-14-F0-77");
            KeyDictionary.Add("[", "54-F0-54");
            KeyDictionary.Add("INSERT", "E0-70-E0-F0-70");
            KeyDictionary.Add("HOME", "E0-6C-E0-F0-6C");
            KeyDictionary.Add("PG UP", "E0-7D-E0-F0-7D");
            KeyDictionary.Add("DELETE", "E0-71-E0-F0-71");
            KeyDictionary.Add("END", "E0-69-E0-F0-69");
            KeyDictionary.Add("PG DN", "E0-7A-E0-F0-7A");
            KeyDictionary.Add("U ARROW", "E0-75-E0-F0-75");
            KeyDictionary.Add("L ARROW", "E0-6B-E0-F0-6B");
            KeyDictionary.Add("D ARROW", "E0-72-E0-F0-72");
            KeyDictionary.Add("R ARROW", "E0-74-E0-F0-74");
            KeyDictionary.Add("NUM", "77-F0-77");
            KeyDictionary.Add("KP //", "E0-4A-E0-F0-4A");//--KP+
            KeyDictionary.Add("KP *", "7C-F0-7C");
            KeyDictionary.Add("KP -", "7B-F0-7B");
            KeyDictionary.Add("KP +", "79-F0-79");
            KeyDictionary.Add("KP EN", "E0-5A-E0-F0-5A");
            KeyDictionary.Add("KP .", "71-F0-71");
            KeyDictionary.Add("KP 0", "70-F0-70");
            KeyDictionary.Add("KP 1", "69-F0-69");
            KeyDictionary.Add("KP 2", "72-F0-72");
            KeyDictionary.Add("KP 3", "7A-F0-7A");
            KeyDictionary.Add("KP 4", "6B-F0-6B");
            KeyDictionary.Add("KP 5", "73-F0-73");
            KeyDictionary.Add("KP 6", "74-F0-74");
            KeyDictionary.Add("KP 7", "6C-F0-6C");
            KeyDictionary.Add("KP 8", "75-F0-75");
            KeyDictionary.Add("KP 9", "7D-F0-7D");
            KeyDictionary.Add("]", "5B-F0-5B");
            KeyDictionary.Add(";", "4C-F0-4C");
            KeyDictionary.Add("'", "52-F0-52");
            KeyDictionary.Add(",", "41-F0-41");
            KeyDictionary.Add(".", "49-F0-49");
            KeyDictionary.Add("//", "4A-F0-4A");
            KeyDictionary.Add("Calculator", "E0-2B-E0-F0-2B");
            KeyDictionary.Add("My Computer", "E0-40-E0-F0-40");
            KeyDictionary.Add("WWW Search", "E0-10-E0-F0-10");//--WWW+
            KeyDictionary.Add("WWW Home", "E0-3A-E0-F0-3A");
            KeyDictionary.Add("WWW Back", "E0-38-E0-F0-38");
            KeyDictionary.Add("WWW Forward", "E0-30-E0-F0-30");
            KeyDictionary.Add("WWW Stop", "E0-28-E0-F0-28");
            KeyDictionary.Add("WWW Refresh", "E0-20-E0-F0-20");
            KeyDictionary.Add("WWW Favorites", "E0-18-E0-F0-18");
            KeyDictionary.Add("Next Track", "E0-4D-E0-F0-4D");
            KeyDictionary.Add("Previous Track", "E0-15-E0-F0-15");
            KeyDictionary.Add("Stop", "E0-3B-E0-F0-3B");
            KeyDictionary.Add("Play/Pause", "E0-34-E0-F0-34");
            KeyDictionary.Add("Mute", "E0-23-E0-F0-23");
            KeyDictionary.Add("Volume Up", "E0-32-E0-F0-32");
            KeyDictionary.Add("Volume Down", "E0-21-E0-F0-21");
            KeyDictionary.Add("Media Select", "E0-50-E0-F0-50");
            KeyDictionary.Add("E-Mail", "E0-48-E0-F0-48");
            KeyDictionary.Add("Power", "E0-37-E0-F0-37");
            KeyDictionary.Add("Sleep", "E0-3F-E0-F0-3F");
            KeyDictionary.Add("Wake", "E0-5E-E0-F0-5E");
            KeyDictionary.Add("Shift+~", "12-0E-F0h-0E-F0-12");//Shift+符号~!@#$%^&*()_+|{}:”<>?
            KeyDictionary.Add("Shift+!", "12-16-F0h-16-F0-12");
            KeyDictionary.Add("Shift+@", "12-1E-F0h-1E-F0-12");
            KeyDictionary.Add("Shift+#", "12-26-F0h-26-F0-12");
            KeyDictionary.Add("Shift+$", "12-25-F0h-25-F0-12");
            KeyDictionary.Add("Shift+%", "12-2E-F0h-2E-F0-12");
            KeyDictionary.Add("Shift+^", "12-36-F0h-36-F0-12");
            KeyDictionary.Add("Shift+&", "12-3D-F0h-3D-F0-12");
            KeyDictionary.Add("Shift+*", "12-3E-F0h-3E-F0-12");
            KeyDictionary.Add("Shift+(", "12-49-F0h-49-F0-12");
            KeyDictionary.Add("Shift+)", "12-45-F0h-45-F0-12");
            KeyDictionary.Add("Shift+_", "12-4E-F0h-4E-F0-12");
            KeyDictionary.Add("Shift++^", "12-55-F0h-55-F0-12");
            KeyDictionary.Add("Shift+|", "12-5D-F0h-5D-F0-12");
            KeyDictionary.Add("Shift+{", "12-54-F0h-54-F0-12");
            KeyDictionary.Add("Shift+}", "12-5B-F0h-5B-F0-12");
            KeyDictionary.Add("Shift+:^", "12-4C-F0h-4C-F0-12");
            KeyDictionary.Add("Shift+\"", "12-5D-F0h-5D-F0-12");
            KeyDictionary.Add("Shift+<", "12-41-F0h-41-F0-12");
            KeyDictionary.Add("Shift+>^", "12-49-F0h-49-F0-12");
            KeyDictionary.Add("Shift+?", "12-4A-F0h-4A-F0-12");
            //
            //KeyDictionary.Add("A", "14-12-76-F0-76-F0-12-F0-14");
        }
        public static byte[] ReturnSerialPortKey(string inputStr)
        {
            SaveSerialPortKeyDictionary();
            byte[] bty = null;
            string paras = "";
            if (KeyDictionary.TryGetValue(inputStr.ToString(), out paras))
            {
                string[] strSplit = paras.Split('-');
                bty = new byte[strSplit.Length];
                for (int i = 0; i < strSplit.Length; i++)
                {
                    bty[i] = Convert.ToByte(strSplit[i].ToString(), 16);
                }
            }
            KeyDictionary.Clear();
            return bty;
        }

        
    }
}
