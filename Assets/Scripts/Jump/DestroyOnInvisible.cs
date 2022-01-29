using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    [SerializeField]
    Camera _camera;

    public Camera Camera
    {
        get { return _camera; }
        set { _camera = value; }
    }

    Renderer _renderer; 

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBelowCamera())
        {
            Destroy(this.gameObject);
        }
    }

    bool IsBelowCamera()
    {
        if (!_renderer.isVisible)
        {
            var delta = transform.position - _camera.transform.position;
            float up_dot = Vector3.Dot(_camera.transform.up, delta);
            float forward_dot = Vector3.Dot(_camera.transform.forward, delta);

            // if is below camera origin
            if (up_dot < 0f)
            {
                Vector2 cutoff = _camera.ViewportToWorldPoint(new Vector3(0, 0, forward_dot)) - _camera.transform.position;

                float cutoff_dot = Vector3.Dot(_camera.transform.up, cutoff);

                return up_dot < cutoff_dot;
            }
        }

        return false;
    }
}
