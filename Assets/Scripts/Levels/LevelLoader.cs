using UnityEngine;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {

    public int cols = 25;
    public int rows = 15;

    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject pathTile;

    private Transform boardHolder;

    public int[,] level = new int[15, 25];

    public Point start { get; private set; }
    public Point goal { get; private set; }

    private void LoadLevel(int level) {
        start = new Point(0, 0);
        goal = new Point(18, 10);
        boardHolder = new GameObject("Grid").transform;

        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) {
                this.level[y, x] = 0;
                GameObject instance = Instantiate(floorTile, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }

        for (int y = 0; y < cols; y++) {
            if (y == 1) continue;
            this.level[1, y] = 1;
            GameObject instance = Instantiate(wallTile, new Vector3(y, 1, 0.0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
        }

        this.level[2, 8] = 1;
        GameObject instanc = Instantiate(wallTile, new Vector3(8, 2, 0.0f), Quaternion.identity) as GameObject;
        instanc.transform.SetParent(boardHolder);
    }

    public void CreatePath(List<Point> path) {
        foreach (var i in path) {
            GameObject instance = Instantiate(pathTile, new Vector3(i.x, i.y, 0.0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
        }
    }

    public void SetupScene(int level) {
        LoadLevel(level);
    }
}
