using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    private LevelManager levelManager;
    private Pathfinder pathFinder;
    private int level = 1;
    private List<Point> path;
    public GameObject enemy;
    public bool placeTurret = false;
    public int turretMode;
    public bool upgradeMode = false;
    public Point selectedTurret;
    public GameObject panel;
    private bool fastForward = false;
    private bool paused = false;

    public override void Awake() {
        base.Awake();

        levelManager = GetComponent<LevelManager>();
        levelManager.SetupScene(level);
        pathFinder = new Pathfinder(levelManager.level, levelManager.rows, levelManager.cols, levelManager.start, levelManager.goal);
        path = pathFinder.GetPath();
        //levelLoader.CreatePath(path);

        panel.SetActive(false);
        //InvokeRepeating("Spawn", 0.0f, 0.5f);
    }

    public void Update() {
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
                        levelManager.ToggleTurret(selectedTurret);
                        upgradeMode = false;
                        //hide the upgrade buttons
                        panel.SetActive(false);
                    } else {
                        panel.SetActive(true);
                        upgradeMode = true;
                        selectedTurret.setPoint((int)hit.transform.position.x, (int)hit.transform.position.y);
                        levelManager.ToggleTurret(selectedTurret);
                    }
                } else {
                    levelManager.ToggleTurret(selectedTurret);
                    upgradeMode = false;
                    //hide the upgrade buttons
                    panel.SetActive(false);
                }
            }
        }
    }

    public void SetPaused() {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
    }

    public void SetFastForward() {
        fastForward = !fastForward;
        Time.timeScale = fastForward ? 1.5f : 1;
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