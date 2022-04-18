using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHasCombat, IHasCollider
{
    public StateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState {get ;set;}
    public EnemyMoveState MoveState {get; set;}
    public EnemyDeathState DeathState { get; set; }
    public EnemyDamagedState DamagedState { get; set; }
    public EnemyMeleeAttackState MeleeAttackState { get; set; }
    public EnemyChargeState ChaseState { get; set; }
    public EnemyHostileSpottedState HostileSpottedState { get; set; }
    public EnemyRangedAttackState RangedAttackState { get; set; }
    public EnemyDodgeState DodgeState { get; set; }
    public EnemySearchState SearchState { get; set; }

    public bool isInteractableOnDeath = false;
    public bool aware;
    public Core Core;
    public Combat Combat => Core.Combat;

    public Animator Anim { get; private set; }
    public EnemyAI enemyAI;
    public GameObject Awarness;

    public Rigidbody2D RigidBody;
    public virtual List<DecisionFunction_State_Tuple> DecisionFunctions {
        get {
           return new List<DecisionFunction_State_Tuple> { 
               new DecisionFunction_State_Tuple(enemyAI.ShouldDodge, DodgeState),
               new DecisionFunction_State_Tuple(enemyAI.ShouldRangeAttack, RangedAttackState),
               new DecisionFunction_State_Tuple(enemyAI.ShouldMelleeAttack, MeleeAttackState),
               new DecisionFunction_State_Tuple(enemyAI.ShouldChase, ChaseState)
               };
        }
    }

    // Start is called before the first frame update
    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        Core = GetComponentInChildren<Core>();

        StateMachine = new StateMachine();
        IdleState = new EnemyIdleState(this, StateMachine, "idle");
        MoveState = new EnemyMoveState(this, StateMachine, "move");
        DeathState = new EnemyDeathState(this, StateMachine, "death");
        DamagedState = new EnemyDamagedState(this, StateMachine, "damaged");

        MeleeAttackState = new EnemyMeleeAttackState(this, StateMachine, "melee");
        ChaseState = new EnemyChargeState(this, StateMachine, "charge");
        HostileSpottedState = new EnemyHostileSpottedState(this, StateMachine, "hostileSpotted");
        RangedAttackState = new EnemyRangedAttackState(this, StateMachine, "ranged");
        DodgeState = new EnemyDodgeState(this, StateMachine, "dodge");
        SearchState = new EnemySearchState(this, StateMachine, "idle");
    }

    public virtual void Start()
    {
        Core.Combat.Data.currentHealth = Core.Combat.Data.maxHealth;
        aware = false;
        StateMachine.Initialize(IdleState);
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Actor"), true);
    }

    private void Update()
    {
        //RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        StateMachine.CurrentState.Update();
        if(Core.Combat.damaged)
        {
            StateMachine.ChangeState(DamagedState);
        }
        else if (Core.Combat.Data.currentHealth <= 0)
        {
            StateMachine.ChangeState(DeathState);
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    public void Damage(float amount)
    {
        Core.Combat.Damage(amount);
        if (Core.Combat.Data.currentHealth > 0.0f)
        {
            Core.Combat.damaged = true;
        }
        else if (Core.Combat.Data.currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Core.Combat.Die();
    }

    public void Knockback(Transform attacker, float amount)
    {
        Core.Combat.Knockback();
        if (attacker.position.x < this.transform.position.x)
        {
            if (Core.Movement.IsFacingRight)
            {
                RigidBody.velocity = new Vector2(amount * Core.Movement.GetFacingDirection(), Core.Combat.Data.knockbackSpeedY);
            }
            else
            {
                RigidBody.velocity = new Vector2(amount * -Core.Movement.GetFacingDirection(), Core.Combat.Data.knockbackSpeedY);
            }
        }
        else if (attacker.position.x > this.transform.position.x)
        {
            if (Core.Movement.IsFacingRight)
            {
                RigidBody.velocity = new Vector2(amount * -Core.Movement.GetFacingDirection(), Core.Combat.Data.knockbackSpeedY);
            }
            else
            {
                RigidBody.velocity = new Vector2(amount * Core.Movement.GetFacingDirection(), Core.Combat.Data.knockbackSpeedY);
            }
        }
    }

    public BoxCollider2D GetBodyCollider2D()
    {
        return this.gameObject.GetComponent<BoxCollider2D>();
    }

    public Collider2D GetGroundCheckCollider2D()
    {
        return GetBodyCollider2D();
    }
}
