using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    public class PlayerCharacterRoutine : CharacterManager, IGetHit
    {
        public enum PlayerRoutine { None, Moving, HitStun, TimeRetro }
        public PlayerRoutine currentRoutine = PlayerRoutine.None;


        private void OnEnable()
        {
            if (!init) Initialize();
            inputHandler.OnFireStart += actions.Fire;
        }
        private void Start()
        {
            currentRoutine = PlayerRoutine.Moving;
        }

        private void OnDisable()
        {
            inputHandler.OnFireStart -= actions.Fire;
        }

        private void Update()
        {
            movement.PlayerLookAt(inputHandler.GetLookTarget());
            switch(currentRoutine)
            {
                case PlayerRoutine.Moving:
                    movement.MovePlayer(inputHandler.GetMoveTarget());
                    break;
            }
        }

        public void HandleHit(int _dmg)
        {
            currentRoutine = PlayerRoutine.HitStun;
            attributes.TakeDamage(_dmg);

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
                ).OnComplete(() => currentRoutine = PlayerRoutine.Moving);
        }
    }
}