using Retro.Character.Input;
using Retro.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    public class CharacterManager : MonoBehaviour
    {
        public IGiveInput inputHandler { get; private set; }

        private CharacterMovement movement;
        private CharacterActions actions;

        [SerializeField] private ProjectileDataSO projectileData;

        private void Awake()
        {
            inputHandler = GetComponent<IGiveInput>();
            movement = GetComponent<CharacterMovement>();

            actions = GetComponent<CharacterActions>();
            actions.projectileData = projectileData;
        }

        private void OnEnable()
        {
            inputHandler.OnFireStart += actions.Fire;
        }

        private void OnDisable()
        {
            inputHandler.OnFireStart -= actions.Fire;
        }

        private void Update()
        {
            movement.MovePlayer(inputHandler.GetMoveTarget(transform.position));
            movement.PlayerLookAt(inputHandler.GetLookTarget());
        }
    }
}