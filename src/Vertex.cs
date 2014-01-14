using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PlanarityTesting
{
    class Vertex
    {
        public int Id { get; private set; }

        public IDictionary<int, Vertex> Neighbours { get; private set; }

        public Vertex(int id)
        {
            Id = id;
            Neighbours = new Dictionary<int, Vertex>();
        }

        public void AddNeighbour(Vertex neighbour)
        {
            Neighbours[neighbour.Id] = neighbour;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Id, string.Join(",", Neighbours.Select(x => x.Key)));
        }
    }
}