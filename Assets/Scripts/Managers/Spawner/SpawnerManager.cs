using Retro.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour, ISpawner
{
    
    [field: SerializeField] public int spawnCount { get; set; }
    [field: SerializeField] public float interval { get; set; }
    [field: SerializeField] public float radialRandomness { get; set; }
    [field: SerializeField] public CharacterAttributesSO characterAttributes { get; set; }

    public List<SpawnerConfigsSO> spawnerConfigs;

    

}
