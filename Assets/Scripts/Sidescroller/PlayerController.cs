using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Tooltip("Multiplier on force applied to player")]
    public float moveSpeed;

    public int maxHealth;
    public int health { get; set; }

    private static readonly Vector3 MINBOUNDS = new Vector3(0,-5.5f,-10);
    private static readonly Vector3 MAXBOUNDS = new Vector3(0,5.5f, 10);

    private Transform tr;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Awake() {
        tr = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
    }

    void Start() {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update() {
    //     transform.Translate(Time.deltaTime * moveSpeed * 
    //         new Vector3(0.0f, movement.y, movement.x),
    // Space.World);
        
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

    private void FixedUpdate() {
        rb.AddForce(moveSpeed * new Vector3(0.0f, movement.y, movement.x));
    }

    private Vector2 movement;
    void OnMovement(InputValue res) {
        movement = res.Get<Vector2>();
    }

    public void InflictDamage(int amount) {
        health = Mathf.Clamp(health - amount, 0, maxHealth);
    }
}
