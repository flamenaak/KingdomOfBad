using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    public EnemyIdleState IdleState {get ;set;}
    public EnemyMoveState MoveState {get; set;}


    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField]
    private bool applyKnockback, knockback;
    [SerializeField]
    private LayerMask layerMask;
    private float currentHealth, knockbackStart;
    public Core Core;
    
    public float slashDamage = 1;
    public float attackRange = 0.5f;
    public Animator Anim { get; private set; }
    public EnemyAI enemyAI;


    public Rigidbody2D RigidBody;
    // Start is called before the first frame update
    private void Awake()
    {
        Anim = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();

        StateMachine = new StateMachine();
        IdleState = new EnemyIdleState(this, StateMachine, "idle");
        MoveState = new EnemyMoveState(this, StateMachine, "walk");
    }

    void Start()
    {
        currentHealth = maxHealth;
        StateMachine.Initialize(IdleState);
    }

    private void Damage(float amount)
    {
        currentHealth -= amount;
        if (applyKnockback && currentHealth > 0.0f)
        {
            Knockback();
        }
        if (currentHealth >= 0.0f)
        {
            Die();
            Debug.Log("Dead");
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Core.Movement.AttackPosition.position, attackRange, layerMask);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.SendMessage("ReceiveDamage", slashDamage);
            Debug.Log("Hitting player");
        }
    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        RigidBody.velocity = new Vector2(knockbackSpeedX * Core.Movement.GetFacingDirection(), knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            RigidBody.velocity = new Vector2(0.0f, RigidBody.velocity.y);
        }
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
        Attack();
        CheckKnockback();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    private void Die()
    {
        RigidBody.velocity = new Vector2(knockbackSpeedX * Core.Movement.GetFacingDirection(), knockbackSpeedY);
    }
}
