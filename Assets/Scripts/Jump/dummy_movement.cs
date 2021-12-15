using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class dummy_movement : MonoBehaviour
{
    
    Collider2D playerCollider;
    private Rigidbody2D rb;
    private float count;
    public LayerMask ground;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f; 
    public float jumpForce = 2f;
    private Animator anim;
    public bool grounded;
    private bool facingLeft = false;
    float cameraBottomBounds;
    public TextMeshProUGUI ScoreText;
    public GameObject coinCollected;

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


    [SerializeField]
    bool _startingDirectionIsLeft = true;

    float _leftScaleSign = -1f; 

    bool _areControlsEnabled = true;

    bool _areCollisionsEnabled = true;
    bool AreCollisionsEnabled
    {
        get { return _areCollisionsEnabled; }

        set
        {
            _areCollisionsEnabled = value;

            Collider2D[] colliders = new Collider2D[rb.attachedColliderCount];
            rb.GetAttachedColliders(colliders);

            foreach (var col in colliders)
            {
                col.enabled = value; 
            }
        }
    }

    bool _isDead = false;
    public bool IsDead
    {
        get { return _isDead; }
    }

    float _timeOfDeath;

    [SerializeField]
    float _gameOverTimeout = 1.5f; // what's the delay before the gameover screen transition?


    [SerializeField]
    int _maxHealth = 3;

    int _health;
    public int Health
    {
        get { return _health; }
    }

    [SerializeField]
    float _squishSpeed = 20f; // frequency multiplier

    [SerializeField]
    float _squishHurtTime = 2f; // seconds

    //bool _isSquishing = false;
    Vector3 _originalScale;

    TimedSquishing _hurtSquish;
    TimedSquishing _bounceSquish; 

    // event delegates

    public enum DeathType
    {
        Fell,
        Killed
    }

    public delegate void PlayerDied(DeathType type);
    public PlayerDied OnPlayerDied;

    public delegate void DeathAnimationEnded();
    public DeathAnimationEnded OnDeathAnimationEnded;

    public delegate void PlayerHurt(GameObject attacker);
    public PlayerHurt OnPlayerHurt;


    IEnumerator SetTimeout(Action callback, float delay)
    {
        yield return new WaitForSeconds(delay);

        callback();

        yield break;
    }


    //void DoSquish()
    //{
    //    float minSquish, maxSquish;
    //    minSquish = 0.6f;
    //    maxSquish = 0.75f; // + minSquish implicitly

    //    var y = _originalScale.y * (maxSquish * 0.5f * (Mathf.Cos(_squishSpeed * Time.time) + 1f) + minSquish);

    //    int flip = facingLeft ? -1 : 1; 

    //    transform.localScale = new Vector3(flip * _originalScale.x, y, _originalScale.z);
    //}


    // Start is called before the first frame update
    void Start()
    {
        _hurtSquish = gameObject.AddComponent<TimedSquishing>();
        _hurtSquish.SquishSpeed = _squishSpeed;
        _hurtSquish.MinSquish = 0.8f;
        _hurtSquish.MaxSquish = 2.4f;

        _bounceSquish = gameObject.AddComponent<TimedSquishing>();
        _bounceSquish.SquishSpeed = _squishSpeed;
        _bounceSquish.MinSquish = 0.7f;
        _bounceSquish.MaxSquish = 1.3f;



        _leftScaleSign = _startingDirectionIsLeft ?
            Mathf.Sign(transform.localScale.x) :
            -1f * Mathf.Sign(transform.localScale.x);

        _health = _maxHealth;

        _originalScale = transform.localScale;

        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        count = 0f;
        SetCountText();

        rb.freezeRotation = true; 
        anim = GetComponent<Animator>();

        cameraBottomBounds = Camera.main.ViewportToWorldPoint(new Vector3 (1f, 1f, 0f)).y;

        OnPlayerDied += HandlePlayerDied;
        OnDeathAnimationEnded += HandleDeathAnimationEnded;
        OnPlayerHurt += HandlePlayerHurt; 
    }

    private void OnDisable()
    {
        OnPlayerDied -= HandlePlayerDied;
        OnDeathAnimationEnded -= HandleDeathAnimationEnded;
        OnPlayerHurt -= HandlePlayerHurt; 
    }

    void HandlePlayerHurt(GameObject attacker)
    {
        _hurtSquish.TriggerSquish(_squishHurtTime);
    }

    void HandleDeathAnimationEnded()
    {
        SceneManager.LoadScene("GameOver");
    }


    void Update(){
        anim.SetBool("grounded", grounded);
        //Debug.Log("Player Position: X = " + transform.position.x + " --- Y = " + transform.position.y);
        if (transform.position.y > count)
        {
            count = transform.position.y;
            SetCountText();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AreCollisionsEnabled)
        {
            grounded = detectGround();
            if (grounded && rb.velocity.y <= 0)
            {
                rb.AddForce(Vector2.up * CalculateJumpForce());
                _bounceSquish.TriggerSquish(1f);
            }
        }


        if (_areControlsEnabled)
        {
            if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(moveVector.x) ||
            Mathf.Abs(rb.velocity.x) < _maxHorizontalSpeed)
            {
                rb.velocity += moveVector * _horizontalAcceleration * Time.deltaTime;
            }

            if (moveVector.x == 0f)
            {
                rb.velocity -= rb.velocity.x * _horizontalDrag * Vector2.right;
            }
        }

        anim.SetFloat("y_velocity", rb.velocity.y);

        //if (moveVector.x < 0 && facingLeft || moveVector.x > 0 && !facingLeft)
        //{
        //    flip();
        //}


        // deterministic flipping
        if (rb.velocity.x != 0f)
        {
            float flag1 = rb.velocity.x < 0 ? 1f : -1f;
            float flag2 = _startingDirectionIsLeft ? 1f : -1f;

            float signX = _leftScaleSign * flag1 * flag2;

            Vector3 scale = transform.localScale;

            scale.x = Mathf.Abs(scale.x) * signX;

            transform.localScale = scale;
        }
            
        
        if (!_isDead && transform.position.y < bottomBounds.position.y)
        {
            Debug.Log("Dead because you fell");
            PlayerPrefs.SetFloat("SheepScore", count);

            OnPlayerDied?.Invoke(DeathType.Fell);
        }

        if (_isDead)
        {
            if (Time.time - _timeOfDeath > _gameOverTimeout)
            {

                if (transform.position.y < bottomBounds.position.y)
                {
                    // is player offscreen and has timeout elapsed? 
                    OnDeathAnimationEnded?.Invoke();
                }
            }
            float highScore = PlayerPrefs.GetFloat("SheepHighScore");
            highScore = (count > highScore) ? count : highScore;
            PlayerPrefs.SetFloat("SheepHighScore", highScore);
            SceneManager.LoadScene("GameOver");
        }
        
    }


    void HandlePlayerDied(DeathType type)
    {
        _isDead = true;
        _timeOfDeath = Time.time; 

        if (type == DeathType.Killed)
        {
            StartKilledAnimation();
        } else if (type == DeathType.Fell)
        {
            StartFellAnimation();
        }
    }

    void StartFellAnimation()
    {
        // TODO
    }

    void StartKilledAnimation()
    {
        _areControlsEnabled = false;
        AreCollisionsEnabled = false;

        // torque the character in direction of velocity
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddTorque(0.6f * CalculateJumpForce() * -1f * Mathf.Sign(Vector2.Dot(Vector2.right, rb.velocity)));

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

        // make clouds bounce
        foreach (var c in new Collider2D[] { left.collider, right.collider })
        {
            if (c != null)
            {
                if (!c.GetComponent<TimedSquishing>())
                {
                    c.gameObject.AddComponent<TimedSquishing>();
                }

                var s = c.GetComponent<TimedSquishing>();
                s.MaxSquish = 1.5f;
                s.MinSquish = 0.8f; 

                //if (!s.IsSquishing)
                //{
                    s.TriggerSquish(1.6f);
                //}
            }
        }

		return (left.collider != null || right.collider != null);
    }

  //  void Jump() 
  //  { 
  //      rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
  //  } 

  //  void BetterJump()
  //  {
		//if (rb.velocity.y < 0)
		//{
		//    rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
		//}
		//else if (rb.velocity.y > 0)
		//{
		//    rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
		//}
  //  }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            int currentCoins = PlayerPrefs.GetInt("coins");
            PlayerPrefs.SetInt("coins", currentCoins++);

            count += 10;
            SetCountText();
            
            Vector3 spawnCoinCollectedLoc = transform.position + new Vector3(0f, 5f, 0f);
            Destroy(other.gameObject);
            Instantiate(coinCollected, spawnCoinCollectedLoc, Quaternion.identity);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("monster");

            var normal = collision.GetContact(0).normal;
            float dot = Vector2.Dot(collision.gameObject.transform.up, normal);


            // did we jump on the monster?
            if (dot > 0.16f)
            {
                rb.AddForce(Vector2.up * 1.2f * CalculateJumpForce());
                Destroy(collision.gameObject);
                Debug.Log("Above");

                _bounceSquish.TriggerSquish(1f);
            } else
            {
                Debug.Log("Below");
                PlayerPrefs.SetFloat("SheepScore", count);

                rb.AddForce(normal.normalized * 1.7f * CalculateJumpForce());

                DoDamage(1, collision.gameObject); 
            }
        }
    }

    public void DoDamage(int amount, GameObject attacker)
    {
        _health -= amount;

        _health = Mathf.Max(0, _health);

        OnPlayerHurt?.Invoke(attacker);

        if (_health < 1)
        {
            OnPlayerDied?.Invoke(DeathType.Killed);
        }
    }

    public void OnMovementChange(InputAction.CallbackContext context){
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

    void SetCountText()
	{
		ScoreText.text = "Score: " + ((int) Mathf.Floor(count*100)).ToString();
	}

}
