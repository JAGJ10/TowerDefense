using UnityEngine;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {
    protected float maxHealth;
    protected float health;
    protected RectTransform healthBar;
	protected float originalSpeed;
    protected float speed;
	protected float alteredSpeedDuration;
	protected float damageOverTime;
	protected float damageOverTimeDuration;
    protected Rigidbody2D rb2d;
    private int waypoint = 0;

    public List<Point> path;

    protected virtual void Awake() {
        healthBar = transform.Find("HealthBar/Health").GetComponent<RectTransform>();
        rb2d = GetComponent<Rigidbody2D>();
    }

	protected virtual void Start() {
		originalSpeed = speed;
	}

    protected virtual void Update() {
        if (health <= 0) Destroy(gameObject);

		if (alteredSpeedDuration != 0) {
			alteredSpeedDuration -= Time.deltaTime;
			if (alteredSpeedDuration <= 0) {
				speed = originalSpeed;
				alteredSpeedDuration = 0;
			}
		}

		if (damageOverTimeDuration != 0) {
			damageOverTimeDuration -= Time.deltaTime;
			if (damageOverTimeDuration > 0) {
				TakeDamage (damageOverTime);
			} else {
				damageOverTimeDuration = 0;
			}
		}
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

    public void TakeDamage(float damage) {
        health -= damage;
        healthBar.localScale = new Vector3(health / maxHealth, 1.0f, 1.0f);
    }

	public void AlterSpeed(float mulitiplier, float duration) {
		speed *= mulitiplier;
		alteredSpeedDuration = duration;
	}

    protected void Move() {
        if (waypoint >= path.Count) {
            Destroy(gameObject);
            return;
        }
        Vector2 pos = transform.position;
        Vector2 direction = new Vector2(path[waypoint].x, path[waypoint].y) - pos;
    }

	public List<Enemy> CheckZapNeighbors(float zapRadius) {
		List<Enemy> zapped = new List<Enemy> ();
		foreach (Collider2D col in Physics2D.OverlapCircleAll (new Vector2 (transform.position.x, transform.position.y), zapRadius)) {
			if (col.gameObject.tag == "Enemy") {
				zapped.Add (col.gameObject.GetComponent<Enemy>());
			}
		}
		return zapped;
	}

	public void TakeDamageOverTime(float _damage, float duration) {
		damageOverTime = _damage;
		damageOverTimeDuration = duration;
	}
}