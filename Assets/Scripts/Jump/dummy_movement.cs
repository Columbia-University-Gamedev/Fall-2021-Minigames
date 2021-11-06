using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy_movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float currentPositionY = transform.position.y;
        transform.position = new Vector3(transform.position.x, currentPositionY+0.1f, transform.position.z);
    }
}
