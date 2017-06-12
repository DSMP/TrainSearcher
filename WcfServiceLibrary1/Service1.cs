using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        List<Trace> Traces;
        public Service1()
        {
            Traces = parseDocument(Path.Combine(Environment.CurrentDirectory, @"Data\", "trains.csv"));
        }
        public List<string> GetTrace(string fromTown, string toTown)
        {
            List<Trace> fromTraces = new List<Trace>();
            List<string> finish = new List<string>();
            for (int i = 0; i < Traces.Count; i++)
            {
                if (Traces[i].FromTown1.Equals(fromTown) && Traces[i].ToTown1.Equals(toTown))
                {
                    //fromTraces.Add(Traces[i]);
                    finish.Add(Traces[i].ToString());
                }
            }
            return finish;
        }

        public List<string> GetTraceDate(string fromTown, DateTime fromTime, string toTown, DateTime toTime)
        {
            throw new NotImplementedException();
        }

        // This Function Returns summation of two integer numbers

        public int sum(int num1, int num2)
        {
            return num1 + num2;
        }
        private List<Trace> parseDocument(string path)
        {
            string[] tab = File.ReadAllLines(path);
            List<Trace> list = new List<Trace>();
            foreach (string s in tab)
            {
                string[] column = s.Split(',');
                list.Add(new Trace(column[0], DateTime.Parse(column[1]), column[2], DateTime.Parse(column[3])));
            }
            return list;
        }
    }
}
