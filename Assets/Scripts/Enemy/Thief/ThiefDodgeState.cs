using UnityEngine;

public class ThiefDodgeState : EnemyDodgeState
{
    Vector2 target;
    Thief thief;
    public ThiefDodgeState(Thief enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 1.02f;
        thief = enemy;
    }

    public override void Exit()
    {
        base.Exit();
        Physics2D.IgnoreLayerCollision(enemy.gameObject.layer, LayerMask.NameToLayer("PlayerWeapon"), false);
        target = Vector2.zero;
        thief.SmokeBomb.Stop();
        thief.shouldEvade = false;
    }

    public override void Enter()
    {
        base.Enter();
        if (detectedHostile && detectedHostile.GetComponent<Player>().Core.CollisionSenses.IsGrounded())
        {
            target = new Vector2(detectedHostile.position.x, detectedHostile.position.y);
            enemy.RigidBody.velocity = Vector2.zero;
        }
        else
        {
            stateMachine.ChangeState(enemy.HostileSpottedState);
        }
    }

    public override void FixedUpdate()
    {
        DoChecks();
        if (Time.time - startTime <= duration)
        {
            if(Time.time - startTime >= 0.42f)
            {
                thief.SmokeBomb.Play();

            }
            // let the thief adapt a little based on the movement of the player
            if (Time.time - startTime <= duration / 2 && detectedHostile)
            {
                target = new Vector2(detectedHostile.position.x, detectedHostile.position.y);
            }
            return;
        }
        else if (target == Vector2.zero)
        {
            stateMachine.ChangeState(enemy.HostileSpottedState);
            return;
        }
        else // when animation if done and target is still available
        {
            Physics2D.IgnoreLayerCollision(enemy.gameObject.layer, LayerMask.NameToLayer("PlayerWeapon"), true);
            Vector2 dodgePos = enemy.enemyAI.DetermineDodgePosition(target);
            enemy.RigidBody.MovePosition(dodgePos);
            thief.CanLunge.StartCooldownTimer();
            thief.CanDodge.StartCooldownTimer();

            RaycastHit2D hit = Physics2D.Raycast(dodgePos,
                Vector2.right * enemy.Core.Movement.GetFacingDirection(),
                enemy.enemyAI.LineOfSight,
                enemy.enemyAI.WhatIsPlayer);

            if (!hit)
            {
                enemy.Core.Movement.Flip();
            }

            enemy.enemyAI.DetectHostile();
            if (enemy.enemyAI.ShouldMelleeAttack(detectedHostile) && !thief.shouldEvade)
            {
                stateMachine.ChangeState(enemy.MeleeAttackState);
                return;
            }
            else
            {
                stateMachine.ChangeState(enemy.HostileSpottedState);
                return;
            }
        }
    }
}