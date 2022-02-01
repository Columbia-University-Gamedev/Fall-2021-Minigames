using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {
    private float timeSinceLastShot;

    public float shotDelay = 5.0f;

    public GameObject bullet;

    public float bulletSpeed;

    public int bulletDamage;
    // Start is called before the first frame update
    void Start() {
        timeSinceLastShot = Random.Range(-(shotDelay/2),shotDelay/2);
    }

    // Update is called once per frame
    void Update() {
        if (shotDelay > 0) {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shotDelay) {
                Fire();
                timeSinceLastShot -= shotDelay;
            }
        }
    }

    public void Fire() {
        GameObject fired_bullet = Instantiate(
            bullet,transform.position,transform.rotation);
        fired_bullet.GetComponent<BulletScript>().InitBullet(
            bulletSpeed,bulletDamage,false);
        
        Destroy(fired_bullet,10f);
    }
}
