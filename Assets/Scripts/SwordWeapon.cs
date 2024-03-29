using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwordWeapon : BaseWeapon
{
    private Collider _swordCollider;
    private Vector3 _originalSwordPosition;

    protected void Start()
    {
        _swordCollider = GetComponent<BoxCollider>();
        _originalSwordPosition = transform.localPosition;
        
        StartCoroutine(AutoAttack(attackSpeed));
    }

    protected override void EnableCollider()
    {
        _swordCollider.enabled = true;
    }
    
    protected override void DisableCollider()
    {
        _swordCollider.enabled = false;
    }

    private IEnumerator AutoAttack(float interval)
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(interval);
        }
    }

    private void Attack()
    {
        StartCoroutine(ShakeSword(0.1f, 0.5f));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        var enemy = other.GetComponent<IDamagable>();
        enemy.Damage(attackDamage);
    }
    
    private IEnumerator ShakeSword(float duration, float magnitude)
    {
        EnableCollider();
        var elapsed = 0.0f;

        while (elapsed < duration)
        {
            var x = _originalSwordPosition.x + Random.Range(-1f, 1f) * magnitude;
            var y = _originalSwordPosition.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, _originalSwordPosition.z);

            elapsed += Time.deltaTime;
            
            yield return null;
        }

        transform.localPosition = _originalSwordPosition;
        DisableCollider();
    }
}