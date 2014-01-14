using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace PlanarityTesting
{
    internal class Graph
    {
        public IDictionary<int, Vertex> Verticies { get; private set; }

        public Graph()
        {
            Verticies = new Dictionary<int, Vertex>();
        }

        public static Graph CreateEmptyGraph()
        {
            return new Graph();
        }

        public static Graph CreateFullGraph(int size)
        {
            var graph = new Graph();
            graph.AddVerticiesRange(size);
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    graph.AddUndirectedEdge(i, j);
                }
            }
            return graph;
        }

        public static Graph CreateBipartiteGraph(int n, int m)
        {
            var graph = new Graph();
            graph.AddVerticiesRange(n + m);
            for (int i = 0; i < n; i++)
            {
                for (int j = n; j < n+m; j++)
                {
                    graph.AddUndirectedEdge(i, j);
                }
            }
            return graph;
        }

        private void AddVerticiesRange(int n)
        {
            for (int i = 0; i < n; i++)
            {
                AddVertex(i);
            }
        }

        public void AddVertex(int id)
        {
            Verticies[id] = new Vertex(id);
        }

        public void AddDirectedEdge(int from, int to)
        {
            Verticies[from].AddNeighbour(Verticies[to]);
        }

        public void AddUndirectedEdge(int u, int v)
        {
            AddDirectedEdge(u, v);
            AddDirectedEdge(v, u);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Verticies.Values);
        }

        public bool IsFullGraph(int size)
        {
            if (Verticies.Count != size)
                return false;

            return Verticies.Values.All(x => x.NeighboursCount == size - 1);
        }
    }
}