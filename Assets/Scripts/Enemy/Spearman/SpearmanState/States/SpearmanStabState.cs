using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanStabState : EnemyState
{
    Spearman spearman;
    public SpearmanStabState(Spearman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 0.2f;
        spearman = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.RigidBody.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
            Vector3 stabPosition = enemy.Core.Movement.DetermineStabPosition(enemy.transform);
            enemy.RigidBody.MovePosition(stabPosition);
            enemy.Core.Combat.Attack();
            if (Time.time - startTime > duration)
            {
                stateMachine.ChangeState(spearman.AfterStabState);
                return;
            }
        
    }

    public override void Update()
    {
        base.Update();
    }
}
