using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventory : MonoBehaviour  
{

    private Task task;
    private GameObject player;
    private bool playerIsOn = false;



    // Start is called before the first frame update
    void Start()
    {

        task = GetComponent<Task>();

        


}

    public bool getIsPlayerOn()
    {
        return playerIsOn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOn = false;
        }
    }
}
