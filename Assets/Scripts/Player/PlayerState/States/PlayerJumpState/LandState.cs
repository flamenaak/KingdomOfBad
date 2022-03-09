using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : PlayerState
{
    public LandState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.LandDust.Play();
    }

    public override void Exit()
    {
        player.LandDust.Stop();
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Mathf.Abs(player.Controller.ReadInputX()) > 0.5)
        {
            if (Mathf.Abs(player.RigidBody.velocity.x) > player.WalkSpeed)
            {
                stateMachine.ChangeState(player.RunState);
                return;
            } else if (Mathf.Abs(player.RigidBody.velocity.x) > player.WalkSpeed/3)
            {
                stateMachine.ChangeState(player.WalkState);
                return;
            }
        } else 
        {
            player.RigidBody.velocity = new Vector2(0,0);
        }
        if (Time.time - startTime >= 0.2f)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }
        
    }
}
