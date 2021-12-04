using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallScript : MonoBehaviour
{

    private UnityAction resetListener;
    private Rigidbody2D rb2d;
    private Transform tf;
    private bool justStarted;

    public int bounces;
    public bool hasBeenHit;
    public bool lastHitPlayer; //true if hit by player one, false if hit by player two

    void OnEnable(){
        EventManager.StartListening("reset",resetListener);
    }

    void OnDisable(){
        EventManager.StopListening("reset",resetListener);
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        resetListener = new UnityAction(resetBall);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")){
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        }

        justStarted = true;
        resetBall();
        justStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bounces > VolleyballConstants.maxBounces){
            //TODO: Add code here keeping track of score
            //resetBall();
            EventManager.TriggerEvent("reset");
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player") Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        else{
            Debug.Log("Touched something");
            EventManager.TriggerEvent("reset");
        }
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.name == "BallBounceTrigger"){
            //Bounce off of the player
            enableGravity();
            rb2d.velocity = (Vector2)(rb2d.velocity.magnitude * (transform.position - other.gameObject.GetComponent<Transform>().position).normalized * VolleyballConstants.ballBounceMultiplier) + other.gameObject.GetComponentInParent<Rigidbody2D>().velocity * VolleyballConstants.ballPlayerVelocityAdditionMultiplier;
            if(lastHitPlayer != other.gameObject.GetComponentInParent<PlayerScript>().isPlayerOne){
                bounces = 0;
                lastHitPlayer = !lastHitPlayer;
            } else {
                bounces += 1;
            }
        }
        else if(other.gameObject.name == "OutOfBoundsTrigger") EventManager.TriggerEvent("reset"); //Ball went out of bounds
    }

    void resetBall(){

        bool startingSide;

        bounces = 0;
        hasBeenHit = false;
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 0.0f;

        if(justStarted){
            if(Random.Range(-1.0f,1.0f) > 0) startingSide = true;
            else startingSide = false;
        }
        else startingSide = !lastHitPlayer;

        tf.position = new Vector3((VolleyballConstants.playerStartDispX)*0.75f, VolleyballConstants.ballStartY, 0);
        if(startingSide) tf.position = new Vector3(-tf.position.x,tf.position.y,tf.position.z);

    }

    public void enableGravity(){ // There's got to be a better way of doing this, but this is simple and probably not too gross
        rb2d.gravityScale = VolleyballConstants.ballGravityScale;
    }

}
