using UnityEngine;

namespace Retro.Managers.Sound
{

    public interface IClipHolder
    {
        public AudioClip GetNextClip();
    }
}