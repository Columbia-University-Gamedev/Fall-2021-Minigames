using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    private float speed;

    private Transform tr;

    private int damage;

    private bool isPlayerBullet;

    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start() {
    }

    public void InitBullet(float _speed, int _damage, bool isPlayer) {
        tr = this.GetComponent<Transform>();
        speed = _speed;
        damage = _damage;
        isPlayerBullet = isPlayer;

        this.GetComponent<Rigidbody>().velocity = tr.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (isPlayerBullet) {
            if (other.gameObject.CompareTag("Enemy")) {
                SpawnFX();
                Destroy(this.gameObject);
            }
        }
        else {
            if (other.gameObject.CompareTag("Player")) {
                PlayerController pc = other.GetComponent<PlayerController>();
                pc.TakeDamage(damage);
                SpawnFX();
                Destroy(this.gameObject);
            }
        }
    }

    private void SpawnFX() {
        if (impactEffect == null) return;
        Destroy(Instantiate(impactEffect,tr.position,tr.rotation),5.0f);
    }
}
