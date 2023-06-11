using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    public class CharacterHealth : MonoBehaviour
    {
        [HideInInspector] public CharacterAttributesSO attributeData;
        public int currentHp { get; private set; }
        public Action onCharacterDied;

        private void Start()
        {
            currentHp = attributeData.maxHealth;
        }

        public bool TakeDamage(int _dmg)
        {
            currentHp -= _dmg;
            if (currentHp <= 0)
            {
                Destroy(gameObject);
                onCharacterDied?.Invoke();
                return true;
            }

            return false;
        }

        private void OnDestroy()
        {
            onCharacterDied = null;
        }
    }
}