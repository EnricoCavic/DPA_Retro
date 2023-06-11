using Retro.Character;
using Retro.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Managers
{
    public class GameplayManager : Singleton<GameplayManager>
    {
        public List<EnemyCHaracterRoutine> spawnedEnemies;

        private void Awake()
        {
            if (!InstanceSetup(this)) return;
        }

        public void StartRewind()
        {
            Time.timeScale = 0.3f;
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                spawnedEnemies[i].currentRoutine = EnemyRoutine.GlobalRewind;
            }
        }

        public void FinishRewind()
        {
            Time.timeScale = 1f;
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                spawnedEnemies[i].currentRoutine = EnemyRoutine.Chasing;
            }
        }
    }
}