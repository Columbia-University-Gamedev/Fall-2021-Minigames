using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private static float horizontalLerpFactor = 2.0f;
    private static float verticalLerpFactor = 6.0f;
    private static Vector3 offset = new Vector3(0,1,-10);
    private Transform tr;
    private Transform player_tr;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    
    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        player_tr = player.transform;
        tr.position = player_tr.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curpos = tr.position;
        Vector3 ppos = player_tr.position;
        Vector3 p = new Vector3(
            Mathf.Lerp(curpos.x,ppos.x + offset.x,Time.deltaTime*horizontalLerpFactor),
            Mathf.Lerp(curpos.y,ppos.y + offset.y,Time.deltaTime*verticalLerpFactor),
            curpos.z);
        Vector3 newPos = new Vector3(Mathf.Clamp(p.x, minX, maxX), Mathf.Clamp(p.y, minY, maxY), p.z);
        tr.position = newPos;
    }
}
