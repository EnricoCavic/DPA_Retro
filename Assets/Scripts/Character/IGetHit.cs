using UnityEngine;

namespace Retro.Character
{
    public interface IGetHit
    {
        void HandleHit(int _dmg, Vector3 _direction);
    }
}