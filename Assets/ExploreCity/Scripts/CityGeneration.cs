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

    public Vector2Int xBounds; //Make private later
    public Vector2Int yBounds; //Make private later

    public Vector2Int currentPos;

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
        xBounds = new Vector2Int(Mathf.FloorToInt(player.position.x - generationRadius), Mathf.CeilToInt(player.position.x + generationRadius));
        yBounds = new Vector2Int(Mathf.FloorToInt(player.position.y - generationRadius), Mathf.CeilToInt(player.position.y + generationRadius));

        viewportHeight = Mathf.CeilToInt(mainCamera.orthographicSize * 2);
        viewportWidth = Mathf.CeilToInt(viewportHeight * mainCamera.aspect);

        minYBuildingMap = GetRandomBlockLine(viewportWidth);
        maxYBuildingMap = GetRandomBlockLine(viewportWidth);
        minXBuildingMap = GetRandomBlockLine(viewportHeight);
        maxXBuildingMap = GetRandomBlockLine(viewportHeight);
    }

    void Update()
    {
        currentPos = new Vector2Int(Mathf.CeilToInt(player.position.x), Mathf.CeilToInt(player.position.y));

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

        switch (dir)
        {
            case GenerationDirection.Up:
                blockLine = ContinueBlockLine(maxYBuildingMap);

                for (int i = 0; i < blockLine.Length; i++)
                {
                    switch (blockLine[i])
                    {
                        case GenerationType.Building:
                            Instantiate(tempBuildingPrefab, new Vector3(xBounds.x + i + 0.5f, yBounds.y + 0.5f, 0.0f), Quaternion.identity);
                            break;
                        case GenerationType.Road:
                            Instantiate(tempRoadPrefab, new Vector3(xBounds.x + i + 0.5f, yBounds.y + 0.5f, 0.0f), Quaternion.identity);
                            break;
                    }
                }
                break;

            case GenerationDirection.Down:
                blockLine = ContinueBlockLine(minYBuildingMap);

                for (int i = 0; i < blockLine.Length; i++)
                {
                    switch (blockLine[i])
                    {
                        case GenerationType.Building:
                            Instantiate(tempBuildingPrefab, new Vector3(xBounds.x + i + 0.5f, yBounds.x - 0.5f, 0.0f), Quaternion.identity);
                            break;
                        case GenerationType.Road:
                            Instantiate(tempRoadPrefab, new Vector3(xBounds.x + i + 0.5f, yBounds.x - 0.5f, 0.0f), Quaternion.identity);
                            break;
                    }
                }
                break;
            case GenerationDirection.Left:
                blockLine = ContinueBlockLine(minXBuildingMap);

                for (int i = 0; i < blockLine.Length; i++)
                {
                    switch (blockLine[i])
                    {
                        case GenerationType.Building:
                            Instantiate(tempBuildingPrefab, new Vector3(xBounds.x - 0.5f, yBounds.x + i + 0.5f, 0.0f), Quaternion.identity);
                            break;
                        case GenerationType.Road:
                            Instantiate(tempRoadPrefab, new Vector3(xBounds.x - 0.5f, yBounds.x + i + 0.5f, 0.0f), Quaternion.identity);
                            break;
                    }
                }
                break;
            case GenerationDirection.Right:
                blockLine = ContinueBlockLine(maxXBuildingMap);
                for (int i = 0; i < blockLine.Length; i++)
                {
                    switch (blockLine[i])
                    {
                        case GenerationType.Building:
                            Instantiate(tempBuildingPrefab, new Vector3(xBounds.y + 0.5f, yBounds.x + i + 0.5f, 0.0f), Quaternion.identity);
                            break;
                        case GenerationType.Road:
                            Instantiate(tempRoadPrefab, new Vector3(xBounds.y + 0.5f, yBounds.x + i + 0.5f, 0.0f), Quaternion.identity);
                            break;
                    }
                }
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

            xBounds.x = Mathf.FloorToInt(pos.x - generationRadius);
        }

        //Max
        if (xBounds.y < Mathf.Ceil(pos.x) + generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Right);

            xBounds.y = Mathf.CeilToInt(pos.x + generationRadius);
        }

        //Check for change in y-bounds

        //Min
        if (yBounds.x > Mathf.Floor(pos.y) - generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Down);

            yBounds.x = Mathf.FloorToInt(pos.y - generationRadius);
        }

        //Max
        if (yBounds.y < Mathf.Ceil(pos.y) + generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Up);

            yBounds.y = Mathf.CeilToInt(pos.y + generationRadius);
        }
    }

    private GenerationType[] GetRandomBlockLine(int length)
    {
        GenerationType[] blockLine = new GenerationType[length];

        int buildingsInARow = 0;

        for (int i = 0; i < length; i++)
        {
            if (Random.Range(1, 100) >= roadProbablity)
            {
                buildingsInARow++;

                blockLine[i] = GenerationType.Building;
            }
            else
            {
                if (buildingsInARow == 0 && i > 0)
                {
                    blockLine[i] = GenerationType.Building;
                }
                else
                {
                    buildingsInARow = 0;

                    blockLine[i] = GenerationType.Road;
                }
            }
        }

        return blockLine;
    }

    private GenerationType[] ContinueBlockLine(GenerationType[] previousBlockLine)
    {
        GenerationType[] blockLine = new GenerationType[previousBlockLine.Length];

        for (int i = 0; i < previousBlockLine.Length; i++)
        {
            switch (previousBlockLine[i])
            {
                case GenerationType.Building:
                    blockLine[i] = GenerationType.Building;
                    break;
                case GenerationType.Road:
                    blockLine[i] = GenerationType.Road;
                    break;
            }
        }

        return blockLine;
    }
}
