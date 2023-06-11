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

        public List<EnemyCHaracterRoutine> spawnedEnemies;

        public float rewindTimeScale = 0.3f;

        private void Awake()
        {
            if (!InstanceSetup(this)) return;
            var player = Instantiate(playerPrefab).transform;

            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                spawnedEnemies[i].attackTarget = player;
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
    }
}