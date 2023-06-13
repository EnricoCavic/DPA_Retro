using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;
using Retro.Character;

namespace Retro.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        public ProjectileDataSO data;
        public ObjectPool<GameObject> myPool;

        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;

        Vector3 direction;
        float currentLifetime;

        bool released;

        public void SetData(ProjectileDataSO _data)
        {
            data = _data;
            transform.localScale = data.scale;
            //transform.localEulerAngles = data.rotation;

            if (meshRenderer == null) 
                meshRenderer = GetComponentInChildren<MeshRenderer>();
            if(meshFilter == null)
                meshFilter = GetComponentInChildren<MeshFilter>();

            meshRenderer.transform.eulerAngles = data.rotation;
            meshRenderer.material = data.material;
            meshFilter.mesh = data.mesh;
        }

        private void OnEnable()
        {
            released = false;
            currentLifetime = 0f;
        }

        private void Update()
        {
            direction = transform.forward.normalized;
            transform.position += direction * (Time.deltaTime * data.speed);

            currentLifetime += Time.deltaTime;
            if (currentLifetime >= data.lifeTimeInSeconds && !released)
                ReleaseObject();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Hitable")) return;
            if (other.gameObject.TryGetComponent(out IGetHit e))
            {
                bool hit = e.HandleHit(1, transform.forward);
                if (!hit) return;
            }
            ReleaseObject();
        }

        private void ReleaseObject()
        {
            if (released) return;
            released = true;
            myPool.Release(gameObject);
        }
    }
}