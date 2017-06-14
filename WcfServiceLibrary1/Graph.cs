using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace WcfServiceLibrary1
{
    public class Graph
    {
        ISet<Vertex> vertices = new HashSet<Vertex>();
        ISet<Trace> connections = new HashSet<Trace>();

        public void AddVertex(Vertex vertex)
        {
            vertices.Add(vertex);
        }

        public bool ContainsVertex(string name)
        {
            return vertices.Any(v => v.Value == name);
        }

        public Vertex GeVertexByName(string name)
        {
            return vertices.First(v => v.Value == name);
        }

        public void AddConnection(Trace connection)
        {
            connections.Add(connection);
        }

        public void AddConnectionWithVertices(Trace connection)
        {
            vertices.Add(connection.FromTown);
            vertices.Add(connection.FromTown);
            connections.Add(connection);
        }

        public List<Trace> GetConnections(Vertex vertex, DateTime afterTime)
        {
            return connections.Where(con => Equals(con.FromTown, vertex))
                .Where(con => DateTime.Compare(afterTime, con.FromDate) <= 0).ToList();
        }

        public static Graph FromCsv(string csvPath)
        {
            Graph graph = new Graph();

            using (TextFieldParser parser = new TextFieldParser(csvPath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.ReadFields(); // ommiting header
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    string sourceCity = fields[0];
                    DateTime leavingTime = DateTime.Parse(fields[1]);
                    string targetCity = fields[2];
                    DateTime arrivalTime = DateTime.Parse(fields[3]);

                    Trace newConnection = new Trace(new Vertex(sourceCity), leavingTime, new Vertex(targetCity), arrivalTime);

                    if (DateTime.Compare(newConnection.FromDate, newConnection.ToDate) < 0)
                    {
                        graph.AddConnectionWithVertices(newConnection);
                    }
                    
                }
            }

            return graph;
        }

        public void CleanState()
        {
            foreach (var vertex in vertices)
            {
                //vertex.Visited = false;
            }
        }
    }
}

