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
        [HideInInspector] public CharacterHealth health;

        [SerializeField] private ProjectileDataSO projectileData;
        [SerializeField] private CharacterAttributesSO attributeData;

        [HideInInspector] public bool init = false;
        public virtual void Initialize()
        {
            init = true;
            inputHandler = GetComponent<IGiveInput>();
            movement = GetComponent<CharacterMovement>();
            actions = GetComponent<CharacterActions>();
            health = GetComponent<CharacterHealth>();

            movement.attributeData = attributeData;
            actions.projectileData = projectileData;
            health.attributeData = attributeData;

        }

    }
}