using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    public PlatformEffector2D effector;
    public LayerMask whatIsPlayer;
    public LayerMask jumpThroughPlatform;
    public LayerMask fallThroughPlatform;

    private void Update()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.transform.position, new Vector2(3, 2), 0.0f, Vector2.up, 1.0f, whatIsPlayer);
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
        float platformWidth = this.transform.localScale.x;
        RaycastHit2D left = Physics2D.Raycast(this.transform.position, Vector2.left, (platformWidth/2) + 1f, whatIsPlayer);
        RaycastHit2D right = Physics2D.Raycast(this.transform.position, Vector2.right, (platformWidth/2) + 1f, whatIsPlayer);
        if(left || right)
        {
            effector.colliderMask = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(this.transform.position, new Vector3(1, 1, 1));
    }
}
