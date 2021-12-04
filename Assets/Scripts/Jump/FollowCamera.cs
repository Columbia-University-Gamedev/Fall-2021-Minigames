using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField]
    Transform _target;

    [SerializeField]
    Vector2 _safeZoneExtent;

    [SerializeField]
    Vector2 _outerZoneExtent; 

    bool _moving = false;

    Vector3 _velocity;

    [SerializeField]
    float _friction = 0.1f;

    [SerializeField]
    float _acceleration = 12f;

    [SerializeField]
    float _maxSpeed = 20f;

    [SerializeField]
    bool _killAcceleration = true; 

    private void Start()
    {
        _velocity = Vector3.zero; 
    }

    // Update is called once per frame
    void Update()
    {
        var flattenedPosition = transform.position;
        flattenedPosition.z = _target.transform.position.z;

        var outerBounds = new Bounds(flattenedPosition, _outerZoneExtent);
        var innerBounds = new Bounds(flattenedPosition, _safeZoneExtent);

        if (!_moving && !outerBounds.Contains(_target.transform.position))
        {
            _moving = true;

            // reset velocity
            if (_killAcceleration)
            {
                _velocity = Vector3.zero;
            }
        } else if (_moving && innerBounds.Contains(_target.transform.position))
        {
            _moving = false;
        }

        if (_moving)
        {
            Vector3 targetPos = _target.position;
            targetPos.z = transform.position.z;

            var direction = (targetPos - transform.position).normalized;
            var acceleration = direction * _acceleration;

            _velocity += acceleration * Time.deltaTime;
        }
        else
        {
            _velocity *= (1f - _friction);
        }

        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);

        transform.position += _velocity * Time.deltaTime;

    }

    // editor visualization
    void OnDrawGizmos()
    { 
        Color lineColor = Color.yellow;

        Gizmos.color = lineColor;
        Gizmos.DrawWireCube(transform.position, _safeZoneExtent);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _outerZoneExtent);

    }
}
