using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject entity;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (entity.tag.Equals("Player"))
        {
            collision.GetComponentInParent<Enemy>().SendMessage("Damage", 1f);
        }*/
        Destroy(this);
    }

}
