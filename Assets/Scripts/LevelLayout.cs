using UnityEngine;
using System.Collections;

[System.Serializable]
public struct LevelLayout {

	public int levelWidth;
	public int levelHeight;

	[System.Serializable]
	public struct LevelStart {
		public int xPos;
		public int yPos;
	}
	public LevelStart levelStart;

	[System.Serializable]
	public struct LevelEnd {
		public int xPos;
		public int yPos;
	}
	public LevelEnd levelEnd;

	[System.Serializable]
	public struct Tiles {
		public bool isWall;
		public int xPos;
		public int yPos;
	}
	public Tiles[] tiles;
}
