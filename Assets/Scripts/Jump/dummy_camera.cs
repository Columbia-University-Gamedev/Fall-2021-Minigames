using UnityEngine;
using System.Collections;
public class dummy_camera: MonoBehaviour
{
    public GameObject player;
    private float offsetX;
    private float offsetY;
    public float camaraSpeed;
    void Start ()
    {
        offsetX = transform.position.x - player.transform.position.x;
        offsetY = camaraSpeed;
    }
    void LateUpdate ()
    {
        transform.position = new Vector3(player.transform.position.x + offsetX, transform.position.y+offsetY, transform.position.z);

    }
}