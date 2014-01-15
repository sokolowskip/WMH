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
            if (graph.GetAllEdges().Count() > 3 * graph.Size - 6)
                return false;
            return IsPlanar(graph);
        }

        private bool IsPlanar(Graph g)
        {
            if (g.IsCompleteBipartite(3, 3) || g.IsComplete(5))
            {
                NonplanarSubgraph = g;
                return false;
            }
            if (g.Size == 5)
                return true;
            foreach (var edge in g.GetAllEdges())
            {
                var h = g.Shrink(edge.U, edge.V);
                var isPlanar = IsPlanar(h);
                if (!isPlanar)
                {
                    ExtendNonplanarSubgraph(g, edge);
                    return false;
                }
            }
            return true;
        }

        //TODO to jeszcze nie działa dobrze niestety
        private void ExtendNonplanarSubgraph(Graph g, Edge edge)
        {
            var nonexistentEdge = GetNonexistentEdgeFronNonplanarSubgraph(g);
            if (nonexistentEdge.HasValue)
            {
                NonplanarSubgraph.RemoveEdge(nonexistentEdge.Value);
                NonplanarSubgraph.AddVertex(edge.V);
                foreach (var neighbour in g.GetVertex(edge.V).AllNeighbours)
                {
                    if (NonplanarSubgraph.ContainsVertex(neighbour.Id))
                    {
                        NonplanarSubgraph.AddUndirectedEdge(edge.V, neighbour.Id);
                    }
                }
            }
        }

        private Edge? GetNonexistentEdgeFronNonplanarSubgraph(Graph graph)
        {
            foreach (var edge in NonplanarSubgraph.GetAllEdges())
            {
                if (!graph.ContainsEdge(edge))
                    return edge;
            }
            return null;
        }
    }
}