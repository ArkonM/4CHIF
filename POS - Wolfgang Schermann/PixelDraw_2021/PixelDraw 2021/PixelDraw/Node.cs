using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDraw
{
    internal class Node
    {

        private int x;
        private int y;
        private int value;
        private bool visited;
        private Node parent;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Value { get => value; set => this.value = value; }
        public bool Visited { get => visited; set => visited = value; }
        public Node Parent { get => parent; set => parent = value; }

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.value = int.MaxValue;
            this.visited = false;
            this.parent = null;
        }

        public double distance(Node n)
        {
            return Math.Sqrt((n.X - x) * (n.X - x) + (n.Y - y) * (n.Y - y));
        }

    }
}
