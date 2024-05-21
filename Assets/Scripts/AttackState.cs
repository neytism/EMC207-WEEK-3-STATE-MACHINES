using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateMachineBehaviour
{
    
    private NavMeshAgent a;
    private AIBehavior b;

    private const string IsPatrol = "IsPatrol";
    private const string IsChase = "IsChase";
    private const string IsAttack = "IsAttack";
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        a = animator.GetComponent<NavMeshAgent>();
        b = animator.GetComponent<AIBehavior>();
        a.speed = 0f;
    }
    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      float distanceToPlayer = Vector3.Distance(a.transform.position, b.player.transform.position);
        
        if (distanceToPlayer >= b.attackDistance)
        {   
            animator.SetBool(IsPatrol, false);
            animator.SetBool(IsChase, true);
            animator.SetBool(IsAttack, false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
