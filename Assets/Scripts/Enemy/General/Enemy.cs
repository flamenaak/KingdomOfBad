using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter, IHasCombat
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


    public bool aware;
    public Core Core;
    
    public float slashDamage = 1;
    public float attackRange = 0.5f;
    public Animator Anim { get; private set; }
    public EnemyAI enemyAI;
    public GameObject Awarness;

    public Rigidbody2D RigidBody;
    public List<DecisionFunction_State_Tuple> DecisionFunctions {
        get {
           return new List<DecisionFunction_State_Tuple> { 
               new (enemyAI.ShouldDodge, DodgeState),
               new (enemyAI.ShouldRangeAttack, RangedAttackState),
               new (enemyAI.ShouldMelleeAttack, MeleeAttackState),
               new (enemyAI.ShouldChase, ChaseState)
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
        Core.Combat.Healthbar.transform.localScale -= new Vector3(Core.Combat.Data.maxHealth / 500, 0, 0);
        if (Core.Combat.Data.currentHealth > 0.0f)
        {
            Core.Combat.Knockback();
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
        Core.Combat.Healthbar.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public void Knockback()
    {
        Core.Combat.Knockback();
        RigidBody.velocity = new Vector2(Core.Combat.Data.knockbackSpeedX * -Core.Movement.GetFacingDirection(), Core.Combat.Data.knockbackSpeedY);
    }
}
