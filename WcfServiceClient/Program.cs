﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfServiceClient.ServiceReference1;

namespace WcfServiceClient
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Creating Proxy for the MyService
            //Service1Client Client = new Service1Client();
            //Console.WriteLine("Client calling the service...");
            //Console.WriteLine("Hello Ram");
            //Console.WriteLine(Client.sum(4, 2));
            //var traces = Client.GetTraceDateInDirection("B", new DateTime(2017,05,9), "D", new DateTime(2017, 05, 14)).ToList();
            //for (int i = 0; i < traces.Count; i++)
            //{
            //    for (int j = 0; j < traces[i].Length; j++)
            //    {
            //        Console.WriteLine(traces[i][j]);
            //    }
            //    Console.WriteLine("");
            //}
            //Console.Read();
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }
}
