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

        NavMeshAgent agent;

        Vector3 lookTarget;
        Vector2 v2Temp;
        Vector3 v3Temp;

        public Transform cameraLookTarget;
        public GameObject projectilePrefab;
        public Transform projectileSource;


        private void Awake()
        {
            playerInput = GetComponent<IGiveInput>();
            agent = GetComponent<NavMeshAgent>();

            playerInput.OnFireStart += Shoot;
        }

        void Update()
        {
            PlayerLookAt();
            MovePlayer();

            v3Temp = (lookTarget - transform.position) * 0.2f;
            cameraLookTarget.position = transform.position + v3Temp;
        }

        void PlayerLookAt()
        {
            v2Temp = playerInput.GetLookTarget();
            lookTarget = new Vector3(v2Temp.x, 0, v2Temp.y);

            transform.LookAt(lookTarget);

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