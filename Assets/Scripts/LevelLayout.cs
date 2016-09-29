[System.Serializable]
public struct LevelLayout {

	public int levelWidth;
	public int levelHeight;
    public LevelStart levelStart;
    public LevelEnd levelEnd;
    public Tile[] tiles;

    [System.Serializable]
	public struct LevelStart {
		public int xPos;
		public int yPos;
	}
	
	[System.Serializable]
	public struct LevelEnd {
		public int xPos;
		public int yPos;
	}

	[System.Serializable]
	public struct Tile {
		public int type;
		public int xPos;
		public int yPos;
	}
}
