using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace act4_LMC.Classes
{
    public static class Functions
    {
        public static void add(int pos) //1xx
        {
            LMC.operation = "Add";
            LMC.calculator += int.Parse(LMC.mailboxes[pos].ToString());
        }

        public static void sub(int pos) //2xx
        {
            LMC.operation = "Substract";

            if (LMC.calculator < int.Parse(LMC.mailboxes[pos].ToString()))
            {
                LMC.positiveFlag = false;
                //LMC.calculator -= int.Parse(LMC.mailboxes[pos].ToString()) + 1000;
                LMC.calculator = 999 - (int.Parse(LMC.mailboxes[pos].ToString()) - LMC.calculator);
            }
            else
            {
                LMC.calculator -= int.Parse(LMC.mailboxes[pos].ToString());
            }
        }

        public static void sto(int pos) //3xx
        {
            LMC.operation = "Store";
            LMC.mailboxes[pos] = LMC.calculator;
        }

        public static void lda(int pos) //5xx
        {
            LMC.operation = "Load";
            LMC.calculator = LMC.mailboxes[pos];
        }

        public static void br(int pos) //6xx
        {
            LMC.operation = "Branch";
            LMC.programCounter = pos;
        }

        public static void brz(int pos) //7xx
        {
            if (LMC.zeroFlag && LMC.calculator == 0)
            {
                LMC.operation = "Branch if Zero (True)";
                LMC.programCounter = pos;
            }
            else
            {
                LMC.operation = "Branch if Zero (False)";
            }
        }

        public static void brp(int pos) //8xx
        {
            if (LMC.positiveFlag == true && LMC.calculator >= 0)
            {
                LMC.operation = "Branch if Positive (True)";
                LMC.programCounter = pos;
            }
            else
            {
                LMC.operation = "Branch if Positive (False)";
            }
        }

        public static void input() //901
        {
            LMC.operation = "Input";
            try
            {
                string str = int.Parse(Interaction.InputBox("Enter value: ", "Input Dialog")).ToString();
                if (str.Length > 3)
                {
                    MessageBox.Show("Input is out of bounds. Please pick a number between 0 and 999.");
                    input();
                }

                LMC.userInput = int.Parse(str);
                LMC.calculator = int.Parse(str);

            }
            catch (FormatException)
            {
                MessageBox.Show("Input entered in invalid format. Please try again.");
                input();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Input was empty. Please try again.");
                input();
            }
        }

        public static void output() //902
        {
            LMC.operation = "Output";
            string item = LMC.calculator.ToString();
            LMC.outputList.Add(item);
        }

        public static void RET(int PC, int calc) //999
        {
            LMC.operation = "Interrupt Ended";
            LMC.programCounter = PC;
            LMC.calculator = calc;
            LMC.interruptFlag = false;
        }

        public static void halt() //000
        {
            LMC.operation = "Halt";
            //Nothing in here really
        }
    }
}
