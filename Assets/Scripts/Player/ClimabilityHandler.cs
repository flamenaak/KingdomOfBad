using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimabilityHandler : MonoBehaviour
{
    private Player player;
    public LayerMask WhatIsClimable;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        if (isTouchingClimable() && player.Controller.ReadInputY() != 0)
        {
            player.StateMachine.ChangeState(player.ClimbMoveState);
        }
    }

    public bool isTouchingClimable()
    {
        Collider2D climable = Physics2D.OverlapBox(this.transform.position,
        new Vector2(1, 1), 0, WhatIsClimable);
        if (climable != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
