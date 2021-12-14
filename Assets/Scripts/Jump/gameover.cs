using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class gameover : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        float count = PlayerPrefs.GetFloat("SheepScore");
        ScoreText.text = ((int) Mathf.Floor(count*100)).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void OnStart()
    {
        SceneManager.LoadScene("jumpPlatformGen");
    }

    public void OnHome()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
