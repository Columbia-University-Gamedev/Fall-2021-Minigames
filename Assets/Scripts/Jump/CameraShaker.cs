using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShaker : MonoBehaviour
{

    bool _isCameraShaking = false;

    [SerializeField]
    float _cameraShakeDuration = 0.35f; // seconds

    [SerializeField]
    float _timeScale = 6;

    [SerializeField]
    float _shakeSeverity = 1;

    const float SHAKE_SCALE = 0.1f; 

    float _cameraShakeStartTime;

    float _xOffset, _yOffset;
    (Phasor[] Phasors, float[] Weights) _shakeData;

    public delegate void CameraShakeStarted();
    public delegate void CameraShakeEnded();

    public CameraShakeStarted OnCameraShakeStarted;
    public CameraShakeEnded OnCameraShakeEnded;

    // Start is called before the first frame update
    void Start()
    {
        _xOffset = Random.value * _timeScale;
        _yOffset = Random.value * _timeScale;

        _shakeData = ShakeComponents();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCameraShaking)
        {
            if (Time.time - _cameraShakeStartTime > _cameraShakeDuration)
            {
                _isCameraShaking = false;
                OnCameraShakeEnded?.Invoke();
            } else
            {
                // do the shake
                float t = Time.time * _timeScale;

                float x = _shakeSeverity * SHAKE_SCALE * SummedWeightedOutput(_shakeData.Phasors, _shakeData.Weights, t + _xOffset);
                float y = _shakeSeverity * SHAKE_SCALE * SummedWeightedOutput(_shakeData.Phasors, _shakeData.Weights, t - _yOffset);

                transform.position += new Vector3(x, y, 0f); 
            }
        }
    }

    public void TriggerCameraShake()
    {
        _isCameraShaking = true;
        _cameraShakeStartTime = Time.time;

        OnCameraShakeStarted?.Invoke();
    }

    public struct Phasor
    {
        public float amplitude;
        public float frequency;
        public float phase;

        public Phasor(float a, float f, float p)
        {
            amplitude = a;
            frequency = f;
            phase = p; 
        }
    }

    public static float SummedWeightedOutput(Phasor[] phasors, float[] weights, float time)
    {
        float output = 0f;

        for (int i = 0; i < phasors.Length; i++) 
        {
            output += weights[i] * PhasorOutput(phasors[i], time);
        }

        return output; 
    }

    public static float PhasorOutput(Phasor p, float time)
    {
        return p.amplitude * Mathf.Cos(p.frequency * time - p.phase);
    }

    public static (Phasor[] Phasors, float[] Weights) ShakeComponents()
    {
        Phasor p1, p2, p3;

        p1 = new Phasor(0.5f, 4.2f, 0.9f);
        p2 = new Phasor(2.2f, -4.1f, 4.3f);
        p3 = new Phasor(-4f, 6.9f, -4.1f);

        float w1, w2, w3;

        w1 = 2.8f;
        w2 = 2.6f;
        w3 = 1.1f;

        var phasors = new Phasor[] { p1, p2, p3 };
        var weights = new float[] { w1, w2, w3 };

        return (phasors, weights);
    }
}
