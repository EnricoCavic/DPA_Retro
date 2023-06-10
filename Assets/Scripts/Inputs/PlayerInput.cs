using System.Collections;
using System.Collections.Generic;
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


        private void Awake()
        {
            inputActions = InputManager.Instance.inputActions;
            mainCam = Camera.main;
        }

        private void OnEnable()
        {
            inputActions.Gameplay.Fire.performed += FirePerformed;
            inputActions.Gameplay.Fire.canceled += FireCanceled;
        }

        private void OnDisable()
        {
            inputActions.Gameplay.Fire.performed -= FirePerformed;
            inputActions.Gameplay.Fire.canceled -= FireCanceled;
        }

        private void FirePerformed(InputAction.CallbackContext obj) => OnFireStart?.Invoke();
        private void FireCanceled(InputAction.CallbackContext obj) => OnFireCanceled?.Invoke();

        public Vector2 GetLookTarget()
        {
            mousePosition = inputActions.Gameplay.MousePosition.ReadValue<Vector2>();
            screenToRay = mainCam.ScreenPointToRay(mousePosition);

            if (!Physics.Raycast(screenToRay, out RaycastHit hit)) return Vector2.zero;
            return new Vector2(hit.point.x, hit.point.z);

        }

        public Vector3 GetMoveTarget(Vector3 _currentPosition)
        {
            moveTarget = inputActions.Gameplay.Move.ReadValue<Vector2>();
            return _currentPosition + new Vector3(moveTarget.x, 0f, moveTarget.y);
        }


    }
}