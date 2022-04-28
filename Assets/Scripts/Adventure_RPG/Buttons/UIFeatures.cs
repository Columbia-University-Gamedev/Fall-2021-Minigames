using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFeatures : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

    [SerializeField] private float pauseOpacity = .9f;
    private float timeScale = 1;
    
    public void OnStart()
    {
        SceneManager.LoadScene("Scene1");
    }

    private void Update()
    {
        timeScale = Time.timeScale;
    }

    public void OnPause()
    {

        Debug.Log("pausing game");
        //enable and fade in the pause panel, disable the pause button, and pause
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        StartCoroutine(ImageFade.FadeImage(false, 0.4f, pauseOpacity, pausePanel.GetComponent<CanvasGroup>()));
        pauseButton.SetActive(false);
        Debug.Log("game paused");

    }

    public void OnUnpause()
    {
        StartCoroutine(Unpause());
        StartCoroutine(ImageFade.FadeImage(true, 0.4f, pauseOpacity, pausePanel.GetComponent<CanvasGroup>()));
    }

    public IEnumerator Unpause()
    {
        Debug.Log("unpausing game");
        //fade the pause panel out, re-enable pause button and resume
        StartCoroutine(ImageFade.FadeImage(false, 0.4f, 1f, pauseButton.GetComponent<CanvasGroup>()));
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("game unpaused");
        yield return null;
    }
    public void OnQuit()
    {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(0);
        #endif
    }
    public void OnRestart()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnDestroyButton(GameObject button)
    {
        Destroy(button);
    }
}
