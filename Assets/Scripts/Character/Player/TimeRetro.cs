using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerRetro
{
    // gravar informa��es em um intervalo de tempo:
    // transform
    // atributos do jogador - hp
    // anima�ao - nome e timestamp atual

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




    // Update() salvar steps de acordo com fra��o do delta time
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
    // l�gica de tempo global
    // voltar nos steps 
    // l�gica de cada step
    // evento fim do rewind

}


