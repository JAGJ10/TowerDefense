using UnityEngine;
using System.Collections.Generic;

public class ProjectileTurret : Turret {
    protected List<GameObject> projectiles { get; set; }
    private int pooledAmount = 10;
    public GameObject projectile;
    private float fireRate = 0.1f;
    private float speed = 8.0f;

    protected override void Start() {
        base.Start();
        damage = 10;
        projectiles = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++) {
            GameObject obj = Instantiate(projectile) as GameObject;
            obj.GetComponent<Projectile>().damage = damage;
            obj.SetActive(false);
            //obj.transform.SetParent(this.transform);
            projectiles.Add(obj);
        }

        InvokeRepeating("Fire", 0.0f, fireRate);
    }

    protected override void Fire() {
        if (TargetEnemy()) {
            for (int i = 0; i < projectiles.Count; i++) {
                if (!projectiles[i].activeSelf) {
                    projectiles[i].transform.position = transform.position;
                    projectiles[i].transform.rotation = transform.rotation;
                    projectiles[i].SetActive(true);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = speed * transform.up;
                    break;
                }
            }
        }
    }
}
