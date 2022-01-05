using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour
{
    private void Awake()
    {

    }

    private void FixedUpdate()
    {

    }

    public int ReadInputX()
    {
        int moveX = 0;
        moveX += Input.GetButton("Left") ? -1 : 0;
        moveX += Input.GetButton("Right") ? 1 : 0;
        return moveX;
    }

    public int ReadInputY()
    {
        int moveY = 0;
        // moveY += Input.GetButton("Up") ? 1 : 0;
        moveY += Input.GetButton("Down") ? -1 : 0;
        return moveY;
    }

    public bool GetJumpInput()
    {
        return Input.GetButton("Jump");
    }

    public bool GetDashOrEvadeInput()
    {
        return Input.GetButton("Dash");
    }

    public bool GetClimbInput()
    {
        return Input.GetButton("Climb");
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(groundCheck.position, k_GroundedRadius);
    //     if (!IsTouchingWall())
    //     {
    //         Gizmos
    //             .DrawWireSphere(wallCheck.position + new Vector3(wallCheckDistance, 0) * GetFacingDirection(), k_GroundedRadius);
    //     }
    //     if (!IsTouchingLedge())
    //     {
    //         Gizmos
    //             .DrawWireSphere(ledgeCheck.position + new Vector3(wallCheckDistance, 0) * GetFacingDirection(), k_GroundedRadius);
    //     }
    //     else
    //     {
    //         // Gizmos.DrawWireSphere((Vector3)DetermineLedgePosition(), k_GroundedRadius);
    //     }
    //     foreach (Vector2 target in stabTargets)
    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawWireSphere(target, 0.1f);
    //     }
    // }

    public bool GetSlashInput()
    {
        return Input.GetButton("Slash");
    }

    public bool GetStabInput()
    {

        return Input.GetButtonUp("Stab");

    }

    public bool GetWindUpInput()
    {
        return Input.GetButtonDown("Stab");
    }

    public bool GetRespawnInput()
    {
        return Input.GetButtonDown("Respawn");
    }
}
