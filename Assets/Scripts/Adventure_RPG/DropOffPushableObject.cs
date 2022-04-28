using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffPushableObject : MonoBehaviour
{
    [SerializeField] private StoryManager StoryManager;
    [SerializeField] private int eventNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PushableObject"))
        {
            //Makes it background item
            Rigidbody2D otherRb = other.gameObject.GetComponent<Rigidbody2D>();
            otherRb.constraints = RigidbodyConstraints2D.FreezeAll;
            StoryManager.PerformEvent(eventNumber);
            Debug.Log("performed event");
        }
    }

}
