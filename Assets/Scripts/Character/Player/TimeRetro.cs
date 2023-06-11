using Retro.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerTimestamp
{
    // gravar informações em um intervalo de tempo:
    // transform
    // atributos do jogador - hp
    // animaçao - nome e timestamp atual

    public Vector3 position;
    public Quaternion rotation;
    public float hp;

    public string animationName;
    public float frameTimestamp;

}

public class TimeRetro : MonoBehaviour
{
    private Queue<PlayerTimestamp> playerTimestamps;
    private PlayerActions inputActions;

    public float intervalFraction;
    public int queueLimit;

    public Action onRewindStart;
    public Action onRewindFinished;

    private float time;
    private float lastTime;


    private void Awake()
    {
        playerTimestamps = new Queue<PlayerTimestamp>();
        inputActions = InputManager.Instance.inputActions;
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Special.performed += Rewind;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Special.performed -= Rewind;
    }

    // Update() salvar steps de acordo com fração do delta time
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
        playerData.hp = player.GetComponent<CharacterAttributes>().currentHp;
        
        return playerData;
    }

    public void SetPlayerData(PlayerTimestamp data)
    {
        transform.position = data.position;
        transform.rotation = data.rotation;
    }

    public void Rewind(UnityEngine.InputSystem.InputAction.CallbackContext _ctx)
    {
        // evento de inicio
        // lógica de tempo global
        // voltar nos steps 
        // lógica de cada step
        // evento fim do rewind
        

        if (playerTimestamps.Count == 0) return;

        StartCoroutine(RewindCo());

        //SetPlayerData(playerTimestamps.Dequeue());
        //playerTimestamps.Clear();
    }

    public IEnumerator RewindCo()
    {
        var array = playerTimestamps.ToArray();
        for (int i = playerTimestamps.Count -1; i >= 0; i--)
        {
            //SetPlayerData(playerTimestamps.Dequeue());

            var current = array[i];
            Debug.Log(current.position);
            SetPlayerData(current);

            yield return new WaitForSeconds(0.1f);

            //yield return null;
        }

        playerTimestamps.Clear();
    }

}


