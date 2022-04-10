using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    private static StoryManager _instance;

    public static StoryManager Instance { get { return _instance; } }

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    [SerializeField] public List<StoryEvent> sceneEvents;
    [SerializeField] private GameObject canvas;
    private ReadCSV readCsv;
    // Start is called before the first frame update
    void Start()
    {
        readCsv = canvas.GetComponent<ReadCSV>();
        foreach(StoryEvent se in sceneEvents)
        {
            se.completed = false;
            if(!se.trigger)
            {
                Debug.LogError($"No trigger object on event {se.eventName}!");
            }
            if(!se.trigger.GetComponent<Collider2D>())
            {
                Debug.LogError($"No collider on trigger for {se.eventName}!");
            }

            EventTrigger et = se.trigger.gameObject.AddComponent<EventTrigger>();
            et.storyEvent = se;
        }
    }

    private bool checkPrereqs(StoryEvent se)
    {
        foreach (int i in se.prereqs)
        {
            if (!sceneEvents[i].completed)
                return false;
        }
        return true;
    }

    public void PerformEvent(StoryEvent se)
    {
        if (se.completed) return;
        if (!checkPrereqs(se)) return;
        if (se.sceneTransition)
        {
            SceneManager.LoadScene(se.nextScene);
        }
        if (se.hasDialogue)
        {
            readCsv.attemptStart(se);
            return; //readCsv will handle the rest
        }

        if(se.progressCharacter)
        {
            se.character.GetComponent<NPCMovement>().ProgressNPC();
        }
        se.completed = true;
    }
}

[Serializable]
public class StoryEvent
{
    public string eventName;
    public GameObject trigger;
    public bool progressCharacter;
    public GameObject character;
    public List<int> prereqs; //could be a bitmask
    [NonSerialized] public bool completed;
    public bool sceneTransition;
    public string nextScene;
    public bool hasDialogue;
    public string dialogueCSVpath;
}
