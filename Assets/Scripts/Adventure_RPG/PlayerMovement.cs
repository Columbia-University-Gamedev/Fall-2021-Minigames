using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove;
    private Vector2 moveVector;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private bool grounded;

    [SerializeField]
    float _horizontalAcceleration = 20f; // meters per second per second

    [SerializeField]
    float _maxHorizontalSpeed = 10f;

    [SerializeField]
    float _horizontalDrag = 0.2f; 
    
    [SerializeField]
    float _floorcastFudgeFactor = 0.2f;
    
    [SerializeField]
    float _jumpHeight = 5f; // in meters

    [SerializeField]
    float _jumpTime = 1f; // in seconds

    [SerializeField]
    float _gravityScaleInfluence = 0.75f; // how much of rigid body's gravity scale to take into account

    [SerializeField] private LayerMask ground;    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //horizontal movement components
        float horizontalComponent = 0f;
        horizontalComponent += moveVector.x;

        horizontalComponent = Mathf.Clamp(horizontalComponent, -1f, 1f);

        if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(horizontalComponent) ||
            Mathf.Abs(rb.velocity.x) < _maxHorizontalSpeed)
        {
            rb.velocity += horizontalComponent * Vector2.right * _horizontalAcceleration * Time.deltaTime;
        }

        if (horizontalComponent == 0f)
        {
            rb.velocity -= rb.velocity.x * _horizontalDrag * Vector2.right;
        }

       
        float depth = playerCollider.bounds.extents.y + 0.3f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, depth, ground);
        grounded = (hit.collider != null);
        if (grounded)
        {            
            transform.up = Vector2.Lerp(transform.up, hit.normal, Time.deltaTime * 5f);
        }
    }

    public void OnMove(InputAction.CallbackContext context){
        if (canMove)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            moveVector = new Vector3(direction.x, 0, direction.y);
        }   
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (grounded && canMove)
        {
            // do player jump
            rb.AddForce(Vector2.up * CalculateJumpForce());
        }
    }

    float CalculateJumpForce()
    {
        /*
            F = (mass (targetVelocity - current_velocity)) / Time.deltaTime
         */

        // doesnt' work perfectly but if you play with the jump inputs 
        // you can get good results

        float h = _jumpHeight;
        float t_flight = _jumpTime;

        float vf = h / t_flight + 0.5f * Physics.gravity.magnitude * rb.gravityScale * _gravityScaleInfluence * t_flight;

        float m = rb.mass;
        float v0 = rb.velocity.y;
        float t_impulse = Time.deltaTime; 

        return m * (vf - v0) / t_impulse; 
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
