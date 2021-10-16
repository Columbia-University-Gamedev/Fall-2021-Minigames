using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(10f, 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        float currentPositionY = transform.position.y;
        transform.position = new Vector3(transform.position.x, currentPositionY+0.5f, transform.position.z);
    }
}
