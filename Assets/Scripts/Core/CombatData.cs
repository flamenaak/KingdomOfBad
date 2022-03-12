using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatData : MonoBehaviour
{
    // layer to detect damage colliders when attacking
    public LayerMask WhatIsEnemyDamage;

    public float damage;

    public float AttackRange = 0.5f;

    public float currentHealth;

    public float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackStart;

    public bool applyKnockback, knockback;
}
