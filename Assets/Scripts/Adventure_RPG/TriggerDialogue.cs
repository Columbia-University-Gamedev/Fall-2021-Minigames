using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextAsset textAsset;
    private ReadCSV readCsv;
    private bool read;
    private Task task;
    // Start is called before the first frame update
    void Start()
    {
        readCsv = canvas.GetComponent<ReadCSV>();
        task = GetComponent<Task>();
        read = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!read)
        {
            readCsv.attemptStart(textAsset);
        }
        read = true;
        task.completed = true;
    }
}
