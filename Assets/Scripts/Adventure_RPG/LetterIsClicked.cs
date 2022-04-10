using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterIsClicked : MonoBehaviour
{

    //Keep track of the player (need to check if they are in the right spot before activating)
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject detect;

    //Script keeping track if player is in right spot
    [SerializeField] private CheckInventory spotCheck;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick() 
    {
        if (spotCheck.getIsPlayerOn())
        {
            Debug.Log("YAY!");
        }


    }
}
