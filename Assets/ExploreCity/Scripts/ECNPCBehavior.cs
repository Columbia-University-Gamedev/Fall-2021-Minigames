using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ECNPCBehavior : MonoBehaviour
{
    //if not empty the NPC will patrol while not in range
    public Transform[] patrolPoints = { };
    private int curWaypoint;

    private bool isPatrolling = true;
    public float npcSpeed;
    public float interactRange;

    private CircleCollider2D cd;

    // Start is called before the first frame update
    void Start()
    {
        //only one patrol point would cause glitches so solve that here
        if(patrolPoints.Length == 0 || patrolPoints.Length == 1)
        {
            //don't patrol if no waypoints supplied
            isPatrolling = false;
        }
        cd = GetComponent<CircleCollider2D>();
        cd.radius = interactRange;
    }

    private void OnDrawGizmosSelected()
    {
        //when the object is selected in the unity editor show its patrol points with lines between each
        if(patrolPoints.Length != 0)
        {
            Gizmos.color = Color.blue;
            for(int i = 0; i < patrolPoints.Length - 1; i++)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
            }
            Gizmos.DrawLine(patrolPoints[0].position, patrolPoints[patrolPoints.Length-1].position);
        }
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            //set the current destination
            Transform waypoint = patrolPoints[curWaypoint];
            if(Vector2.Distance(transform.position, waypoint.position) < .01f)
            {
                //after arriving update to the next waypoint with wraparound
                curWaypoint = (curWaypoint + 1) % patrolPoints.Length;
            } else
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoint.position, npcSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPatrolling = false;
            gameObject.BroadcastMessage("OnInRange");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && patrolPoints.Length > 1)
        {
            isPatrolling = true;
        }
        gameObject.BroadcastMessage("OnLeaveRange");

    }

    public string generateDialogue()
    {
        //access grand list of dialogue

        return null;
    }

}
