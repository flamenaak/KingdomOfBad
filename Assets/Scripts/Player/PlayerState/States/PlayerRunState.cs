using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();


        if (player.velocityX > 0 && player.velocityX < player.RunSpeed)
        {
            stateMachine.ChangeState(player.WalkState);
        }
        if(player.velocityX >= player.SprintSpeed)
        {
            stateMachine.ChangeState(player.SprintState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("run", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("run", false);
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