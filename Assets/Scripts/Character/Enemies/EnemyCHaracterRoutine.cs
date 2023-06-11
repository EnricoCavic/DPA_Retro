using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    public enum EnemyRoutine { None, Chasing, Attaking, HitStun, GlobalRewind }

    public class EnemyCHaracterRoutine : CharacterManager, IGetHit
    {
        public Transform attackTarget;

        public EnemyRoutineDataSO routineData;

        public EnemyRoutine currentRoutine = EnemyRoutine.None;

        private float distanceToTarget;
        private float currentFireInterval;
        private Sequence mySequence;

        private void Start()
        {
            currentRoutine = EnemyRoutine.Chasing;
            var inputs = (EnemyInput)inputHandler;
            inputs.attackTarget = attackTarget;
        }

        public void SetAttackTarget(Transform _newTarget)
        {
            attackTarget = _newTarget;
            var inputs = (EnemyInput)inputHandler;
            inputs.attackTarget = attackTarget;
        }

        private void OnEnable()
        {
            if (!init) Initialize();
        }

        private void Update()
        {
            currentFireInterval += Time.deltaTime;
            if (attackTarget == null) return;

            distanceToTarget = Vector3.Distance(attackTarget.position, transform.position);
            animations.SetMoveSpeed(movement.currentMovementSpeed);

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

            movement.PlayerLookAt(inputHandler.GetLookTarget());
            movement.MovePlayer(inputHandler.GetMoveTarget());
        }

        private void FireRoutine()
        {
            if (distanceToTarget > routineData.attackDistance)
            {
                currentRoutine = EnemyRoutine.Chasing;
                return;
            }

            movement.PlayerLookAt(inputHandler.GetLookTarget());
            movement.Stop();
            if (currentFireInterval >= routineData.fireInterval)
            {
                actions.Fire();
                animations.AttackAnimation();
                currentFireInterval = 0f;
            }
        }

        public bool HandleHit(int _dmg, Vector3 _direction)
        {
            currentRoutine = EnemyRoutine.HitStun;

            mySequence.Kill();
            bool died = health.TakeDamage(_dmg);
            if (died) return true;

            mySequence = DOTween.Sequence();
            mySequence.Insert(0, transform
                .DOPunchPosition
                (
                punch: _direction,
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

            return true;
        }
    }
}