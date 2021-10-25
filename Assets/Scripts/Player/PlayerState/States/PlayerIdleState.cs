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
        base.FixedUpdate();
        if (crouch)
        {
            // stateMachine.ChangeState(player.CrouchState);
            return;
        }

        if (xInput != 0)
        {
            stateMachine.ChangeState(player.WalkState);
        } else {
            player.Velocity = new Vector2(0, player.Velocity.y);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}