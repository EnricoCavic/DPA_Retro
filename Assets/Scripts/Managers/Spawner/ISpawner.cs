using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Retro.Character;
using Retro.Gameplay;

public interface ISpawner
{
    public int maxSpawnCicle { get; set; }
    public int spawnRoundQuantity { get; set; }
    public float interval { get; set; }
    public float radialRandomness { get; set; }
    public GameObject prefab { get; set; }
    public List<SpawnableConfigsSO> spawnableConfigs { get; set; }



}
