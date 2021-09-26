using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    [Tooltip("The speed at which this object rotates")]
    public Vector3 rotation;

    private Transform tr;

    private Quaternion q;
    // Start is called before the first frame update
    void Start() {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        tr.Rotate(Time.deltaTime*rotation);
    }
}
