using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Retro.Character.Input;
using System;

public class EnemyInput : MonoBehaviour, IGiveInput
{
    public float attackDistance = 10f;

    public Transform attackTarget;
    private float distanceToTarget;
    private bool stopFollowing;
    private bool stopAttacking;

    public Action OnFireStart { get; set; }
    public Action OnFireCanceled { get; set; }

    public Action OnMoveStart { get; set; }
    public Action OnMoveCanceled { get; set; }

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(attackTarget.position, transform.position);
        if (distanceToTarget <= attackDistance)
        {
            stopFollowing = true;
        }
        else
        {
            stopFollowing = false;
        }
    }

    public Vector3 GetMoveTarget(Vector3 _currentPosition)
    {
        if (stopFollowing)
        {
            agent.ResetPath();
            return _currentPosition;
        }

        return attackTarget.position;
    }

    public Vector3 GetLookTarget()
    {
        return attackTarget.position;
    }
}
