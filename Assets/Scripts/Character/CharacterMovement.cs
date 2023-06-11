using UnityEngine;
using Retro.Character.Input;
using UnityEngine.AI;

namespace Retro.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [HideInInspector] public CharacterAttributesSO attributeData;
        private NavMeshAgent agent;
        public float currentMovementSpeed => agent.velocity.normalized.magnitude;

        Vector3 lookDirection;
        Quaternion lookRotation;

        private void Awake()
        {
            agent = GetComponentInChildren<NavMeshAgent>();
        }

        public void PlayerLookAt(Vector3 _lookTarget)
        {
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