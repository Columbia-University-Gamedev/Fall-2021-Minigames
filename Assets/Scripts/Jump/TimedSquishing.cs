using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSquishing : MonoBehaviour
{
    [System.Serializable]
    public struct SquishParams
    {
        [SerializeField]
        public float speed, min, max, duration; 

        public SquishParams(float speed, float min, float max, float duration)
        {
            this.speed = speed; 
            this.min = min;
            this.max = max;
            this.duration = duration;
        }
    }

    public void ApplyParams(SquishParams attrs)
    {
        _squishSpeed = attrs.speed;
        _minSquish = attrs.min;
        _maxSquish = attrs.max;
        _duration = attrs.duration; 
    } 

    public bool IsSquishing
    {
        get { return _isSquishing; }
    }

    bool _isSquishing = false;


    float _squishStartTime;

    // squish will sinusoidally oscillate around these two values
    float _minSquish = 0.6f;
    public float MinSquish
    {
        get { return _minSquish; }
        set { _minSquish = value; }
    }


    float _maxSquish = 1.2f;
    public float MaxSquish
    {
        get { return _maxSquish; }
        set { _maxSquish = value; }
    }

    float _dampFactor = 0.2f;
    public float DampFactor
    {
        get { return _dampFactor; }
        set { _dampFactor = value; }
    }

    static float DampingCoefficient(float time, float damping, float duration)
    {
        return -1f * Mathf.Exp(damping * (time - duration)) + 1f;
    }

    static float BaseSinusoid(float time, float min, float max, float frequency)
    {
        return 0.5f * (max - min) * Mathf.Sin(frequency * time) + 0.5f * (max + min);
    }

    static float NetSquish(float time, float damping, float duration, float min, float max, float frequency)
    {
        return DampingCoefficient(time, damping, duration) * 0.5f * (max - min) * Mathf.Sin(frequency * time) + 1f; // 0.5f * (max + min);
    }

    public float SquishSpeed
    {
        get { return _squishSpeed; }
        set { _squishSpeed = value; }
    }
    float _squishSpeed = 20f; // frequency multiplier

    float _duration = 1f; // update to extend or curtail jump

    Vector3 _originalScale;

    public Vector3 OriginalScale
    {
        get { return _originalScale; }
        set { _originalScale = value; }
    }

    public delegate void SquishStarted();
    public delegate void SquishEnded();

    public SquishStarted OnSquishStarted;
    public SquishEnded OnSquishEnded;

    Coroutine _squish;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    IEnumerator DoSquish()
    {
        return DoSquish(_duration); 
    }

    IEnumerator DoSquish(float duration)
    {
        _duration = duration;

        // skip a frame--time for reset?
        yield return null;

        // preserve signs in the current scale (preserves orientation)
        float signX, signY, signZ;

        signX = Mathf.Sign(transform.localScale.x);
        signY = Mathf.Sign(transform.localScale.y);
        signZ = Mathf.Sign(transform.localScale.z);

        _isSquishing = true;
        _squishStartTime = Time.time;

        OnSquishStarted?.Invoke();


        while (Time.time - _squishStartTime < _duration)
        {
            //float decay = -1f * (Time.time - _squishStartTime) / duration + 1f; 
            //float y = _originalScale.y * (_maxSquish * 0.5f * (Mathf.Cos(_squishSpeed * Time.time) + 1f) + _minSquish);

            signX = Mathf.Sign(transform.localScale.x);
            signY = Mathf.Sign(transform.localScale.y);
            signZ = Mathf.Sign(transform.localScale.z);

            float x = NetSquish(Time.time - _squishStartTime + 0.25f * Mathf.PI, _dampFactor, _duration,  1.2f * _minSquish, 0.8f * _maxSquish, _squishSpeed);

            float y = NetSquish(Time.time - _squishStartTime, _dampFactor, _duration, _minSquish, _maxSquish, _squishSpeed);

            transform.localScale = new Vector3(signX * x * Mathf.Abs(_originalScale.x),
                                               signY * y * Mathf.Abs(_originalScale.y),
                                               signZ * Mathf.Abs(_originalScale.z));

            yield return null; 
        }

        var target = new Vector3(signX * Mathf.Abs(_originalScale.x),
                                signY * Mathf.Abs(_originalScale.y),
                                signZ * Mathf.Abs(_originalScale.z));

        while (transform.localScale != target)
        {
            signX = Mathf.Sign(transform.localScale.x);
            signY = Mathf.Sign(transform.localScale.y);
            signZ = Mathf.Sign(transform.localScale.z);

            target = new Vector3(signX * Mathf.Abs(_originalScale.x),
                                signY * Mathf.Abs(_originalScale.y),
                                signZ * Mathf.Abs(_originalScale.z));

            transform.localScale = Vector3.Lerp(transform.localScale, target, 0.08f);

            yield return null; 
        }
        

        _isSquishing = false;

        OnSquishEnded?.Invoke();

    }

    public void TriggerSquish()
    {
        TriggerSquish(_duration);
    }

    public void TriggerSquish(float duration)
    {
        // just extend the current squish if one is already running
        if (_isSquishing)
        {
            _squishStartTime = Time.time;
            _duration = duration; 
        } else
        {
            _squish = StartCoroutine(DoSquish(duration));
        }
    }
}
