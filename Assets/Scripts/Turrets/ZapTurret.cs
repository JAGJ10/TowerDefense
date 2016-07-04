using UnityEngine;
using System.Collections.Generic;

public class ZapTurret : Turret {
	private LineRenderer line;
	private float cooldownLimit = 2.0f;
	private float cooldown = 0.0f;
	private float zapRadius = 4.0f;
	private Enemy currentlyTracking;

    protected override void Awake() {
        base.Awake();
        damage = 5;
		line = GetComponent<LineRenderer>();
		line.SetWidth(1, 1);
    }

	protected override void Update() {
		base.Update();
		Fire();
	}

    protected override void Fire() {
        //TODO: Fix this logic
		if (TargetEnemy() && cooldown > cooldownLimit) {
			cooldown = 0.0f;
			line.enabled = true;
			line.SetPosition(0, transform.position);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, layerMask);
			if (hit.collider != null) {
				Enemy enemy = hit.collider.GetComponent<Enemy>();
				if (enemy != null) {
					currentlyTracking = enemy;
					line.SetPosition(1, hit.point);
					enemy.TakeDamage(damage);
					//Get zapped neighbors and extend line to zapped neighbors
					List<Enemy> zapNeighbors = FindZapNeighbors(enemy, zapRadius);
					for (int i = 2; i < zapNeighbors.Count + 2; i++) {
						line.SetVertexCount(i + 1);
						line.SetPosition(i, zapNeighbors[i - 2].transform.position);
						zapNeighbors[i - 2].TakeDamage(damage);
					}
				}
			}
		} else {
			cooldown += Time.deltaTime;
			line.enabled = false;
			//Reset later vertices
			line.SetVertexCount(2);
		}
    }

    private List<Enemy> FindZapNeighbors(Enemy enemy, float zapRadius) {
        List<Enemy> zapped = new List<Enemy>();
        foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(enemy.transform.position.x, enemy.transform.position.y), zapRadius)) {
            if (col.gameObject.tag == "Enemy") {
                zapped.Add(col.gameObject.GetComponent<Enemy>());
            }
        }
        return zapped;
    }
}