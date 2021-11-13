using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class dummy_movement : MonoBehaviour
{

    public float jumpSpeed = 8f;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<boxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded()){
            Jump();
        }
        //float currentPositionY = transform.position.y;
        //transform.position = new Vector3(transform.position.x, currentPositionY+0.1f, transform.position.z);
    }
    void Jump(){
        player.velocity = Vector2.up * jumpSpeed;
        isJumping = true;
    }
    private bool IsGrounded(){
        RaycastHit2D raycastGit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size,0f,Vector2.down*0.1f);
        return raycastHit2d.collider !=null;
    }
}
