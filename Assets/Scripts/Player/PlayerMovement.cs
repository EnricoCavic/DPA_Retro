using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Retro.Managers;
using Retro.Character.Input;

namespace Retro.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        IGiveInput playerInput;

        Vector2 lookTarget;


        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        void Update()
        {
            PlayerLookAt();

        }

        void PlayerLookAt()
        {
            lookTarget = playerInput.GetLookTarget();

            transform.LookAt(new Vector3(lookTarget.x, 0, lookTarget.y));

            var rotTarget = new Vector3(0, transform.eulerAngles.y, 0);
            transform.eulerAngles = rotTarget;
        }

        void MovePlayer()
        {

        }



    }
}