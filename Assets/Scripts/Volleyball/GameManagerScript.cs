using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    //Maybe I can later create a system where you can have more than two players. However, I don't know when we'll ever need to do this...
    private GameObject playerOne;
    private GameObject playerTwo;
    private GameObject ball;

    public GameObject playerPrefab;
    public GameObject ballPrefab;
    public int score;

    void Start()
    {
        playerOne = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerOne.GetComponent<PlayerScript>().isPlayerOne = true;

        playerTwo = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerTwo.GetComponent<PlayerScript>().isPlayerOne = false;

        ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
