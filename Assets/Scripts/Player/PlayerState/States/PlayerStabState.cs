﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStabState : PlayerGroundedState
{
    public PlayerStabState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        player.RigidBody.AddForce(new Vector2(2.0f , player.RigidBody.velocity.y));
        if (slash)
        {
            stateMachine.ChangeState(player.SlashState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
