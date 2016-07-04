using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public abstract class Turret : MonoBehaviour, IPointerClickHandler {
    protected List<GameObject> enemies;
    protected float damage;
    protected int cost;
    protected CircleCollider2D range;
    protected SpriteRenderer circle;
    protected int layerMask = 1 << 8; //enemy layer
    private Point position;

    protected virtual void Awake() {
        enemies = new List<GameObject>();
        range = GetComponentInChildren<CircleCollider2D>();
        circle = transform.Find("circle").GetComponent<SpriteRenderer>();
        circle.transform.localScale = new Vector2(range.radius, range.radius);
        circle.enabled = false;
        position = new Point((int)transform.position.x, (int)transform.position.y);
    }

    protected virtual bool TargetEnemy() {
        enemies.RemoveAll(enemy => enemy == null);
        if (enemies.Count > 0) return true;
        else return false;
    }

    protected abstract void Fire();

    protected virtual void Update() {
        if (TargetEnemy()) transform.rotation = Quaternion.LookRotation(transform.forward, enemies[0].transform.position - transform.position);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) enemies.Add(other.gameObject);
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy")) enemies.Remove(other.gameObject);
    }

    public virtual void Upgrade() {
        range.radius *= 1.2f;
        circle.transform.localScale = new Vector2(range.radius, range.radius);
    }

    public virtual void ToggleOff() {
        circle.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData) {
        circle.enabled = true;
        GameManager.Instance.ToggleUpgradeMenuOn(position);
    }
}