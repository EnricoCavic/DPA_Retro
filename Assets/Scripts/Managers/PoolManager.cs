using Retro.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Retro.Managers
{
    public class PoolManager : Singleton<PoolManager>
    {
        public List<Pool> pools;
        private void Awake()
        {
            if (!InstanceSetup(this)) return;


        }
    }

    public class Pool
    {
        public GameObject prefab;
        public ObjectPool<GameObject> objects;
    }
}