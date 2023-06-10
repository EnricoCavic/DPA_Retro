using Retro.Generic;
using Retro.Managers.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.Managers
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        public Dictionary<GameObject, Pool> pools { get; private set; }

        private void Awake()
        {
            if (!InstanceSetup(this)) return;
            pools = new();
        }

        public Pool AddNewPool(GameObject _prefabToPool, int _defaultCapacity = 10, int _maxSize = 1000)
        {
            if (pools.ContainsKey(_prefabToPool)) return pools[_prefabToPool];

            GameObject obj = new GameObject("New Pool");
            obj.transform.parent = transform;
            Pool pool = obj.AddComponent<Pool>();
            pool.prefab = _prefabToPool;
            pool.defaultCapacity = _defaultCapacity;
            pool.maxSize = _maxSize;

            pools.Add(_prefabToPool, pool);
            return pool;
        }

    }
}