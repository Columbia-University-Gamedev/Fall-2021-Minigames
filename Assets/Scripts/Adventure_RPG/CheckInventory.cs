using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventory : MonoBehaviour  
{

    private Task task;
    private GameObject player;
    private int playerIsOn = 0;
    [SerializeField] private StoryManager StoryManager;
    //1 = letter, 2 = water, etc
    [SerializeField] private int eventType;
    
    //1 = Flower, 2= Fire, 3 =
    [SerializeField] private int eventNumber;

    [SerializeField] private GameObject flower;
    [SerializeField] private Sprite wateredFlower;
    [SerializeField] private GameObject fire;


    // Start is called before the first frame update
    void Start()
    {

        

    }

    public int getIsPlayerOn()
    {
        return playerIsOn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsOn = eventType;
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


    public void runEvent()
    {
        //1 = FLOWER, 2 = FIRE, 3 = LETTER 1ST TIME, 4 = LETTER 2ND TIME
        switch (eventNumber)
        {
            case 1:
                water_flower();
                break;

            case 2:
                StartCoroutine(water_fire());
                break;

            case 3:
                wrong_letter();
                break;

            case 4:
                right_letter();
                break;
        }

    }



    private void water_flower()
    {
        Debug.Log("YOU HELPED THE FLOWER!");
        StoryManager.PerformEvent(1);

        /*
        flower.GetComponent<SpriteRenderer>().sprite = wateredFlower;
        */

        //Object.Destroy(this.gameObject);

    }

    private IEnumerator water_fire()
    {
        yield return StartCoroutine(ImageFade.FadeSprite(true, 1f, 1f, fire.GetComponent<SpriteRenderer>()));
        fire.SetActive(false);
    }

    private void wrong_letter()
    {
        StoryManager.PerformEvent(4);
    }

    private void right_letter()
    {
        StoryManager.PerformEvent(1);
    }


}
