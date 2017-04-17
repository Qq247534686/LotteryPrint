using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             9101:RSF---让分胜负>RSF|1006212301=1/2,1006212302=1/2|2*1 OK
             9002:SFC---胜负>SFC|1006212301=1/2|2*1 OK
             9103:SFD---胜分差>SFD|1006212301=01/11|2*1  OK
             9104:DXF---大小分>DXF|1006212301=1/2|2*1  OK
             9105:HH ---混合>
             */
            string str = "HH|DXF>160621302=1/2,SFD>160621301=11/01,SFC>160621301=1/2,RSF>160621301=1/2";
            Class1 s = new Class1();
            Console.WriteLine(s.SelectUrl("9105", str));
        }
    }
}
