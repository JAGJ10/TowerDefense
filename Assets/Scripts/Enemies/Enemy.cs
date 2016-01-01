using UnityEngine;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {
    protected int health { get; set; }
    protected float speed { get; set; }
    public List<Point> path { get; set; }
    protected Rigidbody2D rb2d;
    private int waypoint = 0;

    protected virtual void Start() {
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
            other.gameObject.SetActive(false);
            health -= other.gameObject.GetComponent<Projectile>().damage;
        }
    }

    protected void Move() {
        //while (path.Count > 0) {
            Vector2 pos = transform.position;
            Vector2 direction = new Vector2(path[waypoint].x, path[waypoint].y) - pos;

            if (direction.magnitude < 0.001f) {
                waypoint++;
            }

            direction = direction.normalized;
            //rb2d.velocity = new Vector2(direction.x * speed, direction.y * speed);
        transform.Translate(new Vector3(direction.x * speed, direction.y * speed, 0));
        //}
    }
}