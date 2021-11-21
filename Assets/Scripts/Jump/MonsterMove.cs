using UnityEngine;
using System.Collections;

public class MonsterMove : MonoBehaviour
{
    public Vector3 minPos;
    public Vector3 maxPos;
    private Vector3 moveTowards;

    public bool moveHorizontally;

    private void Start()
    {
        moveTowards = minPos; 
    }

    void Update()
    {
       if (moveHorizontally)
       {
           Horizontal();
       }
       else
       {
           Vertical();
       }
    }

    void Horizontal()
    {
        if (transform.position.x == minPos.x)
        {
            moveTowards = maxPos;
        }
        else if (transform.position.x == maxPos.x)
        {
            moveTowards = minPos;
        }
        transform.position = Vector3.Lerp(transform.position, moveTowards, 0.5f);
    }

    void Vertical()
    {
        if (transform.position.y == minPos.y)
        {
            moveTowards = maxPos;
        }
        else if (transform.position.y == maxPos.y)
        {
            moveTowards = minPos;
        }
        transform.position = Vector3.Lerp(transform.position, moveTowards, 0.5f);
    }
}