using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwordWeapon : BaseWeapon
{
    private Collider _swordCollider;
    private Vector3 _originalSwordPosition;
    private bool _isSwinging = false;

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
        if (_isSwinging)
        {
            return;
        }
   
        StartCoroutine(SwingSword(1f));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        var enemy = other.GetComponent<IDamagable>();
        enemy.Damage(attackDamage);
    }

    private IEnumerator SwingSword(float speed)
    {
        _isSwinging = true;
        EnableCollider();
        var elapsed = 0.0f;
        var duration = 2f;


        while (elapsed < duration)
        {
            var angle = Mathf.PingPong(elapsed * speed, 1) * 180;
            transform.localRotation =
                Quaternion.Euler(0, angle + 90, 90); // Rotation around local Y-axis and rotate the sword 90 degrees

            elapsed += Time.deltaTime;
            yield return null;
        }

        DisableCollider();
        _isSwinging = false;
    }
}