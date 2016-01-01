using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemy;
    public bool spawn = false;
    public float spawnTime = 1.0f;

    public void Start() {
        InvokeRepeating("Spawn", 0.0f, spawnTime);
    }

    private void Spawn() {
        if (spawn) {
            Instantiate(enemy[0], new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}