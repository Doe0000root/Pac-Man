using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class Node {
    public Point Pos;
    public int G, H;
    public Node? Parent;
    public int F => G + H;
    public Node(Point p) => Pos = p;
}

public class AStar {
    public static Point GetNextMove(Point start, Point target, int[,] map) {
        if (start == target) return start;

        var openList = new List<Node> { new Node(start) };
        var closedList = new HashSet<Point>(); 

        while (openList.Any()) {
            var current = openList.OrderBy(n => n.F).ThenBy(n => n.H).First();

            if (current.Pos == target) 
                return ReconstructPath(current, start);

            openList.Remove(current);
            closedList.Add(current.Pos);

            foreach (var neighbor in GetNeighbors(current, map)) {
                if (closedList.Contains(neighbor.Pos)) continue;

                int moveCost = current.G + 1;
                var existingNode = openList.FirstOrDefault(n => n.Pos == neighbor.Pos);

                if (existingNode == null) {
                    neighbor.G = moveCost;
                    neighbor.H = Math.Abs(neighbor.Pos.X - target.X) + Math.Abs(neighbor.Pos.Y - target.Y);
                    neighbor.Parent = current;
                    openList.Add(neighbor);
                } 
                else if (moveCost < existingNode.G) {
                    existingNode.G = moveCost;
                    existingNode.Parent = current;
                }
            }
        }
        return start;
    }

    private static Point ReconstructPath(Node node, Point start) {
        while (node.Parent != null && node.Parent.Pos != start) {
            node = node.Parent;
        }
        return node.Pos;
    }

    private static IEnumerable<Node> GetNeighbors(Node n, int[,] map) {
        Point[] dirs = { new Point(0, 1), new Point(0, -1), new Point(1, 0), new Point(-1, 0) };
        foreach (var d in dirs) {
            Point p = new Point(n.Pos.X + d.X, n.Pos.Y + d.Y);
            if (p.X >= 0 && p.Y >= 0 && p.X < map.GetLength(1) && p.Y < map.GetLength(0)) {
                if (map[p.Y, p.X] != 1) {
                    yield return new Node(p);
                }
            }
        }
    }
}