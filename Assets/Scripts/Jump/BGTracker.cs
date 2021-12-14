using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private MeshRenderer mr;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x = transform.position.x / transform.localScale.x;
        offset.y = transform.position.y / transform.localScale.y;

        mat.mainTextureOffset = offset;
    }
}