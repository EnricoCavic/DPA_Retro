using UnityEngine;

namespace Retro.Character
{
    public interface IGetHit
    {
        bool HandleHit(int _dmg, Vector3 _direction);
    }
}