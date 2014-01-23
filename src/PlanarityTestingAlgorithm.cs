using System.Collections.Generic;
using System.Linq;

namespace PlanarityTesting
{
    class PlanarityTestingAlgorithm
    {
        private readonly Graph inputGraph;
        public Graph NonplanarSubgraph { get; private set; }

        public int Counter { get; private set; }

        public PlanarityTestingAlgorithm(Graph inputGraph)
        {
            this.inputGraph = inputGraph;
        }

        public bool IsPlanar()
        {
            if (inputGraph.GetAllEdges().Count() > 3 * inputGraph.Size - 6)
                return false;
            return IsPlanar(inputGraph);
        }

        private bool IsPlanar(Graph g)
        {
            Counter++;
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

        private void ExtendNonplanarSubgraph(Graph g, Edge e)
        {
            var toExtend = GetEdgeToExtend(g, e);
            if (toExtend != null)
            {
               NonplanarSubgraph.RemoveEdge(toExtend.Value);
               NonplanarSubgraph.AddVertex(e.V);
               NonplanarSubgraph.AddUndirectedEdge(e.V, toExtend.Value.V);
               NonplanarSubgraph.AddUndirectedEdge(e.V, toExtend.Value.U);
            }
        }

        private Edge? GetEdgeToExtend(Graph g, Edge e)
        {
            foreach (var e2 in NonplanarSubgraph.GetAllEdges())
            {
                if (!g.ContainsEdge(e2.U, e2.V) && g.ContainsEdge(e.V, e2.U) && g.ContainsEdge(e.V, e2.V))
                {
                    return e2;
                }
            }
            return null;
        }
    }
}