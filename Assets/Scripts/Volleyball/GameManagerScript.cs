using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    //Maybe I can later create a system where you can have more than two players. However, I don't know when we'll ever need to do this...
    private GameObject playerOne;
    private GameObject playerTwo;
    private GameObject ball;

    public GameObject playerPrefab;
    public GameObject ballPrefab;
    public int playerOneScore;
    public int playerTwoScore;

    private UnityAction resetListener;

    void OnEnable(){
        EventManager.StartListening("reset",resetListener);
    }
    void OnDisable(){
        EventManager.StopListening("reset",resetListener);
    }

    void Awake(){
        resetListener = new UnityAction(updateScore);
    }
    void Start()
    {
        playerOne = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerOne.GetComponent<PlayerScript>().isPlayerOne = true;

        playerTwo = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerTwo.GetComponent<PlayerScript>().isPlayerOne = false;
        playerTwo.AddComponent(typeof(SillyAI));

        ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);

        ball.GetComponent<BallScript>().enableGravity();

        playerOneScore = 0;
        playerTwoScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void updateScore() //Where the actual score updating happens
    {
        if(ball.GetComponent<BallScript>().lastHitPlayer) playerOneScore+=1;
        else playerTwoScore += 1;
    }
}
