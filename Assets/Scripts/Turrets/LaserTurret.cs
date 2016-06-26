using UnityEngine;
using System.Collections.Generic;
using System;

public class LaserTurret : Turret {
    private LineRenderer line;
    
    protected override void Awake() {
        base.Awake();
        line = GetComponent<LineRenderer>();
        damage = 30.0f;
    }

    protected override void Fire() {
        if (TargetEnemy()) {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, layerMask);
            if (hit.collider != null) {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null) {
                    line.SetPosition(1, hit.point);
                    enemy.TakeDamage(Time.deltaTime * damage);
                }
            }
        } else {
            line.enabled = false;
        }
    }

    public void FixedUpdate() {
        Fire();
    }
}