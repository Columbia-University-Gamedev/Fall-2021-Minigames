using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FollowCamera))]
[RequireComponent(typeof(CameraShaker))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    dummy_movement _player;

    FollowCamera _followCamera;
    CameraShaker _cameraShaker; 

    // Start is called before the first frame update
    void Awake()
    {
        _followCamera = GetComponent<FollowCamera>();
        _cameraShaker = GetComponent<CameraShaker>();   
    }

    private void OnEnable()
    {
        _player.OnPlayerHurt += HandlePlayerHurt;
        _player.OnPlayerDied += HandlePlayerDied;

        _cameraShaker.OnCameraShakeEnded += HandleCameraShakeEnded;
    }

    private void OnDisable()
    {
        _player.OnPlayerHurt -= HandlePlayerHurt;
        _player.OnPlayerDied -= HandlePlayerDied;

        _cameraShaker.OnCameraShakeEnded -= HandleCameraShakeEnded;
    }

    void HandlePlayerDied(dummy_movement.DeathType type)
    {
        _followCamera.enabled = false;
        _cameraShaker.TriggerCameraShake();
    }

    void HandlePlayerHurt(GameObject attacker)
    {
        _followCamera.enabled = false;
        _cameraShaker.TriggerCameraShake();
    }

    void HandleCameraShakeEnded()
    {
        if (!_player.IsDead)
        {
            _followCamera.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
