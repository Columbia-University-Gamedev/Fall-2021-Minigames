using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private string csvPath;
    private ReadCSV readCsv;
    // Start is called before the first frame update
    void Start()
    {
        readCsv = canvas.GetComponent<ReadCSV>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Test: " + collider.name);
        readCsv.attemptStart(csvPath);
    }
}
