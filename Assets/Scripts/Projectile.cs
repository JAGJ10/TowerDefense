using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public int damage;

    void OnEnable() {
        Invoke("Destroy", 2.0f);
    }

    void Destroy() {
        gameObject.SetActive(false);
    }

    void OnDisable() {
        CancelInvoke();
    }
}
