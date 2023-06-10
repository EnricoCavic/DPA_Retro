using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;

namespace Retro.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        public ProjectileDataSO data;
        public ObjectPool<GameObject> myPool;

        Vector3 direction;
        float currentLifetime;

        bool released;

        private void OnEnable()
        {
            released = false;
            currentLifetime = 0f;
        }

        private void Update()
        {
            direction = transform.forward.normalized;
            transform.position += direction * (Time.deltaTime * data.projectileSpeed);

            currentLifetime += Time.deltaTime;
            if (currentLifetime >= data.lifeTimeInSeconds && !released)
                myPool.Release(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Hitable")) 
            {
                if(collision.gameObject.TryGetComponent(out EnemyInput e))
                    e.HitRoutine();
            }

            if (collision.gameObject.TryGetComponent(out Projectile _)) return;

            if (collision.gameObject.TryGetComponent(out CharacterAttributes attributes))
                attributes.TakeDamage(1);
            
            if (released) return;
            released = true;
            myPool.Release(gameObject); 
        }
    }
}