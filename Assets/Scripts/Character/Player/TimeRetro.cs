using Retro.Character;
using Retro.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public struct PlayerTimestamp
{
    public Vector3 position;
    public Quaternion rotation;
    public float hp;

    public string animationName;
    public float frameTimestamp;

}

public class TimeRetro : MonoBehaviour
{

    public float colorScale;

    private Queue<PlayerTimestamp> playerTimestamps;
    private PlayerCharacterRoutine characterRoutine;
    private GameplayManager gameplayManager;

    public float rewindInterval = 0.1f;
    public float intervalFraction;
    public int queueLimit;

    public Action onRewindStart;
    public Action onRewindFinished;

    private float time;
    private float lastTime;

    



    private void Awake()
    {
        characterRoutine = GetComponent<PlayerCharacterRoutine>();
        playerTimestamps = new Queue<PlayerTimestamp>();
    }

    private void Start()
    {
        gameplayManager = GameplayManager.Instance;
        
    }

    void Update()
    {
        time = Time.time % intervalFraction;
        if(time < lastTime)
        {
            if (playerTimestamps.Count >= queueLimit)
                playerTimestamps.Dequeue();

            playerTimestamps.Enqueue(GetPlayerData(gameObject));
        }
        lastTime = time;
    }

    PlayerTimestamp GetPlayerData(GameObject player)
    {
        PlayerTimestamp playerData = new();

        playerData.position = player.transform.position;
        playerData.rotation = player.transform.rotation;
        playerData.hp = player.GetComponent<CharacterHealth>().currentHp;
        
        return playerData;
    }

    public void SetPlayerData(PlayerTimestamp data)
    {
        transform.position = data.position;
        transform.rotation = data.rotation;
    }

    public void Rewind()
    {
        if (playerTimestamps.Count == 0) return;
        characterRoutine.currentRoutine = PlayerRoutine.TimeRetro;
        gameplayManager.StartRewind();

        StartCoroutine(RewindCo());
    }

    public IEnumerator RewindCo()
    {
        Volume volume = Camera.main.GetComponent<Volume>();

        ColorAdjustments colorAdj;
        bool hasColorAdj = false;
        VolumeParameter<float> originalColor = new VolumeParameter<float>();
        
        if (hasColorAdj = volume.profile.TryGet<ColorAdjustments>(out colorAdj)) originalColor.value = 0;
        
        float reverseProgress = 0f;

        var array = playerTimestamps.ToArray();
            for (int i = playerTimestamps.Count -1; i >= 0; i--)
            {
                var current = array[i];
                SetPlayerData(current);

                if (hasColorAdj) 
                {
                    VolumeParameter<float> newColor = new VolumeParameter<float>();
                //newColor.value = -180f + Mathf.PingPong(Time.time * Time.timeScale, 360f);
                //newColor.value = Mathf.Lerp(newColor.value, 180, i / playerTimestamps.Count - 1);
                reverseProgress = 1f - ((float)i / (playerTimestamps.Count - 1));
                newColor.value = Mathf.Lerp(0, 180, reverseProgress);

                colorAdj.hueShift.SetValue(newColor);
                } 
            
                yield return new WaitForSecondsRealtime(rewindInterval);
            }

        if (hasColorAdj) colorAdj.hueShift.SetValue(originalColor);

        gameplayManager.FinishRewind();
        characterRoutine.currentRoutine = PlayerRoutine.Moving;
        characterRoutine.capsuleCollider.enabled = true;
        playerTimestamps.Clear();
    }

}


