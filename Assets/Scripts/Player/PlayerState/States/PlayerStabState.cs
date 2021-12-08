using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStabState : PlayerGroundedState
{
    public PlayerStabState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.startStabCoolDown();
    }

    public override void Exit()
    {

        base.Exit();
    }

    public override void FixedUpdate()
    {
        Vector3 stabPosition = player.Core.Movement.DetermineStabPosition(player.transform);
        
        player.RigidBody.MovePosition(stabPosition);
        if (Time.time - startTime > 0.32f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
