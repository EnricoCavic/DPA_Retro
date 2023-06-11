using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawner Configs", menuName = "Retro/Gameplay/Spawner/Configs")]
public class SpawnerConfigsSO : ScriptableObject, ISpawner
{
    public bool overrideParent;
    [field: SerializeField] public int spawnCount { get; set; }
    [field: SerializeField] public float interval { get; set; }
    [field: SerializeField] public float radialRandomness { get; set; }

}


