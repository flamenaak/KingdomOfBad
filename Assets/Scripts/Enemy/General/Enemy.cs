using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    public StateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState {get ;set;}
    public EnemyMoveState MoveState {get; set;}
    public EnemyDeathState DeathState { get; set; }
    public EnemyDamagedState DamagedState { get; set; }

    public bool aware;
    public Core Core;
    
    public float slashDamage = 1;
    public float attackRange = 0.5f;
    public Animator Anim { get; private set; }
    public EnemyAI enemyAI;
    public GameObject Awarness;

    public Rigidbody2D RigidBody;
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
    }

    void Start()
    {
        Core.Combat.Data.currentHealth = Core.Combat.Data.maxHealth;
        aware = false;
        StateMachine.Initialize(IdleState);
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

    private void Damage(float dmg)
    {
        Core.Combat.Damage(dmg);
    }

}
