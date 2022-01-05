using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    protected Enemy enemy;
    protected float duration;
    protected float searchDuration;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;
    protected bool isReachingEdge;

    protected Transform detectedHostile;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Anim.SetBool(animBoolName, true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Anim.SetBool(animBoolName, false);
    }

    // every frame
    /**
    * stuff
    */
    public override void Update()
    {
        base.Update();
    }

    // every fixed update
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        DoChecks();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        detectedHostile = enemy.enemyAI.DetectHostile();
        
        isTouchingWall = enemy.Core.CollisionSenses.IsTouchingWall();
        isTouchingLedge = enemy.Core.CollisionSenses.IsTouchingLedge();
        isReachingEdge = enemy.Core.CollisionSenses.IsReachingEdge();
    }
}
