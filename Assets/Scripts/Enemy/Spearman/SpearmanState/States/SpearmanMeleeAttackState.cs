using UnityEngine;

public class SpearmanMeleeAttackState : EnemyMeleeAttackState
{
    Spearman spearman;

    public SpearmanMeleeAttackState(Spearman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.spearman = enemy;
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
        if (spearman.spearmanAI.ShouldSlash(detectedHostile))
        {
            stateMachine.ChangeState(spearman.PreSlashState);
            return;
        }
        else if (spearman.spearmanAI.ShouldStab(detectedHostile))
        {
            stateMachine.ChangeState(spearman.WindUpState);
            return;
        }
        else 
        {
            base.FixedUpdate();
        }
    }

    public override void Update()
    {
        base.Update();
    }
}