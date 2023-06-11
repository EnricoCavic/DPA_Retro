using Retro.Character.Input;
using UnityEngine;
using Cinemachine;

namespace Retro.Character
{
    public class MouseCameraFollow : MonoBehaviour
    {
        public Transform cameraLookTarget;
        public float distanceMultiplier = 0.2f;

        private CinemachineVirtualCamera virtualCamera;
        private PlayerInput playerInput;
        Vector3 scaledDirection;

        private void Start()
        {
            virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            virtualCamera.m_Follow = transform;
            playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            scaledDirection = (playerInput.GetLookTarget() - transform.position) * distanceMultiplier;
            cameraLookTarget.position = transform.position + scaledDirection;
        }
    }
}