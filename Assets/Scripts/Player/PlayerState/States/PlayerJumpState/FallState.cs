using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : PlayerAirState
{
    Vector2 startPosition;
    Vector2 endPosition;
    public FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        startPosition = player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckAirInput();
        if (player.Core.CollisionSenses.IsGrounded())
        {
            endPosition = player.transform.position;
            if (startPosition.y - endPosition.y >= player.allowedFallDistance)
            {
                player.Damage(player.fallDamage);
                stateMachine.ChangeState(player.StunState);
                return;
            }
            else if (startPosition.y - endPosition.y >= player.deathFallDistance)
            {
                player.Damage(100);
                return;
            }
            stateMachine.ChangeState(player.LandState);
        }

        CheckHang();
    }
}
