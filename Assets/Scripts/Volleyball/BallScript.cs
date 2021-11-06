using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnColliderEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player") Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>());
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.name == "BallBounceTrigger"){
            //Bounce off of the player
            rb2d.velocity = (Vector2)(rb2d.velocity.magnitude * (transform.position - other.gameObject.GetComponent<Transform>().position).normalized * VolleyballConstants.ballBounceMultiplier) + other.gameObject.GetComponentInParent<Rigidbody2D>().velocity * VolleyballConstants.ballPlayerVelocityAdditionMultiplier;
        }
        if(other.gameObject.name == "BallHitTrigger"){
            other.gameObject.GetComponentInParent<PlayerScript>().canHitBall = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.name == "BallHitTrigger"){
            other.gameObject.GetComponentInParent<PlayerScript>().canHitBall = false;
        }
    }
}
