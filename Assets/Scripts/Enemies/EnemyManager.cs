using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Transform attackTarget;
    public bool stopFollowing;
    public bool stopAttacking;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FollowTarget();               
    }

    void FollowTarget() 
    {
        if (stopFollowing) return;
        if (attackTarget == null) return;

        agent.SetDestination(attackTarget.position);
    }

    void MeleeAttackTarget() 
    {
        if (stopAttacking) return;
        if (attackTarget == null) return;
    }


}
