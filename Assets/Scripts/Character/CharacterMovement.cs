using UnityEngine;
using Retro.Character.Input;
using UnityEngine.AI;

namespace Retro.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [HideInInspector] public CharacterAttributesSO attributeData;
        private NavMeshAgent agent;

        Vector3 lookDirection;
        Quaternion lookRotation;

        private void Awake()
        {
            agent = GetComponentInChildren<NavMeshAgent>();
        }

        public void PlayerLookAt(Vector3 _lookTarget)
        {
            //transform.LookAt(_lookTarget);

            //var rotTarget = new Vector3(0, transform.eulerAngles.y, 0);
            //transform.eulerAngles = rotTarget;

            lookDirection = (_lookTarget - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * attributeData.rotationSpeed);
        }

        public void MovePlayer(Vector3 _moveTarget)
        {
            if (_moveTarget == transform.position) return;

            agent.speed = attributeData.speed;
            agent.acceleration = attributeData.acceleration;
            agent.SetDestination(_moveTarget);
        }

        public void Stop()
        {
                agent.ResetPath();
        }
    }
}