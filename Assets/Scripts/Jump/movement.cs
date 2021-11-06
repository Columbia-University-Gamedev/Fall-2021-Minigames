using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Collider2D playerCollider;
    private Rigidbody2D rb;
    public LayerMask ground;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;    

    // Start is called before the first frame update
    void Start()
    {
	playerCollider = GetComponent<Collider2D>();
	rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
	bool grounded = detectGround();
	if (grounded)
	{
	    Debug.Log("grounded");
	    Jump();
	}
    }

    bool detectGround()
    {
	Vector3 playerPosLeft = playerCollider.bounds.center - playerCollider.bounds.extents;
	Vector3 playerPosRight = new Vector3(playerPosLeft.x + 2f * playerCollider.bounds.extents.x, playerPosLeft.y, playerPosLeft.z);
	RaycastHit2D left = Physics2D.Raycast(playerPosLeft, Vector2.down, 0.5f, ground);
	RaycastHit2D right = Physics2D.Raycast(playerPosRight, Vector2.down, 0.5f, ground);


	Color rayColor;
	if (left.collider != null || right.collider != null)
	{
	   rayColor = Color.green;
	}
	else
	{
	   rayColor = Color.red;
	}	

	Debug.DrawRay(playerPosLeft, Vector2.down * (playerCollider.bounds.extents.y + 0.5f), rayColor);
	Debug.DrawRay(playerPosRight, Vector2.down * (playerCollider.bounds.extents.y + 0.5f), rayColor);
	
	return (left.collider != null || right.collider != null);
    }

    void Jump()
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

}
