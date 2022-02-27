using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using CharacterStruct;

public class ReadCSV : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private GameObject dialogueBox;
    public Character[] characterArray;
    private StreamReader strReader;
    private bool EOF;
    private bool busy = false; // Busy if some dialogue has been started by not finished
    
    // Start is called before the first frame update
    void Start()
    {
        EOF = true; // No text at start
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Claim control of the dialogue box if possible
    // Returns `true` if control is claimed
    private bool tryClaim()
    {
        if (busy)
        {
            return false;
        }
        else
        {
            busy = true;
            return true;
        }
    }

    // Attempt to start dialogue using a specified CSV file
    // Returns `true` if successful
    public bool attemptStart(string csvPath)
    {
        if (tryClaim())
        {
            strReader = new StreamReader(csvPath);
            EOF = false;
            dialogueBox.SetActive(true);
            strReader.ReadLine();
            ReadNextLine();
            return true; // Successfully started dialogue
        }
        else
        {
            return false;
        }
    }

    public void ReadNextLine()
    {
        if (!EOF)
        {
            string dataString = strReader.ReadLine();
            if (dataString == null)
            {
                EOF = true;
                busy = false;
                dialogueBox.SetActive(false);
                return;
            }
            else if (dataString.Contains("characterName"))
            {
                return;
            }

            int characterIndex = int.Parse(dataValues[0]);
            string dialogueInput = dataValues[1];

            characterName.text = characterArray[characterIndex].GetName();
            dialogue.text = dialogueInput;
            characterImage.sprite = characterArray[characterIndex].GetImage();
        }
    }
}
