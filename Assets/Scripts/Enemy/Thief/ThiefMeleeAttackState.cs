using UnityEngine;

public class ThiefMeleeAttackState : EnemyMeleeAttackState
{
    private Thief thief;

    private float proximityAttackTimeMax = 0.5f;
    private float proximityAttackStartTime;
    public ThiefMeleeAttackState(Thief enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        thief = enemy;
        duration = 0.783f;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        proximityAttackStartTime = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        DoChecks();
        if (detectedHostile == null)
        {
            base.FixedUpdate();
            return;
        }

        if (Mathf.Abs(thief.transform.position.x - detectedHostile.position.x) > 1.5f)
        {
            if (thief.CanLunge)
            {
                stateMachine.ChangeState(thief.LungeState);
                return;
            }
        }
        else
        {
            thief.Core.Combat.Attack();
            if (proximityAttackStartTime == 0)
            {
                proximityAttackStartTime = Time.time;
            }
            else if (Time.time - proximityAttackStartTime > proximityAttackTimeMax && thief.CanDodge)
            {
                thief.shouldEvade = true;
                stateMachine.ChangeState(thief.DodgeState);
                return;
            }
        }
        if (Time.time - startTime >= duration)
        {
            stateMachine.ChangeState(thief.HostileSpottedState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}