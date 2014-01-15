using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanarityTesting
{
    class Program
    {
        private static void Main(string[] args)
        {
            var k5 = Graph.CreateCompleteGraph(5);
            Console.WriteLine(k5);
            Console.WriteLine();

            var k7_3 = Graph.CreateBipartiteGraph(7, 3);
            Console.WriteLine(k7_3);
            Console.WriteLine();

            var g = Graph.CreateEmptyGraph();
            g.AddVertex(3);
            g.AddVertex(7);
            g.AddVertex(19);
            g.AddVertex(111);

            g.AddDirectedEdge(3, 7);
            g.AddDirectedEdge(7, 19);
            g.AddDirectedEdge(7, 111);

            Console.WriteLine(g);
            Console.WriteLine();

            Console.WriteLine(k5.IsComplete(5));
            Console.WriteLine(k7_3.IsComplete(5));
            Console.WriteLine();

            Console.WriteLine(k7_3.IsBipartite(7, 3));
            Console.WriteLine(k7_3.IsBipartite(3, 7));
            Console.WriteLine(k5.IsBipartite(2, 3));
            Console.WriteLine();

            var k3_3 = Graph.CreateBipartiteGraph(3, 3);
            Console.WriteLine(k3_3.IsBipartite(3, 3));
            k3_3.AddUndirectedEdge(0, 1);
            Console.WriteLine(k3_3.IsBipartite(3, 3));

            var g2 = Graph.CreateCompleteGraph(3);
            g2.AddVertex(3);
            g2.AddVertex(4);
            g2.AddVertex(5);
            g2.AddUndirectedEdge(0,4);
            g2.AddUndirectedEdge(0,3);
            g2.AddUndirectedEdge(3,5);
            g2.AddUndirectedEdge(4,5);
            Console.WriteLine(g2);
            Console.WriteLine();

            var h = g2.Shrink(0, 4);
            Console.WriteLine(h);
            Console.WriteLine();

            var h2 = h.Shrink(0, 1);
            Console.WriteLine(h2);
            Console.WriteLine();
            Console.WriteLine(new PlanarityTestingAlgorithm(Graph.CreateCompleteGraph(6)).IsPlanar());

            var fromFile = ReadFromFile("../../../graphs/graph1.txt");
        }

        private static Graph ReadFromFile(string fileName)
        {
            //The format of line is: 
            //   Id: id_neighbour_1, id_neighbour_2, ... , id_nieghbour_n
            var g = new Graph();
            var allLines = File.ReadAllLines(fileName);
            var splittedAllLines = allLines.Select(x => x.Split(':'));
            foreach (var id in splittedAllLines.Select(x => x[0]))
            {
                g.AddVertex(int.Parse(id));
            }
            foreach (var line in splittedAllLines)
            {
                var fromId = int.Parse(line[0]);
                var neighbours = line[1].Split(',');
                foreach (var neighbour in neighbours)
                {
                    int toId = int.Parse(neighbour);
                    g.AddDirectedEdge(fromId, toId);
                }
            }
            return g;
        }

        

        
    }
}
