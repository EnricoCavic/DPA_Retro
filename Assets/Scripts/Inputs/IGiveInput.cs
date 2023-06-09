using UnityEngine;

public interface IGiveInput
{
    bool startAttack { get; set; }

    Vector3 GetMoveTarget(Vector3 _currentPosition);

    Vector2 GetLookTarget();
}
