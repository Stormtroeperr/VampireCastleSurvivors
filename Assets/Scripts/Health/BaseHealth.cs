using Interfaces;
using UnityEngine;

namespace Health
{
    public class BaseHealth : MonoBehaviour, IDamagable
    {
        [Header("Health Settings")]
        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected float currentHealth = 100f;
    
        public delegate void DieAction(GameObject deadObject);
        public event DieAction OnDie;
    
        public delegate void DamageAction(float damage);
        public event DamageAction OnDamage;

        public float CurrentHealth => currentHealth;

        public virtual float Damage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            OnDamage?.Invoke(damage);
        
            if (!(currentHealth <= 0)) return currentHealth;
        
            SetToDeath();

            return currentHealth;
        }
    
        protected virtual void SetToDeath()
        {
            OnDie?.Invoke(gameObject);
            gameObject.SetActive(false);
            currentHealth = maxHealth;
        }
    

    }
}