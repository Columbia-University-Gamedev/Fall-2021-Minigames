using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ReadCSV : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private string path;

    private StreamReader strReader;
    private bool EOF;
    
    // Start is called before the first frame update
    void Start()
    {
        strReader = new StreamReader(path);
        EOF = false;
        
        strReader.ReadLine();
        ReadNextLine();
        Debug.Log("ahdskjflahsdkjlfsdah");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadNextLine()
    {
        if (!EOF)
        {
            string dataString = strReader.ReadLine();
            if (dataString == null)
            {
                EOF = true;
                return;
            }
            else if (dataString.Contains("characterName"))
            {
                return;
            }
            var dataValues = dataString.Split(',');

            string nameInput = dataValues[0];
            string dialogueInput = dataValues[1];

            characterName.text = nameInput;
            dialogue.text = dialogueInput;
        }
    }
}
