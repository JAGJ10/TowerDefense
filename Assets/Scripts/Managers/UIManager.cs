using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager> {

    public override void Awake() {
        base.Awake();
    }

    public void OnStartButton() {
        SceneManager.LoadScene("level1");
    }

    public void OnUpgradeButton() {
        GameManager.Instance.upgradeMode = true;
        GameManager.Instance.Upgrade();
    }

    public void OnPauseButton() {
        GameManager.Instance.SetPaused();
    }

    public void OnFastForward() {
        GameManager.Instance.SetFastForward();
    }
}
