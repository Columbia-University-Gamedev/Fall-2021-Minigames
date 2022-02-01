using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class AdaptCameraSizeToAspect : MonoBehaviour
{
    [SerializeField]
    float _portraitModeScaleFactor = 3f;

    public float PortraitModeScaleFactor { get { return _portraitModeScaleFactor; } }

    [SerializeField]
    AdaptiveAspectRatio _aspectTracker;

    Camera _camera;
    float _baseSize; 

    // Use this for initialization
    void Awake()
    {
        _camera = GetComponent<Camera>();
        _baseSize = _camera.orthographicSize;


        var width = _camera.scaledPixelWidth;
        var height = _camera.scaledPixelHeight;

        var ratio = width / height;

        ScaleCamera(ratio);
    }

    void ScaleCamera(float ratio)
    {
        if (_camera.orthographic)
        {

            if (ratio < 1f)
            {
                // portrait mode
                _camera.orthographicSize = _baseSize * _portraitModeScaleFactor;
            }
            else
            {
                _camera.orthographicSize = _baseSize;

            }

        }
        
    }

    private void OnEnable()
    {
        if (_aspectTracker)
        {
            _aspectTracker.OnAspectUpdated += ScaleCamera; 
        }
    }

    private void OnDisable()
    {
        if (_aspectTracker)
        {
            _aspectTracker.OnAspectUpdated -= ScaleCamera;
        }
    }
}
