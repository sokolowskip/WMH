using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanarityTesting
{
    class Program
    {
        private static void Main(string[] args)
        {
            var k5 = Graph.CreateFullGraph(5);
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

            Console.WriteLine(k5.IsFull(5));
            Console.WriteLine(k7_3.IsFull(5));
            Console.WriteLine();

            Console.WriteLine(k7_3.IsBipartite(7, 3));
            Console.WriteLine(k7_3.IsBipartite(3, 7));
            Console.WriteLine(k5.IsBipartite(2, 3));
            Console.WriteLine();

            var k3_3 = Graph.CreateBipartiteGraph(3, 3);
            Console.WriteLine(k3_3.IsBipartite(3, 3));
            k3_3.AddUndirectedEdge(0,1);
            Console.WriteLine(k3_3.IsBipartite(3, 3));
        }
    }
}
