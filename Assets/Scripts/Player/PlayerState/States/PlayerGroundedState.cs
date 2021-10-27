using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected bool sprint;
    protected bool jump;
    protected bool crouch;
    protected int xInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        jump = player.Controller.GetJumpInput();
        crouch = player.Controller.GetCrouchInput();
        sprint = player.Controller.GetDashInput();
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
        if (xInput == 0)
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
