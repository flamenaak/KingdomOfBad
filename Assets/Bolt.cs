using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Crossbowman");
        if (Player.GetComponent<Crossbowman>().Core.Movement.IsFacingRight)
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
            collision.GetComponentInParent<Player>().SendMessage("Damage", 1f);
            Destroy(gameObject);
        }
    }

}
