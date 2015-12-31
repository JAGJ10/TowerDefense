using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
    private int health = 10;
    private float speed = 1;

    void Start() {
        GetComponent<Rigidbody2D>().velocity = speed * transform.up;
    }

    void Update() {
        if (health <= 0) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Projectile")) {
            other.gameObject.SetActive(false);
            health -= other.gameObject.GetComponent<Projectile>().damage;
        }
    }
}