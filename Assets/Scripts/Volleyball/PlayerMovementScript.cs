using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2d;
    private Transform tf;
    private bool isGrounded;
    
    private Vector2 movement;
    private Vector2 playerVelocity;

    public bool isPlayerOne = true;
    
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tf.Translate(movement*Time.deltaTime);
    }

    void FixedUpdate()
    {
    }

    void OnP1Move(InputValue m){
        if(isPlayerOne){
            movement = m.Get<Vector2>();
            playerVelocity.x = movement.x * VolleyballConstants.playerSpeed * VolleyballConstants.playerVelocityMultiplier;
        }
    }

}
