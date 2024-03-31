using UnityEngine;
using UnityEngine.Serialization;

namespace Health
{
    public class EnemyHealth: BaseHealth
    {
        [SerializeField] protected ParticleSystem onDeathParticlePrefab;
        protected override void SetToDeath()
        {
            var deathParticle = Instantiate(onDeathParticlePrefab, transform.position, Quaternion.identity);
            
            if (!deathParticle.isPlaying)
            {
                deathParticle.Play();
            }
          
            base.SetToDeath();
        }
    }
}