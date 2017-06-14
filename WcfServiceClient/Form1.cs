﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                    for (int j = 0; j < traces[i].Length; j++)
                    {
                        OutputTextBox.AppendText(traces[i][j]);
                    }
                    Console.WriteLine("");
                }
            }

        }
    }
}