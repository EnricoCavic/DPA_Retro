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
        [HideInInspector] public CharacterMovement movement;
        [HideInInspector] public CharacterActions actions;
        [HideInInspector] public CharacterAttributes attributes;

        [SerializeField] private ProjectileDataSO projectileData;


        [HideInInspector] public bool init = false;
        public void Initialize()
        {
            init = true;
            inputHandler = GetComponent<IGiveInput>();
            movement = GetComponent<CharacterMovement>();
            actions = GetComponent<CharacterActions>();
            attributes = GetComponent<CharacterAttributes>();

            actions.projectileData = projectileData;
        }

    }
}