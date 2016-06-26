using UnityEngine;
using System.Collections.Generic;

public class ProjectileTurret : Turret {
    private int pooledAmount = 30;
    private float fireRate = 0.1f;
    private float speed = 12.0f;

    private List<GameObject> projectiles;
    public GameObject projectile;

    protected override void Awake() {
        base.Awake();
        damage = 10.0f;
        projectiles = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++) {
            GameObject obj = Instantiate(projectile) as GameObject;
            obj.GetComponent<Projectile>().damage = damage;
            obj.SetActive(false);
            //obj.transform.SetParent(transform);
            projectiles.Add(obj);
        }

        //InvokeRepeating("Fire", 0.0f, fireRate);
    }

    protected override void Update() {
        base.Update();
        //TODO: Is this the best way to set a delay?
        if (TargetEnemy() && !IsInvoking("Fire")) Invoke("Fire", fireRate);
    }

    protected override void Fire() {
        //if (TargetEnemy()) {
            foreach (GameObject proj in projectiles) {
                if (!proj.activeSelf) {
                    proj.transform.position = transform.position;
                    proj.transform.rotation = transform.rotation;
                    proj.SetActive(true);
                    proj.GetComponent<Rigidbody2D>().velocity = speed * transform.up;
                    break;
                }
            }
        //}
    }
}
