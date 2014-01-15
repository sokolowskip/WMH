namespace PlanarityTesting
{
    struct Edge
    {
        public Edge(int u, int v) : this()
        {
            U = u;
            V = v;
        }

        public int U { get; set; }
        public int V { get; set; }
    }
}