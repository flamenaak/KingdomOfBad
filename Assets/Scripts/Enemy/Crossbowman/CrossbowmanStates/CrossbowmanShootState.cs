using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowmanShootState : EnemyRangedAttackState
{
    Crossbowman crossbowman;
    public GameObject boltPrefab;
    public CrossbowmanShootState(Crossbowman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 0.3f;
        crossbowman = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        crossbowman.Fire();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Time.time - startTime >= duration)
        {
            crossbowman.reloaded = false;
            stateMachine.ChangeState(crossbowman.CrossbowmanReloadState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
