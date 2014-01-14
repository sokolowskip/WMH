using System.Collections.Generic;
using System.Linq;

namespace PlanarityTesting
{
    /// <summary>
    /// Implements http://www.geeksforgeeks.org/bipartite-graph/
    /// </summary>
    class BipartiteTestingAlgorithm
    {
        private enum Color
        {
            Even,
            Odd
        }

        private Color GetOther(Color color)
        {
            return color == Color.Even ? Color.Odd : Color.Even;
        }

        private readonly Dictionary<int, Color?> vertexColors;
        private readonly IList<Vertex> verticies;

        public BipartiteTestingAlgorithm(Graph graph)
        {
            this.verticies = graph.AllVerticies.ToList();
            vertexColors = new Dictionary<int, Color?>();
            foreach (var vertex in graph.AllVerticies)
            {
                vertexColors[vertex.Id] = null;
            }
        }

        public bool IsBipartite(int n, int m)
        {
            vertexColors[verticies[0].Id] = Color.Even;
            var isBipartite = TryColoring(verticies[0]);
            if (!isBipartite)
                return false;

            var oddVerticiesCount = vertexColors.Values.Count(x => x.Value == Color.Odd);
            return oddVerticiesCount == n || oddVerticiesCount == m;
        }

        private bool TryColoring(Vertex vertex)
        {
            foreach (var neighbour in vertex.AllNeighbours)
            {
                if (!vertexColors[neighbour.Id].HasValue)
                {
                    vertexColors[neighbour.Id] = GetOther(vertexColors[vertex.Id].Value);
                    var reslut = TryColoring(neighbour);
                    if (reslut == false)
                        return false;
                }
                else if (vertexColors[vertex.Id] == vertexColors[neighbour.Id])
                    return false;
            }
            return true;
        }

        
    }
}