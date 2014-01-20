using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PlanarityTesting
{
    class Program
    {
        private static void Main(string[] args)
        {
            var fromFile = ReadFromFile("../../../graphs/zadanie.txt");
            var planarityTestingAlgorithm = new PlanarityTestingAlgorithm(fromFile);
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (!planarityTestingAlgorithm.IsPlanar())
            {
                Console.WriteLine("Graph is nonplanar.");
                Console.WriteLine(planarityTestingAlgorithm.NonplanarSubgraph);
            }
            else
            {
                Console.WriteLine("Graph is planar.");
            }
            stopwatch.Stop();
            Console.WriteLine("Total time: " + stopwatch.Elapsed);
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
