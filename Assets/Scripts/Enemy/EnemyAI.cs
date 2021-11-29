using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField]
    private bool applyKnockback, knockback;
    [SerializeField]
    private LayerMask layerMask;
    private float currentHealth, knockbackStart;
    private Player player;
    public CharacterController2D characterController;
    public Transform target;
    public float speed = 10f;
    public float nextWaypointDistance = 3f;
    public Transform attackPoint;
    public float slashDamage = 1;
    public float attackRange = 0.5f;

    public Transform EnemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.Find("Player").GetComponent<Player>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void Damage(float amount)
    {
        currentHealth -= amount; 
        if(applyKnockback && currentHealth > 0.0f)
        {
            Knockback();
        }
        if(currentHealth >= 0.0f)
        {
            Die();
            Debug.Log("Dead");
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layerMask);

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
        rb.velocity = new Vector2(knockbackSpeedX * characterController.GetFacingDirection(), knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if(Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndPath = true;
            return;
        }
        else
        {
            reachedEndPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01F)
        {
            EnemyGFX.localScale = new Vector3(10f, 10f, 1f);
            
        }
        else if (force.x <= -0.01f)
        {
            EnemyGFX.localScale = new Vector3(-10f, 10f, 1f);
        }
    }

    private void Update()
    {
        Attack();
        CheckKnockback();
    }

    private void Die()
    {
        rb.velocity = new Vector2(knockbackSpeedX * characterController.GetFacingDirection(), knockbackSpeedY);
    }
}
