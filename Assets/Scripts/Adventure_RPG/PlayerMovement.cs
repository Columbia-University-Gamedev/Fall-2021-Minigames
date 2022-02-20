using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 moveVector;
    private Rigidbody2D rb;
    private Collider2D playerCollider;

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
    }

    public void OnMove(InputAction.CallbackContext context){
        Vector2 direction = context.ReadValue<Vector2>();
        moveVector = new Vector3(direction.x, 0, direction.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        bool grounded = detectGround();
        Debug.Log(grounded);
        if (grounded)
        {
            // do player jump
            rb.AddForce(Vector2.up * CalculateJumpForce());
        }
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
}
