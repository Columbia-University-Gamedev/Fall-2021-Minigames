using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
    [Tooltip("Bullet prefab")] public GameObject bullet;
    [Tooltip("Time between shots")] public float fireDelay;
    [Tooltip("Velocity of bullet")] public float bulletVelocity;
    [Tooltip("Damage of bullet")] public int damage;

    private float timeSinceLastShot;

    private Transform tr;
    
    // Start is called before the first frame update
    void Start() {
        timeSinceLastShot = 0.0f;
        tr = transform;
    }

    // Update is called once per frame
    void Update() {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > fireDelay) {
            Fire();
            timeSinceLastShot = 0.0f;
        }
    }

    void Fire() {
        Bullet bscript = Instantiate(
            bullet, tr.position, tr.rotation).GetComponent<Bullet>();
        bscript.bulletVelocity = bulletVelocity;
        bscript.damage = damage;
        bscript.isPlayer = false;
    }
}
