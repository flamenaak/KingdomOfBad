using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField]
    private bool applyKnockback, knockback;
    [SerializeField]
    private LayerMask layerMask;
    private float currentHealth, knockbackStart;
    public Core Core;
    public float speed = 10f;
    public float nextWaypointDistance = 3f;
    public float slashDamage = 1;
    public float attackRange = 0.5f;
    public Animator Anim { get; private set; }
    public EnemyAI enemyAI;

    public Transform EnemyGFX;

    int currentWaypoint = 0;
    bool reachedEndPath = false;

    public Rigidbody2D RigidBody;
    // Start is called before the first frame update
    private void Awake()
    {
        Anim = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
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
        Attack();
        CheckKnockback();
    }

    private void Die()
    {
        RigidBody.velocity = new Vector2(knockbackSpeedX * Core.Movement.GetFacingDirection(), knockbackSpeedY);
    }
}
