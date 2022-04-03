using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleWalkerBehaviourProto : BadBehaviour<Enemy>
{
    CooldownComponent hasWaited = new CooldownComponent(0f);    // true if ready to move
    System.Random r = new System.Random();
    List<BadTarget> pathToGoal = new List<BadTarget>();

    public IdleWalkerBehaviourProto(Enemy entity, BadTarget target) : base(entity, target)
    {
        states.Add(new BadAI.EnemyMoveState(entity, stateMachine, "move"));
        states.Add(new BadAI.EnemyIdleState(entity, stateMachine, "idle"));
    }

        public override void Exit()
    {
    }

    public override void Start()
    {
    }


    public override void FixedUpdate()
    {
        // stay idle
        if (!hasWaited && stateMachine.CurrentState.GetType() == typeof(BadAI.EnemyIdleState)) return;

        if (hasWaited)
        {
            hasWaited = new CooldownComponent((float)(r.Next(1,3) + r.NextDouble()));
            hasWaited.StartCooldownTimer();
        }
    }
    public override void Update()
    {
    }

    private void FindPath(BadTarget destination)
    {
        RaycastHit2D obstacle = Physics2D.Linecast(entity.transform.position, destination.GetLocation());
    }

    public override void StateUpdate()
    {
        stateMachine.CurrentState.Update();
    }
    public override void StateFixedUpdate()
    {
        stateMachine.CurrentState.Update();
    }
}