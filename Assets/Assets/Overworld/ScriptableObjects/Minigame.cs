using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "ScriptableObjects/MinigameScriptableObject", order = 1)]
public class Minigame : ScriptableObject
{
    public string game_name;
    public Sprite cover_art;
    public Sprite arcade_art;
    public string scene_name;
}
