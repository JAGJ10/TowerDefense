using UnityEngine;
using System.Collections.Generic;

public class SimpleEnemy : Enemy {

    protected override void Start() {
        base.Start();
        health = 100;
        speed = 0.05f;
    }
}