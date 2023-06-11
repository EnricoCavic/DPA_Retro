using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawner Set", menuName = "Retro/Gameplay/Spawner/Set")]
public class SpawnerSetSO : ScriptableObject, ISpawner
{
    [Header("Override configs")]
    public bool overrideAll;

    [field: SerializeField] public int spawnCount { get; set; }
    [field: SerializeField] public float interval { get; set; }
    [field: SerializeField] public float radialRandomness { get; set; }

    [Header("Sets")]
    public List<SpawnerConfigsSO> spawners;

}
