using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float speed; 
    [SerializeField] private Rigidbody2D playerRb;
    private bool _startingDirectionIsLeft = true;
    private float _leftScaleSign = 1f;
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

        if (Mathf.Abs(playerRb.velocity.x) > 0.2f)
        {
            float flag1 = playerRb.velocity.x < 0 ? 1f : -1f;
            float flag2 = _startingDirectionIsLeft ? 1f : -1f;

            float signX = _leftScaleSign * flag1 * flag2;

            Vector3 scale = transform.localScale;

            scale.x = Mathf.Abs(scale.x) * signX;

            transform.localScale = scale;
        }
    }
}
