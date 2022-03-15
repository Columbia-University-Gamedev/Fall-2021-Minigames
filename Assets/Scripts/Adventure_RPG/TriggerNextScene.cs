using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public bool[] tasks;
    [SerializeField] private string nextScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (bool b in tasks)
            {
                if (!b)
                {
                    return;
                }
            }
            SceneManager.LoadScene(nextScene);
        }
    }
}
