using UnityEngine;
using Retro.Character.Input;
using UnityEngine.AI;

namespace Retro.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [HideInInspector] public CharacterAttributesSO attributeData;
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponentInChildren<NavMeshAgent>();
        }

        public void PlayerLookAt(Vector3 _lookTarget)
        {
            transform.LookAt(_lookTarget);

            var rotTarget = new Vector3(0, transform.eulerAngles.y, 0);
            transform.eulerAngles = rotTarget;
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