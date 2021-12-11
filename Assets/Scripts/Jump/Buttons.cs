using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private float pauseOpacity = .9f;
    private float timeScale = 1;
    void Start()
    {
        pausePanel.SetActive(false);
    }

    public static void OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    //debug
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

    public void OnHome()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}