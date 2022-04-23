using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventory : MonoBehaviour  
{

    private Task task;
    private GameObject player;
    private int playerIsOn = 0;
    [SerializeField] private int eventNumber;
    //1 = letter, 2 = water, etc



    // Start is called before the first frame update
    void Start()
    {

        task = GetComponent<Task>();

        


}

    public int getIsPlayerOn()
    {
        return playerIsOn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsOn = eventNumber;
            Debug.Log("IN");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOn = 0;
            Debug.Log("OUT");
        }
    }


}
