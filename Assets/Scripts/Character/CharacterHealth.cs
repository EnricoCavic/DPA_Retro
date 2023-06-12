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
        public Action<GameObject> onCharacterDied;

        private void Start()
        {
            ResetHP();
        }

        public void ResetHP()
        {
            currentHp = attributeData.maxHealth;
        }

        public bool TakeDamage(int _dmg)
        {
            currentHp -= _dmg;
            if (currentHp <= 0)
            {
                //Destroy(gameObject);
                onCharacterDied?.Invoke(gameObject);
                onCharacterDied = null;
                return true;
            }

            return false;
        }

    }
}