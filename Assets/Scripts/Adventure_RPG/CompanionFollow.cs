using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;
    [SerializeField]  float stoppingDistance;
    [SerializeField]  float speed; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        }
    }
}
