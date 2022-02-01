using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyAI : MonoBehaviour
{
    public enum AIState
    {
        Moving, 
        Hitting, 
        Idle
    }

    AIState _state; 

    public AIState State
    {
        get { return _state; }
    }

    public enum MoveDirection
    {
        Up, 
        Down, 
        Left, 
        Right,
        None
    }

    PlayerScript _player;

    [SerializeField]
    [Tooltip("Extent of the AI's line of sight, in meters")]
    float _lookRadius = 7f; // meters 

    BallScript _targetBall; 

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerScript>();

        _state = AIState.Idle; 
    }

    // Update is called once per frame
    void Update()
    {
        // very basic finite state machine AI

        if (!IsTargetInRange())
        {
            _targetBall = LookForBall();
        }

        if (!_targetBall && _player.OnGround)
        {
            _state = AIState.Idle; 
        } else if (IsTargetHittable() && _player.ChargePercentage >= 1f) {
            _state = AIState.Hitting; 
        } else
        {
            _state = AIState.Moving; 
        }

        switch (_state)
        {
            case AIState.Idle:

                // stop charging if a ball is too far away
                if (_player.ChargePercentage > 0f)
                {
                    _player.chargeUp(chargeStart: false, chargeRelease: true);
                }
                break;

            case AIState.Hitting:
                _player.chargeUp(chargeStart: false, chargeRelease: true);
                break;

            case AIState.Moving:
 
                // check if ball close enough to charge
                if (GetTargetDistance() < VolleyballConstants.ballMaxHitDistance * 10f && !_player.Charging) { 
                    _player.chargeUp(chargeStart: true, chargeRelease: false);
                }

                var directions = GetToTargetDirections();

                bool startJump = _player.CanJump && directions.Vertical == MoveDirection.Up;

                bool moveLeft = directions.Horizontal == MoveDirection.Left;
                bool moveRight = directions.Horizontal == MoveDirection.Right;

                _player.move(moveLeft, moveRight, startJump, startJump, !startJump); 

                break;
        }
    }

    (MoveDirection Horizontal, MoveDirection Vertical) GetToTargetDirections()
    {
        var offset = _targetBall.transform.position - _player.transform.position;

        MoveDirection h, v;

        if (Mathf.Abs(offset.x) <= 0.125f) 
        {
            h = MoveDirection.None; 
        } else {
            h = Vector2.Dot(Vector2.right, offset) < 0f ? MoveDirection.Left : MoveDirection.Right;
        }

        if (Mathf.Abs(offset.y) <= 0.125f)
        {
            v = MoveDirection.None;
        } else
        {
            v = Vector2.Dot(Vector2.up, offset) < 0f ? MoveDirection.Down : MoveDirection.Up;
        }

        return (h, v);
    }

    float GetTargetDistance()
    {
        if (!_targetBall)
        {
            return Mathf.Infinity; 
        }

        return (_player.transform.position - _targetBall.transform.position).magnitude; 
    }

    bool IsTargetHittable()
    {
        return _targetBall != null && (_targetBall.transform.position - _player.transform.position).magnitude < VolleyballConstants.ballMaxHitDistance;
    }

    bool IsTargetInRange()
    {
        if (!_targetBall || (_targetBall.transform.position - _player.transform.position).magnitude > _lookRadius)
        {
            return false; 
        }

        return true; 
    }

    BallScript LookForBall()
    {
        Collider2D[] colliders;

        colliders = Physics2D.OverlapCircleAll(_player.transform.position, _lookRadius);

        foreach (var c in colliders)
        {
            var b = c.gameObject.GetComponent<BallScript>(); 
            
            if (b != null)
            {
                return b; 
            }
        }

        return null; 
    }
} 
