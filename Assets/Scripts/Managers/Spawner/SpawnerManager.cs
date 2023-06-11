using Retro.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour, ISpawner
{
    [Header("Global")]
    [Space(10)]
    public bool overrideAllconfigs;

    [field: SerializeField] public int maxSpawnCicle { get; set; }
    [field: SerializeField] public int spawnRoundQuantity { get; set; }
    [field: SerializeField] public float interval { get; set; }
    [field: SerializeField] [field: Range(0, 100)] public float radialRandomness { get; set; }
    [field: SerializeField] public GameObject prefab { get; set; }
    [field: SerializeField] List<CharacterAttributesSO> ISpawner.characterAttributes { get; set; }

    [Space(15)]

    [Header("Spawner")]
    [Space(7.5f)]
    public List<SpawnerConfigsSO> spawnerConfigs;

    public List<List<EnemyCHaracterRoutine>> allSpawned;

    void Spawn()
    {
        foreach (var s in spawnerConfigs)
        {
            if (overrideAllconfigs)
            {
                StartCoroutine(SpawnRoutine(this));
            }

            else
            {
                StartCoroutine(SpawnRoutine(s));
            }
        }
    }

    IEnumerator SpawnRoutine(ISpawner spawnConfigs)
    {
        List<EnemyCHaracterRoutine> spawnRound = new();

        Vector3 spawnPosition = transform.position;

        for (int i = 0; i < spawnConfigs.spawnRoundQuantity; i++)
        {
            //REVER
            if (radialRandomness != 0)
            {
                Random.Range(0f, radialRandomness);
                spawnPosition = new Vector3(spawnPosition.x, 0f, spawnPosition.z);
            }

            GameObject spawned = Instantiate(spawnConfigs.prefab, spawnPosition, transform.rotation);
            spawnRound.Add(spawned.GetComponent<EnemyCHaracterRoutine>());
        }

        allSpawned.Add(spawnRound);

        
        yield return new WaitForSeconds(spawnConfigs.interval);
    }

    

}
