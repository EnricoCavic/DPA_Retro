using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform lookTarget;


    void Update()
    {
        var posTarget = new Vector3(lookTarget.position.x, 0, lookTarget.position.z);

        //this.transform.LookAt(posTarget, this.transform.up);
        this.transform.LookAt(posTarget);

        var rotTarget = new Vector3(0, this.transform.eulerAngles.y, 0);
        //this.transform.Rotate(rotTarget);
        this.transform.eulerAngles = rotTarget;
    }
}
