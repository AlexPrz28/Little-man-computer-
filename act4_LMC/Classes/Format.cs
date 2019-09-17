using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using act4_LMC.Classes;


namespace act4_LMC.Classes
{
    public static class Format
    {
        public static string final = "";
        public static void consolePrinter()
        {
            string console = "Program counter is at " + LMC.programCounter.ToString();
            LMC.consoleList.Add(console);
            console = "Reading mailbox " + LMC.programCounter.ToString();
            LMC.consoleList.Add(console);
            console = "Instruction: " + LMC.mailboxes[LMC.programCounter].ToString();
            LMC.consoleList.Add(console);
            console = "Mnemonic: " + LMC.operation;
            LMC.consoleList.Add(console);
            LMC.consoleList.Add("");
        }

        public static string splitMailbox(string str, bool isAddress)
        {
            if (isAddress)
            {
                final = str.Substring(1, 2);
            }
            else
            {
                final = str.Substring(0, 1);
            }
            return final;
        }

        public static string formatNum(string content)
        {
            while (content.Length < 3)
            {
                content = content.PadLeft(3, '0');
            }
            return content;
        }

        public static string formatIC(string IC)
        {
            while (IC.Length < 2)
            {
                IC = IC.PadLeft(2, '0');
            }
            return IC;
        }
    }
}
