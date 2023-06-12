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

    private GameplayManager gameplayManager;


    private void Start()
    {
        gameplayManager = this.GetComponent<GameplayManager>();
        healthSequence = health.GetComponent<UISpriteSequence>().sprites;
    }

    private void Update()
    {
        if (gameplayManager.spawnedPlayer == null) return;

    }

    void Health()
    {
        

        Debug.Log(gameplayManager.spawnedPlayer.GetComponent<CharacterHealth>().currentHp);
    }





}
