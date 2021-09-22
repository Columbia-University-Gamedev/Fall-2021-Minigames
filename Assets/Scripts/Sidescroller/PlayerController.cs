using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Time.deltaTime * moveSpeed *
                            new Vector3(0.0f, movement.y, movement.x));
    }

    private Vector2 movement;
    void OnMovement(InputValue res) {
        movement = res.Get<Vector2>();
    }
}
