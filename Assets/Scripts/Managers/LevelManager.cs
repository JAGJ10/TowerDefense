using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    public int cols = 25;
    public int rows = 15;

    public GameObject wallTile;
    public GameObject pathTile;
    public GameObject[] turrets;
    public Sprite turretSprite;
    public Sprite wallSprite;

    private Transform grid;
    private GameObject[,] tiles = new GameObject[15, 25];
    private Point lastTile = new Point(-1, -1);

    public int[,] level = new int[15, 25] { {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 3, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

    public Point start { get; private set; }
    public Point goal { get; private set; }

    private void LoadLevel(int level) {
        start = new Point(0, 1);
        grid = new GameObject("Grid").transform;

        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) {
                if (this.level[y, x] == 1) {
                    GameObject instance = Instantiate(wallTile, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                    tiles[y, x] = instance;
                    instance.transform.SetParent(grid);
                } else if (this.level[y, x] == 3) {
                    goal = new Point(x, y);
                }
            }
        }
    }

    public void CreatePath(List<Point> path) {
        foreach (var i in path) {
            GameObject instance = Instantiate(pathTile, new Vector3(i.x, i.y, 0.0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(grid);
        }
    }

    public void SetupScene(int level) {
        LoadLevel(level);
    }

    public void placeTurret(Point p, int turretMode) {
        if (level[p.y, p.x] == 1) {
            GameObject temp = tiles[p.y, p.x];
            tiles[p.y, p.x] = Instantiate(turrets[turretMode], new Vector3(p.x, p.y, 0.0f), Quaternion.identity) as GameObject;
            tiles[p.y, p.x].transform.SetParent(grid);
            level[p.y, p.x] = 2;
            Destroy(temp);
        }
    }

    public void updateTile(Point p) {
        if (lastTile.x != -1 && lastTile.y != -1) {
            if (level[lastTile.y, lastTile.x] == 1) {
                tiles[lastTile.y, lastTile.x].GetComponent<SpriteRenderer>().sprite = wallSprite;
            }
        }

        if (level[p.y, p.x] == 1) {
            tiles[p.y, p.x].GetComponent<SpriteRenderer>().sprite = turretSprite;
            lastTile.x = p.x;
            lastTile.y = p.y;
        }
    }

    public void UpgradeTurret(Point p) {
        tiles[p.y, p.x].GetComponent<Turret>().Upgrade();
    }

    public void ToggleTurret(Point p) {
        if (level[p.y, p.x] == 2) {
            tiles[p.y, p.x].GetComponent<Turret>().ToggleOff();
        }
    }
}
