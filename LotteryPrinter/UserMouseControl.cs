using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace LotteryPrinter
{
    public partial class UserMouseControl : UserControl
    {
        public UserMouseControl()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll", EntryPoint = "LoadCursorFromFile")]
        public static extern IntPtr IntLoadCursorFromFile(string IpFileName);
        public Cursor MouseImage(string pathStr)
        {
            Cursor myCursors = new Cursor(Cursor.Current.Handle);
            IntPtr colorCursor = IntLoadCursorFromFile(pathStr);
            myCursors.GetType().InvokeMember("handle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, myCursors, new object[] { colorCursor });
            return myCursors;
        }

    }
}
