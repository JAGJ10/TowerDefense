using UnityEngine;
using System.Collections.Generic;

public abstract class Turret : MonoBehaviour {

    protected List<GameObject> enemies { get; set; }
    protected int damage { get; set; }
    protected int cost { get; set; }
    protected CircleCollider2D range { get; set; }

    protected virtual void Start() {
        enemies = new List<GameObject>();
        range = this.GetComponentInChildren<CircleCollider2D>();
    }

    protected virtual bool TargetEnemy() {
        enemies.RemoveAll(enemy => enemy == null);
        if (enemies.Count > 0) {
            return true;
        } else {
            return false;
        }
    }

    protected abstract void Fire();

    protected virtual void Update() {
        if (TargetEnemy()) transform.rotation = Quaternion.LookRotation(transform.forward, enemies[0].transform.position - transform.position);
	}

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            enemies.Add(other.gameObject);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            enemies.Remove(other.gameObject);
        }
    }

    public virtual void Upgrade() {
        range.radius *= 1.2f;
    }

    //protected void OnMouseDown() {
    //    GameManager.Instance.upgradeMode = true;
    //    GameManager.Instance.selectedTurret.setPoint((int)transform.position.x, (int)transform.position.y);
   // }
}