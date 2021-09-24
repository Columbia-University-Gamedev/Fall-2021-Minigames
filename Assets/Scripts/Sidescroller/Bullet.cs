using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletVelocity;

    public int damage;

    [Tooltip("Whether the bullet is launched by the player")] public bool isPlayer;

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
            return;
        }
        else { //enemy bullet, so it should hit players
            if (!other.gameObject.CompareTag("Player")) return;
            
            PlayerController.Instance.InflictDamage(damage);
            
            Destroy(this.gameObject);
        }
    }
}
