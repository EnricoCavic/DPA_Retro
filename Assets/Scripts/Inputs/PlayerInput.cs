using UnityEngine;
using Retro.Managers;
using System;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace Retro.Character.Input
{
    public class PlayerInput : MonoBehaviour, IGiveInput, IGetHit
    {
        // criar rotina de volta no tempo e suas condições, retorno e funcionalidades
        enum PlayerRoutine { None, Moving, HitStun }
        private PlayerRoutine currentRoutine = PlayerRoutine.None;
        
        private PlayerActions inputActions;
        private Camera mainCam;

        private Vector2 mousePosition;
        private Ray screenToRay;
        private Vector3 moveTarget;

        public Action OnFireStart { get; set; }
        public Action OnFireCanceled { get; set; }

        public Action OnMoveStart { get; set; }
        public Action OnMoveCanceled { get; set; }

        private void Awake()
        {
            inputActions = InputManager.Instance.inputActions;
            mainCam = Camera.main;
        }

        private void Update() => HandleRoutines();
        private void HandleRoutines()
        {
            switch (currentRoutine)
            {
                case PlayerRoutine.Moving:
                    moveTarget = inputActions.Gameplay.Move.ReadValue<Vector2>();
                    break;

                case PlayerRoutine.HitStun:
                    moveTarget = Vector3.zero;
                    break;
            }
        }

        private void OnEnable()
        {
            inputActions.Gameplay.Fire.performed += FirePerformed;
            inputActions.Gameplay.Fire.canceled += FireCanceled;

            inputActions.Gameplay.Move.performed += MovePerformed;
            inputActions.Gameplay.Move.canceled += MoveCanceled;
        }

        private void OnDisable()
        {
            inputActions.Gameplay.Fire.performed -= FirePerformed;
            inputActions.Gameplay.Fire.canceled -= FireCanceled;

            inputActions.Gameplay.Move.performed -= MovePerformed;
            inputActions.Gameplay.Move.canceled -= MoveCanceled;
        }

        private void FirePerformed(InputAction.CallbackContext obj) => OnFireStart?.Invoke();
        private void FireCanceled(InputAction.CallbackContext obj) => OnFireCanceled?.Invoke();  
        
        private void MovePerformed(InputAction.CallbackContext obj) => OnMoveStart?.Invoke();
        private void MoveCanceled(InputAction.CallbackContext obj) => OnMoveCanceled?.Invoke();

        public Vector3 GetLookTarget()
        {
            mousePosition = inputActions.Gameplay.MousePosition.ReadValue<Vector2>();
            screenToRay = mainCam.ScreenPointToRay(mousePosition);

            if (!Physics.Raycast(screenToRay, out RaycastHit hit)) return Vector2.zero;
            return hit.point;

        }

        public Vector3 GetMoveTarget()
        {
            return transform.position + new Vector3(moveTarget.x, 0f, moveTarget.y);
        }

        public void HandleHit()
        {
            currentRoutine = PlayerRoutine.HitStun;

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