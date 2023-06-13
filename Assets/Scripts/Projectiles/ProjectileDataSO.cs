using UnityEngine;

namespace Retro.Gameplay
{
    [CreateAssetMenu(fileName = "New Projectile Data", menuName = "Retro/Gameplay/Projectile")]
    public class ProjectileDataSO : ScriptableObject
    {
        public float speed;
        public float lifeTimeInSeconds;
        public Vector3 scale;
        public Vector3 rotation;
        public Material material;
        public Mesh mesh;
    }
}