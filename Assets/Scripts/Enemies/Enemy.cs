﻿using UnityEngine;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {
    protected float maxHealth;
    protected float health;
    protected int value;
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
        if (health <= 0) {
            GameManager.Instance.UpdateMoney(value);
            Destroy(gameObject);
        }

		if (alteredSpeedDuration != 0) {
			alteredSpeedDuration -= Time.deltaTime;
			if (alteredSpeedDuration <= 0) {
				speed = originalSpeed;
				alteredSpeedDuration = 0;
			}
		}

		if (damageOverTimeDuration != 0) {
			damageOverTimeDuration -= Time.deltaTime;
			if (damageOverTimeDuration > 0) TakeDamage(damageOverTime);
			else damageOverTimeDuration = 0;
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

    public void TakeDamage(float damage) {
        health -= damage;
        healthBar.localScale = new Vector3(health / maxHealth, 1.0f, 1.0f);
    }

    public void TakeDamageOverTime(float damage, float duration) {
		damageOverTime = damage;
		damageOverTimeDuration = duration;
	}

    public void AlterSpeed(float mulitiplier, float duration) {
        speed *= mulitiplier;
        alteredSpeedDuration = duration;
    }
}