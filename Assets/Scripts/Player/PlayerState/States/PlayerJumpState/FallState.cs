using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : PlayerAirState
{
    public FallState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        CheckAirInput();
        if (player.Controller.m_Grounded)
        {
            stateMachine.ChangeState(player.LandState);
        }

        CheckHang();
    }
}
