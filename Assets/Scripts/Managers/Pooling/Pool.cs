using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Retro.Managers.Pooling
{
    public class Pool : MonoBehaviour
    {
        public GameObject prefab;
        public int defaultCapacity = 10;
        public int maxSize = 1000;

        public ObjectPool<GameObject> available;

        private void Awake()
        {
            available = new(Create, OnDequeue, OnRelease, DestroyObject, true, defaultCapacity, maxSize);
        }

        private GameObject Create()
        {
            var intantiatedObj = Instantiate(prefab, transform);
            return intantiatedObj;
        }

        private void OnDequeue(GameObject _dequeuedObject) { }

        private void OnRelease(GameObject _releasedObject)
        {
            _releasedObject.SetActive(false);
        }

        private void DestroyObject(GameObject _objectToDestroy)
        {
            Destroy(_objectToDestroy);
        }
    }
}