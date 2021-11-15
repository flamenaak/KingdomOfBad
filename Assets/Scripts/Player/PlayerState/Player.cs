using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set;}
    public PlayerIdleState IdleState {get; private set;}
    public PlayerWalkState WalkState {get; private set;}
    public PlayerRunState RunState {get; private set;}
    public PlayerDashState DashState { get; private set; }
    public PlayerEvadeState EvadeState { get; private set; }
    public PlayerJumpState JumpState {get; private set;}
    public LiftState LiftState { get; private set; }
    public RiseState RiseState { get; private set; }
    public FloatState FloatState { get; private set; }
    public FallState FallState { get; private set; }
    public LandState LandState { get; private set; }
    public PlayerSlashState SlashState { get; private set; }
    public PlayerStabState StabState { get; private set; }

    public PlayerWindUpState WindUpState { get; private set; }

    [SerializeField] public LayerMask layerMask;
    public Animator Anim {get; private set;}

    public Rigidbody2D RigidBody;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    public SpriteRenderer SpriteRenderer;
    public CharacterController2D Controller;
    private CameraMovement camera;

    public bool Crouch = false;
    public bool Jump = false;

    public float WalkSpeed = 2f;
    public float RunSpeed = 5f;
    public float SprintSpeed = 7f;

    public float DashForce = 1f;
    public float DashCooldown = 3f;
    public bool canDashOrEvade = true;

    public float SlashForce = 0.5f;
    public float SlashCooldown = 0.5f;
    public bool canSlash = true;

    public float StabForce = 3f;
    public float StabCooldown = 1.5f;
    public bool canStab = true;

    public int hitPoint;
    public int maxHitPoint;
    public float pushRecoverySpeed = 0.2F;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;


    protected float immuneTime = 1.0F;
    protected float lastImmune;

    protected Vector3 pushDirection;


    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        WalkState = new PlayerWalkState(this, StateMachine, "walk");
        RunState = new PlayerRunState(this, StateMachine, "run");
        DashState = new PlayerDashState(this, StateMachine, "dash");
        EvadeState = new PlayerEvadeState(this, StateMachine, "evade");
        JumpState = new PlayerJumpState(this, StateMachine, "jump");
        LiftState = new LiftState(this, StateMachine, "lift");
        RiseState = new RiseState(this, StateMachine, "rise");
        FloatState = new FloatState(this, StateMachine, "float");
        FallState = new FallState(this, StateMachine, "fall");
        LandState = new LandState(this, StateMachine, "land");
        SlashState = new PlayerSlashState(this, StateMachine, "slash");
        StabState = new PlayerStabState(this, StateMachine, "stab");
        WindUpState = new PlayerWindUpState(this, StateMachine, "windUp");

        Anim = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        StateMachine.Initialize(IdleState);
    }

    private void Start()
    {
        camera = (CameraMovement)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CameraMovement");

        if (Controller == null)
            Debug.Log("no controller");
    }

    // logic
    private void Update()
    {
        StateMachine.CurrentState.Update();
        Vector2 velocity = RigidBody.velocity;

        if ((velocity.x < -0.1f && transform.localScale.x > 0)
        || (velocity.x > 0.1f && transform.localScale.x < 0))
        {
            Controller.Flip();
        }
    }

    public void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
        }
    }

    public void Death()
    {

    }

    // physics
    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();     
    }

    public void OnLanding()
    {
        //StateMachine.ChangeState(IdleState);
    }


    private int ReadInputX()
    {
        float moveX = Input.GetAxis("Horizontal");
        int normalized = (int)Math.Round(moveX/Math.Abs(moveX));

        return  Math.Abs(moveX) > 0.3 ? normalized : 0;
    }

    public void startDashCoolDown()
    {
        canDashOrEvade = false;
        Invoke("clearDashOrEvadeCooldown", DashCooldown);
    }

    void clearDashOrEvadeCooldown()
    {
        canDashOrEvade = true;
    }

    public void startSlashCoolDown()
    {
        canSlash = false;
        Invoke("clearSlashCooldown", SlashCooldown);
    }

    void clearSlashCooldown()
    {
        canSlash = true;
    }

    public void startStabCoolDown()
    {
        canStab = false;
        Invoke("clearStabCooldown", StabCooldown);
    }

    void clearStabCooldown()
    {
        canStab = true;
    }

    public void startDashGravityEffect()
    {
        RigidBody.gravityScale = 0f;
        Invoke("clearDashGravityEffect", 0.5f);
    }

    void clearDashGravityEffect()
    {
        RigidBody.gravityScale = 3f;
    }

    public void Attack()
    {
       Collider2D hitEnemies =  Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);

        /*foreach(Collider2D enemy in hitEnemies)
        {
            //Debug.Log("We hit" + enemy.name);
        }*/
        if (hitEnemies.tag.Equals("Enemy"))
        {
            Debug.Log("We hit enemy");
        }
    }

    private void OnDrawGizmos()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}