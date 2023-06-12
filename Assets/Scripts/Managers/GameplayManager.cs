using Retro.Character;
using Retro.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Managers
{
    public class GameplayManager : Singleton<GameplayManager>
    {
        public GameObject playerPrefab;
        public Transform spawnedPlayer;
        public float rewindTimeScale = 0.3f;
        public int currentLifes = 3;

        public List<EnemyCHaracterRoutine> spawnedEnemies;
        private List<SpawnerManager> spawners;

        private void Awake()
        {
            if (!InstanceSetup(this)) return;

            spawners = new(GetComponentsInChildren<SpawnerManager>());
        }

        private void Start()
        {
            SpawnPlayer();
            for (int i = 0; i < spawners.Count; i++)
            {
                spawners[i].Spawn();
            }
        }

        private void SpawnPlayer()
        {
            spawnedPlayer = Instantiate(playerPrefab).transform;
            spawnedPlayer.GetComponent<CharacterHealth>().onCharacterDied += DestroyPlayer;

            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                spawnedEnemies[i].SetAttackTarget(spawnedPlayer);
            }
        }

        public void StartRewind()
        {
            Time.timeScale = rewindTimeScale;
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                spawnedEnemies[i].currentRoutine = EnemyRoutine.GlobalRewind;
            }
        }


        public void FinishRewind()
        {
            StartCoroutine(RestoreTimeScale());
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                spawnedEnemies[i].currentRoutine = EnemyRoutine.Chasing;
            }
        }

        private IEnumerator RestoreTimeScale()
        {
            while(Time.timeScale < 1f)
            { 
                yield return null;
                Time.timeScale += Time.unscaledDeltaTime * 1.4f;
            }
        }

        private void DestroyPlayer(GameObject player)
        {
            StartCoroutine(DestroyCoroutine());
            IEnumerator DestroyCoroutine()
            {
                currentLifes--;
                Destroy(spawnedPlayer.gameObject);
                if (currentLifes <= 0)
                {
                    GetComponent<LoadNextScene>().Load();
                    yield break;
                }

                spawnedPlayer = null;
                for (int i = 0; i < spawnedEnemies.Count; i++)
                {
                    spawnedEnemies[i].SetAttackTarget(null);
                }
                yield return new WaitForSeconds(1f);
                SpawnPlayer();
            }
        }

        private void OnDestroy()
        {
            GameObject obj;
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                var enemy = spawnedEnemies[i];
                if (enemy.released) continue;

                enemy.released = true;
                obj = enemy.gameObject;
                enemy.myPool.Release(obj);
            }
        }
    }
}