using System;
using UnityEngine;

public interface IGiveInput
{
    Action OnFireStart { get; set; }
    Action OnFireCanceled { get; set; }

    Vector3 GetMoveTarget(Vector3 _currentPosition);
    Vector2 GetLookTarget();
}
