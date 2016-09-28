using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class JSONLevelParser : MonoBehaviour {

	public string levelName;
	public GameObject wall;
	public LevelLayout levelLayout;
	public int[,] tiles;

	void Start () {
		levelLayout = new LevelLayout();
		levelLayout = JsonUtility.FromJson<LevelLayout>(LoadJson("Assets/Scripts/LevelLayouts/" + levelName + ".json"));
		TilesTo2DArray();
		CreateWalls();
	}
	
	public string LoadJson(string file) {
		return System.IO.File.ReadAllText(file);
	}
		
	public void TilesTo2DArray() {
		/************
		 * 0 = EMPTY
		 * 1 = WALL
		 * 2 = START
		 * 3 = END
		 ***********/
		int tilesWidth = levelLayout.levelWidth;
		int tilesHeight = levelLayout.levelHeight;
		tiles = new int[tilesWidth, tilesHeight];

		for (int i = 0; i < levelLayout.levelHeight; i++) {
			for (int j = 0; j < levelLayout.levelWidth; j++) {
				int tileIndex = levelLayout.levelWidth * i + j;
				if (levelLayout.tiles[tileIndex].isWall) {
					tiles[levelLayout.tiles[tileIndex].xPos, levelLayout.tiles[tileIndex].yPos] = 1;
				} else {
					tiles[levelLayout.tiles[tileIndex].xPos, levelLayout.tiles[tileIndex].yPos] = 0;
				}
			}
		}
		tiles[levelLayout.levelStart.xPos, levelLayout.levelStart.yPos] = 2;
		tiles[levelLayout.levelEnd.xPos, levelLayout.levelEnd.yPos] = 3;
	}

	public void CreateWalls() {
		for (int i = 0; i < tiles.GetLength(0); i++) {
			for (int j = 0; j < tiles.GetLength(1); i++) {
				if (tiles[i, j] == 1) {
					print ("here");
					wall = Instantiate (wall, new Vector3 (i, j, 0), Quaternion.identity) as GameObject;
				}
			}
		}
	}
}
