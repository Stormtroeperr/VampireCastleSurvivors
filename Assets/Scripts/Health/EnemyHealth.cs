using UnityEngine;
using UnityEngine.Serialization;

namespace Health
{
    public class EnemyHealth: BaseHealth
    {
        [Header("Death Settings")]
        [SerializeField] protected ParticleSystem onDeathParticlePrefab;
        [SerializeField] protected int xpValue = 10;
        [SerializeField] protected int goldValue = 5;
        
        public int XpValue => xpValue;
        public int GoldValue => goldValue;
        
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