using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraScript : MonoBehaviour
{

    private GameObject[] players;
    private GameObject[] balls;

    private Camera cam;
    private Transform tf;

    private UnityAction initCameraListener;

    void OnEnable(){
        EventManager.StartListening("initCamera",initCameraListener);
    }
    void OnDisable(){
        EventManager.StopListening("initCamera",initCameraListener);
    }

    void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
        tf = gameObject.GetComponent<Transform>();
        
        initCameraListener = new UnityAction(initCamera);
    }

    // Start is called before the first frame update
    void Start()
    {
        initCamera();
    }

    // Update is called once per frame
    void Update()
    {

        //Make the camera follow the players
        Transform t;

        float minX = 100.0f;
        float maxX = -100.0f;
        float maxY = -100.0f;
        float minY = 100.0f;

        foreach(GameObject player in players){
            t = player.GetComponent<Transform>();
            if(t.position.x > maxX) maxX = t.position.x;
            if(t.position.x < minX) minX = t.position.x;
            if(t.position.y > maxY) maxY = t.position.y;
            if(t.position.y < minY) minY = t.position.y;
        }
        foreach(GameObject ball in balls){
            t = ball.GetComponent<Transform>();
            if(t.position.x > maxX) maxX = t.position.x;
            if(t.position.x < minX) minX = t.position.x;
            if(t.position.y > maxY) maxY = t.position.y;
            if(t.position.y < minY) minY = t.position.y;
        }

        tf.position = new Vector3((minX+maxX)/2.0f,(maxY+minY)/2.0f,-1.0f);
    }

    void initCamera(){
        players = GameObject.FindGameObjectsWithTag("Player");
        balls = GameObject.FindGameObjectsWithTag("Ball");

        cam.orthographicSize = VolleyballConstants.cameraSize;
        tf.position = new Vector3(0.0f, 0.0f, -1.0f);
    }
}
