using UnityEngine;
using Retro.Managers;
using System;
using UnityEngine.InputSystem;

namespace Retro.Character.Input
{
    public class PlayerInput : MonoBehaviour, IGiveInput
    {
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

        public Vector3 GetMoveTarget(Vector3 _currentPosition)
        {
            moveTarget = inputActions.Gameplay.Move.ReadValue<Vector2>();
            return _currentPosition + new Vector3(moveTarget.x, 0f, moveTarget.y);
        }


    }
}