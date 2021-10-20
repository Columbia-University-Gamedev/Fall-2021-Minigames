using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject groundTile;
    public GameObject wallTile;

    public float z = 0;

    private Dictionary<Vector2, GameObject> grid;

    public void AddGroundTile(Vector2 position) {
        grid.Add(position, groundTile);
    }

    public void AddWallTile(Vector2 position) {
        grid.Add(position, wallTile);
    }

    public void ClearBoard() {
        grid.Clear();
    }

    public bool isInBoard(Vector2 position) {
        return grid.ContainsKey(position);
    }

    public void RenderBoard() {
        foreach (var entry in grid) {
            Vector2 pos = entry.Key;
            Vector3 location = new Vector3(pos.x, pos.y, z);
            GameObject tile = entry.Value;
            var inserted = Instantiate(tile, location, Quaternion.identity);
            inserted.transform.parent = gameObject.transform;
        }
    }

    void Awake() {
        grid = new Dictionary<Vector2, GameObject>();
    }


}
