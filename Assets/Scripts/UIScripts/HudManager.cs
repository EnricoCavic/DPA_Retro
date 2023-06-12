using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Retro.Managers;
using Retro.Character;

public class HudManager : MonoBehaviour
{
    public Image cooldown;

    public Image health;
    List<Sprite> healthSequence;

    public Image life;
    List<Sprite> lifeSequence;

    private GameplayManager gameplayManager;


    private void Start()
    {
        gameplayManager = this.GetComponent<GameplayManager>();
        healthSequence = health.GetComponent<UISpriteSequence>().sprites;
        lifeSequence = life.GetComponent<UISpriteSequence>().sprites;
    }

    private void Update()
    {
        if (gameplayManager.spawnedPlayerPosition == null) return;

        HealthUpdate();
        LifeUpdate();
    }

    void HealthUpdate()
    {
        int currentHp = gameplayManager.spawnedPlayer.GetComponent<CharacterHealth>().currentHp - 1;
        health.sprite = healthSequence[currentHp];
    }

    void LifeUpdate()
    {
        int currentLife = gameplayManager.currentLifes - 1;
        life.sprite = lifeSequence[currentLife];
    }





}
