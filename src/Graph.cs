using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Edge> GetAllEdges()
        {
            foreach (var vertex in AllVerticies)
            {
                foreach (var neighbour in vertex.AllNeighbours)
                {
                    if (vertex.Id < neighbour.Id)
                        yield return new Edge(vertex.Id, neighbour.Id);
                }
            }
        }

        public static Graph CreateEmptyGraph()
        {
            return new Graph();
        }

        public static Graph CreateCompleteGraph(int size)
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

        public bool IsComplete(int n)
        {
            if (Size != n)
                return false;

            return AllVerticies.All(x => x.NeighboursCount == n - 1);
        }
        
        public bool IsCompleteBipartite(int n, int m)
        {
            if (Size != n + m)
                return false;
            if (GetAllEdges().Count() != n*m)
                return false;

            var algortihm = new BipartiteTestingAlgorithm(this);
            return algortihm.IsBipartite(n, m);
        }

        public Graph Shrink(int n, int m)
        {
            var h = new Graph();
            foreach (var v in AllVerticies.Where(x => x.Id != m))
            {
                h.AddVertex(v.Id);
            }
            foreach (var vertex in AllVerticies.Where(x => x.Id != m))
            {
                foreach (var neighbour in vertex.AllNeighbours.Where(x => x.Id != m))
                {
                    h.AddDirectedEdge(vertex.Id, neighbour.Id);
                }
            }
            var mVertex = verticies[m];
            foreach (var neighbourOfM in mVertex.AllNeighbours.Where(x => x.Id != n))
            {
                h.AddUndirectedEdge(n, neighbourOfM.Id);
            }
            return h;
        }

        public bool ContainsEdge(int u, int v)
        {
            if (!verticies.ContainsKey(u))
                return false;
            return verticies[v].HasNeighbour(u);
        }

        public bool ContainsEdge(Edge edge)
        {
            return ContainsEdge(edge.U, edge.V);
        }

        public void RemoveEdge(Edge edge)
        {
            RemoveEdge(edge.U, edge.V);
        }

        private void RemoveEdge(int u, int v)
        {
            verticies[u].RemoveNeighbour(verticies[v]);
            verticies[v].RemoveNeighbour(verticies[u]);
        }

        public Vertex GetVertex(int u)
        {
            return verticies[u];
        }

        public bool ContainsVertex(int id)
        {
            return verticies.ContainsKey(id);
        }
    }
}