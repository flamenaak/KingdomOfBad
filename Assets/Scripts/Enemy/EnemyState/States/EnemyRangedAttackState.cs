using UnityEngine;

public class EnemyRangedAttackState : EnemyState
{
    public EnemyRangedAttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}