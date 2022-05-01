using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI textComponent;
    private string[] lines;
    public float scrollSpeed;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerDialogue(string[] retrievedLines)
    {
        //if interact is pressed while dialogue is being displayed, skip or progress
        if (dialoguePanel.activeSelf)
        {
            Debug.Log("Canvas on");
            if (textComponent.text == lines[index])
            {
                //line finished, go to next
                NextLine();
            }
            else
            {
                //stop scrolling in text and skip
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        else
        {
            //otherwise load new dialogue and start it
            Debug.Log("Canvas off");
            lines = retrievedLines;
            index = 0;
            dialoguePanel.SetActive(true);
            StartCoroutine(TypeLine());
        }

    }

    public void StopDialogue()
    {
        StopAllCoroutines();
        textComponent.text = string.Empty;
        dialoguePanel.SetActive(false);

    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            //scroll out the text in each line
            textComponent.text += c;
            yield return new WaitForSeconds(scrollSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            Debug.Log("Empty text, scroll next line");
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            textComponent.text = string.Empty;
            dialoguePanel.SetActive(false);
        }
    }
}
