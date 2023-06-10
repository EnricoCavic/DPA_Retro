using UnityEngine;

namespace Retro.Gameplay
{
    [CreateAssetMenu(fileName = "New Projectile Data", menuName = "Retro/Gameplay/Projectile")]
    public class ProjectileDataSO : ScriptableObject
    {
        public float projectileSpeed;
        public float lifeTimeInSeconds;
    }
}