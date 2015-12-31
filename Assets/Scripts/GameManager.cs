using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private LevelLoader levelLoader;

    private int level = 1;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        levelLoader = GetComponent<LevelLoader>();
        levelLoader.SetupScene(level);
    }
}
