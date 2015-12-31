using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class LevelLoader : MonoBehaviour {

    private int cols = 25;
    private int rows = 15;

    private Pathfinder pathFinder;

    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject pathTile;

    private Transform boardHolder;

    private int[,] level1 = new int[15, 25];

    private Point start = new Point(0, 0);
    private Point goal = new Point(18, 10);

    private void LoadLevel(int level) {
        boardHolder = new GameObject("Grid").transform;

        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) {
                level1[y, x] = 0;
                GameObject instance = Instantiate(floorTile, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }

        for (int y = 0; y < cols; y++) {
            if (y == 1) continue;
            level1[1, y] = 1;
            GameObject instance = Instantiate(wallTile, new Vector3(y, 1, 0.0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
        }

        level1[2, 8] = 1;
        GameObject instanc = Instantiate(wallTile, new Vector3(8, 2, 0.0f), Quaternion.identity) as GameObject;
        instanc.transform.SetParent(boardHolder);
    }

    private void CreatePath() {
        foreach (var i in Pathfinder.path) {
            GameObject instance = Instantiate(pathTile, new Vector3(i.x, i.y, 0.0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
        }
    }

    public void SetupScene(int level) {
        LoadLevel(level);
        pathFinder = new Pathfinder(level1, rows, cols, start, goal);
        CreatePath();
    }
}
