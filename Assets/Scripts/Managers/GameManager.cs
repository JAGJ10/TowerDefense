using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

    private LevelLoader levelLoader;
    private Pathfinder pathFinder;
    private int level = 1;
    private List<Point> path;
    public GameObject enemy;

    public override void Awake() {
        base.Awake();

        levelLoader = GetComponent<LevelLoader>();
        levelLoader.SetupScene(level);
        pathFinder = new Pathfinder(levelLoader.level, levelLoader.rows, levelLoader.cols, levelLoader.start, levelLoader.goal);
        path = pathFinder.GetPath();
        //levelLoader.CreatePath(path);

        InvokeRepeating("Spawn", 0.0f, 2.0f);


    }

    private void Spawn() {
        GameObject instance = Instantiate(enemy, new Vector3(0, 0, 0.0f), Quaternion.identity) as GameObject;
        var enem = instance.GetComponent<Enemy>();
        enem.path = path;
    }
}

//Create an enemy spawner object
//Have game manager hold the pathfiner object
//Pass the path from game manager to the enemy spawner
//Also pass the spawning point from the level?