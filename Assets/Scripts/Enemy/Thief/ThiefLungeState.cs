using UnityEngine;

public class ThiefLungeState : EnemyHostileSpottedState
{
    public Thief thief;
    private Vector2 startLocation;

    public ThiefLungeState(Thief enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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
        startLocation = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
    }

    public override void Exit()
    {
        base.Exit();
        thief.CanLunge.StartCooldownTimer();
    }

    public override void FixedUpdate()
    {
        float travelDistance = Vector2.Distance(startLocation, thief.transform.position);
        bool finished = travelDistance >= thief.Core.Movement.Data.StabDistance;

        if (detectedHostile == null || finished || Vector2.Distance(thief.transform.position, detectedHostile.position) <= 1)
        {
            base.FixedUpdate();
        }
        else 
        {
            Vector2 target = thief.Core.Movement.DetermineStabPosition(enemy.transform);
            thief.RigidBody.MovePosition(target);
            DoChecks();
        }
    }

    public override void Update()
    {
        base.Update();
    }
}