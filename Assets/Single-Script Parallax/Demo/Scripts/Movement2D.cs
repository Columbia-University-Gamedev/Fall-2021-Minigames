using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour {

    float velX = 0f;
    float velY = 0f;

    [Tooltip("Units per second")]
    public float rate = 1f;
	
	// Update is called once per frame
	void Update () {
        /// move around

        // slow down 
        velX *= 0.85f;
        velY *= 0.85f;

        if (Mathf.Abs(velX) < 0.125f) { velX = 0f; }
        if (Mathf.Abs(velY) < 0.125f) { velY = 0f; }

        // now take input
        velX += rate * Input.GetAxis("Horizontal");
        velY += rate * Input.GetAxis("Vertical"); 

        // constrain total speed to rate
        if (Mathf.Abs(velX) >= rate || Mathf.Abs(velY) >= rate)
        {
            var dist = new Vector2(velX, velY).magnitude;

            velX = velX / dist * rate; // cos(dir) * speed
            velY = velY / dist * rate; // sin(dir) * speed
        }

        // update actual position 
        transform.position += new Vector3(velX * Time.deltaTime, velY * Time.deltaTime);
    }
}
