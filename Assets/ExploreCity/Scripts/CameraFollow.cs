using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed;

    public Vector2 cameraRangeCoefficient;

    public Transform playerTransform;

    public void FixedUpdate() //FixedUpdate so camera movement syncs with
    {
        var playerPos = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

        var cursorWorldPos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        var posDiff = playerPos - cursorWorldPos;

        var endCameraPos = playerPos - new Vector3(posDiff.x / (1 / cameraRangeCoefficient.x), posDiff.y / (1 / cameraRangeCoefficient.y), transform.position.z);
        endCameraPos = new Vector3(endCameraPos.x, endCameraPos.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, endCameraPos, smoothSpeed);
    }
}