using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_spaceship : MonoBehaviour
{

    private float moveSpeed;

    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(2.5f, 10.0f);
        tr = transform;
        Destroy(gameObject, 25.0f);
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Time.deltaTime * moveSpeed * Vector2.right);
    }
}
