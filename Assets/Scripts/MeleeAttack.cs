using System.Collections;
using Interfaces;
using UnityEngine;

public class MeleeAttack: MonoBehaviour
{
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private IDamagable _targetHealth;

    private Coroutine _attackCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _targetHealth = other.GetComponentInParent<IDamagable>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsAbleToAttack(other)) return;
        _targetHealth?.Damage(attackDamage);
    }
    
    /*
     * We check if the other collider is the player and if the target health is is initialized and if the attack coroutine is not already running.
     */
    private bool IsAbleToAttack(Collider other)
    {
        return other.CompareTag("Player") && _targetHealth != null && _attackCoroutine == null;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        _attackCoroutine = null;
        _targetHealth = null;
    }
}