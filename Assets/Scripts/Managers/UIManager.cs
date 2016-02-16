using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager> {

    public void OnStartButton() {
        SceneManager.LoadScene("level1");
    }

    public void OnTurretButton(int turret) {
        GameManager.Instance.placeTurret = true;
        GameManager.Instance.turretMode = turret;
    }

    public void OnUpgradeButton() {
        GameManager.Instance.upgradeMode = true;
        GameManager.Instance.Upgrade();
    }
}
