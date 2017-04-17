using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Model
{
   public class PrintingSetting
    {

        private int printer;

        public int Printer
        {
            get { return printer; }
            set { printer = value; }
        }
        private int portName;

        public int PortName
        {
            get { return portName; }
            set { portName = value; }
        }
        private int printerSettingP;

        public int PrinterSettingP
        {
            get { return printerSettingP; }
            set { printerSettingP = value; }
        }
        private int printerPrompt;

        public int PrinterPrompt
        {
            get { return printerPrompt; }
            set { printerPrompt = value; }
        }
        private int promptType;

        public int PromptType
        {
            get { return promptType; }
            set { promptType = value; }
        }
        private int delayF;

        public int DelayF
        {
            get { return delayF; }
            set { delayF = value; }
        }
        private int issueWaitF;

        public int IssueWaitF
        {
            get { return issueWaitF; }
            set { issueWaitF = value; }
        }
        private int everyDelay;

        public int EveryDelay
        {
            get { return everyDelay; }
            set { everyDelay = value; }
        }
        private int issueWaitD;

        public int IssueWaitD
        {
            get { return issueWaitD; }
            set { issueWaitD = value; }
        }
    }
}
