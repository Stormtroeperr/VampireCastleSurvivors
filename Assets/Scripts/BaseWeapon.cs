using System;
using Interfaces;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    protected float attackSpeed = 1f;
    protected float attackDamage = 10f;

    protected virtual void EnableCollider() { }

    protected virtual void DisableCollider() { }
}
