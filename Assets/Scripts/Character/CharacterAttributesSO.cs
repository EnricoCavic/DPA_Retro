using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Character
{
    [CreateAssetMenu(fileName = "New Character Attributes", menuName = "Retro/Gameplay/Character Attributes")]
    public class CharacterAttributesSO : ScriptableObject
    {
        public int maxHealth;
        public int speed;
        public int acceleration;
    }
}