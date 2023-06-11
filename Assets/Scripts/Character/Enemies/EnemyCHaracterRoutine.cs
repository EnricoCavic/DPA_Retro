using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    public class EnemyCHaracterRoutine : CharacterManager, IGetHit
    {
        public Transform attackTarget;

        public EnemyRoutineDataSO routineData;

        public enum EnemyRoutine { None, Chasing, Attaking, HitStun }
        public EnemyRoutine currentRoutine = EnemyRoutine.None;

        private float distanceToTarget;
        private float currentFireInterval;

        private void Start()
        {
            currentRoutine = EnemyRoutine.Chasing;
            var inputs = (EnemyInput)inputHandler;
            inputs.attackTarget = attackTarget;
        }

        private void OnEnable()
        {
            if (!init) Initialize();
        }

        private void Update()
        {
            movement.PlayerLookAt(inputHandler.GetLookTarget());
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
            }
        }


        private void FollowRoutine()
        {
            if (distanceToTarget <= routineData.attackDistance)
            {
                currentRoutine = EnemyRoutine.Attaking;
                return;
            }

            movement.MovePlayer(inputHandler.GetMoveTarget());
        }

        private void FireRoutine()
        {
            if (distanceToTarget > routineData.attackDistance)
            {
                currentRoutine = EnemyRoutine.Chasing;
                return;
            }

            movement.Stop();
            if (currentFireInterval >= routineData.fireInterval)
            {
                actions.Fire();
                currentFireInterval = 0f;
            }
        }

        public void HandleHit(int _dmg)
        {
            currentRoutine = EnemyRoutine.HitStun;
            health.TakeDamage(_dmg);

            Sequence mySequence = DOTween.Sequence();
            mySequence.Insert(0, transform
                .DOPunchPosition
                (
                punch: transform.forward,
                duration: 0.2f,
                vibrato: 1,
                elasticity: 1
                ))
                .Insert(0,
                    transform
                        .DOShakePosition
                        (
                            duration: 0.2f,
                            strength: 1f,
                            vibrato: 1,
                            randomness: 5f
                        )
                ).OnComplete(() => currentRoutine = EnemyRoutine.Chasing);
        }
    }
}