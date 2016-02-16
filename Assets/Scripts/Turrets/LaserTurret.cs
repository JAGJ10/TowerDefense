using UnityEngine;
using System.Collections.Generic;
using System;

public class LaserTurret : Turret {
    private LineRenderer line;
    private int layerMask = 1 << 8; // enemy layer

    protected override void Start() {
        base.Start();
        damage = 1;
        line = this.GetComponent<LineRenderer>();
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
                    enemy.TakeDamage(damage);
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