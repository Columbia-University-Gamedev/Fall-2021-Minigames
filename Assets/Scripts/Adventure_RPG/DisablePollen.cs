using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePollen : MonoBehaviour
{
    [SerializeField] private ParticleSystem p;
    
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        p.Stop();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        p.Play();
    }
}
