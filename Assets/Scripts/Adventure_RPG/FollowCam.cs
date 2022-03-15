using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform leftBounds;
    [SerializeField] private Transform rightBounds;
    [SerializeField] private GameObject player;
    private Vector3 offset;
    private Vector3 leftOffset;
    private Vector3 rightOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        leftOffset = (transform.position.x - leftBounds.position.x) * Vector3.right;
        rightOffset = (transform.position.x - rightBounds.position.x) * Vector3.right;
        Debug.Log(rightOffset);
        Debug.Log(leftOffset);
    }

    // Update is called once per frame
    void Update()
    {
        float rightBoundsx = rightBounds.position.x;
        float leftBoundsx = leftBounds.position.x;

        bool hitRightBounds = player.transform.position.x >= rightBoundsx;
        bool hitLeftBounds = player.transform.position.x <= leftBoundsx;
        
        Vector3 newPos;
        if (hitLeftBounds || hitRightBounds)
        {
            Debug.Log("moving cam");
            offset = (hitLeftBounds) ? leftOffset : rightOffset;            

            newPos = offset + player.transform.position - 10f * Vector3.forward;
            transform.position = Vector3.Lerp(transform.position, newPos, 0.5f);
        }
    }
}
