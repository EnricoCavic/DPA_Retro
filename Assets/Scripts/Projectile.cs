using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    // teste
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

    void Update()
    {
        Shoot();
    }

    void Shoot() 
    {
        direction = transform.forward.normalized;
        transform.position += direction * (Time.deltaTime * projectileSpeed);
    }


    public IEnumerator Countdown() 
    {
        yield return new WaitForSeconds(lifeTimeInSeconds);

        myPool.Release(gameObject);
        //INSERIR RETORNO PARA A POOL AQUI
    }
}
