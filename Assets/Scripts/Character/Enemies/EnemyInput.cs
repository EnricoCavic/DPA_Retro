using UnityEngine;
using UnityEngine.AI;
using Retro.Character.Input;
using System;

using DG.Tweening;

namespace Retro.Character
{
    public class EnemyInput : MonoBehaviour, IGiveInput
    {
        [HideInInspector] public Transform attackTarget;

        Vector3 lookTarget;

        public Action OnFireStart { get; set; }
        public Action OnFireCanceled { get; set; }

        public Action OnMoveStart { get; set; }
        public Action OnMoveCanceled { get; set; }

        public Vector3 GetMoveTarget() => attackTarget.position;
        public Vector3 GetLookTarget()
        {
            if (attackTarget == null) return Vector3.zero;

            lookTarget = attackTarget.position;
            lookTarget.y = 0f;
            return lookTarget;
        }
    }
}