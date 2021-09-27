using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletVelocity;

    public int damage;

    [Tooltip("Whether the bullet is launched by the player")] public bool isPlayer;

    [Tooltip("Visual effect to spawn on impact")]
    public GameObject hitEffect;
    
    private const float FIELD_WIDTH = 20f; 
    //used for calculating bullet lifetime
    //set to the approximate length of the screen in world units
    
    // Start is called before the first frame update
    void Start() {
        this.GetComponent<Rigidbody>().AddRelativeForce(
            Vector3.forward*bulletVelocity,
            ForceMode.VelocityChange);
        
        Destroy(this.gameObject,FIELD_WIDTH/bulletVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (isPlayer) { //this is a player bullet, so it should hit enemies
            if (other.gameObject.CompareTag("Player")) return;

            Impact();
        }
        else { //enemy bullet, so it should hit players
            if (!other.gameObject.CompareTag("Player")) return;
            
            PlayerController.Instance.InflictDamage(damage);

            Impact();
        }
    }

    private void Impact() {
        if (hitEffect != null) {
            Transform tr = transform;
            GameObject eff = Instantiate(hitEffect, tr.position,
                tr.rotation * Quaternion.Euler(0,180.0f,0));
            Destroy(eff, 5.0f);
        }
        Destroy(this.gameObject);
    }
}
