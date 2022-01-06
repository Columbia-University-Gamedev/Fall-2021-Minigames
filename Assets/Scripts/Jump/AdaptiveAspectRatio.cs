using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class AdaptiveAspectRatio : MonoBehaviour
{
    float _width;
    float _height;

    float _aspectRatio;

    public float AspectRatio
    {
        get { return _aspectRatio; }
    }

    public delegate void AspectUpdated(float ratio);
    public AspectUpdated OnAspectUpdated; 

    [SerializeField]
    Camera _camera;

    private void Awake()
    {
        UpdateAndSetRatio();
    }

    private void Update()
    {
        if (DidDimensionsChange())
        {
            UpdateAndSetRatio();
        }
    }

    void UpdateAndSetRatio()
    {
        _width = _camera.scaledPixelWidth;
        _height = _camera.scaledPixelHeight;

        _aspectRatio = _width / _height;

        OnAspectUpdated?.Invoke(_aspectRatio);
    }

    bool DidDimensionsChange()
    {
        return _width == _camera.scaledPixelWidth && _height == _camera.scaledPixelHeight; 
    }
}
