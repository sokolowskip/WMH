using System.Linq;

namespace PlanarityTesting
{
    class PlanarityTestingAlgorithm
    {
        private readonly Graph graph;

        public PlanarityTestingAlgorithm(Graph graph)
        {
            this.graph = graph;
        }

        public bool IsPlanar()
        {
            if (graph.GetAllEdges().Count() > graph.Size)
                return false;
            return IsPlanar(graph);
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