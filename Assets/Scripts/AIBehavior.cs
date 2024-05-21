using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBehavior : MonoBehaviour
{
    public static event Action AttackPlayerEvent; 
    
    public NavMeshAgent navMeshAgent;
    public Transform waypointHolder;
    public List<Transform> waypoints = new List<Transform>();
    public int currentWaypointID;

    public float agroDistance;
    public float attackDistance;
    public float loseDistance;

    public PlayerController player;
    

    public float patrolSpeed = 0.5f;
    public float chaseSpeed = 3;
    
    
    private void Awake()
    {
        Setup();    
    }
    private void Setup()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.autoTraverseOffMeshLink = false;
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
            navMeshAgent.SetDestination(waypoints[currentWaypointID].position);
        }
    }

    private int GetRandomInt()
    {
        return Random.Range(0, waypoints.Count);
    }
    
    public void AttackPlayer()
    {
        AttackPlayerEvent?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, agroDistance);
        
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, attackDistance);
    }
}