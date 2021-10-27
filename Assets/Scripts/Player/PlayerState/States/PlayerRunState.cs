using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (jump && player.Controller.m_Grounded)
        {
            stateMachine.ChangeState(player.JumpState);
        } else if (sprint)
        {
            stateMachine.ChangeState(player.SprintState);
        } else
        {
            player.RigidBody.velocity = new Vector2(player.RunSpeed * xInput, player.RigidBody.velocity.y);
        }

    }

    public override void Update()
    {
        base.Update();
    }
}