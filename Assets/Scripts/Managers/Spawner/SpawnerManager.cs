using Retro.Character;
using Retro.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Managers
{
    public class SpawnerManager : MonoBehaviour, ISpawner
    {
        [Header("Global")]
        [Space(10)]
        public bool overrideAllconfigs;
        private GameplayManager gameplayManager;

        [field: SerializeField] public int maxSpawnCicle { get; set; }
        [field: SerializeField] public int spawnRoundQuantity { get; set; }
        [field: SerializeField] public float interval { get; set; }
        [field: SerializeField] [field: Range(0, 100)] public float radialRandomness { get; set; }
        [field: SerializeField] public GameObject prefab { get; set; }
        [field: SerializeField] public List<SpawnableConfigsSO> spawnableConfigs { get; set; }


        [Space(15)]

        [Header("Spawner")]
        [Space(7.5f)]
        public List<SpawnerConfigsSO> spawnerConfigs;

        public List<List<EnemyCHaracterRoutine>> allSpawned = new();

        private void Start()
        {
            gameplayManager = GameplayManager.Instance;
        }

        [ContextMenu("Spawn")]
        public void Spawn()
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
            //// ciclo completo da rotina de spawn
            //for (int a = 0; a < spawnConfigs.maxSpawnCicle; a++)
            //{
            //    SpawnEnemies(spawnConfigs);
            //    // esperar os inimigos morreres pra começar o próximo ciclo
            //    yield return new WaitForSeconds(spawnConfigs.interval);
            //}

            yield return null;

            while(gameplayManager.spawnedPlayer != null)
            {
                SpawnEnemies(spawnConfigs);
                float randomWait = Random.Range(0, spawnConfigs.interval);
                yield return new WaitForSeconds(randomWait);
            }
        }

        private void SpawnEnemies(ISpawner spawnConfigs)
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

                int randomAttribute = Random.Range(0, spawnConfigs.spawnableConfigs.Count);
                enemyRoutine.routineData = spawnConfigs.spawnableConfigs[randomAttribute].characterRoutine;
                enemyRoutine.projectileData = spawnConfigs.spawnableConfigs[randomAttribute].projectile;
                enemyRoutine.attributeData = spawnConfigs.spawnableConfigs[randomAttribute].characterAttributes;


                EnemyCHaracterRoutine spawned = Instantiate(toBeSpawned, spawnPosition, transform.rotation).GetComponent<EnemyCHaracterRoutine>();
                spawned.health.onCharacterDied += () => gameplayManager.spawnedEnemies.Remove(spawned);
                spawned.SetAttackTarget(gameplayManager.spawnedPlayer);
                spawnRound.Add(spawned);
            }

            gameplayManager.spawnedEnemies.AddRange(spawnRound);
            allSpawned.Add(spawnRound);
        }


    }
}