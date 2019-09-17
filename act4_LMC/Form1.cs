using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using act4_LMC.Classes;
using Microsoft.VisualBasic;
using System.IO;

namespace act4_LMC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LMC.calculator = int.Parse(lblCalc.Text);
            LMC.programCounter = int.Parse(txtIC.Text);
            LMC.positiveFlag = true;
            LMC.zeroFlag = true;
            LMC.interruptFlag = false;
                
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox x = (TextBox)ctrl;
                    x.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool status = true;

        public void run()
        {
            GC.Collect();
            int opCode = int.Parse(Format.splitMailbox(LMC.mailboxes[LMC.programCounter].ToString(), false));
            LMC.checkOp(opCode);
            Format.consolePrinter();
            updateTextBoxes();
            LMC.checkZF();
            updateForm();

            if (LMC.operation == "Branch" || LMC.operation == "Branch if Zero (True)" || LMC.operation == "Branch if Positive (True)" || LMC.operation == "Interrupt Ended")
            {
                LMC.programCounter--;
            }

            if (LMC.operation != "Halt")
                LMC.programCounter++;
            if (LMC.operation == "Halt")
                MessageBox.Show("LMC execution has ended.");
        }
        private void BttnRun_Click(object sender, EventArgs e)
        {
            if (status)
            {
                initForm();
            }
            status = false;

            while (LMC.operation != "Halt")
            {
                run();
            }
        }

        private void BttnSBS_Click(object sender, EventArgs e)
        {
            if (status)
            {
                initForm();
            }
            run();
            status = false;
        }

        private void updateForm()
        {
            lblCalc.Text = Format.formatNum(LMC.calculator.ToString());
            txtIC.Text = Format.formatIC(LMC.programCounter.ToString());

            foreach (string text in LMC.consoleList)
            {
                lstConsole.Items.Add(text);
            }

            LMC.consoleList.Clear();

            foreach(string text in LMC.outputList)
            {
                lstOutput.Items.Add(Format.formatNum(text));
            }

            LMC.outputList.Clear();

            if (!LMC.positiveFlag)
            {
                txtPF.BackColor = Color.Red;
            }
            else
            {
                txtPF.BackColor = Color.Lime;
            }

            if (LMC.zeroFlag)
            {
                txtZF.BackColor = Color.Lime;
            }
            else
            {
                txtZF.BackColor = Color.Red;
            }

            if (!LMC.interruptFlag)
            {
                txtIF.BackColor = Color.Red;
            }
            else
            {
                txtIF.BackColor = Color.Lime;
            }

            lblInput.Text = LMC.userInput.ToString();
        }

        private void updateTextBoxes()
        {
            foreach(TextBox txt in LMC.txtBxList)
            {
                txt.Text = Format.formatNum(LMC.mailboxes[int.Parse(txt.Name.Substring(3, 2))].ToString());
            }
        }

        private void updateMailboxes()
        {
            foreach (TextBox txt in LMC.txtBxList)
            {
                LMC.mailboxes[int.Parse(txt.Name.Substring(3, 2))] = int.Parse(txt.Text);
            }
        }

        private void initForm()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox x = (TextBox)ctrl;
                    if (x.Name != "txtInput" && x.Name != "txtConsole" && x.Name != "txtIC" && x.Name != "txtIF" && x.Name != "txtZF" && x.Name != "txtPF" && x.Name != "txtOutput")
                    {
                        LMC.mailboxes.Add(int.Parse(x.Text));
                        LMC.txtBxList.Add(x);
                    }
                }
            }
        }

        private void BttnReset_Click(object sender, EventArgs e)
        {
            foreach (TextBox x in LMC.txtBxList)
            {
                x.Text = "000";
            }

            lstConsole.Items.Clear();
            lstOutput.Items.Clear();
            lblCalc.Text = "000";
            lblInput.Text = "";
            txtIC.Text = "00";
            txtPF.BackColor = Color.Lime;
            txtZF.BackColor = Color.Lime;
            txtIF.BackColor = Color.Red;

            LMC.operation = "";
            LMC.programCounter = 0;
            LMC.calculator = 0;
            LMC.mailboxes.Clear();
            LMC.txtBxList.Clear();
            LMC.consoleList.Clear();
            LMC.interruptFlag = false;
            LMC.zeroFlag = true;
            LMC.positiveFlag = true;
            LMC.savedPC = 0;

            status = true;
        }

        private void BttnInterrupt_Click(object sender, EventArgs e)
        {
            LMC.interruptFlag = true;
            LMC.programCounter = LMC.interruptHandler();
            LMC.calculator = 0;

            while (LMC.interruptFlag)
            {
                run();
            }

            LMC.interruptFlag = true; //Turned on just to see it flick to green.
            updateForm();
            LMC.interruptFlag = false; //Get it back to red.
        }
    }
}
