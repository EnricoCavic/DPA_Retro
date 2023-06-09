using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Retro.Managers;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;

    Vector2 lookTarget;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }


    void Update()
    {
        lookTarget = playerInput.GetLookTarget();

        transform.LookAt(new Vector3(lookTarget.x, 0, lookTarget.y));
        
        var rotTarget = new Vector3(0, this.transform.eulerAngles.y, 0);
        this.transform.eulerAngles = rotTarget;



    }
}
