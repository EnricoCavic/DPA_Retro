using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    Vector3 direction;

    public float lifeTimeInSeconds;


    private void OnEnable()
    {
        StartCoroutine(Countdown());
        direction = transform.forward.normalized;
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
        transform.position += direction * (Time.deltaTime * projectileSpeed);
    }


    public IEnumerator Countdown() 
    {
        yield return new WaitForSeconds(lifeTimeInSeconds);

        gameObject.SetActive(false);
    }

}
