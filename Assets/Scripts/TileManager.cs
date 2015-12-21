using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour {
    [Serializable]
    public class Count {
        public int minimum, maximum;

        public Count(int min, int max) {
            minimum = min;
            maximum = max;
        }
    }

    public int cols = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);

    public GameObject floorTile;
    public GameObject wallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    private void BoardSetup() {
        boardHolder = new GameObject("Board").transform;
        gridPositions.Clear();

        for (int x = 0; x < cols; x++) {
            for (int y = 0; y < rows; y++) {
                gridPositions.Add(new Vector3(x, y, 0.0f));
                GameObject instance = Instantiate(floorTile, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public void SetupScene(int level) {
        BoardSetup();
    }
}
