using UnityEngine;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager> {
    public int cols = 25;
    public int rows = 15;

    public GameObject wallTile;
    public GameObject pathTile;
    public GameObject[] turrets;
    public Sprite turretSprite;
    public Sprite wallSprite;

    private Transform grid;
    private GameObject[,] tiles = new GameObject[15, 25];

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

    private void LoadLevel(int levelNum) {
        start = new Point(0, 1);
        grid = new GameObject("Grid").transform;

        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) {
                if (level[y, x] == 1) {
                    GameObject instance = Instantiate(wallTile, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                    tiles[y, x] = instance;
                    instance.transform.SetParent(grid);
                } else if (level[y, x] == 3) {
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

    public void PlaceTurret(Point p, int turretMode) {
        if (level[p.y, p.x] == 0) {
            tiles[p.y, p.x] = Instantiate(turrets[turretMode], new Vector3(p.x, p.y, 0.0f), Quaternion.identity) as GameObject;
            tiles[p.y, p.x].transform.SetParent(grid);
            level[p.y, p.x] = 2;
        }
    }

    public bool IsTileValid(Point p) {
        if (level[p.y, p.x] == 0) return true;
        else return false;
    }

    public void UpgradeTurret(Point p) {
        tiles[p.y, p.x].GetComponent<Turret>().Upgrade();
    }

    public void ToggleTurret(Point p) {
        if (level[p.y, p.x] == 2) tiles[p.y, p.x].GetComponent<Turret>().ToggleOff();
    }
}
