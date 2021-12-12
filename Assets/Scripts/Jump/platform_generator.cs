using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_generator : MonoBehaviour
{
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Transform player;

    [SerializeField] List<GameObject> _monsterList;
    [SerializeField] bool _shouldSpawnMonsters = true;

    [SerializeField] int _monsterProbability = 7; // higher => lower e.g. 7 => 1 in 7

    [SerializeField] Camera _camera; 

    private Vector3 lastEndPosition;
    private const float gapY = 1f;
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;
    // Start is called before the first frame update
    void Start()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        SpawnLevelPart();

    }

    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        spawnPosition.y += gapY;
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);

        // enable out-of-view despawn
        foreach (var r in levelPartTransform.GetComponentsInChildren<Renderer>())
        {
            var d = r.gameObject.AddComponent<DestroyOnInvisible>();
            d.Camera = _camera;
        }


        // spawn in monsters once in a while
        if (_shouldSpawnMonsters && _monsterList.Count > 0 &&
                                    Random.Range(0, _monsterProbability) == 0)
        {
            var prefab = _monsterList[Random.Range(0, _monsterList.Count - 1)];

            GameObject monster = Instantiate(prefab);

            var monsterBounds = monster.GetComponent<Collider2D>().bounds;

            var colliders = levelPartTransform.GetComponentsInChildren<Collider2D>();

            var platform1 = colliders[Random.Range(0, colliders.Length)];
            var platform2 = colliders[Random.Range(0, colliders.Length)];

            if (colliders.Length > 1) { 
                while (platform1 == platform2)
                {
                    platform2 = colliders[Random.Range(0, colliders.Length)];
                }
            }

            Vector3 monsterLower =  Vector3.up * monsterBounds.extents.y;
            Vector3 platform1Upper = Vector3.up * platform1.bounds.extents.y;
            Vector3 platform2Upper = Vector3.up * platform2.bounds.extents.y;

            Vector3 pos1 = platform1.gameObject.transform.position + platform1Upper + monsterLower;
            Vector3 pos2 = platform2.gameObject.transform.position + platform2Upper + monsterLower;

            monster.transform.position = pos1;

            var movement = monster.GetComponent<MonsterMove>();

            Vector3 offset1 = Vector3.right * (platform1.bounds.extents.x * 0.9f * Random.value - monsterBounds.extents.x);
            Vector3 offset2 = Vector3.right * (platform2.bounds.extents.x * 0.9f * Random.value - monsterBounds.extents.x);

            movement.MinPos = offset1;
            movement.MaxPos = offset2 + pos2 - pos1;
            movement.AverageMoveSpeed = 3.5f;

            movement.SetRandomDirection();

            // enable out-of-view despawn
            var d = monster.AddComponent<DestroyOnInvisible>();
            d.Camera = _camera; 

        }

        return levelPartTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }
        
    }
}
