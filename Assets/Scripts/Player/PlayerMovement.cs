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

        public GameObject projectilePrefab;
        public Transform projectileSource;


        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            agent = GetComponent<NavMeshAgent>();

            playerInput.OnFireStart += Shoot;
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
            agent.SetDestination(target);
        }

        void Shoot()
        {
            var pool = ObjectPoolManager.Instance.AddNewPool(projectilePrefab);

            var projectile = pool.available.Get();

            projectile.transform.position = projectileSource.position;
            projectile.transform.rotation = projectileSource.rotation;
            projectile.gameObject.SetActive(true);
            projectile.GetComponent<Projectile>().myPool = pool.available;
        }


    }
}