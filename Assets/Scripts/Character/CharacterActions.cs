using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Retro.Managers;
using Retro.Character.Input;
using Retro.Gameplay;

namespace Retro.Character
{
    public class CharacterActions : MonoBehaviour
    {
        private ObjectPoolManager poolManager;

        public GameObject projectilePrefab;
        public Transform projectileSource;

        [HideInInspector] public ProjectileDataSO projectileData;

        private void Awake()
        {
            poolManager = ObjectPoolManager.Instance;
        }

        public void Fire()
        {
            var instance = poolManager.GetPoolInstance(projectilePrefab);

            var obj = instance.pool.Get();

            obj.transform.position = projectileSource.position;
            obj.transform.rotation = projectileSource.rotation;
            obj.SetActive(true);

            var projectile = obj.GetComponent<Projectile>();
            projectile.myPool = instance.pool;
            projectile.SetData(projectileData);
        }


    }
}