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
        if (detectedHostile == null){
            base.FixedUpdate();
            return;
        }

        if (Mathf.Abs(thief.transform.position.x - detectedHostile.position.x) > 1)
        {
            if (thief.CanLunge)
            {
                Vector2 forceVct = new Vector2(thief.Core.Movement.GetFacingDirection(), 1);
                forceVct.Scale(thief.LungeForce);
                // lunge
                Debug.Log("lunging " + forceVct.ToString());
                thief.RigidBody.AddForce(forceVct);
                thief.CanLunge.StartCooldownTimer();
            }
        } else {
            if (proximityAttackStartTime == 0)
            {
                proximityAttackStartTime = Time.time;
            }
            else if (Time.time - proximityAttackStartTime > proximityAttackTimeMax)
            {
                thief.shouldEvade = true;
                stateMachine.ChangeState(thief.DodgeState);
                return;
            } 
            // do attack
        }
        if (Time.time - startTime >= duration){
            base.FixedUpdate();    
        }
    }

    public override void Update()
    {
        base.Update();
    }
}