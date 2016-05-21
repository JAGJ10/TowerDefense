using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    private LevelManager levelManager;
    private Pathfinder pathFinder;
    private List<Point> path;
    private int level = 1;
    private bool fastForward = false;
    private bool paused = false;
    private Point selectedTurret;

    public GameObject upgradePanel;
    public GameObject turretPanel;
    public GameObject enemy;
    public Button upgradeButton;
    public bool placeTurret = false;
    public int turretMode;
    public bool upgradeMode = false;

    public override void Awake() {
        base.Awake();

        levelManager = GetComponent<LevelManager>();
        levelManager.SetupScene(level);
        pathFinder = new Pathfinder(levelManager.level, levelManager.rows, levelManager.cols, levelManager.start, levelManager.goal);
        path = pathFinder.GetPath();
        levelManager.CreatePath(path);

        upgradePanel.SetActive(false);
        //InvokeRepeating("Spawn", 0.0f, 0.5f);
    }

    public void Start() {
        Spawn();
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
            if (Input.GetMouseButtonDown(0)) {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if (hit.collider != null) {
                    if (hit.collider.tag != "Turret") {
                        ToggleUpgradeMenuOff();
                    }
                } else {
                    if (upgradePanel.activeSelf && !RectTransformUtility.RectangleContainsScreenPoint(upgradeButton.GetComponent<RectTransform>(), Input.mousePosition, Camera.main)) {
                        levelManager.ToggleTurret(selectedTurret);
                        ToggleUpgradeMenuOff();
                    }
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

    public void ToggleUpgradeMenuOn(Point p) {
        selectedTurret = p;
        upgradeMode = true;
        turretPanel.SetActive(false);
        upgradePanel.SetActive(true);
    }

    public void ToggleUpgradeMenuOff() {
        upgradeMode = false;
        turretPanel.SetActive(true);
        upgradePanel.SetActive(false);
    }

    private void Spawn() {
        GameObject instance = Instantiate(enemy, new Vector3(0, 1, 0.0f), Quaternion.identity) as GameObject;
        var enem = instance.GetComponent<Enemy>();
        enem.path = path;
    }
}