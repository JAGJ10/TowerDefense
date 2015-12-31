using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Pathfinder {

    private List<Node> openList;
    private List<Node> closedList;
    private int[,] level;
    private int rows, cols;
    private Point goal;

    public static List<Point> path;

    public Pathfinder(int[,] level, int rows, int cols, Point start, Point goal) {
        openList = new List<Node>();
        closedList = new List<Node>();
        path = new List<Point>();
        this.level = level;
        this.rows = rows;
        this.cols = cols;
        this.goal = goal;
        GetPath(start, goal);
    }

    private bool IsValidTile(Point loc) {
        if (loc.x < 0 || loc.x >= cols || loc.y < 0 || loc.y >= rows) return false;
        else return true;
    }

    private bool IsWall(Point loc) {
        if (level[loc.y, loc.x] == 1) return true;
        else return false;
    }

    private void InsertOpen(Point adj, Node cur, Node n, ref List<Node> ret) {
        if (IsValidTile(adj) && !IsWall(adj) && !closedList.Contains(n)) {
            var temp = openList.SingleOrDefault(node => node.p.Equals(n.p));
            if (temp != null) {
                if (n.gScore < temp.gScore) {
                    temp.parent = cur;
                }
            } else {
                openList.Add(n);
            }

            ret.Add(n);
        }
    }

    private List<Node> GetAdjacentNodes(Node cur) {
        List<Node> ret = new List<Node>();
        Point adj = new Point(cur.p.x, cur.p.y + 1);
        Node n = new Node(adj, goal, cur);
        //Top
        InsertOpen(adj, cur, n, ref ret);

        //Left
        adj.x = cur.p.x - 1;
        adj.y = cur.p.y;
        n = new Node(adj, goal, cur);
        InsertOpen(adj, cur, n, ref ret);

        //Bottom
        adj.x = cur.p.x;
        adj.y = cur.p.y - 1;
        n = new Node(adj, goal, cur);
        InsertOpen(adj, cur, n, ref ret);

        //Right
        adj.x = cur.p.x + 1;
        adj.y = cur.p.y;
        n = new Node(adj, goal, cur);
        InsertOpen(adj, cur, n, ref ret);

        return ret;
    }

    private bool CalcPath(Node cur) {
        closedList.Add(cur);
        openList.Remove(cur);

        List<Node> adjNodes = GetAdjacentNodes(cur);

        //Sort by F-value for shortest path
        adjNodes.Sort((node1, node2) => node1.fScore.CompareTo(node2.fScore));
        foreach (var n in adjNodes) {
            //Check for end node
            if (n.p.Equals(goal)) {
                closedList.Add(n);
                return true;
            } else {
                //Check next node then
                if (CalcPath(n)) return true;
            }
        }

        return false;
    }

    private void GetPath(Point start, Point goal) {
        //Start node is the first one in the open list
        Node n = new Node(start, goal);
        openList.Add(n);
        bool success = CalcPath(n);

        if (success) {
            //If path was found, backtrace through parents for ordered path
            var temp = closedList.SingleOrDefault(node => node.p.Equals(goal));
            if (temp != null) {
                while (temp.parent != null) {
                    path.Add(temp.p);
                    temp = temp.parent;
                }

                //Reverse the path
                path.Reverse();
            }
        }

        //Cleanup
        openList.Clear();
        closedList.Clear();
    }
}