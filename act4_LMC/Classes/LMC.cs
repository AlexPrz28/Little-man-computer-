using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace act4_LMC.Classes
{
   public static class LMC
   {
        public static List<int> mailboxes = new List<int>();
        public static List<string> outputList = new List<string>();
        public static List<string> consoleList = new List<string>();
        public static List<TextBox> txtBxList = new List<TextBox>();
        public static int calculator;
        public static int programCounter;
        public static string operation;
        public static bool positiveFlag;
        public static bool zeroFlag;
        public static bool interruptFlag;
        public static int userInput;
        public static int programOutput;
        public static int savedPC;
        public static int savedCalc;

        public static void runSBS()
        {
            int opCode = int.Parse(Format.splitMailbox(mailboxes[programCounter].ToString(), false));
            checkOp(opCode);
            Format.consolePrinter();
            checkZF();
            programCounter++;
        }

        public static void checkOp(int opCode)
        {
            switch (opCode)
            {
                case (0):
                    Functions.halt();
                    break;
                case (1):
                    Functions.add(int.Parse(Format.splitMailbox(mailboxes[programCounter].ToString(), true)));
                    break;
                case (2):
                    Functions.sub(int.Parse(Format.splitMailbox(mailboxes[programCounter].ToString(), true)));
                    break;
                case (3):
                    Functions.sto(int.Parse(Format.splitMailbox(mailboxes[programCounter].ToString(), true)));
                    break;
                case (5):
                    Functions.lda(int.Parse(Format.splitMailbox(mailboxes[programCounter].ToString(), true)));
                    break;
                case (6):
                    Functions.br(int.Parse(Format.splitMailbox(mailboxes[programCounter].ToString(), true)));
                    break;
                case (7):
                    Functions.brz(int.Parse(Format.splitMailbox(mailboxes[programCounter].ToString(), true)));
                    break;
                case (8):
                    Functions.brp(int.Parse(Format.splitMailbox(mailboxes[programCounter].ToString(), true)));
                    break;
                case (9):
                    string temp = Format.splitMailbox(mailboxes[programCounter].ToString(), true);
                    if (temp == "01")
                    {
                        Functions.input();
                    }
                    else if (temp == "02")
                    {
                        Functions.output();
                    }
                    else if (temp == "99")
                    {
                        Functions.RET(savedPC, savedCalc);
                    }
                    break;
                default:
                    MessageBox.Show("Operation could not be completed. Please try again.");
                    break;
            }
        }

        public static int interruptHandler()
        {
            savedPC = programCounter;
            savedCalc = calculator;
            try
            {
                int num = int.Parse(Interaction.InputBox("Enter address for interrupt handler: ", "Input Dialog"));
                if (num > 999 || num < 0)
                {
                    MessageBox.Show("Input is out of bounds. Please pick a number between 0 and 999.");
                    interruptHandler();
                }

                return num;
            }
            catch (FormatException)
            {
                MessageBox.Show("Input entered in invalid format. Please try again.");
                interruptHandler();
                return 0;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Input was empty. Please try again.");
                interruptHandler();
                return 0;
            }
        }


        public static void checkZF()
        {
            if (calculator == 0)
            {
                zeroFlag = true;
            }
            else
            {
                zeroFlag = false;
            }
        }
   }
}
