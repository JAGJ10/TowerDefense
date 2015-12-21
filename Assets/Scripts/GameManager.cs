using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public TileManager tileScript;

    private int level = 1;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        tileScript = GetComponent<TileManager>();
        tileScript.SetupScene(level);
    }
}
