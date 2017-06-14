using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
    public class Finder
    {
        private Graph graph;

        private Vertex source;

        private Vertex target;

        private List<List<Trace>> paths = new List<List<Trace>>();

        public DateTime? LeaveTime { get; set; } = null;

        public DateTime? ArrivalTime { get; set; } = null;

        public Finder(Graph graph, Vertex source, Vertex target)
        {
            this.graph = graph;
            this.source = source;
            this.target = target;
        }


        public List<List<Trace>> GetAllPaths()
        {
            graph.CleanState();
            DepthSearchFirst(source, LeaveTime, new List<Trace>());
            if (ArrivalTime != null)
            {
                paths = paths.Where(path =>
                {
                    return DateTime.Compare(
                               path[path.Count - 1].ToDate,
                               ArrivalTime.Value) <= 0;

                }).ToList();
            }
            return paths;
        }


        private void DepthSearchFirst(Vertex vertex, DateTime? arrivalTime, List<Trace> path)
        {
            //vertex.Visited = true;
            foreach (var trace in graph.GetConnections(vertex, arrivalTime ?? DateTime.MinValue))
            {

                
                {
                    List<Trace> newPath = new List<Trace>(path);
                    newPath.Add(trace);
                    if (trace.ToTown.Equals(target))
                    {
                        paths.Add(newPath);
                    }
                    DepthSearchFirst(trace.ToTown, trace.ToDate, newPath);
                }
               

            }

        }
    }
}

