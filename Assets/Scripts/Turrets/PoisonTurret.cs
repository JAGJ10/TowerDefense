using UnityEngine;

public class PoisonTurret : Turret {
	private float damageDuration = 2.0f;
	private float angle = 60.0f;
	private float cooldownLimit = 2.0f;
	private float cooldown = 0.0f;

	private ParticleSystem particles;
	private float particlesPlayTime = 1.0f;

    protected override void Awake() {
        base.Awake();
        damage = 0.3f;
        particles = GetComponentInChildren<ParticleSystem>();
    }

	protected override void Update() {
		base.Update();
		Fire();
	}

    protected override void Fire() {
		if (TargetEnemy() && cooldown > cooldownLimit) {
			cooldown = 0;
			foreach (GameObject e in enemies) {
				Vector3 vectorToEnemy = (e.transform.position - transform.position).normalized;
				float angleDotProduct = Vector3.Dot(vectorToEnemy, transform.up);
				float enemyAngle = 360;
				if (angleDotProduct != 1) {
					 enemyAngle = Mathf.Acos(angleDotProduct) * Mathf.Rad2Deg;
				}
				//Only do damage if within arc
				if (Mathf.Approximately(angleDotProduct, 1) || enemyAngle < angle) {
					e.GetComponent<Enemy>().TakeDamageOverTime(damage, damageDuration);
					if (!particles.isPlaying) {
						particles.Play();
					}
				}
			}
		} else {
			cooldown += Time.deltaTime;
			if (cooldown > particlesPlayTime) {
				particles.Stop();
			}
		}
    }
}