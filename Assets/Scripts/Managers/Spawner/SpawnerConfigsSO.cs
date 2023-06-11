using System.Collections.Generic;
using UnityEngine;

using Retro.Character;
using Retro.Gameplay;

[CreateAssetMenu(fileName = "New Spawner Configs", menuName = "Retro/Gameplay/Spawner/Configs")]
public class SpawnerConfigsSO : ScriptableObject, ISpawner
{
    public bool parentOverride;
    [field: SerializeField] public int maxSpawnCicle { get; set; }
    [field: SerializeField]  public int spawnRoundQuantity { get; set; }
    [field: SerializeField] public float interval { get; set; }
    [field: SerializeField] [field: Range(0, 100)] public float radialRandomness { get; set; }
    [field: SerializeField] public GameObject prefab { get; set; }
    [field: SerializeField] public List<CharacterAttributesSO> characterAttributes { get; set; }
    [field: SerializeField] public List<EnemyRoutineDataSO> characterRoutine { get; set; }
    [field: SerializeField] public List<ProjectileDataSO> projectile { get; set; }
}


