using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    public int spawnCount { get; set; }
    public float interval { get; set; }
    public float radialRandomness { get; set; }

}
