using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public ParticleSystem particleSystem;
  
    [SerializeField] private Camera Camera = null;
   /// [SerializeField] private Transform arrow = null;
    private NavMeshAgent _agent;
    private Animator _anim;

    private void Awake()
    {
        Setup();
    }

    private void OnEnable()
    {
        AIBehavior.AttackPlayerEvent += DamagePlayer;
    }
    
    private void OnDisable()
    {
        AIBehavior.AttackPlayerEvent -= DamagePlayer;
    }

    private void Setup()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoTraverseOffMeshLink = false;
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _agent.SetDestination(hit.point);
                _anim.SetBool("isMove", true);
               // arrow.transform.position = hit.point;
            }
        }
        
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    _anim.SetBool("isMove", false);
                }
            }
        }


    }

    public void DamagePlayer()
    {
        particleSystem.Play();
    }
}