using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AlienAI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] waypoints;
    private int currentWaypoint = -1;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //anim = GetComponent<Animator>();
        SetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.remainingDistance < 0.5f && !navMeshAgent.pathPending) {
                    SetNextWaypoint();
        }
    }
    private void SetNextWaypoint() {
        currentWaypoint++;
        if (currentWaypoint >= waypoints.Length) {
            currentWaypoint = 0;
        }
        if (waypoints.Length > 0) {
            navMeshAgent.SetDestination(waypoints[currentWaypoint].transform.position);
        } else {
            Debug.Log("No waypoints found");   
        }
    }
}
