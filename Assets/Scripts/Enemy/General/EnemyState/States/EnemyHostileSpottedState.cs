using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHostileSpottedState : EnemyState
{
    public EnemyHostileSpottedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!detectedHostile)
            stateMachine.ChangeState(enemy.SearchState);

        if (enemy.enemyAI.ShouldDodge(detectedHostile))
        {
            stateMachine.ChangeState(enemy.DodgeState);
            return;
        } else if (enemy.enemyAI.ShouldRangeAttack(detectedHostile))
        {
            stateMachine.ChangeState(enemy.RangedAttackState);
            return;
        } else if (enemy.enemyAI.ShouldMelleeAttack(detectedHostile))
        {
            stateMachine.ChangeState(enemy.MeleeAttackState);
            return;
        } else if (enemy.enemyAI.ShouldChase(detectedHostile))
        {
            stateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        stateMachine.ChangeState(enemy.SearchState);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
