using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerRetro
{
    // gravar informações em um intervalo de tempo:
    // transform
    // atributos do jogador - hp
    // animaçao - nome e timestamp atual

    public Transform transform;
    public float hp;
    public string animationName;
    public float frameTimestamp;

}

public class TimeRetro : MonoBehaviour
{
    public float intervalFraction;
    public int queueLimit;


    private float time;
    private float lastTime;




    // Update() salvar steps de acordo com fração do delta time
    void Update()
    {
        time = Time.time % intervalFraction;
        if(time < lastTime) Debug.Log("zerou");

        lastTime = time;
    }

    PlayerRetro GetPlayerData(GameObject player)
    {
        PlayerRetro playerData = new();

        playerData.transform = player.transform;
        //playerData.hp = (player.TryGetComponent<CharacterAttributes>(out CharacterAttributes test) ? test.currentHp : 0);
        playerData.hp = player.GetComponent<CharacterAttributes>().currentHp;



        return playerData;
    }


    // Rewind()
    // evento de inicio
    // lógica de tempo global
    // voltar nos steps 
    // lógica de cada step
    // evento fim do rewind

}


