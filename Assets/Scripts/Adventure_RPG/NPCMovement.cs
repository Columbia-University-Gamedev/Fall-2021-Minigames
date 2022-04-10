using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private List<Vector3> characterLocations;
    private int locIndex = 0;
    private Transform tr;
    [SerializeField] private float lerpFactor;

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
            tr.position = Vector3.Lerp(tr.position,
                characterLocations[locIndex], Time.deltaTime * lerpFactor);
        }
    }

    public void ProgressNPC()
    {
        locIndex++;
    }
    }
