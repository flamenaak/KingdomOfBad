using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefDeathState : EnemyDeathState
{
    Thief thief;
    public ThiefDeathState(Thief enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        thief = enemy;
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
        base.FixedUpdate();
        enemy.RigidBody.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();
    }
}
