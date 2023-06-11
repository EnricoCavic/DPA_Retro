using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Retro.Character;
using Retro.Gameplay;

[CreateAssetMenu(fileName = "New Spawner Configs", menuName = "Retro/Gameplay/Spawner/Spawnable Configs")]
public class SpawnableConfigsSO : ScriptableObject
{
    [field: SerializeField] public CharacterAttributesSO characterAttributes { get; set; }
    [field: SerializeField] public EnemyRoutineDataSO characterRoutine { get; set; }
    [field: SerializeField] public ProjectileDataSO projectile { get; set; }
}
