﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseState : PlayerAirState
{
    public RiseState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        player.DepleteStamina(1);
        player.RigidBody.velocity = new Vector2(player.RigidBody.velocity.x, 10);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckHang();
        CheckAirInput();
        if (Time.time - startTime > 0.4f)
        {
            if (player.RigidBody.velocity.y < -0.2f)
            {
                stateMachine.ChangeState(player.FallState);
            }
            else if (player.RigidBody.velocity.y < 0.2f)
            {
                stateMachine.ChangeState(player.FloatState);
            }
        }

    }

    public override void Update()
    {
        base.Update();
        player.PickDropHandling();
        if (player.Core.CollisionSenses.isTouchingClimable() && player.Controller.ReadInputY() != 0)
        {
            player.RigidBody.velocity = Vector2.zero;
            stateMachine.ChangeState(player.ClimbIdleState);
        }
    }
}
