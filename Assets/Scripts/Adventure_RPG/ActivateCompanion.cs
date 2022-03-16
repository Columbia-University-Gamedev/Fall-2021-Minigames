using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCompanion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CompanionFollow companionFollow;
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            companionFollow.enabled = true;
        }
    }
}
