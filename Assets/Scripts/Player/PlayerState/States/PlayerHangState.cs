using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangState : PlayerState
{
    public Vector2 detectedPos;
    public Vector2 ledgePos;

    public PlayerHangState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        if (!player.CanHang)
        {
            stateMachine.ChangeState(player.FallState);
        }
        else
        {
            base.Enter();
            player.transform.position = detectedPos;
            player.RigidBody.gravityScale = 0f;
            player.Core.Combat.DamageCollider.enabled = false;
            try
            {
                ledgePos = player.Core.CollisionSenses.DetermineLedgePosition();
            }
            catch (Exception e)
            {
                stateMachine.ChangeState(player.FallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.RigidBody.gravityScale = 3f;
        player.Core.Combat.DamageCollider.enabled = true;
        player.StartHangCooldown();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.RigidBody.velocity = Vector2.zero;
        player.transform.position = ledgePos - new Vector2(player.xLedgeOffset * player.Core.Movement.GetFacingDirection(), player.yLedgeOffset);
    }

    public override void Update()
    {
        base.Update();
        if (player.Controller.ReadInputY() < 0 || (player.Core.CollisionSenses.IsTouchingLedge() && player.Core.CollisionSenses.IsTouchingWall()))
        {
            stateMachine.ChangeState(player.FallState);
        }
        if (player.Controller.GetClimbInput())
        {
            stateMachine.ChangeState(player.ClimbState);
        }
    }
}
