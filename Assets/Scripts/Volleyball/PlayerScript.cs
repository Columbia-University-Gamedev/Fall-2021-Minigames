using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2d;
    private Transform tf;
    private SpriteRenderer sr;
    private Keyboard kb;
    private GameObject[] balls;
    private Color defaultColor; //I'll probably remove this later once the actual art is set up

    private bool canJump;
    public bool CanJump
    {
        get { return canJump; }
    }

    private bool onGround;    
    public bool OnGround
    {
        get { return onGround; }
    }
    
    private float jumpStartTime;
    

    private Vector2 movement;
    private Vector2 playerVelocity;

    private bool charging;
    public bool Charging
    {
        get { return charging; }
    }

    private float chargePercentage;
    public float ChargePercentage
    {
        get { return chargePercentage; }
    }

    private float chargeStartTime;

    public bool isPlayerOne = true;
    
    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        tf = gameObject.GetComponent<Transform>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        resetPlayer();
    }

    void Update()
    {
        kb = InputSystem.GetDevice<Keyboard>();

        movement.x = 0;

        if(isPlayerOne){
            move(kb.aKey.isPressed, kb.dKey.isPressed, kb.wKey.isPressed, kb.wKey.wasPressedThisFrame, kb.wKey.wasReleasedThisFrame);
            chargeUp(kb.fKey.wasPressedThisFrame,kb.fKey.wasReleasedThisFrame);
        }
        else{
            move(kb.jKey.isPressed, kb.lKey.isPressed, kb.iKey.isPressed, kb.iKey.wasPressedThisFrame, kb.iKey.wasReleasedThisFrame);
            chargeUp(kb.semicolonKey.wasPressedThisFrame, kb.semicolonKey.wasReleasedThisFrame);
        }

        if(charging){
            chargePercentage = Mathf.Clamp((Time.time-chargeStartTime)/VolleyballConstants.playerChargeTime,0,1.0f);
            sr.color = Color.Lerp(defaultColor,Color.green, chargePercentage);
        }
        
    }

    void FixedUpdate()
    {
        rb2d.velocity = playerVelocity;
    }

    //Controls for movement and jumping
    public void move(bool moveLeft, bool moveRight, bool jump, bool jumpStart, bool jumpStop){

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

    //Charging up and hitting the ball
    public void chargeUp(bool chargeStart, bool chargeRelease){
        
        Rigidbody2D ballRB;
        Transform ballTF;
        BallScript ballS;
        Vector2 playerToBallVec;

        if(chargeStart){
            charging = true;
            chargeStartTime = Time.time;
        }

        if(chargeRelease){

            charging = false;
            sr.color = defaultColor;

            foreach(GameObject ball in balls){

                ballRB = ball.GetComponent<Rigidbody2D>();
                ballTF = ball.GetComponent<Transform>();
                ballS = ball.GetComponent<BallScript>();

                playerToBallVec = (Vector2)(ballTF.position-tf.position);

                if(playerToBallVec.magnitude <= VolleyballConstants.ballMaxHitDistance){
                    ballRB.velocity = playerToBallVec.normalized * VolleyballConstants.playerMaxStoredEnergy * chargePercentage;
                    ballS.bounces += 1;
                    if(ballS.lastHitPlayer != isPlayerOne){
                        ballS.lastHitPlayer = !ballS.lastHitPlayer;
                        ballS.bounces = 0;
                    } else {
                        ballS.bounces += 1;
                    }
                    ballS.enableGravity();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "CanJumpTrigger") onGround = true;
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.name == "CanJumpTrigger") onGround = false;
    }

    void resetPlayer(){
        canJump = false;
        onGround = false;
        charging = false;

        chargeStartTime = 0;
        chargePercentage = 0;

        rb2d.gravityScale = VolleyballConstants.playerGravityScale;

        if(isPlayerOne){
            tf.position = new Vector3(-8.0f,-8.0f,0.0f);
            defaultColor = Color.red;
        } 
        else{
            tf.position = new Vector3(8.0f,-8.0f,0.0f);
            defaultColor = Color.blue;
        }
        sr.color = defaultColor;

        balls = GameObject.FindGameObjectsWithTag("Ball");
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
