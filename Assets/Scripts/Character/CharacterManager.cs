using Retro.Character.Input;
using Retro.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    public class CharacterManager : MonoBehaviour
    {
        public IGiveInput inputHandler;
        [HideInInspector] public CharacterMovement movement;
        [HideInInspector] public CharacterActions actions;
        [HideInInspector] public CharacterHealth health;
        [HideInInspector] public CharacterAnimations animations;
        [HideInInspector] public CapsuleCollider capsuleCollider;

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
            animations = GetComponent<CharacterAnimations>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            movement.attributeData = attributeData;
            actions.projectileData = projectileData;
            health.attributeData = attributeData;

        }

    }
}