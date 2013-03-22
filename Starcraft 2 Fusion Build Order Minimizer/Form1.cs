using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starcraft_2_Fusion_Build_Order_Minimizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList ret = new ArrayList();
            
            string[] buildLines = inputTextBox.Lines;
            for (int i = 0; i < buildLines.Length ; i++){
                ret.Add(new Command(buildLines[i]));
            }

            try
            {

                for (int i = ret.Count - 1; i >= 0; i--)
                {
                    Command command = (Command)ret[i];
                    if (command.supply == null ||
                       (command.command.Contains("Build SCV") && IgnoreSCVCheckbox.Checked) ||
                       command.command.Contains("MULE")
                       ) ret.RemoveAt(i);
                }

                for (int i = 0; i < ret.Count; i++)
                {
                    Command command = (Command)ret[i];

                    if (command.supply == null) continue;

                    for (int j = i + 1; j < ret.Count; j++)
                    {
                        Command command2 = (Command)ret[j];
                        if (command2.supply == null) continue;

                        if (command2.command.Equals(command.command) && (command2.supply.Equals(command.supply) == false && command2.isUnit))
                        {
                            command2.supply = null;
                            command.timesRepeated++;
                        }
                        else break;
                    }

                    for (int j = i + 1; j < ret.Count; j++)
                    {
                        Command command2 = (Command)ret[j];
                        if (command2.supply == null) break;

                        if (command2.supply.Equals(command.supply))
                        {
                            command2.sameAsLastSupply = true;
                        }
                        else break;
                    }

                }

                outputTextBox.Text = "";
                foreach (Command command in ret)
                {
                    if (command.supply == null) continue;
                    outputTextBox.Text += command.ToString() + "\r\n";
                }
            }
            catch { }
        }
    }
}
