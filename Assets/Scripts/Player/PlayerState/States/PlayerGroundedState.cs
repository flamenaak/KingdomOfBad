using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected bool dashAndEvade;
    protected bool jump;
    protected bool slash;
    protected bool stab;
    protected bool windUp;
    protected bool interact;
    protected int xInput;
    public PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        jump = player.Controller.GetJumpInput();
        dashAndEvade = player.Controller.GetDashOrEvadeInput();
        slash = player.Controller.GetSlashInput();
        stab = player.Controller.GetStabInput();
        windUp = player.Controller.GetWindUpInput();
        xInput = player.Controller.ReadInputX();
        interact = player.Controller.GetInputInteract();
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
       
    }
    
    public override void Update()
    {
        base.Update();
    }
}
