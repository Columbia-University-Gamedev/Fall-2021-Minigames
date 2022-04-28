using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffPushableObject : MonoBehaviour
{
    private Task task;
    [SerializeField] private StoryManager StoryManager;
    [SerializeField] private int eventNumber;
    void Start()
    {
        task = GetComponent<Task>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PushableObject"))
        {
            //Makes it background item
            Rigidbody2D otherRb = other.gameObject.GetComponent<Rigidbody2D>();
            otherRb.constraints = RigidbodyConstraints2D.FreezeAll;
            task.completed = true;
            StoryManager.PerformEvent(eventNumber);
        }
    }

}
