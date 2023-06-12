using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Retro.Managers;
using Retro.Character;

public class HudManager : MonoBehaviour
{
    public Image cooldown;
    List<Sprite> cooldownSequence;

    public Image health;
    List<Sprite> healthSequence;

    public Image life;
    List<Sprite> lifeSequence;

    private GameplayManager gameplayManager;


    private void Awake()
    {
        gameplayManager = this.GetComponent<GameplayManager>();
        
        healthSequence = health.GetComponent<UISpriteSequence>().sprites;
        lifeSequence = life.GetComponent<UISpriteSequence>().sprites;
        cooldownSequence = cooldown.GetComponent<UISpriteSequence>().sprites;
    }

    private void Start()
    {

    }


    private void Update()
    {
        //cooldownSequence.Count
        //Debug.Log(gameplayManager.currentRewindCooldown);

        int cooldownValue = Mathf.FloorToInt(gameplayManager.currentRewindCooldown * 10f) - 1;
        if (cooldownValue < 0) return;

        Debug.Log(cooldownValue);

        cooldown.sprite = cooldownSequence[cooldownValue];


    }


    public void HealthUpdate(int current)
    {
        int currentHp = current -1;
        if (currentHp < 0) return;
        
        //int currentHp = gameplayManager.spawnedPlayerPosition.GetComponent<CharacterHealth>().currentHp - 1;
        health.sprite = healthSequence[currentHp];
    }

    public void LifeUpdate(int current)
    {
        int currentLife = current -1;
        if (currentLife < 0) return;

        //int currentLife = gameplayManager.currentLifes - 1;
        life.sprite = lifeSequence[currentLife];
    }





}
