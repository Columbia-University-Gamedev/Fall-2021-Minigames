using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private List<Vector3> characterLocations;
    private int locIndex = 0;
    private Transform tr;
    [SerializeField] private float lerpFactor;
    private float stopDistSq = 0.05f;
    
    //at this distance it will start to rotate to face the player
    //rather than the direction it's moving
    private float rotateDistSq = 1.0f;

    public void Start()
    {
        tr = this.transform;
        if (characterLocations.Count > 0)
        {
            tr.position = characterLocations[0];
        }
    }
    public void Update()
    {
        if (characterLocations.Count > 0)
        {
            float distSq = Vector3.SqrMagnitude(tr.position - characterLocations[locIndex]);
            if (distSq >= stopDistSq) {
                tr.position = Vector3.Lerp(tr.position,
                    characterLocations[locIndex], Time.deltaTime * lerpFactor / distSq);
            }
            if (distSq >= rotateDistSq) {
                tr.localScale = new Vector3(
                    (tr.position.x - characterLocations[locIndex].x < 0 ? 1 : -1) * Mathf.Abs(tr.localScale.x),
                    tr.localScale.y, tr.localScale.z);
            }
            else
            {
                tr.localScale = new Vector3(
                    (tr.position.x - PlayerMovement.PlayerTransform.position.x < 0 ? 1 : -1) 
                    * Mathf.Abs(tr.localScale.x),
                    tr.localScale.y, tr.localScale.z);
            }
        }
    }

    public void ProgressNPC()
    {
        locIndex++;
    }
    }
