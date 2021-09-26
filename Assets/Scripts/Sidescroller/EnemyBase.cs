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
        timeSinceLastShot = Random.Range(0.0f,fireDelay/2.0f);
        tr = transform;
        
        Animator anim = GetComponent<Animator>();
        if (anim != null) {
            AnimatorStateInfo
                state = anim.GetCurrentAnimatorStateInfo(0); //could replace 0 by any other animation layer index
            anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        }
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
