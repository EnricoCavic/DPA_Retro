using UnityEngine;

namespace Retro.Character
{
    [CreateAssetMenu(fileName = "New Enemy Routine Data", menuName = "Retro/Gameplay/Enemy Routine")]
    public class EnemyRoutineDataSO : ScriptableObject
    {
        public float attackDistance = 10f;
        public float fireInterval = 0.5f;
    }
}