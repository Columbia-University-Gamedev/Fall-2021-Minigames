using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;

    private Vector3 minBounds = new Vector3(0,-5.5f,-10);

    private Vector3 maxBounds = new Vector3(0,5.5f, 10);

    private Transform tr;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake() {
        tr = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
    //     transform.Translate(Time.deltaTime * moveSpeed * 
    //         new Vector3(0.0f, movement.y, movement.x),
    // Space.World);
        
        Vector3 curPos = tr.position;
        transform.position = new Vector3(curPos.x,
            Mathf.Clamp(curPos.y,
            minBounds.y, maxBounds.y),
            Mathf.Clamp(curPos.z,
                minBounds.z, maxBounds.z));

        Vector3 vel = rb.velocity;
        float x_tilt = vel.z > 0 ? 0 : vel.z * 1.5f;
        transform.eulerAngles = new Vector3(x_tilt,
            0, vel.y * -5);
    }

    private void FixedUpdate() {
        rb.AddForce(moveSpeed * new Vector3(0.0f, movement.y, movement.x));
    }

    private Vector2 movement;
    void OnMovement(InputValue res) {
        movement = res.Get<Vector2>();
    }
}
