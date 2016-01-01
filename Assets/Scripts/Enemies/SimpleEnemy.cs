using UnityEngine;
using System.Collections.Generic;

public class SimpleEnemy : Enemy {

    protected override void Start() {
        base.Start();
        health = 10;
        speed = 0.02f;
    }
}