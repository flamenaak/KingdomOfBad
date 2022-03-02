using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    GameObject Entity;
    public CircleCollider2D CircleCollider2D;
    void Start()
    {
        Entity = GameObject.Find("Crossbowman");
        if (Entity.GetComponent<Crossbowman>().Core.Movement.IsFacingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = -transform.right * speed;

        }
   }

    public void FixedUpdate()
    {
        Collider2D collision = CircleCollider2D;
        if (!collision.enabled)
        {
            return;
        }
        else
        {
            var colliders = Physics2D.OverlapCircleAll(collision.bounds.center, collision.bounds.extents.magnitude, Entity.GetComponent<Crossbowman>().Core.Combat.Data.WhatIsEnemy);

            if (colliders.Length > 0)
            {
                IHasCombat IHasCombat = colliders[0].GetComponentInParent<IHasCombat>();
                IHasCombat.Knockback(Entity.transform, 10);
                IHasCombat.Damage(1);
                Destroy(gameObject);
            }
        }
    }
}
