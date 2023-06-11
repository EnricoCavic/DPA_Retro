using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Retro.Character;

public interface ISpawner
{
    public int maxSpawnCicle { get; set; }
    public int spawnRoundQuantity { get; set; }
    public float interval { get; set; }
    public float radialRandomness { get; set; }
    public List<CharacterAttributesSO> characterAttributes { get; set; }
    public GameObject prefab { get; set; }


}
