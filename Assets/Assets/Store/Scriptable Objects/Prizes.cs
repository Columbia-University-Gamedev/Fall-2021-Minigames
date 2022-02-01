using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prize", menuName = "ScriptableObjects/PrizesScriptableObject", order = 1)]
public class Prizes : ScriptableObject
{
    public string prize_name;
    public float size;
    public Sprite prize_art;
    public int price;
    public bool is_bought;
}
