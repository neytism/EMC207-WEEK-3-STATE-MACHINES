using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBehavior : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform waypointHolder;
    public List<Transform> waypoints = new List<Transform>();
    public int currentWaypointID;
    
    private void Awake()
    {
        Setup();    
    }

    private void Update()
    {
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    MoveToRandomWaypoint();
                }
            }
        }
    }

    private void Setup()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoTraverseOffMeshLink = false;
        foreach (Transform child in waypointHolder)
        {
            waypoints.Add(child.gameObject.transform);
        }
        
    }
    
    public void MoveToRandomWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogWarning("No waypoints available.");
            return;
        }
        
        int newWaypointID = GetRandomInt();
        
        if (newWaypointID == currentWaypointID)
        {
            MoveToRandomWaypoint();
        }
        else
        {
            currentWaypointID = newWaypointID;
            _agent.SetDestination(waypoints[currentWaypointID].position);
        }
    }

    private int GetRandomInt()
    {
        return Random.Range(0, waypoints.Count);
    }
    
}