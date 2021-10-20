using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (player.Velocity.x != 0)
        {
            stateMachine.ChangeState(player.WalkState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("idle", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("idle", false);
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