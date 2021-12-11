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

            var platform = colliders[Random.Range(0, colliders.Length)];

            Vector3 monsterLower =  Vector3.up * monsterBounds.extents.y;
            Vector3 platformUpper = Vector3.up * platform.bounds.extents.y;

            Vector3 pos = platform.gameObject.transform.position + platformUpper + monsterLower;

            monster.transform.position = pos;

            var movement = monster.GetComponent<MonsterMove>();

            Vector3 offset = Vector3.right * (platform.bounds.extents.x * 0.9f - monsterBounds.extents.x);

            movement.MinPos = -1 * offset;
            movement.MaxPos = offset;
            movement.AverageMoveSpeed = 2f;

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
