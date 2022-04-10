using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    [SerializeField] private GameObject player;
    private Inventory inventory;
    public GameObject itemButton;
    SpriteRenderer letterSprite;
    Collider2D letterCollision;
    private Task task;
    //public GameObject effect;

    private void Start()
    {
        inventory = player.GetComponent<Inventory>();
        letterSprite = GetComponent<SpriteRenderer>();
        letterCollision = GetComponent<Collider2D>();
        task = GetComponent<Task>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            // spawn the sun button at the first available inventory slot ! 

            letterSprite.enabled = false;
            letterCollision.enabled = false;
            task.completed = true;
            for (int i = 0; i < inventory.items.Length; i++)
            {
                if (inventory.items[i] == 0) { // check whether the slot is EMPTY
                    //Instantiate(effect, transform.position, Quaternion.identity);
                    inventory.items[i] = 1; // makes sure that the slot is now considered FULL
                    Instantiate(itemButton, inventory.slots[i].transform, false); // spawn the button so that the player can interact with it
                    break;

                }
            }
        }
        
    }
}