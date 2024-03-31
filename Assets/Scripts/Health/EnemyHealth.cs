using UnityEngine;
using UnityEngine.Serialization;

namespace Health
{
    public class EnemyHealth: BaseHealth
    {
        [Header("Death Settings")]
        [SerializeField] protected ParticleSystem onDeathParticlePrefab;
        
        [Header("Rewards Settings")]
        [SerializeField] protected int xpValue = 10;
        [SerializeField] protected int goldValue = 5;
        [SerializeField] protected FloatObject orbPrefab;
        
        public int GoldValue => goldValue;
        
        protected override void SetToDeath()
        {
            var deathParticle = Instantiate(onDeathParticlePrefab, transform.position, Quaternion.identity);
            var orb = Instantiate(orbPrefab, transform.position, Quaternion.identity);
            orb.xpValue = xpValue;
            
            if (!deathParticle.isPlaying)
            {
                deathParticle.Play();
            }
          
            base.SetToDeath();
        }
    }
}