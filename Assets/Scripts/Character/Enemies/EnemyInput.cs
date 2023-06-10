using UnityEngine;
using UnityEngine.AI;
using Retro.Character.Input;
using System;

namespace Retro.Character
{
    public class EnemyInput : MonoBehaviour, IGiveInput
    {
        enum EnemyRoutine { None, Chasing, Attaking }
        private EnemyRoutine currentRoutine = EnemyRoutine.None;

        public EnemyRoutineDataSO data;

        public Transform attackTarget;
        private Vector3 movePosition;

        private float distanceToTarget;
        private float currentFireInterval;

        public Action OnFireStart { get; set; }
        public Action OnFireCanceled { get; set; }

        public Action OnMoveStart { get; set; }
        public Action OnMoveCanceled { get; set; }

        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            movePosition = new();
            currentRoutine = EnemyRoutine.Chasing;
        }

        private void Update()
        {
            distanceToTarget = Vector3.Distance(attackTarget.position, transform.position);
            currentFireInterval += Time.deltaTime;

            switch (currentRoutine)
            {
                case EnemyRoutine.Chasing:
                    FollowRoutine();
                    break;

                case EnemyRoutine.Attaking:
                    FireRoutine();
                    break;

                case EnemyRoutine.None:
                    return;
            }

        }

        private void FollowRoutine()
        {
            if (distanceToTarget <= data.attackDistance)
            {
                currentRoutine = EnemyRoutine.Attaking;
                return;
            }

            movePosition = attackTarget.position;

        }

        private void FireRoutine()
        {
            if (distanceToTarget > data.attackDistance)
            {
                currentRoutine = EnemyRoutine.Chasing;
                return;
            }

            agent.ResetPath();
            movePosition = transform.position;
            if (currentFireInterval >= data.fireInterval)
            {
                OnFireStart?.Invoke();
                currentFireInterval = 0f;
            }
        }

        public Vector3 GetMoveTarget() => movePosition;

        public Vector3 GetLookTarget() => attackTarget.position;
    }
}