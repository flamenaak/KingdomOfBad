using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbMoveState : PlayerGroundedState
{
    public PlayerClimbMoveState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.up, 1f, player.Core.CollisionSenses.Data.WhatIsInteractable);
        if (!hit)
        {
            //Top of the stack
            player.isAtTop = true;
        }
        else if (hit)
        {
            player.isAtTop = false;
        }
        //Climbing movement
        if ((yInput != 0 && player.Core.CollisionSenses.isTouchingClimable()) || (xInput != 0 && player.Core.CollisionSenses.isTouchingClimable()))
        {
            player.RigidBody.velocity = new Vector2(xInput * player.WalkSpeed, yInput * player.WalkSpeed);
        }
    }

    public override void Update()
    {
        base.Update();
        //Falling out of the climable entity
        if (xInput != 0 && !player.Core.CollisionSenses.isTouchingClimable() || yInput != 0 && !player.Core.CollisionSenses.isTouchingClimable())
        {
            stateMachine.ChangeState(player.FallState);
        }
        else if (yInput == 0 && player.Core.CollisionSenses.IsGrounded())
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (yInput == 0 && xInput == 0 && !player.Core.CollisionSenses.IsGrounded())
        {
            player.RigidBody.velocity = Vector2.zero;
            stateMachine.ChangeState(player.ClimbIdleState);
        }
        else if(Input.GetButton("Jump"))
        {
            stateMachine.ChangeState(player.LiftState);
        }
    }
}
