using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2d;
    private Transform tf;
    private Keyboard kb;

    private bool canJump;
    private bool onGround;
    private float jumpStartTime;
    private Vector2 movement;
    private Vector2 playerVelocity;

    public bool isPlayerOne = true;
    
    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        tf = gameObject.GetComponent<Transform>();
    }

    void Start()
    {
        canJump = false;
        onGround = false;

        rb2d.gravityScale = VolleyballConstants.playerGravityScale;
    }

    void Update()
    {
        kb = InputSystem.GetDevice<Keyboard>();

        movement.x = 0;

        if(isPlayerOne) move(kb.aKey.isPressed, kb.dKey.isPressed, kb.fKey.isPressed, kb.wKey.isPressed, kb.wKey.wasPressedThisFrame, kb.wKey.wasReleasedThisFrame);
        else move(kb.jKey.isPressed, kb.lKey.isPressed, kb.semicolonKey.isPressed, kb.iKey.isPressed, kb.iKey.wasPressedThisFrame, kb.iKey.wasReleasedThisFrame);
        
    }

    void FixedUpdate()
    {
        rb2d.velocity = playerVelocity;
    }

    void move(bool moveLeft, bool moveRight, bool charging, bool jump, bool jumpStart, bool jumpStop){

        movement.x = 0;

        if(moveLeft) movement.x -= VolleyballConstants.playerSpeed;
        if(moveRight) movement.x += VolleyballConstants.playerSpeed;
        if(charging) movement.x *= VolleyballConstants.playerChargeSpeedMultiplier;

        if(jumpStart && onGround){
            canJump = true;
            jumpStartTime = Time.time;
        } 
        if(jumpStop) canJump = false;

        if(jump && canJump && Time.time - jumpStartTime <= VolleyballConstants.playerMaxJumpTime){
            rb2d.velocity = new Vector2(rb2d.velocity.x,VolleyballConstants.playerJumpSpeed);
        }

        playerVelocity.x = movement.x;
        playerVelocity.y = rb2d.velocity.y;

        //tf.Translate(movement*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "CanJumpTrigger") onGround = true;
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.name == "CanJumpTrigger") onGround = false;
    }

    // void OnP1Move(InputValue v){
    //     if(isPlayerOne){
    //         movePlayer(v);   
    //     }
    // }

    // void OnP2Move(InputValue v){
    //     if(!isPlayerOne){
    //         movePlayer(v);
    //     }
    // }

    // void OnP1Jump(InputAction a){
    //     Debug.Log();
    // }

    // void movePlayer(InputValue v){
    //     movement = v.Get<Vector2>();
    //     playerVelocity.x = movement.x * VolleyballConstants.playerSpeed * VolleyballConstants.playerVelocityMultiplier;
    // }

}
