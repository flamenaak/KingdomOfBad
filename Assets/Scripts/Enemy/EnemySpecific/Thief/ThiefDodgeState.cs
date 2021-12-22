using UnityEngine;

public class ThiefDodgeState : EnemyDodgeState
{
    Vector2 target;
    Thief thief;
    public ThiefDodgeState(Thief enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 1.017f;
        thief = enemy;
    }

    public override void Exit()
    {
        base.Exit();
        Physics2D.IgnoreLayerCollision(enemy.gameObject.layer, LayerMask.NameToLayer("Actor"), false);
        target = Vector2.zero;
        thief.StartDodgeCooldown();
    }

    public override void Enter()
    {
        base.Enter();
        if (!detectedHostile)
        {
            target = new Vector2(detectedHostile.position.x, detectedHostile.position.y);
        }
        else
        {
            stateMachine.ChangeState(enemy.HostileSpottedState);
        }
    }

    public override void FixedUpdate()
    {
        if (Time.time - startTime <= duration)
        {
            DoChecks();
            return;
        }
        else
        {
            Physics2D.IgnoreLayerCollision(enemy.gameObject.layer, LayerMask.NameToLayer("Actor"), true);
            enemy.RigidBody.MovePosition(enemy.enemyAI.DetermineDodgePosition(target));

            Vector2 enemyToTargetVct = enemy.RigidBody.position - target;
            // if vector.x and facing direction of enemy are in the same direction, multiplication gives positive number
            bool isEnemyFacingTarget = (enemyToTargetVct.x * enemy.Core.Movement.GetFacingDirection() > 0);
            if (!isEnemyFacingTarget)
            {
                enemy.Core.Movement.Flip();
            }

            enemy.enemyAI.DetectHostile();
            if (enemy.enemyAI.ShouldMelleeAttack(detectedHostile))
            {
                stateMachine.ChangeState(enemy.MeleeAttackState);
                return;
            }
            else
            {
                stateMachine.ChangeState(enemy.HostileSpottedState);
            }

        }
    }
}