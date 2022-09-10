using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("Death"))
            {
                PlayerMovement.TakeDamage();
                PlayerMovement.TakeDamage();
            }
            PlayerMovement.TakeDamage();
            Debug.Log("take fall damage");
        }
    }
}
