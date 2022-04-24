using UnityEngine;
using UnityEngine.InputSystem;

public class ECPlayerController : MonoBehaviour
{

    public DialogueController dialogueController;
    public float moveSpeed;
    private Vector2 move;
    [SerializeField]
    private Rigidbody2D rb2d;
    private Canvas canvas;
    private bool isNpcInRange = false;
    private GameObject inRangeNpc;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            isNpcInRange = true;
            inRangeNpc = other.gameObject;
            Debug.Log("Target Locked");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            isNpcInRange = false;
            inRangeNpc = null;
            dialogueController.StopDialogue();
            Debug.Log("Lock Lost");
        }
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
            Debug.Log("INPUT: Interact pressed");
            if (isNpcInRange)
            {
                    dialogueController.TriggerDialogue(inRangeNpc.GetComponent<ECNPCBehavior>().getDialogue());
            }
        }
    }
    private void Move()
    {
        rb2d.MovePosition(rb2d.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}