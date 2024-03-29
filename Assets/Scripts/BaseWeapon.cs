using System;
using Interfaces;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected float attackSpeed = 1f;
    [SerializeField] protected float attackDamage = 10f;
    [SerializeField] protected int weaponLevel = 1;

    protected virtual void EnableCollider() { }

    protected virtual void DisableCollider() { }

    protected virtual void UpgradeWeapon()
    {
        weaponLevel++;
    }
}