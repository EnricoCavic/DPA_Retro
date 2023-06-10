using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    Vector3 direction;


    private void OnEnable()
    {
        direction = transform.forward.normalized;
    }
    private void OnDisable()
    {
           
    }

    void Update()
    {
        Shoot();
    }

    void Shoot() 
    {
        transform.position += direction * (Time.deltaTime * projectileSpeed);
    }

}
