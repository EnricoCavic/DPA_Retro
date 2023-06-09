using Retro.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Retro.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public PlayerActions inputActions;

        private void Awake()
        {
            if (!InstanceSetup(this)) return;

            inputActions = new();
            inputActions.Enable();
        }
    }
}