using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Retro.Managers;

public class PlayerInput : MonoBehaviour, IGiveInput
{
    PlayerActions inputActions;
    Camera mainCam;

    Vector2 mousePosition;
    Ray screenToRay;

    Vector2 moveTarget;

    public bool startAttack { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        inputActions = InputManager.Instance.inputActions;
        mainCam = Camera.main;
    }


    public Vector2 GetLookTarget()
    {
        mousePosition = inputActions.Gameplay.MousePosition.ReadValue<Vector2>();
        screenToRay = mainCam.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(screenToRay, out RaycastHit hit)) return Vector2.zero;
        return new Vector2(hit.point.x, hit.point.z);

    }

    //public Vector3 GetMoveTarget(Vector3 _currentPosition)
    //{
        
    //}


}
