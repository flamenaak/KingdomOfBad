using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject Entity;

    public GameObject BloodSplash;

    public GameObject Healthbar;

    public bool damaged;

    [SerializeField]
    private Transform attackPosition;

    public void Damage(float amount)
    {
        Data.currentHealth -= amount;
        Healthbar.transform.localScale -= new Vector3(Data.maxHealth/100, 0 ,0);
        if (Data.currentHealth > 0.0f)
        {
            Knockback();
            damaged = true;
        }
        else if (Data.currentHealth <= 0.0f)
        {
            Healthbar.transform.localScale = new Vector3(0, 0, 0);
            Die();
        }
    }

    public void Attack()
    {
        Collider2D collision = attackPosition.GetComponent<CircleCollider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = Data.WhatIsEnemy;
            List<Collider2D> colliders = new List<Collider2D>();
            collision.GetContacts(filter, colliders);
        if(colliders.Count > 0)
        {
            if (Entity.tag.Equals("Player"))
            {
                colliders[0].GetComponentInParent<Enemy>().SendMessage("Damage", Data.damage);
            }
            else
            {
                colliders[0].GetComponentInParent<Player>().SendMessage("Damage", Data.damage);
            }
        }

    }

    private void Knockback()
    {
        Data.knockback = true;
        Data.knockbackStart = Time.time;
        Entity.GetComponent<Rigidbody2D>().velocity = new Vector2(Data.knockbackSpeedX * -Core.Movement.GetFacingDirection(), Data.knockbackSpeedY);
    }

    public void CheckKnockback()
    {
        if (Time.time >= Data.knockbackStart + Data.knockbackDuration && Data.knockback)
        {
            Data.knockback = false;
            Entity.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, Entity.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void Die()
    {
        Instantiate(BloodSplash, transform.position, Quaternion.identity);
    }

}
