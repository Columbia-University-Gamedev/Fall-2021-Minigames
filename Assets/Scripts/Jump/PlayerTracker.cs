using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField]
    GameObject _target;

    [SerializeField]
    float _verticalSpeed = 2f; // meters per second

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // track character horizontally, but scroll upwards
        var x = _target.transform.position.x;

        var y = Mathf.Max(transform.position.y + Time.deltaTime * _verticalSpeed,
                          _target.transform.position.y);

        var z = transform.position.z;

        transform.position = new Vector3(x, y, z);


    }
}
