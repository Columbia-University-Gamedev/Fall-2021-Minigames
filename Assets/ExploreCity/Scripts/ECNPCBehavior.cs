using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ECNPCBehavior : MonoBehaviour
{
    //if not empty the NPC will patrol while not in range
    public List<Vector2> patrolPoints = new List<Vector2>();

    //i am aware this causes all NPCs to have the same dialogue, in a proper system they'd read dialogue from a file    

    private GameObject dialogueContainer;
    private Dialogues d;
    private int curWaypoint;
    private bool isPatrolling = true;
    public float npcSpeed;
    public float interactRange;

    public int waypointRange = 5;

    private CircleCollider2D cd;

    // Start is called before the first frame update
    void Start()
    {
        dialogueContainer = GameObject.Find("Dialogue Container");
        d = dialogueContainer.GetComponent<Dialogues>();
        //only one patrol point would cause glitches so solve that here
        if (patrolPoints.Count == 0 || patrolPoints.Count == 1)
        {
            //don't patrol if no waypoints supplied
            isPatrolling = false;
        }
        cd = GetComponent<CircleCollider2D>();
        cd.radius = interactRange / 10;

    }

    public void init()
    {
        var r = new System.Random();
        patrolPoints.Add(new Vector2(transform.position.x + r.Next(-waypointRange, waypointRange), transform.position.x + r.Next(-waypointRange, waypointRange)));
        patrolPoints.Add(new Vector2(transform.position.x + r.Next(-waypointRange, waypointRange), transform.position.x + r.Next(-waypointRange, waypointRange)));
        patrolPoints.Add(new Vector2(transform.position.x + r.Next(-waypointRange, waypointRange), transform.position.x + r.Next(-waypointRange, waypointRange)));
    }

    public string[] getDialogue()
    {
        var random = new System.Random();
        string[] output = d.dialogues[random.Next(d.dialogues.Length)];
        Debug.Log(output);
        return output;
    }

    private void OnDrawGizmosSelected()
    {
        //when the object is selected in the unity editor show its patrol points with lines between each
        if (patrolPoints.Count != 0)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < patrolPoints.Count - 1; i++)
            {
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[i + 1]);
            }
            Gizmos.DrawLine(patrolPoints[0], patrolPoints[patrolPoints.Count - 1]);
        }
        Gizmos.DrawWireSphere(transform.position, interactRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, waypointRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            //set the current destination
            Vector2 waypoint = patrolPoints[curWaypoint];
            if (Vector2.Distance(transform.position, waypoint) < .01f)
            {
                //after arriving update to the next waypoint with wraparound
                curWaypoint = (curWaypoint + 1) % patrolPoints.Count;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoint, npcSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPatrolling = false;
            gameObject.BroadcastMessage("OnInRange");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && patrolPoints.Count > 1)
        {
            isPatrolling = true;
        }
        gameObject.BroadcastMessage("OnLeaveRange");

    }

}
