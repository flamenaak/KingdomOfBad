using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected bool dashAndEvade;
    protected bool jump;
    protected int xInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        jump = player.Controller.GetJumpInput();
        dashAndEvade = player.Controller.GetDashOrEvadeInput();
        xInput = player.Controller.ReadInputX();
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
        if (xInput == 0 && stateMachine.CurrentState != player.IdleState)
        {
            stateMachine.ChangeState(player.IdleState);
        } else {
            
        }
    }
    
    public override void Update()
    {
        base.Update();
    }
}
