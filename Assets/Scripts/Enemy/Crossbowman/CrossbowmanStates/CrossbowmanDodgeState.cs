using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowmanDodgeState : EnemyDodgeState
{
    Crossbowman crossbowman;
    public CrossbowmanDodgeState(Crossbowman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        crossbowman = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Core.Movement.Flip();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        crossbowman.RigidBody.velocity = (Vector2.right * enemy.Core.Movement.GetFacingDirection() * enemy.Core.Movement.Data.RunSpeed);
        crossbowman.CanDodge.StartCooldownTimer();
        if (crossbowman.enemyAI.Distance(detectedHostile) >= 8f)
        {
            enemy.Core.Movement.Flip();
            if (crossbowman.canShoot)
            {
                Debug.Log("Shooting");
                stateMachine.ChangeState(crossbowman.RangedAttackState);
            }
            else if(!crossbowman.canShoot)
            {
                Debug.Log("Reloading");
                stateMachine.ChangeState(crossbowman.CrossbowmanReloadState);
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
