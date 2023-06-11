using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    public class CharacterAnimations : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void SetMoveSpeed(float _charMoveSpeed)
        {
            // altera a variavel de animação de movimentação
            // variavel altera entre as anim idle e movendo
        }

        public void AttackAnimation()
        {
            // altera a variavel de trigger de ataque

        }

        public void DeathAnimation()
        {
            // altera a variavel de trigger de ataque
        }

    }
}