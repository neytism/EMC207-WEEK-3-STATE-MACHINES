using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vector2 = System.Numerics.Vector2;

public class PatrolState : StateMachineBehaviour
{
    private NavMeshAgent a;
    private AIBehavior b;

    private const string IsPatrol = "IsPatrol";
    private const string IsChase = "IsChase";
    
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        a = animator.GetComponent<NavMeshAgent>();
        b = animator.GetComponent<AIBehavior>();
        
        a.speed = b.patrolSpeed;
        
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToPlayer = Vector3.Distance(a.transform.position, b.player.transform.position);

        if (distanceToPlayer < b.agroDistance)
        {
            animator.SetBool(IsPatrol, false);
            animator.SetBool(IsChase, true);
            return;
        }
        
        if (!b.navMeshAgent.pathPending)
        {
            if (b.navMeshAgent.remainingDistance <= b.navMeshAgent.stoppingDistance)
            {
                if (!b.navMeshAgent.hasPath || b.navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    b.MoveToRandomWaypoint();
                }
            }
        }
    }
    
}
