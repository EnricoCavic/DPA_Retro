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
            if (meshRenderer == null) 
                meshRenderer = GetComponent<MeshRenderer>();
            if(meshFilter == null)
                meshFilter = GetComponent<MeshFilter>();
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
                myPool.Release(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Hitable"))
            {
                if (collision.gameObject.TryGetComponent(out IGetHit e))
                    e.HandleHit();

                if (collision.gameObject.TryGetComponent(out CharacterAttributes attributes))
                    attributes.TakeDamage(1);

            }

            if (released) return;
            released = true;
            myPool.Release(gameObject);
        }
    }
}