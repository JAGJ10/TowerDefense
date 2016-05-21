using UnityEngine;
using System.Collections.Generic;

public class FlameTurret : Turret {
    private int angle = 20;
    private ParticleSystem particles;

    protected override void Awake() {
        base.Awake();
        damage = 1;
        particles = GetComponent<ParticleSystem>();
        //particles.Pause();
    }

    protected override void Fire() {
        if (TargetEnemy()) {
            foreach (var e in enemies) {
                if (Vector3.Angle(e.transform.position - transform.position, transform.forward) < angle) {
                    //enemy is within cone
                    //particles.Play();
                }
            }
        } else {
            //particles.Pause();
        }
    }
}