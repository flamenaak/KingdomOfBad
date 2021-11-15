using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAirState : PlayerState
{
    protected bool ShouldHang {get; set;}

    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        ShouldHang = !player.Controller.IsTouchingLedge() 
            && player.Controller.IsTouchingWall() 
            && !player.Controller.m_Grounded;
        if (ShouldHang) {
            player.HangState.detectedPos = player.transform.position;
        }
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        DoChecks();
    }

    protected void CheckHang()
    {
        if (ShouldHang)
        {
            stateMachine.ChangeState(player.HangState);
        }
    }
}
