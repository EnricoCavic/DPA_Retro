using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Retro.Managers;
using Retro.Character.Input;
using UnityEngine.AI;

namespace Retro.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        IGiveInput playerInput;

        Vector2 lookTarget;
        NavMeshAgent agent;



        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            PlayerLookAt();
            MovePlayer();
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
            var target = playerInput.GetMoveTarget(transform.position);

            Debug.Log($"X: {target.x} | Y: {target.y} | Z: {target.z} ");
            agent.SetDestination(target);
        }



    }
}