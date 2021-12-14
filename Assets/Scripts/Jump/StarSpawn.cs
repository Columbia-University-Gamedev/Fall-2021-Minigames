using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawn : MonoBehaviour
{
    public GameObject starPrefab;

    void Start()
    {
        SpawnStar();
    }

    void SpawnStar()
    {
        float cameraTopLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).x;
        float cameraTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f)).x;
        float spawnX = Random.Range(cameraTopLeft, cameraTopRight);
        Instantiate(starPrefab, new Vector3(spawnX, transform.position.y), Quaternion.identity);
        Invoke("SpawnStar", 1f);
    }
}
