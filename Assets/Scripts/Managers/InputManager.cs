using Retro.Generic;

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