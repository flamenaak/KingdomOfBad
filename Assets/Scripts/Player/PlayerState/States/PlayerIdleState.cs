using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (jump && player.Controller.m_Grounded)
        {
            stateMachine.ChangeState(player.JumpState);
        } else if (xInput != 0)
        {
            stateMachine.ChangeState(player.WalkState);
        } else {
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
        }
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}