using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    public class CharacterHealth : MonoBehaviour
    {
        [HideInInspector] public CharacterAttributesSO attributeData;
        public int currentHp { get; private set; }

        private void Start()
        {
            currentHp = attributeData.maxHealth;
        }

        public void TakeDamage(int _dmg)
        {
            currentHp -= _dmg;
            if (currentHp <= 0)
                Destroy(gameObject);
        }
    }
}