using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CrossbowmanDeathState : EnemyDeathState
{
    Crossbowman crossbowman;
    public CrossbowmanDeathState(Crossbowman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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
        crossbowman.stackability.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.RigidBody.velocity = Vector2.zero;
    }
}