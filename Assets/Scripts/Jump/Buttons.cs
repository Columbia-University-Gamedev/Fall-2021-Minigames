using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public OnPause()
    {
        Time.timeScale = 0f;
    }

    public OnUnpause()
    {
        Time.timeScale = 1f;
    }

    public OnQuit()
    {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;            
        #else
            Application.Quit(0);
        #endif
    }
}