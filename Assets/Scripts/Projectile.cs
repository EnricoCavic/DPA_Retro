using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    Vector3 direction;

    public float lifeTimeInSeconds;

    public ObjectPool<GameObject> myPool;

    private void OnEnable()
    {
        StartCoroutine(Countdown());
        
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        direction = transform.forward.normalized;
        transform.position += direction * (Time.deltaTime * projectileSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        myPool.Release(gameObject);
    }

    public IEnumerator Countdown() 
    {
        yield return new WaitForSeconds(lifeTimeInSeconds);
        myPool.Release(gameObject);
    }
}
