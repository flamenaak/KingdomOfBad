using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerState
{
    public PlayerSprintState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (player.velocityX < player.SprintSpeed)
        {
            stateMachine.ChangeState(player.RunState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("sprint", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("sprint", false);
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
