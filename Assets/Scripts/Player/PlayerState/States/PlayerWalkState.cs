using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public int acc = 0;
    public PlayerWalkState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (player.velocityX == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if(acc > 100)
        {
            stateMachine.ChangeState(player.RunState);
            acc = 0;
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("walk", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("walk", false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(player.velocityX > 0)
        {
            acc++;
        }
        else
        {
            acc = 0;
        }
        Debug.Log(acc);
    }

    public override void Update()
    {
        base.Update();
    }
}