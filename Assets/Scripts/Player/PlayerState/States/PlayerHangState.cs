﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangState : PlayerState
{
    public Vector2 detectedPos;
    public Vector2 ledgePos;

    public PlayerHangState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.transform.position = detectedPos;
        player.RigidBody.gravityScale = 0f;
        try{
            ledgePos = player.Controller.DetermineLedgePosition();
        } catch (Exception e)
        {
            stateMachine.ChangeState(player.FallState);
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        player.RigidBody.gravityScale = 3f;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.RigidBody.velocity = Vector2.zero;
        player.transform.position = ledgePos - new Vector2(player.xLedgeOffset * player.Controller.GetFacingDirection(), player.yLedgeOffset);
    }

    public override void Update()
    {
        base.Update();
        if (player.Controller.ReadInputY() < 0 || (player.Controller.IsTouchingLedge() && player.Controller.IsTouchingWall()))
        {
            player.RigidBody.gravityScale = 3f;
            stateMachine.ChangeState(player.FallState);
        }
    }
}
