using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private bool isReady = false;

    LayerMask whatToHit;

    public void Start()
    {
        if (isReady)
            enabled = true;
        else
            enabled = false;
    }

    public Bolt StartBolt(Vector2 direction, LayerMask whatToHit)
    {
        rb.velocity = Vector2.right * direction * speed;
        this.whatToHit = whatToHit;
        isReady = true;
        Start();
        return this;
    }

    public void FixedUpdate()
    {
        Collider2D collision = GetComponentInChildren<BoxCollider2D>();
        var colliders = Physics2D.OverlapBoxAll(
            collision.bounds.center,
            collision.bounds.extents,
            0,
            whatToHit);

        if (colliders.Length > 0)
        {
            IHasCombat IHasCombat = colliders[0].GetComponentInParent<IHasCombat>();
            if (IHasCombat != null)
            {
                IHasCombat.Knockback(transform, 10);
                IHasCombat.Damage(1);
            }
            Destroy(gameObject);
        }
    }
}
