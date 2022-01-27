using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyHostileSpottedState
{
    public EnemyMeleeAttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        if (detectedHostile == null)
        {
            base.FixedUpdate();
        }

        if (enemy.enemyAI.ShouldMelleeAttack(detectedHostile))
        {
            enemy.Core.Combat.Attack();
            DoChecks();
        } else {
            base.FixedUpdate();
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
