using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerGroundedState
{
    public Vector2 ledgePos;
      
    public PlayerClimbState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.RigidBody.gravityScale = 0f;
        ledgePos = player.Controller.DetermineLedgePosition();

    }

    public override void Exit()
    {
        base.Exit();
        player.RigidBody.gravityScale = 3f;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector2 ClimbingPosition = new Vector2(ledgePos.x + 0.25f * player.Controller.GetFacingDirection(), ledgePos.y + 0.15f);
        player.RigidBody.MovePosition(ClimbingPosition);
        if (Time.time - startTime > 0.36f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
