using System;
using UnityEngine;

namespace Retro.Character.Input
{
    public interface IGiveInput
    {
        Action OnFireStart { get; set; }
        Action OnFireCanceled { get; set; }   
        
        Action OnMoveStart { get; set; }
        Action OnMoveCanceled { get; set; }

        Vector3 GetMoveTarget();
        Vector3 GetLookTarget();
    }
}