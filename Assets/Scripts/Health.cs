using Interfaces;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float currentHealth = 100f;
    
    [SerializeField] protected bool isGodMode;
    
    public delegate void DieAction(GameObject deadObject);
    public event DieAction OnDie;
    
    public delegate void DamageAction(float damage);
    public event DamageAction OnDamage;

    public float CurrentHealth => currentHealth;

    public float Damage(float damage)
    {
        if (isGodMode || damage <= 0) return maxHealth;
        
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnDamage?.Invoke(damage);
        
        if (!(currentHealth <= 0)) return currentHealth;
        
        SetToDeath();

        return currentHealth;
    }
    
    private void SetToDeath()
    {
        OnDie?.Invoke(gameObject);
        gameObject.SetActive(false);
        currentHealth = maxHealth;
    }
    

}