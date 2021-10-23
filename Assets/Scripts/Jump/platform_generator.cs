using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_generator : MonoBehaviour
{
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private Transform levelPart_1;
    [SerializeField] private Transform player;

    private Vector3 lastEndPosition;

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;
    // Start is called before the first frame update
    void Start()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        SpawnLevelPart();

    }

    private void SpawnLevelPart()
    {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(Vector3 spawnPosition) {
        Transform levelPartTransform = Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, lastEndPosition)< PLAYER_DISTANCE_SPAWN_LEVEL_PART )
        
    }
}
