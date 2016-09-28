using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    [SerializeField]
    private Transform[] enemies;
    [SerializeField]
    private Transform spawnPoint;

    private float timeBetweenWaves = 5.0f;
    private float countdown = 2.0f;

    private int waveIndex = 0;

    void Update() {
        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave() {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy() {
        //Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}