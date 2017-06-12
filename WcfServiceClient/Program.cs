using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfServiceClient.ServiceReference1;

namespace WcfServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating Proxy for the MyService
            Service1Client Client = new Service1Client();
            Console.WriteLine("Client calling the service...");
            Console.WriteLine("Hello Ram");
            Console.WriteLine(Client.sum(4, 2));
            List<string> traces = Client.GetTraceDateDirection("A", new DateTime(2017,05,9), "B", new DateTime(2017, 05, 14)).ToList();
            for (int i = 0; i < traces.Count; i++)
            {
                Console.WriteLine(traces[i]);
            }
            Console.Read();
        }
    }
}
