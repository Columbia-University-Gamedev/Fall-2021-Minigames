using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2d;
    private bool isGrounded;

    public float playerSpeed = 2.0f;
    public float jumpPower = 500.0f;
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            rb2d.velocity = new Vector2(-playerSpeed,rb2d.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            rb2d.velocity = new Vector2(playerSpeed,rb2d.velocity.y);
        }

        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)){
            rb2d.velocity = new Vector2(0,rb2d.velocity.y);
        }

    }

}
