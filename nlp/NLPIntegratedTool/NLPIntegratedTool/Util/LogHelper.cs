using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPIntegratedTool.Util
{
    public class LogHelper
    {
        private static System.Windows.Forms.TextBox LogTb;

        public static void Init(System.Windows.Forms.TextBox logTb)
        {
            LogTb = logTb;
        }

        public static void Log(string log)
        {
            LogTb.Text += log + "\r\n";
        }

        public static void Enter()
        {
            LogTb.Text += "\r\n";
        }
    }
}
