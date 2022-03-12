using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    public PlatformEffector2D effector;
    public LayerMask whatIsPlayer;

    private void Update()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.transform.position, new Vector2(3, 2), 0.0f, Vector2.up, 1.0f, whatIsPlayer);
        if (raycastHit)
        {
            if (Input.GetKey(KeyCode.S))
            {
                //Collider mask of the enemy and other in the future important layers(excluding player)
                effector.colliderMask = 2850;

            }
            else if (Input.GetKey(KeyCode.Space))
            {
                //Collider mask of everything including enemy
                effector.colliderMask = 16383;
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            effector.colliderMask = 16383;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(this.transform.position, new Vector3(1, 1, 1));

    }
}
