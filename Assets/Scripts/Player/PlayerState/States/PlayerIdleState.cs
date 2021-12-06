using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (jump && player.Core.CollisionSenses.IsGrounded())
        {
            stateMachine.ChangeState(player.LiftState);
        } 
        else if (dashAndEvade && player.canDashOrEvade)
        {
            stateMachine.ChangeState(player.EvadeState);           
        } 
        else if (slash && player.canSlash)
        {
            stateMachine.ChangeState(player.SlashState);
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
        }
        else if (xInput != 0)
        {
            stateMachine.ChangeState(player.WalkState);
        } 
        else if (!player.Core.CollisionSenses.IsGrounded())
        {
            stateMachine.ChangeState(player.FloatState);
        }
        else
        {
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
        }
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Stab") && player.canStab)
        {
            stateMachine.ChangeState(player.WindUpState);
        }
    }
}