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

        private ObjectPoolManager poolManager;

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
            poolManager = ObjectPoolManager.Instance;
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

            float randomWait;
            while (gameplayManager.spawnedPlayerPosition != null)
            {
                SpawnEnemies(spawnConfigs);
                randomWait = Random.Range(spawnConfigs.interval, spawnConfigs.interval * 2);
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

                var poolInstance = poolManager.GetPoolInstance(spawnConfigs.prefab);
                var obj = poolInstance.pool.Get();
                obj.transform.position = spawnPosition;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);

                EnemyCHaracterRoutine spawned = obj.GetComponent<EnemyCHaracterRoutine>();
                spawned.myPool = poolInstance.pool;
                spawned.health.onCharacterDied += OnCharDied;
                spawned.SetAttackTarget(gameplayManager.spawnedPlayerPosition);
                spawned.health.ResetHP();
                spawnRound.Add(spawned);
            }

            gameplayManager.spawnedEnemies.AddRange(spawnRound);
            allSpawned.Add(spawnRound);
        }

        public void OnCharDied(GameObject character)
        {
            var enemy = character.GetComponent<EnemyCHaracterRoutine>();
            if (enemy.released) return;
            enemy.released = true;
            gameplayManager.spawnedEnemies.Remove(enemy);
            enemy.myPool.Release(character);

        }
    }
}