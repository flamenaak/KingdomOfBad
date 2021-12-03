using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerGroundedState
{
    public PlayerDashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.startDashCoolDown();
    }

    public override void Exit()
    {
        base.Exit();
        player.boxCollider2D.enabled = true;
        player.circleCollider2D.enabled = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();        
        Vector3 dashPosition = player.Controller.DetermineDashDestination(player);
        player.RigidBody.MovePosition(dashPosition);

        if (Time.time - startTime > 0.52f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Update()
    {
        base.Update();

    }
}
