﻿using System;
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
            k3_3.AddUndirectedEdge(0, 1);
            Console.WriteLine(k3_3.IsBipartite(3, 3));

            var g2 = Graph.CreateFullGraph(3);
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
            Console.WriteLine(Algorithm(Graph.CreateFullGraph(6)));
        }

        private static bool Algorithm(Graph g)
        {
            if (g.GetAllEdges().Count() > g.Size)
                return false;
            return IsPlanar(g);
        }

        private static bool IsPlanar(Graph g)
        {
            if (g.Size == 6)
            {
                var isK3_3 = g.IsBipartite(3, 3);
                if (isK3_3)
                    return false;
            }
            if (g.Size == 5)
            {
                var isK5 = g.IsFull(5);
                if (isK5)
                    return false;
            }
            foreach (var edge in g.GetAllEdges())
            {
                var h = g.Shrink(edge.Item1, edge.Item2);
                var isPlanar = IsPlanar(h);
                if (!isPlanar)
                    return false;
            }
            return true;
        }
    }
}
