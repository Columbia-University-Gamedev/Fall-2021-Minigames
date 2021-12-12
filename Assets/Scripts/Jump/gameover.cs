using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameover : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        int count = PlayerPrefs.GetInt("SheepScore");
        ScoreText.text = "Score: " + (count*10).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
