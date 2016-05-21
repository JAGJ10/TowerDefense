using System;

public struct Point {
    public int x, y;
    public Point(int i, int j) {
        x = i;
        y = j;
    }

    public void setPoint(int i, int j) {
        x = i;
        y = j;
    }
}

public class Node : IEquatable<Node> {

    public Point p;
    public int gScore, hScore, fScore;
    private Node mParent;
    public Node parent {
        get { return this.mParent; }
        set {
            this.mParent = value;
            if (value != null) this.gScore = this.mParent.gScore + ManhattanDist(this.p, this.mParent.p);
            else this.gScore = 0;
        }
    }

    public Node(Point pos, Point end, Node par = null) {
        p = pos;
        parent = par;
        hScore = ManhattanDist(pos, end);
        fScore = gScore + hScore;
    }

    private int ManhattanDist(Point start, Point end) {
        return Math.Abs(start.x - end.x) + Math.Abs(start.y - end.y);
    }

    public bool Equals(Node other) {
        return p.Equals(other.p);
    }
}
