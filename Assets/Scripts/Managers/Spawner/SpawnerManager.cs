using Retro.Character;
using Retro.Gameplay;
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
    [field: SerializeField] public List<CharacterAttributesSO> characterAttributes { get; set; }
    [field: SerializeField] public List<EnemyRoutineDataSO> characterRoutine { get; set; }
    [field: SerializeField] public List<ProjectileDataSO> projectile { get; set; }

    [Space(15)]

    [Header("Spawner")]
    [Space(7.5f)]
    public List<SpawnerConfigsSO> spawnerConfigs;

    public List<List<EnemyCHaracterRoutine>> allSpawned;

    public Transform player;

    [ContextMenu("Spawn")]
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
        for(int a = 0; a < spawnConfigs.maxSpawnCicle; a++)
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


                var toBeSpawned = spawnConfigs.prefab;
                var enemyRoutine = toBeSpawned.GetComponent<EnemyCHaracterRoutine>();
                
                enemyRoutine.routineData = spawnConfigs.characterRoutine[0];
                enemyRoutine.projectileData = spawnConfigs.projectile[0];
                enemyRoutine.attributeData = spawnConfigs.characterAttributes[0];

                EnemyCHaracterRoutine spawned = Instantiate(toBeSpawned, spawnPosition, transform.rotation).GetComponent<EnemyCHaracterRoutine>();
                spawned.attackTarget = player;
                spawnRound.Add(spawned);
            }

            allSpawned.Add(spawnRound);


            yield return new WaitForSeconds(spawnConfigs.interval);
        }
    }

    

}
