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

        if (player.Velocity.x == 0)
        {
            stateMachine.ChangeState(player.IdleState);
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