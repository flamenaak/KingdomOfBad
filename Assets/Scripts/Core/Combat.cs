using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : CoreComponent
{
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

    public CombatData Data;

    public IHasCombat Entity;

    public GameObject BloodSplash;

    public GameObject Healthbar;

    public bool damaged;

    public float canTakeDamageCooldown = 0.2f;

    public bool canTakeDamage = true;

    [SerializeField]
    private Transform attackPosition;

    public void Damage(float amount)
    {
        if (canTakeDamage)
        {
            Data.currentHealth -= amount;
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
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = Data.WhatIsEnemy;
            List<Collider2D> colliders = new List<Collider2D>();
            collision.GetContacts(filter, colliders);
            if (colliders.Count > 0 && collision.enabled)
            {
                IHasCombat IHasCombat = colliders[0].GetComponentInParent<IHasCombat>();
                Debug.Log("attack from: " + this.tag + " attack to: " + IHasCombat.tag);
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
    void Damage(float amount);

    void Die();

    //Applies knockback to itself depending on the position of the attacker and facing direction of the receiver
    void Knockback(Transform attacker, float amount);

    string tag { get; set; }
}
