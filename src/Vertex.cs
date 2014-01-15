using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PlanarityTesting
{
    internal class Vertex
    {
        public int Id { get; private set; }

        private readonly IDictionary<int, Vertex> neighbours;

        public IEnumerable<Vertex> AllNeighbours { get { return neighbours.Values; } }  

        public int NeighboursCount
        {
            get { return neighbours.Count; }
        }

        public Vertex(int id)
        {
            Id = id;
            neighbours = new Dictionary<int, Vertex>();
        }

        public void AddNeighbour(Vertex neighbour)
        {
            neighbours[neighbour.Id] = neighbour;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Id, string.Join(",", neighbours.Select(x => x.Key)));
        }

        public void RemoveNeighbour(Vertex vertexToRemove)
        {
            if (neighbours.ContainsKey(vertexToRemove.Id))
                neighbours.Remove(vertexToRemove.Id);
        }
    }
}