using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    private Transform tr;

    private Rigidbody rb;

    private Vector3 MINBOUNDS = new Vector3(0,-5.5f, -10);

    private Vector3 MAXBOUNDS = new Vector3(0,5.5f, 10);

    public int health;

    public int max_health;

    public GameObject bullet;

    public int bullet_damage;

    public int bullet_speed;
    // Start is called before the first frame update
    void Start() {
        tr = this.transform;
        rb = this.GetComponent<Rigidbody>();

        health = max_health;
    }

    // Update is called once per frame
    void Update() {
        //tr.Translate(moveSpeed*Time.deltaTime*(
        //    new Vector3(0.0f,movement.y,movement.x)));
        
        Vector3 curPos = tr.position;
        transform.position = new Vector3(curPos.x,
            Mathf.Clamp(curPos.y,
                MINBOUNDS.y, MAXBOUNDS.y),
            Mathf.Clamp(curPos.z,
                MINBOUNDS.z, MAXBOUNDS.z));

        Vector3 vel = rb.velocity;
        float x_tilt = vel.z > 0 ? 0 : vel.z * 1.5f;
        tr.eulerAngles = new Vector3(x_tilt,
            0, vel.y * -5);
    }

    void FixedUpdate() {
        rb.AddForce(moveSpeed*(new Vector3(0.0f,movement.y,movement.x)));
    }

    private Vector2 movement;
    void OnMovement(InputValue res) {
        movement = res.Get<Vector2>();
    }

    public void TakeDamage(int damage) {
        health = Mathf.Clamp(health - damage, 0, max_health);
    }

    public void OnFire() {
        GameObject fired_bullet = Instantiate(
            bullet, tr.position, Quaternion.identity);
        fired_bullet.GetComponent<BulletScript>().InitBullet(
            bullet_speed,bullet_damage,true);
        
        Destroy(fired_bullet,10f);
    }
}
