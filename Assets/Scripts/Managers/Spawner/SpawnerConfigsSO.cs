using Retro.Character;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawner Configs", menuName = "Retro/Gameplay/Spawner/Configs")]
public class SpawnerConfigsSO : ScriptableObject, ISpawner
{
    public bool parentOverride;
    [field: SerializeField] public int spawnCount { get; set; }
    [field: SerializeField] public float interval { get; set; }
    [field: SerializeField] public float radialRandomness { get; set; }
    [field: SerializeField] public CharacterAttributesSO characterAttributes { get; set; }

    public GameObject prefab { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    List<CharacterAttributesSO> ISpawner.characterAttributes { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}


