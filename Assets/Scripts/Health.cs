using Interfaces;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    
    [SerializeField] private bool isGodMode = false;
    
    public delegate void DieAction(GameObject deadObject);
    public event DieAction OnDie;

    public float CurrentHealth => currentHealth;
    
    public float Damage(float damage)
    {
        if (isGodMode) return currentHealth;
        
        currentHealth -= damage;

        if (currentHealth > 0) return currentHealth;
        
        OnDie?.Invoke(gameObject);
        SetToDeath();
        
        return 0;
    }

    private void SetToDeath()
    {
        gameObject.SetActive(false);
        currentHealth = maxHealth;
    }
}