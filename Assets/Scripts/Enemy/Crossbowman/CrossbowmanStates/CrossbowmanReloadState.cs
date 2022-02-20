using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowmanReloadState : EnemyRangedAttackState
{
    Crossbowman crossbowman;
    public CrossbowmanReloadState(Crossbowman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 2;
        crossbowman = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        crossbowman.CanShoot.StartCooldownTimer();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (crossbowman.enemyAI.ShouldDodge(detectedHostile))
        {
            stateMachine.ChangeState(crossbowman.DodgeState);
        }
        if(Time.time - startTime >= duration)
        {
            crossbowman.reloaded = true;
            stateMachine.ChangeState(crossbowman.HostileSpottedState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
