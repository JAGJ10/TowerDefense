public class SimpleEnemy : Enemy {

    protected override void Awake() {
        base.Awake();
        maxHealth = 100;
        health = 100;
        speed = 0.05f;
        value = 1;
    }
}