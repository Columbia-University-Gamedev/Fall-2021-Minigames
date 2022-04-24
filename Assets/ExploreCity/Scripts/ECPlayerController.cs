using UnityEngine;
using UnityEngine.InputSystem;

public class ECPlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 move;
    [SerializeField] 
    private Rigidbody2D rb2d;
    private Canvas canvas;

    private void Awake()
    {

    }

    private void Update() //Every frame
    {

    }

    private void FixedUpdate() //Every tick
    {
        Move();
    }

    public void OnMovement(InputValue res)
    {
        move = res.Get<Vector2>();
        Debug.Log("INPUT: Input Vector: " + move);
    }

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            //check if any NPC is in range, if so get their dialogue and 
            Debug.Log("INPUT: Interact pressed");
        }
    }
    private void Move()
    {
        rb2d.MovePosition(rb2d.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}