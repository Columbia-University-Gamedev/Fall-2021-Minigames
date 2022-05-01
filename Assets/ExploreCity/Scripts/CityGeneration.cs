using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CityGeneration : MonoBehaviour
{
    
    private int viewportWidth; //In world space
    private int viewportHeight; //In world space

    /*GenerationType[] minYBuildingMap; //Lowest generated line of blocks
    GenerationType[] maxYBuildingMap; //Highest generated line of blocks

    GenerationType[] minXBuildingMap; //Leftmost generated line of blocks
    GenerationType[] maxXBuildingMap; //Rightmost generated line of blocks*/
    public GameObject npc;

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

    /*[SerializeField] private Camera mainCamera;*/
    [SerializeField] private List<GameObject> buildings;

    //public GameObject tempBuildingPrefab;
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
        xBounds = new Vector2Int(0,0);
        yBounds = new Vector2Int(0,0);

        /*viewportHeight = Mathf.CeilToInt(mainCamera.orthographicSize * 2);
        viewportWidth = Mathf.CeilToInt(viewportHeight * mainCamera.aspect);

        minYBuildingMap = GetRandomBlockLine(viewportWidth);
        maxYBuildingMap = GetRandomBlockLine(viewportWidth);
        minXBuildingMap = GetRandomBlockLine(viewportHeight);
        maxXBuildingMap = GetRandomBlockLine(viewportHeight);*/
    }

    private GameObject getRandomBuilding(){

        int randomnumber = Random.Range(0, buildings.Count);
        Debug.Log(buildings.Count);
        Debug.Log(randomnumber);
        return buildings[randomnumber];
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
        float far = generationRadius;
        GenerationType[] blockLine = new GenerationType[viewportWidth > viewportHeight ? viewportWidth : viewportHeight];
        Debug.Log("Xbounds: " +  xBounds);
        Debug.Log("Ybounds: " + yBounds);
        GameObject createdBuilding;
        RectTransform rt;

 
        float width=1;
        float height=1;
        float maxWidth=0;
        float maxHeight=0;


        if(dir == GenerationDirection.Right){
            for (int i=Mathf.CeilToInt(yBounds.x); i<Mathf.CeilToInt(yBounds.y); i+=Mathf.CeilToInt(generationRadius+ height)){
                createdBuilding = getRandomBuilding();
                rt = createdBuilding.GetComponent<RectTransform>();
                width = rt.rect.width;
                height = rt.rect.height;
                if(maxWidth<width){
                    maxWidth = width;
                }
                Instantiate(createdBuilding, new Vector3(xBounds.y + far + width, i, 0.0f), Quaternion.identity);
                //generate a new npc
                GameObject npcref = Instantiate(npc, new Vector3(xBounds.y + far + width, i, 0.0f), Quaternion.identity);
                npcref.GetComponent<ECNPCBehavior>().init();
            }
            xBounds.y = Mathf.CeilToInt(xBounds.y + generationRadius + maxWidth);
        }
        else if (dir == GenerationDirection.Left){
            for (int i=Mathf.CeilToInt(yBounds.x); i<Mathf.CeilToInt(yBounds.y); i+=Mathf.CeilToInt(generationRadius+ height)){
                createdBuilding = getRandomBuilding();
                rt = createdBuilding.GetComponent<RectTransform>();
                width = rt.rect.width;
                height = rt.rect.height;
                if(maxWidth<width){
                    maxWidth = width;
                }
                Instantiate(createdBuilding, new Vector3(xBounds.x - far - 1f*width, i, 0.0f), Quaternion.identity);
                //generate a new npc
                GameObject npcref = Instantiate(npc, new Vector3(xBounds.x - far - 1f * width, i, 0.0f), Quaternion.identity);
                npcref.GetComponent<ECNPCBehavior>().init();
            }
            xBounds.x = Mathf.FloorToInt(xBounds.x - generationRadius - maxWidth);
        }
        else if (dir == GenerationDirection.Up){
            for (int i=Mathf.CeilToInt(xBounds.x); i<Mathf.CeilToInt(xBounds.y); i+=Mathf.CeilToInt(generationRadius+ width)){
                createdBuilding = getRandomBuilding();
                rt = createdBuilding.GetComponent<RectTransform>();
                width = rt.rect.width;
                height = rt.rect.height;
                if(maxHeight<height){
                    maxHeight = height;
                }
                Instantiate(createdBuilding, new Vector3(i, yBounds.y + far + 0.5f*height, 0.0f), Quaternion.identity);
                //generate a new npc
                GameObject npcref = Instantiate(npc, new Vector3(i, yBounds.y + far + 0.5f * height, 0.0f), Quaternion.identity);
                npcref.GetComponent<ECNPCBehavior>().init();
            }
            yBounds.y = Mathf.CeilToInt(yBounds.y + generationRadius + maxHeight);
        }
        else if (dir == GenerationDirection.Down){
             for (int i=Mathf.CeilToInt(xBounds.x); i<Mathf.CeilToInt(xBounds.y); i+=Mathf.CeilToInt(generationRadius+ width)){
                createdBuilding = getRandomBuilding();
                rt = createdBuilding.GetComponent<RectTransform>();
                width = rt.rect.width;
                height = rt.rect.height;
                if(maxHeight<height){
                    maxHeight = height;
                }
                Instantiate(createdBuilding, new Vector3(i, yBounds.x- far - 0.5f*height, 0.0f), Quaternion.identity);
                //generate a new npc
                GameObject npcref = Instantiate(npc,  new Vector3(i, yBounds.y + far + 0.5f*height, 0.0f), Quaternion.identity);
                npcref.GetComponent<ECNPCBehavior>().init();
            }
            yBounds.x = Mathf.FloorToInt(yBounds.x - generationRadius - maxHeight);
        }
    }

    private void CheckIfOutOfRange(Vector2 pos)
    {
        //Check for change in x-bounds

        //Min
        if (xBounds.x > Mathf.Floor(pos.x) - generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Left);
        }

        //Max
        if (xBounds.y < Mathf.Ceil(pos.x) + generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Right);
        }

        //Check for change in y-bounds

        //Min
        if (yBounds.x > Mathf.Floor(pos.y) - generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Down);

        }

        //Max
        if (yBounds.y < Mathf.Ceil(pos.y) + generationRadius)
        {
            GenerateNewBlockLine(GenerationDirection.Up);

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
