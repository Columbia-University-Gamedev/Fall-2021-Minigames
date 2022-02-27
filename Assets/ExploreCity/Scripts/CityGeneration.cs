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

    private void Awake()
    {
        xBounds = new Vector2(player.position.x - generationRadius, player.position.x + generationRadius);
        yBounds = new Vector2(player.position.y - generationRadius, player.position.y + generationRadius);
    }

    void Update()
    {
        currentPos = player.position;

        //Check for change in x-bounds

        //Min
        if (xBounds.x > player.position.x - generationRadius)
        {
            generateNewBlock();
        }

        //Max
        if (xBounds.y < player.position.x + generationRadius)
        {
            generateNewBlock();
        }

        //Check for change in y-bounds

        //Min
        if (yBounds.x > player.position.y - generationRadius)
        {
            generateNewBlock();
        }

        //Max
        if (yBounds.y < player.position.y + generationRadius)
        {
            generateNewBlock();
        }

    }

    //Generate blocks in the player's FOV and in a radius outside of that
    private void generateNewBlock()
    {

    }
}
