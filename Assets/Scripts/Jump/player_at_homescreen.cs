using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class player_at_homescreen : MonoBehaviour
{
    
    Collider2D playerCollider;
    private Rigidbody2D rb;
    private int count;
    public LayerMask ground;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f; 
    public float jumpForce = 2f;
    private Animator anim;
    public bool grounded;
    private bool facingLeft = false;
    float cameraBottomBounds;
    float cameraLeftBounds;
    float cameraRightBounds;

    

    [SerializeField] private Transform bottomBounds;


    Vector2 moveVector;

    [SerializeField]
    float _horizontalAcceleration = 20f; // meters per second per second

    [SerializeField]
    float _maxHorizontalSpeed = 10f;

    [SerializeField]
    float _horizontalDrag = 0.2f; 

    [SerializeField]
    float _jumpHeight = 5f; // in meters

    [SerializeField]
    float _jumpTime = 1f; // in seconds

    [SerializeField]
    float _floorcastFudgeFactor = 0.23f; // magic number found through playtesting

    [SerializeField]
    float _gravityScaleInfluence = 0.75f; // how much of rigid body's gravity scale to take into account

    [SerializeField] Transform leftBounds;
    [SerializeField] Transform rightBounds;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        rb.freezeRotation = true; 
        anim = GetComponent<Animator>();

        cameraBottomBounds = Camera.main.ViewportToWorldPoint(new Vector3 (1f, 1f, 0f)).y;
        
        Debug.Log(cameraLeftBounds);
    }


    void Update(){
        anim.SetBool("grounded", grounded);
        //Debug.Log("Player Position: X = " + transform.position.x + " --- Y = " + transform.position.y);
                
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = detectGround();
        if (grounded){
            rb.AddForce(Vector2.up * CalculateJumpForce());
        }

       

        if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(moveVector.x) ||
            Mathf.Abs(rb.velocity.x) < _maxHorizontalSpeed)
        {
            rb.velocity += moveVector * _horizontalAcceleration * Time.deltaTime;
        }

        if (moveVector.x == 0f)
        {
            rb.velocity -= rb.velocity.x * _horizontalDrag * Vector2.right;
        }

        anim.SetFloat("y_velocity", rb.velocity.y);
        if (moveVector.x < 0 && facingLeft || moveVector.x  > 0 && !facingLeft)
        {
            flip();
        }

        if (transform.position.x > rightBounds.position.x)
        {
            Debug.Log("it is past right bound");
            rb.position = new Vector3(leftBounds.position.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < leftBounds.position.x)
        {
            Debug.Log("it is past left bound");
            rb.position = new Vector3(rightBounds.position.x, transform.position.y, transform.position.z);
        }
        
        
    }
    void flip()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    float CalculateJumpForce()
    {
        float h = _jumpHeight;
        float t_flight = _jumpTime;

        float vf = h / t_flight + 0.5f * Physics.gravity.magnitude * rb.gravityScale * _gravityScaleInfluence * t_flight;

        float m = rb.mass;
        float v0 = rb.velocity.y;
        float t_impulse = Time.deltaTime; 

        return m * (vf - v0) / t_impulse; 
    }
   

    bool detectGround()
    {
        float size = playerCollider.bounds.extents.magnitude; 

        Vector3 playerPosLeft = playerCollider.bounds.center - playerCollider.bounds.extents;
		Vector3 playerPosRight = new Vector3(playerPosLeft.x + 2f * playerCollider.bounds.extents.x, playerPosLeft.y, playerPosLeft.z);
		RaycastHit2D left = Physics2D.Raycast(playerPosLeft, Vector2.down, _floorcastFudgeFactor * size, ground);
		RaycastHit2D right = Physics2D.Raycast(playerPosRight, Vector2.down, _floorcastFudgeFactor * size, ground);


		Color rayColor;
		if (left.collider != null || right.collider != null)
		{
		   rayColor = Color.green;
		}
		else
		{
		   rayColor = Color.red;
		}	

		Debug.DrawRay(playerPosLeft, Vector2.down * _floorcastFudgeFactor * size, rayColor);
		Debug.DrawRay(playerPosRight, Vector2.down * _floorcastFudgeFactor * size, rayColor);
		
		return (left.collider != null || right.collider != null);
    }

    void Jump() 
    { 
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
    } 
    void OnTriggerEnter2D(Collider2D other)
    {
    }
    void BetterJump()
    {
		if (rb.velocity.y < 0)
		{
		    rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
		}
		else if (rb.velocity.y > 0)
		{
		    rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
    }
    
    public void OnMovementChange(InputAction.CallbackContext context){
        Debug.Log("on move");
        Vector2 direction = context.ReadValue<Vector2>();
        moveVector = new Vector3(direction.x, 0, direction.y);
    }
    
    public void OnStartGame(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name == "HomeScreen")
        {
            Buttons.OnStart();
        }
    }

}
