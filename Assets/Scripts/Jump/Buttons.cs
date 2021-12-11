using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Image pausePanel;
    private float pausePanelOpacity;

    [SerializeField] private GameObject UnpauseButton;
    void Start()
    {
        pausePanelOpacity = pausePanel.color.a;
    }
    
    public void OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        StartCoroutine(ImageFade.FadeImage(true, 0.5f, pausePanelOpacity, pausePanel));
        StartCoroutine(ImageFade.FadeImage(false, 0.5f, 1f, pauseButton.GetComponent<Image>()));
        pauseButton.GetComponent<Button>().enabled = true;
    }

    public void OnUnpause()
    {
        StartCoroutine(Unpause());
    }

    public IEnumerator Unpause()
    {
        yield return new StartCoroutine(ImageFade.FadeImage(true, 0.5f, pausePanelOpacity, pausePanel));
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

    public void OnHome()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}