using UnityEngine;
using System.Collections.Generic;

public class Turret : MonoBehaviour {

    private List<GameObject> enemies;
    private List<GameObject> projectiles;
    private int pooledAmount = 20;
    public GameObject projectile;
    private float fireRate = 0.1f;
    private float speed = 4.0f;
    private int damage = 1;

    void Start() {
        enemies = new List<GameObject>();
        projectiles = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++) {
            GameObject obj = Instantiate(projectile) as GameObject;
            obj.SetActive(false);
            projectiles.Add(obj);
        }

        InvokeRepeating("Fire", 0.0f, fireRate);
    }

    bool TargetEnemy() {
        if (enemies.Count > 0) {
            if (enemies[0] == null) {
                enemies.RemoveAt(0);
                return false;
            }
            return true;
        } else {
            return false;
        }
    }

    void Fire() {
        if (TargetEnemy()) {
            //GameObject clone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            for (int i = 0; i < projectiles.Count; i++) {
                if (!projectiles[i].activeInHierarchy) {
                    projectiles[i].transform.position = transform.position;
                    projectiles[i].transform.rotation = transform.rotation;
                    projectiles[i].SetActive(true);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = speed * transform.up;
                    projectiles[i].GetComponent<Projectile>().damage = damage;
                    break;
                }
            }
        }
    }

    void Update() {
        if (TargetEnemy()) transform.rotation = Quaternion.LookRotation(Vector3.forward, enemies[0].transform.position - transform.position);
	}
    

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            enemies.Remove(other.gameObject);
        }
    }
}