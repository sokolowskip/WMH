using System.Linq;

namespace PlanarityTesting
{
    class PlanarityTestingAlgorithm
    {
        private readonly Graph graph;

        public Graph NonplanarSubgraph { get; private set; }

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

        private bool IsPlanar(Graph g)
        {
            if (g.IsBipartite(3, 3) || g.IsComplete(5))
            {
                NonplanarSubgraph = g;
                return false;
            }
            if (g.Size == 5)
                return true;
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