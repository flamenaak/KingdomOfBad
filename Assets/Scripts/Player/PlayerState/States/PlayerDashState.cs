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
        player.DepleteStamina(1);
        player.startDashCoolDown();
        player.startDashGravityEffect();
        Physics2D.IgnoreLayerCollision(player.gameObject.layer, LayerMask.NameToLayer("EnemyWeapon"), true);
    }

    public override void Exit()
    {
        base.Exit();
        Physics2D.IgnoreLayerCollision(player.gameObject.layer, LayerMask.NameToLayer("EnemyWeapon"), false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();        
        Vector3 dashPosition = player.Core.Movement.DetermineDashDestination(player.transform);
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
