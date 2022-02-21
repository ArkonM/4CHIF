using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PixelDraw
{
    internal class AStern
    {

        private Node[,] nodes;
        private Node start;
        private Node current;
        private Node end;
        private int width, height;
        private int distance = 1;
        private bool foundEndNode;

        public AStern(int x, int y)
        {
            nodes = new Node[x, y];
            foundEndNode = false;
        }

        public void AddNode(int x, int y)
        {
            nodes[x, y] = new Node(x, y);
            this.width = x;
            this.height = y;
        }

        public void setStart(int x, int y)
        {
            start = nodes[x, y];
            start.Value = 0;
        }

        public void setEnd(int x, int y)
        {
            end = nodes[x, y];
        }


        public void SearchRoute()
        {

            HashSet<Node> nodeSet = new HashSet<Node>();
            nodeSet.Add(start);

            while (nodeSet.Count > 0)
            {
                current = GetMinNode(nodeSet);

                if(current.Equals(end))
                {
                    foundEndNode = true;
                    break;
                }

                List<Point> neighbourNodePoints = new List<Point>();
                neighbourNodePoints.Add(new Point(current.X + 1, current.Y));
                neighbourNodePoints.Add(new Point(current.X - 1, current.Y));
                neighbourNodePoints.Add(new Point(current.X, current.Y + 1));
                neighbourNodePoints.Add(new Point(current.X, current.Y - 1));

                foreach (Point p in neighbourNodePoints)
                {
                    if (p.X >= 0 && p.X <= width && p.Y >= 0 && p.Y <= height)
                    {
                        Node n = nodes[(int)p.X, (int)p.Y];

                        if (n != null && !n.Visited)
                        {
                            if (current.Value + distance < n.Value)
                            {
                                n.Value = current.Value + distance;
                                n.Parent = current;
                            }
                            nodeSet.Add(n);
                        }
                    }
                }

                current.Visited = true;
                nodeSet.Remove(current);

            }
            
        }
        
        private Node GetMinNode(HashSet<Node> nodeSet)
        {
            int dis = int.MaxValue;
            Node smallestDistanceNode = null;

            foreach(Node n in nodeSet)
            {
                if (n.Value + n.distance(end) < dis)
                {
                    smallestDistanceNode = n;
                    dis = n.Value + (int)n.distance(end);
                }
            }
            return smallestDistanceNode;
        }

        public List<Point> getRoute()
        {
            List<Point> pointList = new List<Point>();

            if (foundEndNode)
            {
                Node n = end;

                while (n != null)
                {
                    pointList.Add(new Point((int)n.X, (int)n.Y));
                    n = n.Parent;
                }
            }

            return pointList;
        }

    }
}
