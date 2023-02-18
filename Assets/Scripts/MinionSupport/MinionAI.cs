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
    int currWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currWaypoint = -1;

        setNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!nav.pathPending && nav.remainingDistance <= 2)
        {
            setNextWaypoint();
        }
    }

    private void setNextWaypoint()
    {
        if (waypoints.Length > 0)
        {
            if (currWaypoint == waypoints.Length - 1)
            {
                currWaypoint = 0;
            }
            else
            {
                currWaypoint = currWaypoint + 1;
            }

            nav.SetDestination(waypoints[currWaypoint].transform.position);
        }
       
    }
}
