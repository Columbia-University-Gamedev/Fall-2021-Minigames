using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
    [Tooltip("Bullet prefab")] public GameObject bullet;
    [Tooltip("Time between shots")] public float fireDelay;
    [Tooltip("Velocity of bullet")] public float bulletVelocity;
    [Tooltip("Damage of bullet")] public int damage;

    [Tooltip("Use animation events instead of normal timers?")] 
    public bool animEvent;

    private float timeSinceLastShot;
    private Animator anim;
    private Transform tr;
    
    // Start is called before the first frame update
    void Start() {
        timeSinceLastShot = Random.Range(0.0f,fireDelay/2.0f);
        tr = transform;
        
        Animator anim = GetComponent<Animator>();
        if (anim != null) {
            AnimatorStateInfo
                state = anim.GetCurrentAnimatorStateInfo(0); //could replace 0 by any other animation layer index
            float moment = Random.Range(0f, 1f);
            anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        }
    }

    // Update is called once per frame
    void Update() {
        timeSinceLastShot += Time.deltaTime;
        if (animEvent) return;
        if (timeSinceLastShot > fireDelay) {
            Fire();
            timeSinceLastShot -= fireDelay;
        }
    }

    void Fire() {
        Bullet bscript = Instantiate(
            bullet, tr.position, tr.rotation).GetComponent<Bullet>();
        bscript.bulletVelocity = bulletVelocity;
        bscript.damage = damage;
        bscript.isPlayer = false;
    }

    void FireAnim() {
        if (animEvent) 
            Fire();
    }
}
