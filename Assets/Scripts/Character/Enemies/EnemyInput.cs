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

        public Action OnFireStart { get; set; }
        public Action OnFireCanceled { get; set; }

        public Action OnMoveStart { get; set; }
        public Action OnMoveCanceled { get; set; }

        public Vector3 GetMoveTarget() => attackTarget.position;
        public Vector3 GetLookTarget() => attackTarget.position;
    }
}