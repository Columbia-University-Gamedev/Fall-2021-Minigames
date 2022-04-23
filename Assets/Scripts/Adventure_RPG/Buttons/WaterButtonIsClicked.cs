using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterButtonIsClicked : MonoBehaviour
{

    //Keep track of the player (need to check if they are in the right spot before activating)
    [SerializeField] private GameObject player;
    //[SerializeField] private GameObject detect;

    //Script keeping track if player is in right spot
    [SerializeField] private CheckInventory spotCheck;
    //[SerializeField] private int EventType;
    [SerializeField] private GameObject trigger;
    //private Task task;

    [SerializeField] private StoryManager StoryManager;

    //Checks if button has already been clicked
    bool click;


    // Start is called before the first frame update
    void Start()
    {
        click = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void onClick()
    {
        if (spotCheck.getIsPlayerOn() == 2 && !click)
        {
            Debug.Log("YOU CLICKED THE WATER!");

            click = true;
            StoryManager.PerformEvent(0);
            
        }
    }
}
