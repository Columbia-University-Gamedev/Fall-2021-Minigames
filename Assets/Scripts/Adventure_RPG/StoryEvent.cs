using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Story Manager/New Story Event")]
public class StoryEvent : ScriptableObject
{
    public string eventName;
    public GameObject trigger;
    public bool progressCharacter;
    public GameObject character;
    public List<StoryEvent> prereqs; //could be a bitmask
    [NonSerialized] public bool completed;
    public bool sceneTransition;
    public string nextScene;
    public bool hasDialogue;
    public TextAsset textAsset;
}