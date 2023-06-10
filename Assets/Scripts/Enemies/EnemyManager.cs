using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Retro.Character.Input;
using System;

public class EnemyManager : MonoBehaviour, IGiveInput
{
    public Transform attackTarget;
    public bool stopFollowing;
    public bool stopAttacking;

    private NavMeshAgent agent;

    public Action OnFireStart { get; set; }
    public Action OnFireCanceled { get; set; }
    public Action OnMoveStart { get; set; }
    public Action OnMoveCanceled { get; set; }

    public Vector3 GetMoveTarget(Vector3 _currentPosition)
    {
        return attackTarget.position;
    }

    public Vector3 GetLookTarget()
    {
        return attackTarget.position;
    }
}
