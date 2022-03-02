using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeState : PlayerGroundedState
{
 
    public PlayerEvadeState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.DepleteStamina(1);
        player.startDashCoolDown();
        player.startDashGravityEffect();
        player.boxCollider2D.enabled = false;
        player.circleCollider2D.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.boxCollider2D.enabled = true;
        player.circleCollider2D.enabled = true;
    }

    public override void FixedUpdate()
    {
        Vector3 dashPosition = player.Core.Movement.DetermineEvadePosition(player.transform);
        player.RigidBody.MovePosition(dashPosition);
        if(Time.time - startTime > 0.3f)
        {
            stateMachine.ChangeState(player.IdleState);
        }        
    }

    public override void Update()
    {
        base.Update();
    }
}
