using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    private NavMeshAgent _agent;
    private AIBehavior _behavior;

    private const string IsPatrol = "IsPatrol";
    private const string IsChase = "IsChase";
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
        _behavior = animator.GetComponent<AIBehavior>();
        
    }
}
