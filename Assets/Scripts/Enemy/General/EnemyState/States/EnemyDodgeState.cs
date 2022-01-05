using UnityEngine;

public class EnemyDodgeState : EnemyHostileSpottedState
{
    public EnemyDodgeState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void FixedUpdate()
    {
        if (Time.time - startTime < duration)
        {
            // dodge
            base.DoChecks();
        } else {
            base.FixedUpdate();
        }
    }
}