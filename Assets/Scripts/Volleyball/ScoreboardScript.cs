using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreboardScript : MonoBehaviour
{
    // Start is called before the first frame update

    private UnityAction resetListener;

    void Awake()
    {
        resetListener = new UnityAction(updateScore);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateScore(){
        
    }
}
