using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using CharacterStruct;

public class ReadCSV : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    
    [SerializeField] private Image characterImage;
    //[SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private GameObject dialogueBox;
    public Character[] characterArray;
    private StringReader strReader;
    private bool EOF;
    private bool busy = false; // Busy if some dialogue has been started by not finished
    private StoryEvent currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        EOF = true; // No text at start
        dialogueBox.SetActive(false);
        playerMovement = player.GetComponent<PlayerMovement>();
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
    public bool attemptStart(StoryEvent se)
    {
        if (tryClaim())
        {
            strReader = new StringReader(se.textAsset.text);
            currentEvent = se;
            EOF = false;
            dialogueBox.SetActive(true);
            strReader.ReadLine();
            ReadNextLine();
            playerMovement.canMove = false;
            return true; // Successfully started dialogue
        }
        else
        {
            return false;
        }
    }

    // Parse a string in a CSV-like format which can handle comma delimiters
    // and also double-quoted text
    // Note: does not currently handle actually using the double quote character in cells
    private string[] csvSplit(string line)
    {
        string quoteStrip(string text)
        { // Remove surrounding double quotes if present
            if (text[0] == '"' && text[text.Length-1] == '"')
            {
                return text.Substring(1, text.Length - 2);
            }
            return text;
        }

        // Adds cleaned substring of [startIdx, endIdx)
        void addCell(List<string> ls, string s, int startIdx, int endIdx)
        {
            ls.Add(quoteStrip(s.Substring(startIdx, endIdx - startIdx)));
        }

        List<string> res = new List<string>();
        int idxStart = 0;
        bool inQuote = false;
        for (int i = 0; i < line.Length; i++)
        {
            switch (line[i])
            {
                case ',':
                    if (inQuote)
                    {
                        break; // Ignore commas enclosed in a quoted string
                    }
                    // Add stuff up to but not including comma
                    addCell(res, line, idxStart, i);

                    // Start next cell after the comma
                    idxStart = i + 1;
                    break;
                case '"':
                    inQuote = !inQuote;
                    break;
            }
        }
        if (idxStart != line.Length) // Add the last cell if it hasn't been added yet
        {
            addCell(res, line, idxStart, line.Length);
        }
        return res.ToArray();
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
                playerMovement.canMove = true;

                if(currentEvent.progressCharacter)
                {
                    currentEvent.character.GetComponent<NPCMovement>().ProgressNPC();
                }
                currentEvent.completed = true;
                return;
            }
            else if (dataString.Contains("characterName"))
            {
                return;
            }
            var dataValues = csvSplit(dataString);

            int characterIndex = int.Parse(dataValues[0]);
            string dialogueInput = dataValues[1];

            //characterName.text = characterArray[characterIndex].GetName();
            dialogue.text = dialogueInput;
            characterImage.sprite = characterArray[characterIndex].GetImage();
        }
    }
}
