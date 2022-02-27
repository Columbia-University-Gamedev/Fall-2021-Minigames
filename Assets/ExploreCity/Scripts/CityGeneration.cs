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

    enum GenerationDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    private void Awake()
    {
        xBounds = new Vector2(player.position.x - generationRadius, player.position.x + generationRadius);
        yBounds = new Vector2(player.position.y - generationRadius, player.position.y + generationRadius);
    }

    void Update()
    {
        currentPos = new Vector2(Mathf.Ceil(player.position.x), Mathf.Ceil(player.position.y));

        //Check for change in x-bounds

        //Min
        if (xBounds.x > Mathf.Floor(player.position.x) - generationRadius)
        {
            generateNewBlock(GenerationDirection.Left);

            xBounds.x = Mathf.Floor(player.position.x) - generationRadius;
        }

        //Max
        if (xBounds.y < Mathf.Ceil(player.position.x) + generationRadius)
        {
            generateNewBlock(GenerationDirection.Right);

            xBounds.y = Mathf.Ceil(player.position.x) + generationRadius;
        }

        //Check for change in y-bounds

        //Min
        if (yBounds.x > Mathf.Floor(player.position.y) - generationRadius)
        {
            generateNewBlock(GenerationDirection.Down);

            yBounds.x = Mathf.Floor(player.position.y) - generationRadius;
        }

        //Max
        if (yBounds.y < Mathf.Ceil(player.position.y) + generationRadius)
        {
            generateNewBlock(GenerationDirection.Up);

            yBounds.y = Mathf.Ceil(player.position.y) + generationRadius;
        }

    }

    //Generate blocks in the player's FOV and in a radius outside of that
    private void generateNewBlock(GenerationDirection dir)
    {
        switch (dir)
        {
            case GenerationDirection.Up:
                Debug.Log("Generate New Block Above");
                break;
            case GenerationDirection.Down:
                Debug.Log("Generate New Block Below");
                break;
            case GenerationDirection.Left:
                Debug.Log("Generate New Block Leftward");
                break;
            case GenerationDirection.Right:
                Debug.Log("Generate New Block Rightward");
                break;
            default:
                Debug.LogError("Invalid Direction Given: " + dir);
                break;
        }
    }
}
