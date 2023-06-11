using Retro.Character.Input;
using UnityEngine;

namespace Retro.Character
{
    public class MouseCameraFollow : MonoBehaviour
    {
        public Transform cameraLookTarget;
        public float distanceMultiplier = 0.2f;

        private PlayerInput playerInput;
        Vector3 scaledDirection;

        private void Start() => playerInput = GetComponent<PlayerInput>();

        private void Update()
        {
            scaledDirection = (playerInput.GetLookTarget() - transform.position) * distanceMultiplier;
            cameraLookTarget.position = transform.position + scaledDirection;
        }
    }
}