using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 _moveDirection;

    private float moveX;
    private float moveY;

    [SerializeField] private Rigidbody2D rb2d;

    private void Update() //Every frame
    {
        ProcessInputs();
    }

    private void FixedUpdate() //Every tick
    {
        Move();
    }

    private void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb2d.MovePosition(rb2d.position + _moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}