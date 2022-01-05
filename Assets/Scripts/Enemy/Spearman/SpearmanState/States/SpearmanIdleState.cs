using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanIdleState : EnemyIdleState
{
    Spearman spearman;
    public SpearmanIdleState(Spearman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        spearman = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (enemy.aware && canSeePlayer)
        {
            if (spearman.spearmanAI.PlayerDistance() <= 5f)
            {
                stateMachine.ChangeState(spearman.PreSlashState);
                return;
            }
            else if (spearman.spearmanAI.PlayerDistance() >= 5f && spearman.spearmanAI.PlayerDistance() <= 10f)
            {
                stateMachine.ChangeState(spearman.WindUpState);
                return;
            }
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void Update()
    {
        base.Update();
    }

}
