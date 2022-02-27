using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneration : MonoBehaviour
{
    int[,] buildingMap;

    //Vectors
    [Header("Generation Modifiers")]
    [SerializeField] Vector2 buildingRange = new Vector2(2, 10); //(Min, max) number of buildings in succession

    public Vector2 xBounds; //Make private later
    public Vector2 yBounds; //Make private later

    public Vector2 currentPos;
    [Range(5, 30)] public float generationRadius; //Radius that generates outside of player position (is what is used to check with bounds)

    //GameObjects/Transforms
    [Header("Serializable Objects")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] buildings;

    void Update()
    {
        currentPos = player.position;


    }

    //Generate blocks in the player's FOV and in a radius outside of that
    private void generateNewBlock()
    {

    }
}
