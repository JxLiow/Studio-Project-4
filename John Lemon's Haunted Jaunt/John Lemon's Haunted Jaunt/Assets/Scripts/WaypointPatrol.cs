using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public List<Transform> waypoints = new List<Transform>();

    int m_CurrentWaypointIndex;

    public void StartAI()
    {
        enabled = true;
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update ()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && waypoints.Count > 0)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Count;
            navMeshAgent.SetDestination (waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
