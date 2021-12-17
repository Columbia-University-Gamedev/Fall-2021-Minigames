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
    Vector2 _safezoneOffset;


    [SerializeField]
    Vector2 _outerZoneExtent;

    [SerializeField]
    Vector2 _outerZoneOffset;


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

    [SerializeField]
    bool _scaleBoundsWithAspect = true;

    float _aspectScaling = 1f;

    float _baseAspectScaling = 1f; 

    [SerializeField]
    AdaptiveAspectRatio _aspectTracker;


    private void HandleAspectUpdate(float ratio)
    {
        _aspectScaling = _baseAspectScaling * ratio; 
    }

    private void OnEnable()
    {
        if (_aspectTracker)
        {
            _aspectTracker.OnAspectUpdated += HandleAspectUpdate;
        }
    }

    private void OnDisable()
    {
        if (_aspectTracker)
        {
            _aspectTracker.OnAspectUpdated -= HandleAspectUpdate;
        }
    }

    private void Start()
    {
        _velocity = Vector3.zero; 
    }


    // Update is called once per frame
    void Update()
    {
        var flattenedPosition = transform.position;
        flattenedPosition.z = _target.transform.position.z;

        float scaleFactor = 1f;

        // scaling based on aspect ratio
        if (_scaleBoundsWithAspect && _aspectTracker)
        {
            scaleFactor = _aspectScaling; 
        }

        var outerBounds = new Bounds(flattenedPosition + scaleFactor * (Vector3)_outerZoneOffset, scaleFactor * _outerZoneExtent);
        var innerBounds = new Bounds(flattenedPosition + scaleFactor * (Vector3)_safezoneOffset, scaleFactor * _safeZoneExtent);

        if (!_moving && !outerBounds.Contains(_target.transform.position))
        {
            _moving = true;

            // reset velocity
            if (_killAcceleration)
            {
                _velocity = Vector3.zero;
            }

            //Vector3 targetPos = _target.position;
            //targetPos.z = transform.position.z;

            //var direction = (targetPos - transform.position).normalized;
            //_velocity = direction * _maxSpeed; 

        } else if (_moving && innerBounds.Contains(_target.transform.position))
        {
            _moving = false;
        }

        if (_moving)
        {
            Vector3 targetPos = _target.transform.position;
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
        Gizmos.DrawWireCube(transform.position + (Vector3)_safezoneOffset, _safeZoneExtent);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)_outerZoneOffset, _outerZoneExtent);

    }
}
