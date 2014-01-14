using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace PlanarityTesting
{
    internal class Graph
    {
        private readonly IDictionary<int, Vertex> verticies;

        public int Size { get { return verticies.Count; } }

        public IEnumerable<Vertex> AllVerticies { get { return verticies.Values; } } 

        public Graph()
        {
            verticies = new Dictionary<int, Vertex>();
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
            verticies[id] = new Vertex(id);
        }

        public void AddDirectedEdge(int from, int to)
        {
            verticies[from].AddNeighbour(verticies[to]);
        }

        public void AddUndirectedEdge(int u, int v)
        {
            AddDirectedEdge(u, v);
            AddDirectedEdge(v, u);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, AllVerticies);
        }

        public bool IsFull(int n)
        {
            if (Size != n)
                return false;

            return AllVerticies.All(x => x.NeighboursCount == n - 1);
        }
        
        public bool IsBipartite(int n, int m)
        {
            if (Size != n + m)
                return false;

            var algortihm = new BipartiteTestingAlgorithm(this);
            return algortihm.IsBipartite(n, m);
        }
      
    }
}