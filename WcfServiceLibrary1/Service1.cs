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
        List<String> Towns = new List<string>();
        List<Trace> Traces;
        public Service1()
        {
            Traces = parseDocument(Path.Combine(Environment.CurrentDirectory, @"Data\", "trains.csv"));
            CountTowns(true);
            CountTowns(false);
        }
        public List<string> GetTraceDirection(string fromTown, string toTown)
        {
            List<Trace> fromTraces = new List<Trace>();
            List<string> finish = new List<string>();
            for (int i = 0; i < Traces.Count; i++)
            {
                if (Traces[i].FromTown1.Equals(fromTown) && Traces[i].ToTown1.Equals(toTown))
                {
                    finish.Add(Traces[i].ToString());
                }
            }
            return finish;
        }

        public List<string> GetTraceInDirection(string fromTown, string toTown)
        {
            List<Trace> MainTraces = new List<Trace>();
            List<string> finish = new List<string>();
            for (int i = 0; i < Traces.Count; i++)
            {
                if (Traces[i].FromTown1.Equals(fromTown))
                {
                    MainTraces.Add(Traces[i]);
                }
            }
            for (int i = 0; i < MainTraces.Count; i++)
            {
                if (Traces[i].FromTown1.Equals(fromTown))
                {
                    MainTraces.Add(Traces[i]);
                }
            }
            return finish;
        }


        public List<string> GetTraceDateDirection(string fromTown, DateTime fromTime, string toTown, DateTime toTime)
        {
            List<Trace> fromTraces = new List<Trace>();
            List<string> finish = new List<string>();
            for (int i = 0; i < Traces.Count; i++)
            {
                if (Traces[i].FromTown1.Equals(fromTown) && Traces[i].ToTown1.Equals(toTown)
                    && Traces[i].FromDate1 >= fromTime && Traces[i].ToDate1 <= toTime)
                {
                    finish.Add(Traces[i].ToString());
                }
            }
            return finish;

        }
        public List<string> GetTraceDateInDirection(string FromTown, DateTime FromTime, string ToTown, DateTime ToTime)
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
        private void CountTowns(bool stateTown)
        {
            bool isNewTown = true;
            int j = 0;
            for (int i = 0; i < Traces.Count; i++)
            {
                j = 0;
                do
                {
                    if (Towns.Count == 0)
                    {
                        //Towns.Add(Traces[i].ToTown1);
                        break;
                    }
                    if (stateTown)
                    {
                        if (Traces[i].ToTown1 == Towns[j])
                        {
                            isNewTown = false;
                            break;
                        }
                    }
                    else
                    {
                        if (Traces[i].FromTown1 == Towns[j])
                        {
                            isNewTown = false;
                            break;
                        }
                    }

                    j++;
                } while (j < Towns.Count);
                if (stateTown)
                {
                    if (isNewTown)
                    {
                        Towns.Add(Traces[i].ToTown1);
                    }
                }
                else
                {
                    if (isNewTown)
                    {
                        Towns.Add(Traces[i].FromTown1);
                    }
                }

            }
        }

    }
}
