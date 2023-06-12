using DG.Tweening;
using Retro.Character.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    public enum PlayerRoutine { None, Moving, HitStun, TimeRetro }

    public class PlayerCharacterRoutine : CharacterManager, IGetHit
    {
        public PlayerRoutine currentRoutine = PlayerRoutine.None;

        private TimeRetro timeRetro;
        private PlayerInput playerInput;
        private Sequence mySequence;

        public float rewindCooldown = 2f;
        public float currentRewindCooldown;

        public override void Initialize()
        {
            base.Initialize();
            timeRetro = GetComponent<TimeRetro>();
            playerInput = (PlayerInput)inputHandler;
        }

        private void OnEnable()
        {
            if (!init) Initialize();
            currentRewindCooldown = rewindCooldown;
            inputHandler.OnFireStart += FireInput;
            playerInput.OnSpecialStart += RewindInput;
        }
        private void Start()
        {
            currentRoutine = PlayerRoutine.Moving;
        }

        private void OnDisable()
        {
            inputHandler.OnFireStart -= FireInput;
            playerInput.OnSpecialStart -= RewindInput;
        }

        private void Update()
        {
            currentRewindCooldown += Time.deltaTime;
            if (currentRewindCooldown >= rewindCooldown)
                currentRewindCooldown = rewindCooldown;

            switch(currentRoutine)
            {
                case PlayerRoutine.Moving:
                    movement.PlayerLookAt(inputHandler.GetLookTarget());
                    movement.MovePlayer(inputHandler.GetMoveTarget());
                    animations.SetMoveSpeed(movement.currentMovementSpeed);

                    break;
            }
        }

        public void FireInput()
        {
            if (currentRoutine != PlayerRoutine.Moving) return;

            animations.AttackAnimation();
            actions.Fire();
        }

        public void RewindInput()
        {
            if (currentRoutine != PlayerRoutine.Moving) return;
            if (currentRewindCooldown < rewindCooldown) return;

            currentRewindCooldown = 0f;
            capsuleCollider.enabled = false;
            movement.Stop();
            timeRetro.Rewind();
        }

        public bool HandleHit(int _dmg, Vector3 _direction)
        {
            if (currentRoutine == PlayerRoutine.TimeRetro)
                return false;

            currentRoutine = PlayerRoutine.HitStun;
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
                ).OnComplete(() => currentRoutine = PlayerRoutine.Moving);
            return true;
        }
    }
}