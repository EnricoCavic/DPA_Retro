using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    public int maxHp;
    public int currentHp { get; private set; }

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int _dmg)
    {
        currentHp -= _dmg;
        if (currentHp <= 0)
            Destroy(gameObject);
    }
}
