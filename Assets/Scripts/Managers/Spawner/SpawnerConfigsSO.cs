using Retro.Character;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawner Configs", menuName = "Retro/Gameplay/Spawner/Configs")]
public class SpawnerConfigsSO : ScriptableObject, ISpawner
{
    public bool parentOverride;
    [field: SerializeField] public int maxSpawnCicle { get; set; }
    [field: SerializeField]  public int spawnRoundQuantity { get; set; }
    [field: SerializeField] public float interval { get; set; }
    [field: SerializeField] [field: Range(0, 100)] public float radialRandomness { get; set; }
    [field: SerializeField] public GameObject prefab { get; set; }
    [field: SerializeField] public int maxSpawn { get; set; }
    [field: SerializeField] List<CharacterAttributesSO> ISpawner.characterAttributes { get; set; }
}


