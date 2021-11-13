using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy_movement : MonoBehaviour
{
    Collider2D playerCollider;
    private Rigidbody2D rb;
    public LayerMask ground;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;    

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD:Assets/Scripts/Jump/movement.cs
	playerCollider = GetComponent<Collider2D>();
	rb = GetComponent<Rigidbody2D>();
=======
>>>>>>> fdb5752289261716f28da1454889dce70ade2128:Assets/Scripts/Jump/dummy_movement.cs
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD:Assets/Scripts/Jump/movement.cs
	bool grounded = detectGround();
	if (grounded)
	{
	    Debug.Log("grounded");
	    Jump();
	}
=======
        float currentPositionY = transform.position.y;
        transform.position = new Vector3(transform.position.x, currentPositionY+0.1f, transform.position.z);
>>>>>>> fdb5752289261716f28da1454889dce70ade2128:Assets/Scripts/Jump/dummy_movement.cs
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
