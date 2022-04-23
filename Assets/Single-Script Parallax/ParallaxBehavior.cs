using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehavior : MonoBehaviour, ISerializationCallbackReceiver {
    
    // A note: this implementation only works with one camera at a time. (Splitscreen won't work.) 

    [Header("Hover over public fields for important tooltips on this parallax implementation.")]
    [Space(16)]


    [Tooltip("A note on the orthographic camera: It's recommended that you set the near clip plane to a value like -1000. Think of the camera's z-coord as the depth of the scene which is in focus. Parallax elements deeper than the camera (closer to the horizon) will move more slowly, while elements shallower than the camera (farther from the horizon than even the camera) will move more quickly. Those shallower layers will only render if the near-clip distance is set to a negative value.")]
    [Header("The camera used for the parallax calculations. Defaults to Camera.main.")]
    public Camera cam;

    // You can't serialize static variables, so to make depth editable in inspector, need to use a helper.
    private static float anchorZDepth = 10f;

    [Tooltip("Don't position parallax elements behind the anchor. Think of the anchor as the horizon point--objects at the same depth (z coord) as the anchor (aka objects at the horizon) won't move. Objects beyond the horizon, because of the formula used in this implementation, will move backwards.")]
    [Header("The depth of the horizon point.")]
    public float anchorZDepthSerializationHelper = anchorZDepth;

    public static Vector3 AnchorPosition { get { return new Vector3(0f, 0f, anchorZDepth); } }

    private Vector3 originalPosition;

    // for debug
    private static ParallaxBehavior gizmoDrawer = null;

    public void OnBeforeSerialize()
    {
        anchorZDepthSerializationHelper = anchorZDepth; 
    }

    public void OnAfterDeserialize()
    {
        anchorZDepth = anchorZDepthSerializationHelper; 
    }

    private void Awake()
    {
        originalPosition = transform.position;

        if (cam == null)
        {
            cam = Camera.main; 
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Draw a 16:9 panel at the anchor position (but only once)
        
        if (gizmoDrawer == null)
        {
            gizmoDrawer = this; 
        }

        bool AmISelected = false;
        bool IsGizmoDrawerSelected = false; 

        foreach (Transform transform in UnityEditor.Selection.transforms)
        {
            if (transform == this.transform)
            {
                AmISelected = true; 
            }

            if (transform == gizmoDrawer.transform)
            {
                IsGizmoDrawerSelected = true; 
            }
        }

        if (AmISelected && !IsGizmoDrawerSelected)
        {
            gizmoDrawer = this;
            IsGizmoDrawerSelected = true; 
        }

        if (gizmoDrawer == this && IsGizmoDrawerSelected)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(AnchorPosition, new Vector3(1.6f, 0.9f, 0.1f));
            Gizmos.DrawSphere(AnchorPosition, 0.1f);
        }
    }
#endif

    // Update is called after camera has moved
    void LateUpdate () {
        /// update position

        Vector2 startPointX, endPointX, startPointY, endPointY;

        startPointX = new Vector2(AnchorPosition.x, AnchorPosition.z);
        endPointX = new Vector2(cam.transform.position.x, cam.transform.position.z);

        startPointY = new Vector2(AnchorPosition.y, AnchorPosition.z);
        endPointY = new Vector2(cam.transform.position.y, cam.transform.position.z);

        float z = transform.position.z; // the depth value; can be modified at runtime. Will generally be in between anchorZ and camZ.

        float x = originalPosition.x - FindPointOnSide(startPointX, endPointX, z) + cam.transform.position.x - AnchorPosition.x;
        float y = originalPosition.y - FindPointOnSide(startPointY, endPointY, z) + cam.transform.position.y - AnchorPosition.y;

        transform.position = new Vector3(x, y, z);
    }

    public static float FindPointOnSide(float x1, float y1, float y2, float x3, float y3)
    {
        /// this is a port of a parallax implementation I wrote for GameMaker: Studio in 2014 called Infinite Parallax.
        /// scr_triangle_find_side_similar(x1,y1,y2,x3,y3)
        /// solves for x2, a point on the hypotenuse of a right triangle. 
        /// also solves for x coord of a point on any line

        float x2;
        
        if (y1 - y3 != 0)
        {
            x2 = -(((x1 - x3) * ((y1 - y2) / (y1 - y3)) - x1));
        }
        else
        {
            x2 = -(((x1 - x3) * ((y1 - y2) / (0.000001f)) - x1));
        }
        
        return x2;
    }

    public static float FindPointOnSide(Vector2 startPoint, Vector2 endPoint, float midPointYComponent)
    {
        float x1, y1, x2, y2, x3, y3;

        x1 = startPoint.x; // x of endpoint 1 
        y1 = startPoint.y; // y of endpoint 1
      //x2                x of point on line - what we're solving for  
        y2 = midPointYComponent; // y of point on line
        x3 = endPoint.x; // x of endpoint 2
        y3 = endPoint.y; // y of endpoint 2

        x2 = FindPointOnSide(x1, y1, y2, x3, y3);

        return x2; 
    }
}
