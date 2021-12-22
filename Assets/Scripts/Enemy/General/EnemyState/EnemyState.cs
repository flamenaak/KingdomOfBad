using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    protected Enemy enemy;
    protected float duration;
    protected float searchDuration;
    protected bool canSeePlayer;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;
    protected bool isReachingEdge;

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

        if(canSeePlayer)
        {
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        canSeePlayer = enemy.enemyAI.CanSeePlayer();
        isTouchingWall = enemy.Core.CollisionSenses.IsTouchingWall();
        isTouchingLedge = enemy.Core.CollisionSenses.IsTouchingLedge();
        isReachingEdge = enemy.Core.CollisionSenses.IsReachingEdge();
    }
}
