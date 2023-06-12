using Retro.Character;
using Retro.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Managers
{
    public class GameplayManager : Singleton<GameplayManager>
    {
        public GameObject playerPrefab;
        public Transform spawnedPlayerPosition;
        public PlayerCharacterRoutine player;
        public float rewindTimeScale = 0.3f;
        public int currentLifes = 3;

        public Action<int> onLifeLost;

        public float currentRewindCooldown => player != null ? player.currentRewindCooldown / player.rewindCooldown : 1f;

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
            spawnedPlayerPosition = Instantiate(playerPrefab).transform;
            player = spawnedPlayerPosition.GetComponent<PlayerCharacterRoutine>();
            
            player.health.onCharacterDied += DestroyPlayer;

            var hud = GetComponent<HudManager>();

            onLifeLost += hud.LifeUpdate;
            player.health.onCharacterDamage += hud.HealthUpdate;

            hud.HealthUpdate(player.attributeData.maxHealth);

            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                spawnedEnemies[i].SetAttackTarget(spawnedPlayerPosition);
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
                onLifeLost?.Invoke(currentLifes);
                Destroy(spawnedPlayerPosition.gameObject);
                if (currentLifes <= 0)
                {
                    GetComponent<LoadNextScene>().Load();
                    yield break;
                }

                spawnedPlayerPosition = null;
                player = null;
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
            onLifeLost = null;

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