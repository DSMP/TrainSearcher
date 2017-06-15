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
                if (Traces[i].FromTown.Value.Equals(fromTown) && Traces[i].ToTown.Value.Equals(toTown))
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
                if (Traces[i].FromTown.Equals(fromTown))
                {
                    MainTraces.Add(Traces[i]);
                }
            }
            for (int i = 0; i < MainTraces.Count; i++)
            {
                if (Traces[i].FromTown.Equals(fromTown))
                {
                    MainTraces.Add(Traces[i]);
                }
            }
            return finish;
        }

        
        public List<string> GetTraceDateDirection(string fromTown, DateTime fromTime, string toTown, DateTime toTime)
        {
            if (!Towns.Contains(fromTown.ToString()) || !Towns.Contains(toTown.ToString()))
            {
                throw new FaultException("No such city in database.");
            }
            List<Trace> fromTraces = new List<Trace>();
            List<string> finish = new List<string>();
            for (int i = 0; i < Traces.Count; i++)
            {
                if (Traces[i].FromTown.ToString().Contains(fromTown) && Traces[i].ToTown.ToString().Contains(toTown)
                    && (DateTime.Compare(Traces[i].FromDate, fromTime) >= 0 && DateTime.Compare(Traces[i].ToDate, toTime) <= 0))
                {
                    finish.Add(Traces[i].ToString());
                }
            }
            return finish;

        }
        public List<List<string>> GetTraceDateInDirection(string FromTown, DateTime FromTime, string ToTown, DateTime ToTime)
        {
            Graph graph = Graph.FromCsv(Path.Combine(Environment.CurrentDirectory, @"Data\", "trains.csv")); //@"D:\Pobrane\trains.csv"

            if (!graph.ContainsVertex(FromTown) || !graph.ContainsVertex(ToTown))
            {
                throw new FaultException("No such city in database.");
            }


            Finder pathFinder = new Finder(graph, graph.GeVertexByName(FromTown), graph.GeVertexByName(ToTown));
            try
            {
                if (FromTime != null)
                {
                    pathFinder.LeaveTime = (FromTime);
                }

                if (ToTime != null)
                {
                    pathFinder.ArrivalTime = (ToTime);
                }
            }
            catch (Exception e)
            {
                throw new FaultException("Date parsing error.");
            }
            List<List<Trace>> paths = pathFinder.GetAllPaths();
            List < List <string>> toReturn = new List<List<string>>();

            foreach (var path in paths)
            {
                List<string> pathList = new List<string>();

                foreach (var trace in path)
                {
                    pathList.Add("FROM: " + trace.FromTown + " AT: " + trace.FromDate + " TO: " + trace.ToTown + " AT: " + trace.ToDate);
                }

                List<string> newPath = new List<string>();
                newPath = pathList;
                toReturn.Add(newPath);
            }

            return toReturn;
            
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
                list.Add(new Trace(new Vertex(column[0]), DateTime.Parse(column[1]), new Vertex(column[2]), DateTime.Parse(column[3])));
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
                        if (Traces[i].ToTown.Value == Towns[j])
                        {
                            isNewTown = false;
                            break;
                        }
                    }
                    else
                    {
                        if (Traces[i].FromTown.Value == Towns[j])
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
                        Towns.Add(Traces[i].ToTown.Value);
                    }
                }
                else
                {
                    if (isNewTown)
                    {
                        Towns.Add(Traces[i].FromTown.Value);
                    }
                }

            }
        }

    }
}
