using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

    private LevelManager levelManager;
    private Pathfinder pathFinder;
    private int level = 1;
    private List<Point> path;
    public GameObject enemy;
    public GameObject turret;
    public bool placeTurret = false;
    public int turretMode;
    public bool upgradeMode = false;
    public Point selectedTurret;

    public override void Awake() {
        base.Awake();

        levelManager = GetComponent<LevelManager>();
        levelManager.SetupScene(level);
        pathFinder = new Pathfinder(levelManager.level, levelManager.rows, levelManager.cols, levelManager.start, levelManager.goal);
        path = pathFinder.GetPath();
        //levelLoader.CreatePath(path);

        //InvokeRepeating("Spawn", 0.0f, 0.5f);
    }

    public void Update() {
        if (Input.GetKeyDown("p")) {
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown("f")) {
            Time.timeScale = 0.2f;
        }

        if (placeTurret) {
            Point p = new Point((int)(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 0.5f), (int)(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 0.5f));
            if (!Input.GetMouseButton(0)) {
                levelManager.updateTile(p);
                levelManager.placeTurret(p, turretMode);
                placeTurret = false;
            } else {
                levelManager.updateTile(p);
            }
        } else {
            if (Input.GetMouseButtonUp(0)) {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if (hit.collider != null) {
                    if (hit.collider.tag != "Turret") {
                        upgradeMode = false;
                        //hide the upgrade buttons
                    } else {
                        upgradeMode = true;
                        selectedTurret.setPoint((int)hit.transform.position.x, (int)hit.transform.position.y);
                    }
                } else {
                    upgradeMode = false;
                    //hide the upgrade buttons
                }
            }
        }
    }

    public void Upgrade() {
        if (upgradeMode) levelManager.UpgradeTurret(selectedTurret);
    }

    private void Spawn() {
        GameObject instance = Instantiate(enemy, new Vector3(0, 1, 0.0f), Quaternion.identity) as GameObject;
        var enem = instance.GetComponent<Enemy>();
        enem.path = path;
    }
}