using UnityEngine;

public class ThiefMeleeAttackState : EnemyMeleeAttackState
{
    private bool lunged = false;
    private Thief thief;

    private float proximityAttackTimeMax = 0.5f;
    private float proximityAttackStartTime;
    public ThiefMeleeAttackState(Thief enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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
        lunged = false;
        proximityAttackStartTime = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        DoChecks();
        if (thief.enemyAI.ShouldMelleeAttack(detectedHostile))
        {
            if (Mathf.Abs(enemy.transform.position.x - detectedHostile.position.x) > 1)
            {
                if (!lunged)
                {
                    // lunge
                    enemy.RigidBody.AddForce(thief.LungeForce * new Vector2(thief.Core.Movement.GetFacingDirection(), 1));
                    lunged = true;
                } else if (thief.Core.CollisionSenses.IsGrounded())
                {
                    lunged = false;
                }
            } else {
                if (proximityAttackStartTime == 0)
                {
                    proximityAttackStartTime = Time.time;
                }
                else if (Time.time - proximityAttackStartTime > proximityAttackTimeMax)
                {
                    thief.evadeDodge = true;
                    stateMachine.ChangeState(thief.DodgeState);
                    return;
                } 
                // do attack
            }            
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