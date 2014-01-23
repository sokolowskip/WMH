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
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: PlanarityTesting fileName");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File {0} does not exist.", args[0]);
                return;
            }
            Graph graph;
            try
            {
                graph = ReadFromFile(args[0]);
            }
            catch (InvalidFileFormatException)
            {
                Console.WriteLine("Incorrect file format.");
                return;
            }

            var planarityTestingAlgorithm = new PlanarityTestingAlgorithm(graph);
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
            Console.WriteLine("Number of operations: " + planarityTestingAlgorithm.Counter);
        }

        private static Graph ReadFromFile(string fileName)
        {
            //The format of line is: 
            //   Id: id_neighbour_1, id_neighbour_2, ... , id_nieghbour_n
            var g = new Graph();
            var allLines = File.ReadAllLines(fileName);
            var splittedAllLines = allLines.Select(x => x.Split(':'));
            if (splittedAllLines.Any(x => x.Count() != 2))
                throw new InvalidFileFormatException();

            foreach (var id in splittedAllLines.Select(x => x[0]))
            {
                g.AddVertex(ParseVertexId(id));
            }
            foreach (var line in splittedAllLines)
            {
                var fromId = ParseVertexId(line[0]);
                var neighbours = line[1].Split(',');
                foreach (var neighbour in neighbours)
                {
                    int toId = ParseVertexId(neighbour);
                    if(!g.ContainsVertex(toId))
                        throw new InvalidFileFormatException();
                    g.AddDirectedEdge(fromId, toId);
                }
            }
            return g;
        }

        private static int ParseVertexId(string id)
        {
            int result;
            if (!int.TryParse(id, out result))
                throw new InvalidFileFormatException();
            return result;
        }
    }
}
