using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public Task[] tasks;
    [SerializeField] private string nextScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Task task in tasks)
            {
                if (!task.completed)
                {
                    return;
                }
            }
            SceneManager.LoadScene(nextScene);
        }
    }
}
