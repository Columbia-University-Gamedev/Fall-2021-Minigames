using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(AspectRatioFitter))]
public class AdaptiveAspectRatio : MonoBehaviour
{
    float _width;
    float _height;

    AspectRatioFitter _fitter;


    [SerializeField]
    Camera _camera; 

    private void Awake()
    {
        _fitter = GetComponent<AspectRatioFitter>();

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

        _fitter.aspectRatio = _width / _height; 
    }

    bool DidDimensionsChange()
    {
        return _width == _camera.scaledPixelWidth && _height == _camera.scaledPixelHeight; 
    }
}
