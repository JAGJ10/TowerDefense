using UnityEngine;
using System.Collections.Generic;

public class SimpleEnemy : Enemy {

    protected override void Awake() {
        base.Awake();
        health = 100;
        speed = 0.05f;
    }
}