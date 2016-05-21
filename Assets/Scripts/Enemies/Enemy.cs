using UnityEngine;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {
    protected int health;
    protected float speed;
    protected Rigidbody2D rb2d;
    private int waypoint = 0;

    public List<Point> path;

    protected virtual void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() {
        if (health <= 0) Destroy(gameObject);
    }

    protected virtual void FixedUpdate() {
        Move();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Projectile")) {
            TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
            other.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
    }

    protected void Move() {
        if (waypoint >= path.Count) {
            Destroy(gameObject);
            return;
        }
        Vector2 pos = transform.position;
        Vector2 direction = new Vector2(path[waypoint].x, path[waypoint].y) - pos;

        if (direction.magnitude < 0.01f) {
            waypoint++;
        }

        direction = direction.normalized;
        transform.Translate(new Vector3(direction.x * speed, direction.y * speed, 0));
    }
}