using UnityEngine;
using Retro.Managers;
using System;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace Retro.Character.Input
{
    public class PlayerInput : MonoBehaviour, IGiveInput
    {       
        private PlayerActions inputActions;
        private Camera mainCam;

        private Vector2 mousePosition;
        private Ray screenToRay;
        private Vector3 moveTarget;
        private Vector3 lookTarget;

        public Action OnFireStart { get; set; }
        public Action OnFireCanceled { get; set; }

        public Action OnSpecialStart { get; set; }

        public Action OnMoveStart { get; set; }
        public Action OnMoveCanceled { get; set; }


        private void OnEnable()
        {
            inputActions = InputManager.Instance.inputActions;
            mainCam = Camera.main;

            inputActions.Gameplay.Fire.performed += FirePerformed;
            inputActions.Gameplay.Fire.canceled += FireCanceled;

            inputActions.Gameplay.Special.performed += SpecialPerformed;

            inputActions.Gameplay.Move.performed += MovePerformed;
            inputActions.Gameplay.Move.canceled += MoveCanceled;
        }

        private void OnDisable()
        {
            inputActions.Gameplay.Fire.performed -= FirePerformed;
            inputActions.Gameplay.Fire.canceled -= FireCanceled;

            inputActions.Gameplay.Special.performed -= SpecialPerformed;

            inputActions.Gameplay.Move.performed -= MovePerformed;
            inputActions.Gameplay.Move.canceled -= MoveCanceled;
        }

        private void SpecialPerformed(InputAction.CallbackContext obj) => OnSpecialStart?.Invoke();

        private void FirePerformed(InputAction.CallbackContext obj) => OnFireStart?.Invoke();
        private void FireCanceled(InputAction.CallbackContext obj) => OnFireCanceled?.Invoke();  
        
        private void MovePerformed(InputAction.CallbackContext obj) => OnMoveStart?.Invoke();
        private void MoveCanceled(InputAction.CallbackContext obj) => OnMoveCanceled?.Invoke();

        public Vector3 GetLookTarget()
        {
            mousePosition = inputActions.Gameplay.MousePosition.ReadValue<Vector2>();
            screenToRay = mainCam.ScreenPointToRay(mousePosition);

            if (!Physics.Raycast(screenToRay, out RaycastHit hit)) return Vector2.zero;
            lookTarget = hit.point;
            lookTarget.y = 0f;
            return lookTarget;

        }

        public Vector3 GetMoveTarget()
        {
            moveTarget = inputActions.Gameplay.Move.ReadValue<Vector2>();
            return transform.position + new Vector3(moveTarget.x, 0f, moveTarget.y);
        }

    }
}