using UnityEngine;

public class Wall : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    public Sprite wall;
    public Sprite turret;

    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /*public void Update() {
        spriteRenderer.sprite = wall;
        if (GameManager.Instance.placeTurret) {
            if (Input.GetMouseButton(0)) {
                int x = (int)(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);    
                int y = (int)(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                if (x == (int)transform.position.x)
                    spriteRenderer.sprite = turret;
           }
       }
    }*/

    public void OnMouseEnter() {
        if (GameManager.Instance.placeTurret) {
            if (Input.GetMouseButton(0)) {
                print("joel");
                spriteRenderer.sprite = turret;
            }
        }
    }

    public void OnMouseExit() {
        if (spriteRenderer.sprite == turret) {
            spriteRenderer.sprite = wall;
        }
    }
}
