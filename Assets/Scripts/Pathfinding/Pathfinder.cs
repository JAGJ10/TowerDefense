using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Pathfinder {
    private List<Node> openList;
    private List<Node> closedList;
    private int[,] level;
    private int rows, cols;
    private Point start, goal;

    public Pathfinder(int[,] level, int rows, int cols, Point start, Point goal) {
        openList = new List<Node>();
        closedList = new List<Node>();
        this.level = level;
        this.rows = rows;
        this.cols = cols;
        this.start = start;
        this.goal = goal;
    }

    private bool IsTileValid(Point loc) {
        if (loc.x < 0 || loc.x >= cols || loc.y < 0 || loc.y >= rows) return false;
        else return true;
    }

    private bool IsBlocked(Point loc) {
        if (level[loc.y, loc.x] == 1 || level[loc.y, loc.x] == 2) return true;
        else return false;
    }

    private void InsertOpen(Point adj, Node cur, Node n) {
        if (IsTileValid(adj) && !IsBlocked(adj) && !closedList.Contains(n)) {
            var temp = openList.SingleOrDefault(node => node.p.Equals(n.p));
            if (temp != null) {
                if (n.fScore < temp.fScore) {
                    temp.parent = cur;
                }
            } else {
                openList.Add(n);
            }
        }
    }

    private void AddAdjacentNodes(Node cur) {
        Point adj = new Point(cur.p.x, cur.p.y + 1);
        Node n = new Node(adj, goal, cur);
        //Top
        InsertOpen(adj, cur, n);

        //Left
        adj.x = cur.p.x - 1;
        adj.y = cur.p.y;
        n = new Node(adj, goal, cur);
        InsertOpen(adj, cur, n);

        //Bottom
        adj.x = cur.p.x;
        adj.y = cur.p.y - 1;
        n = new Node(adj, goal, cur);
        InsertOpen(adj, cur, n);

        //Right
        adj.x = cur.p.x + 1;
        adj.y = cur.p.y;
        n = new Node(adj, goal, cur);
        InsertOpen(adj, cur, n);
    }

    public List<Point> GetPath() {
        //Start node is the first one in the open list
        List<Point> path = new List<Point>();
        Node n = new Node(start, goal);
        openList.Add(n);

        while (openList.Any()) {
            //Get node from open list with lowest fScore
            openList.Aggregate((curMin, x) => x.fScore < curMin.fScore ? x : curMin);
            Node cur = openList[0];
            openList.RemoveAt(0);
            closedList.Add(cur);

            if (cur.p.Equals(goal)) {
                while (cur.parent != null) {
                    path.Add(cur.p);
                    cur = cur.parent;
                }
                path.Reverse();
            }

            AddAdjacentNodes(cur);
        }

        //Cleanup
        openList.Clear();
        closedList.Clear();
        
        return path;
    }
}