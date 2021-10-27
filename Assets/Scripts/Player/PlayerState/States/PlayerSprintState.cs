using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerGroundedState
{
    public PlayerSprintState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (!sprint)
        {
            player.StateMachine.ChangeState(player.RunState);
        }
        else
        {
            player.RigidBody.velocity = new Vector2(player.SprintSpeed * xInput, player.RigidBody.velocity.y);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
