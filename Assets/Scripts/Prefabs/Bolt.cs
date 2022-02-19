using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    GameObject Entity;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponentInParent<IHasCombat>().Damage(1);
            Destroy(gameObject);
        }
    }

}
