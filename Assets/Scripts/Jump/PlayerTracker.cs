using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField]
    GameObject _target;

    [SerializeField]
    float _verticalSpeed = 2f; // meters per second

    [SerializeField]
    bool _allowVerticalMovement = false;

    [SerializeField]
    bool _shouldAccelerate = true;

    [SerializeField]
    float _accleration = 0.1f;

    [SerializeField]
    bool _shouldClampSpeed = false;

    [SerializeField]
    float _maxSpeed = 20f;

    [SerializeField]
    bool _isMovementEnabled = true; 

    dummy_movement _playerController;

    [SerializeField]
    float _freezeOnHurtTimeout = 0.35f;

    float _freezeStartTime;
    bool _isFrozen = false;


    // Start is called before the first frame update
    void Awake()
    {
        transform.position = _target.transform.position;

        _playerController = _target.GetComponent<dummy_movement>(); 
    }

    private void OnEnable()
    {
        _playerController.OnPlayerDied += HandlePlayerDied;
        _playerController.OnPlayerHurt += HandlePlayerHurt;
    }

    private void OnDisable()
    {
        _playerController.OnPlayerDied -= HandlePlayerDied;
        _playerController.OnPlayerHurt -= HandlePlayerHurt;
    }

    void HandlePlayerHurt(GameObject attacker)
    {
        _isFrozen = true; 
        _freezeStartTime = Time.time;
        _isMovementEnabled = false;
    }

    void HandlePlayerDied(dummy_movement.DeathType type)
    {
        _isMovementEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMovementEnabled)
        {
            if (_shouldAccelerate)
            {
                _verticalSpeed += _accleration * Time.deltaTime;
            }

            if (_shouldClampSpeed)
            {
                _verticalSpeed = Mathf.Min(_verticalSpeed, _maxSpeed);
            }

            // track character horizontally, but scroll upwards
            var x = _target.transform.position.x;

            var y = _allowVerticalMovement ?
                        Mathf.Max(transform.position.y + Time.deltaTime * _verticalSpeed,
                              _target.transform.position.y) :
                        _target.transform.position.y; 

            var z = transform.position.z;

            transform.position = new Vector3(x, y, z);
        } else if (_isFrozen)
        {
            if (Time.time - _freezeStartTime > _freezeOnHurtTimeout && !_playerController.IsDead)
            {
                _isFrozen = false;
                _isMovementEnabled = true; 
            }
        }


    }
}
