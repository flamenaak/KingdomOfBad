using UnityEngine;

public class EnemyRangedAttackState : EnemyHostileSpottedState
{
    public EnemyRangedAttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}