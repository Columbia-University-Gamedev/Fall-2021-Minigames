using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCompanion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CompanionFollow companionFollow;
    [SerializeField] private StoryManager storyManager;
    [SerializeField] private int index;
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && storyManager.sceneEvents[index].completed)
        {
            companionFollow.enabled = true;
        }
    }
}
