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
        _targetHealth = other.GetComponent<IDamagable>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsAbleToAttack(other)) return;
        _attackCoroutine = StartCoroutine(DoDamage());
    }
    
    /*
     * We check if the other collider is the player and if the target health is is initialized and if the attack coroutine is not already running.
     */
    private bool IsAbleToAttack(Collider other)
    {
        return other.CompareTag("Player") && _targetHealth != null && _attackCoroutine == null;
    }
    
    private IEnumerator DoDamage()
    {
        yield return new WaitForSeconds(0.5f);
        _targetHealth?.Damage(attackDamage);
        
        StopCoroutine(_attackCoroutine);
        _attackCoroutine = null;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _targetHealth = null;
    }
}