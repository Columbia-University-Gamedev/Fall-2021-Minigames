using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private static Transform _playerTransform;

    public static Transform PlayerTransform { get { return _playerTransform; } }
    
    public bool canMove;
    private Vector2 moveVector;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private bool grounded;
    private Animator anim;

    [SerializeField]
    float _horizontalAcceleration = 20f; // meters per second per second

    [SerializeField]
    float _maxHorizontalSpeed = 1f;

    [SerializeField]
    float _groundFrictionCoefficient = 0.2f; 
    
    [SerializeField]
    float _floorcastFudgeFactor = 0.2f;
    
    [SerializeField]
    float _jumpHeight = 5f; // in meters
    
    [SerializeField]
    bool _startingDirectionIsLeft = false;

    float _leftScaleSign = -1f; 
    
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private int maxHp;
    private static int hp;
    private static AudioSource audioSource;

    void Awake()
    {
        _playerTransform = transform;
    }

    void Start()
    {
        hp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        canMove = true;
        HealthUI.Instance.InitializeHP(hp);

        anim = GetComponent<Animator>();
        Debug.Log(anim);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(moveVector.x) ||
            Mathf.Abs(rb.velocity.x) < _maxHorizontalSpeed)
        {            
            float force = rb.mass * moveVector.x * _horizontalAcceleration; 
            rb.AddForce(force * Vector2.right);
        }

        Vector2 extents = playerCollider.bounds.extents; 
        float colliderHeight = extents.y + 0.1f;
        float checkRadius = 0.5f * extents.magnitude;
        Vector2 offset = (Vector2)playerCollider.bounds.center + colliderHeight * Vector2.down; 
        
        Collider2D ground = Physics2D.OverlapCircle(offset, checkRadius, _groundMask);
        
        grounded = ground != null; 
        anim.SetBool("grounded", grounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        
        if (grounded)
        {
            Vector2 normal =
                (rb.worldCenterOfMass - ground.ClosestPoint(rb.worldCenterOfMass))
                .normalized;
            
            Vector2 tangent = Quaternion.FromToRotation(Vector3.up, Vector3.right) * normal;
            float zAngle = Vector2.Angle(normal, Vector2.up);
            
            // orientation 
            if (Mathf.Abs(zAngle) < 15f)
            transform.up = Vector3.Lerp(transform.up, normal, Time.fixedDeltaTime * 5f);
            
            
            // friction (only apply if no lateral force on player) 
            if (Mathf.Abs(moveVector.x) < 0.07f)
            {
                // project gravity vector onto the ground normal 
                float dottedAcceleration = Mathf.Abs(Vector2.Dot(normal, Physics2D.gravity));
                float normalForce = rb.mass * dottedAcceleration;

                // does velocity oppose tangent? friction needs to oppose velocity. 
                int sign = Vector2.Dot(tangent, rb.velocity) < 0f ? 1 : -1;

                // construct friction vector
                Vector2 friction = sign * _groundFrictionCoefficient * normalForce * tangent;

                rb.AddForce(friction);
            }

            anim.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        }

        // deterministic flipping
        if (Mathf.Abs(rb.velocity.x) > 0.2f)
        {
            float flag1 = rb.velocity.x < 0 ? 1f : -1f;
            float flag2 = _startingDirectionIsLeft ? 1f : -1f;

            float signX = _leftScaleSign * flag1 * flag2;

            Vector3 scale = transform.localScale;

            scale.x = Mathf.Abs(scale.x) * signX;

            transform.localScale = scale;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.performed && canMove ? context.ReadValue<Vector2>() : Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return; 
        
        if (grounded && canMove)
        {
            rb.AddForce(CalculateJumpForce() * Vector2.up);
        }
    }

    float CalculateJumpForce()
    {
        /*
            F = (mass (targetVelocity - current_velocity)) / Time.deltaTime
        */
        
        float h = _jumpHeight;
        float g = Physics2D.gravity.magnitude;

        float t_flight = Mathf.Sqrt(2 * h / g);

        float vf = h / t_flight + 0.5f * g * t_flight;

        float m = rb.mass;
        float v0 = rb.velocity.y;
        float t_impulse = Time.fixedDeltaTime;

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

    public static void TakeDamage()
    {
        hp--;
        HealthUI.Instance.UpdateHP(hp);
        audioSource.Play();
        if(hp == 0)
        {
            StoryManager.currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("GameOver");
        }
    }
}
