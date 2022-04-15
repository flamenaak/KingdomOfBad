using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    public PlatformEffector2D effector;
    public LayerMask whatIsPlayer;
    public LayerMask jumpThroughPlatform;
    public LayerMask fallThroughPlatform;
    BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.transform.position, new Vector2(boxCollider2D.bounds.size.x, boxCollider2D.bounds.size.y + 1f), 0f, Vector2.zero, 0f, whatIsPlayer);
        if (raycastHit)
        {
            if (Input.GetKey(KeyCode.S))
            {
                //Collider mask of the enemy and other in the future important layers(excluding player)
                effector.colliderMask = fallThroughPlatform;

            }
            else if (Input.GetButton("Jump") || Input.GetButton("Climb"))
            {
                //Collider mask of everything including enemy
                effector.colliderMask = jumpThroughPlatform;
            }
        }
        else if (Input.GetButton("Jump") || Input.GetButton("Climb"))
        {
            effector.colliderMask = jumpThroughPlatform;
        }
        //Checking for sides of the platform
        RaycastHit2D left = Physics2D.BoxCast(new Vector2(boxCollider2D.bounds.center.x - boxCollider2D.bounds.extents.x, boxCollider2D.bounds.center.y - 0.1f), 
            new Vector2(0.1f, boxCollider2D.bounds.size.y), 0 , Vector2.left, 0, whatIsPlayer);
        RaycastHit2D right = Physics2D.BoxCast(new Vector2(boxCollider2D.bounds.center.x + boxCollider2D.bounds.extents.x, boxCollider2D.bounds.center.y + 0.1f), 
            new Vector2(0.1f, boxCollider2D.bounds.size.y), 0 , Vector2.right, 0, whatIsPlayer);
        if (left && GameObject.FindObjectOfType<Player>().Core.CollisionSenses.IsGrounded() || right && GameObject.FindObjectOfType<Player>().Core.CollisionSenses.IsGrounded())
        {
            effector.colliderMask = fallThroughPlatform;
        }
    }
}
