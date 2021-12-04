using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
    }

    public void OnUnpause()
    {
        Time.timeScale = 1f;
    }

    public void OnQuit()
    {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;            
        #else
            Application.Quit(0);
        #endif
    }
}