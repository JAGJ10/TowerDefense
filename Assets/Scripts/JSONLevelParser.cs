using UnityEngine;

public class JSONLevelParser : MonoBehaviour {

    private enum TileType {
        EMPTY,
        WALL,
        START,
        END
    }

	public string levelName;
    [SerializeField]
    private GameObject wallTile;
    private LevelLayout levelLayout;
	private int[,] tiles;

	void Start () {
		levelLayout = new LevelLayout();
		levelLayout = JsonUtility.FromJson<LevelLayout>(LoadJson("Assets/Scripts/LevelLayouts/" + levelName + ".json"));
		LoadLevel();
	}
	
	private string LoadJson(string file) {
		return System.IO.File.ReadAllText(file);
	}
		
	private void LoadLevel() {
		int tilesWidth = levelLayout.levelWidth;
		int tilesHeight = levelLayout.levelHeight;
		tiles = new int[tilesWidth, tilesHeight];

        foreach (var t in levelLayout.tiles) {
            tiles[t.xPos, t.yPos] = t.type;
            wallTile = Instantiate(wallTile, new Vector3(t.yPos, t.xPos, 0), Quaternion.identity) as GameObject;
        }

		tiles[levelLayout.levelStart.xPos, levelLayout.levelStart.yPos] = (int)TileType.START;
        tiles[levelLayout.levelEnd.xPos, levelLayout.levelEnd.yPos] = (int)TileType.END;
    }
}
