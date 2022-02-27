using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneration : MonoBehaviour
{
    private int viewportWidth; //In world space
    private int viewportHeight; //In world space

    GenerationType[] minYBuildingMap; //Lowest generated line of blocks
    GenerationType[] maxYBuildingMap; //Highest generated line of blocks

    GenerationType[] minXBuildingMap; //Leftmost generated line of blocks
    GenerationType[] maxXBuildingMap; //Rightmost generated line of blocks


    [Header("Generation Modifiers")]
    //Vectors
    [SerializeField] Vector2 buildingRange = new Vector2(2, 10); //(Min, max) number of buildings in succession

    public Vector2 xBounds; //Make private later
    public Vector2 yBounds; //Make private later

    public Vector2 currentPos;

    //Floats
    [Range(5, 30)] public float generationRadius; //Radius that generates outside of player position (is what is used to check with bounds)

    [Range(1, 100)] public float roadProbablity;

    //GameObjects/Transforms
    [Header("Serializable Objects")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] buildings;

    [SerializeField] private Camera mainCamera;

    public GameObject tempBuildingPrefab;
    public GameObject tempRoadPrefab;

    enum GenerationDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    enum GenerationType
    {
        Road,
        Building
    }

    private void Awake()
    {
        xBounds = new Vector2(player.position.x - generationRadius, player.position.x + generationRadius);
        yBounds = new Vector2(player.position.y - generationRadius, player.position.y + generationRadius);

        viewportHeight = Mathf.CeilToInt(mainCamera.orthographicSize * 2);
        viewportWidth = Mathf.CeilToInt(viewportHeight * mainCamera.aspect);

        minYBuildingMap = new GenerationType[viewportWidth];
        maxYBuildingMap = new GenerationType[viewportWidth];
        minXBuildingMap = new GenerationType[viewportHeight];
        maxXBuildingMap = new GenerationType[viewportHeight];
    }

    void Update()
    {
        currentPos = new Vector2(Mathf.Ceil(player.position.x), Mathf.Ceil(player.position.y));

        CheckIfOutOfRange(currentPos);
    }
    //Generate blocks in the player's FOV and in a radius outside of that
    private void GenerateNewBlockLine(GenerationDirection dir)
    {
        //General generation behavior:
        GenerationType[] blockLine = new GenerationType[viewportWidth > viewportHeight ? viewportWidth : viewportHeight];
        switch (dir)
        {
            case GenerationDirection.Up:
                blockLine = new GenerationType[viewportWidth];
                break;
            case GenerationDirection.Down:
                blockLine = new GenerationType[viewportWidth];

                Debug.Log("Generate New Block Below");
                break;
            case GenerationDirection.Left:
                blockLine = new GenerationType[viewportHeight];

                Debug.Log("Generate New Block Leftward");
                break;
            case GenerationDirection.Right:
                blockLine = new GenerationType[viewportHeight];

                Debug.Log("Generate New Block Rightward");
                break;
            default:
                Debug.LogError("Invalid Direction Given: " + dir);
                return;
        }

        for (int i = 0; i < maxYBuildingMap.Length; i++)
        {
            switch (maxYBuildingMap[i])
            {
                case GenerationType.Building:
                    if (Random.Range(1, 100) >= roadProbablity)
                        blockLine[i] = GenerationType.Building;
                    else
                        blockLine[i] = GenerationType.Road;
                    break;
                case GenerationType.Road:
                    blockLine[i] = GenerationType.Road;
                    break;
            }
        }

        switch (dir)
        {
            case GenerationDirection.Up:
                blockLine = new GenerationType[viewportWidth];
                break;
            case GenerationDirection.Down:
                blockLine = new GenerationType[viewportWidth];

                Debug.Log("Generate New Block Below");
                break;
            case GenerationDirection.Left:
                blockLine = new GenerationType[viewportHeight];

                Debug.Log("Generate New Block Leftward");
                break;
            case GenerationDirection.Right:
                blockLine = new GenerationType[viewportHeight];

                Debug.Log("Generate New Block Rightward");
                break;
            default:
                Debug.LogError("Invalid Direction Given: " + dir);
                return;
        }
    }

    private void CheckIfOutOfRange(Vector2 pos)
    {
        //Check for change in x-bounds

        //Min
        if (xBounds.x > Mathf.Floor(pos.x) - generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Left);

            xBounds.x = Mathf.Floor(pos.x) - generationRadius;
        }

        //Max
        if (xBounds.y < Mathf.Ceil(pos.x) + generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Right);

            xBounds.y = Mathf.Ceil(pos.x) + generationRadius;
        }

        //Check for change in y-bounds

        //Min
        if (yBounds.x > Mathf.Floor(pos.y) - generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Down);

            yBounds.x = Mathf.Floor(pos.y) - generationRadius;
        }

        //Max
        if (yBounds.y < Mathf.Ceil(pos.y) + generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Up);

            yBounds.y = Mathf.Ceil(pos.y) + generationRadius;
        }
    }

}
