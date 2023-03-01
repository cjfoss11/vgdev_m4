using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class MinionAI : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent nav;
    private Animator anim;
    public GameObject[] waypoints;
    public GameObject movingWaypoint;
    int currWaypoint;
    VelocityReporter velocityReporter;
    public enum AIState
    {
        Patrol,
        ChaseWaypoint
    };
    public AIState aiState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currWaypoint = -1;
        aiState = AIState.Patrol;
        Debug.Log(waypoints.Length);
        setNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vely", nav.velocity.magnitude / nav.speed);

        switch(aiState)
        {
            case AIState.Patrol:
                if (currWaypoint == waypoints.Length - 1)
                {
                    currWaypoint = -1;
                    aiState = AIState.ChaseWaypoint;
                } else
                {
                    if (!nav.pathPending && nav.remainingDistance <= 2)
                    {
                        setNextWaypoint();
                    }
                }
                break;
            case AIState.ChaseWaypoint:
                velocityReporter = movingWaypoint.GetComponent<VelocityReporter>();
                float distance = (movingWaypoint.transform.position - nav.transform.position).magnitude;
                float lookAheadT = distance / nav.speed;
                Vector3 futureTarget = movingWaypoint.transform.position + lookAheadT * velocityReporter.velocity;

                nav.SetDestination(futureTarget);
                if (nav.remainingDistance <= 2)
                {
                    aiState = AIState.Patrol;
                }
                break;
            default:
                break;
        }
    }

    private void setNextWaypoint()
    {
        if (waypoints.Length > 0)
        {
            currWaypoint = currWaypoint + 1;
            nav.SetDestination(waypoints[currWaypoint].transform.position);
        }
       
    }
}
