using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfServiceClient.ServiceReference1;

namespace WcfServiceClient
{
    public partial class Form1 : Form
    {
        Service1Client Client;

        public Form1()
        {
            InitializeComponent();

            Client = new Service1Client();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            OutputTextBox.Text = "";
            try
            {
                if (DirectionChecker.Checked)
                {
                    OutputTextBox.Lines =
                    Client.GetTraceDateDirection(FromTownText.Text, DateTimeFrom.Value, ToTownText.Text, DateTimeTo.Value);
                }
                else
                {
                    var traces =
                    Client.GetTraceDateInDirection(FromTownText.Text, DateTimeFrom.Value, ToTownText.Text, DateTimeTo.Value).ToList();
                    for (int i = 0; i < traces.Count; i++)
                    {
                        OutputTextBox.AppendText(" " + (i+1).ToString() + ": ");
                        for (int j = 0; j < traces[i].Length; j++)
                        {
                            OutputTextBox.AppendText(traces[i][j]);
                            OutputTextBox.AppendText(" \t ");
                        }
                        OutputTextBox.AppendText("\n");
                    }
                }
                if (OutputTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Brak Takich połączeń");
                }
            }
            catch (FaultException faultMsg)
            {
                MessageBox.Show(faultMsg.Message);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Server nie odpowiada, prawdopodobnie jest wyłączony");
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show("Server nie odpowiada, prawdopodobnie jest wyłączony");
            }            
            catch (Exception exc)
            {
                OutputTextBox.Text = "";
                MessageBox.Show(exc.Message);
            }
            finally
            {
                Client = new Service1Client();
            }
            

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DateTimeFrom.Enabled = !checkBox1.Checked;
            DateTimeTo.Enabled = !checkBox1.Checked;
            DateTimeFrom.Value = new DateTime(1900,1,1,0,0,0);
        }
    }
}
