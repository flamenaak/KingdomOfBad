using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : CoreComponent
{
    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private GameObject healthbar;
    [SerializeField]
    private GameObject bloodSplash;
    [SerializeField]
    private IHasCombat entity;
    [SerializeField]
    private CombatData data;

    [SerializeField]
    private Collider2D damageCollider;

    public Transform AttackPosition
    {
        get
        {
            if (attackPosition)
                return attackPosition;

            Debug.LogError("Missing attack position on " + Core.transform.parent.name);
            return null;
        }

        private set { attackPosition = value; }
    }

    public CombatData Data
    {
        get
        {
            if (data)
                return data;

            Debug.LogError("Missing Data on " + Core.transform.parent.name);
            return null;
        }

        private set { data = value; }
    }

    public IHasCombat Entity
    {
        get
        {
            if (entity != null)
                return entity;

            Debug.LogError("Missing Entity on " + Core.transform.parent.name);
            return null;
        }

        private set { entity = value; }
    }

    public GameObject BloodSplash
    {
        get
        {
            if (bloodSplash)
                return bloodSplash;

            Debug.LogError("Missing Bloodsplash on " + Core.transform.parent.name);
            return null;
        }

        private set { bloodSplash = value; }
    }

    public GameObject Healthbar
    {
        get
        {
            if (healthbar)
                return healthbar;

            Debug.LogError("Missing Healthbar on " + Core.transform.parent.name);
            return null;
        }

        private set { healthbar = value; }
    }

    /// <summary>
    /// Colider used for detecting when parrent is hit
    /// </summary>
    /// <value></value>
    public Collider2D DamageCollider
    {
        get
        {
            if (!damageCollider)
                Debug.LogError($"Missing {nameof(damageCollider)} on {Core.transform.parent.name}");

            return damageCollider ?? null;
        }

        private set { damageCollider = value; }
    }

    public bool damaged;

    public float canTakeDamageCooldown = 0.2f;

    public bool canTakeDamage = true;

    public void Damage(float amount)
    {
        if (canTakeDamage)
        {
            Data.currentHealth -= amount;
            if (Data.currentHealth < 0) Data.currentHealth = 0;
            
            startCanTakeDamageCoolDown();
        }

    }

    public void FixedUpdate()
    {
        Collider2D collision = attackPosition.GetComponent<CircleCollider2D>();
        if (!collision.enabled)
        {
            return;
        }
        else
        {
            var colliders = Physics2D.OverlapCircleAll(collision.bounds.center, collision.bounds.extents.magnitude, Data.WhatIsEnemyDamage);

            if (colliders.Length > 0)
            {
                IHasCombat IHasCombat = colliders[0].GetComponentInParent<IHasCombat>();
                IHasCombat.Knockback(attackPosition, Data.knockbackSpeedX);
                IHasCombat.Damage(Data.damage);
            }
        }

    }

    public void Knockback()
    {
        Core.Combat.Data.knockback = true;
        Core.Combat.Data.knockbackStart = Time.time;
    }

    public void Die()
    {
        Instantiate(BloodSplash, transform.position, Quaternion.identity);
        damageCollider.enabled = false;
    }

    public void startCanTakeDamageCoolDown()
    {
        canTakeDamage = false;
        Invoke("clearSlashCooldown", canTakeDamageCooldown);
    }

    void clearSlashCooldown()
    {
        canTakeDamage = true;
    }

}

public interface IHasCombat
{
    Combat Combat { get; }

    // Receive damage
    void Damage(float amount);

    void Die();

    //Applies knockback to itself depending on the position of the attacker and facing direction of the receiver
    void Knockback(Transform attacker, float amount);

}
