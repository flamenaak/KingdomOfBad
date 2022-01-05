using UnityEngine;
using System;

public class Player : Fighter
{
    public StateMachine StateMachine { get; private set; }

    #region PlayerStates
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerEvadeState EvadeState { get; private set; }
    public LiftState LiftState { get; private set; }
    public RiseState RiseState { get; private set; }
    public FloatState FloatState { get; private set; }
    public FallState FallState { get; private set; }
    public LandState LandState { get; private set; }
    public PlayerSlashState SlashState { get; private set; }
    public PlayerStabState StabState { get; private set; }
    public PlayerWindUpState WindUpState { get; private set; }
    public PlayerHangState HangState { get; private set; }
    public PlayerClimbState ClimbState { get; private set; }
    public PlayerDamagedState DamagedState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    #endregion

    #region SpeedForceVariables
    public float horJumpSpeed = 0.2f;
    public float WalkSpeed = 2f;
    public float RunSpeed = 5f;
    public float SprintSpeed = 7f;
    public float DashForce = 0.8f;
    public float SlashForce = 0.15f;
    public float StabForce = 1f;
    #endregion

    #region CooldownVariable
    public float DashCooldown = 2.0f;
    public bool canDashOrEvade = true;    
    public float SlashCooldown = 0.5f;
    public bool canSlash = true;

    public float StabCooldown = 1.5f;
    public bool canStab = true;

    // cooldown to allow fall from hanging state without continuos re-latching
    float hangCooldown = 0.5f;
    public bool CanHang = true;

    #endregion
    public Animator Anim { get; private set; }

    public Rigidbody2D RigidBody;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    public SpriteRenderer SpriteRenderer;
    public CharacterController2D Controller;

    public Core Core { get; set; }
    private CameraMovement camera;

    private Vector2 startPosition;


    public float xLedgeOffset = 0.43f;
    public float yLedgeOffset = 0f;
    public float xClimbOffset = 0.25f;
    public float yClimbOffset = 0.15f;

    public float pushRecoverySpeed = 0.2F;
    public float slashDamage = 1;
    public float attackRange = 0.5f;

    protected float immuneTime = 1.0F;
    protected float lastImmune;



    private void Awake()
    {
        StateMachine = new StateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        WalkState = new PlayerWalkState(this, StateMachine, "walk");
        RunState = new PlayerRunState(this, StateMachine, "run");
        DashState = new PlayerDashState(this, StateMachine, "dash");
        EvadeState = new PlayerEvadeState(this, StateMachine, "evade");
        LiftState = new LiftState(this, StateMachine, "lift");
        RiseState = new RiseState(this, StateMachine, "rise");
        FloatState = new FloatState(this, StateMachine, "float");
        FallState = new FallState(this, StateMachine, "fall");
        LandState = new LandState(this, StateMachine, "land");
        HangState = new PlayerHangState(this, StateMachine, "hang");
        SlashState = new PlayerSlashState(this, StateMachine, "slash");
        StabState = new PlayerStabState(this, StateMachine, "stab");
        WindUpState = new PlayerWindUpState(this, StateMachine, "windUp");
        ClimbState = new PlayerClimbState(this, StateMachine, "climb");
        DeathState = new PlayerDeathState(this, StateMachine, "death");
        DamagedState = new PlayerDamagedState(this, StateMachine, "damaged");

        Anim = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        Core = GetComponentInChildren<Core>();
        StateMachine.Initialize(IdleState);

        startPosition = transform.position;
    }

    private void Start()
    {
        camera = (CameraMovement)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CameraMovement");
        Core.Combat.Data.currentHealth = Core.Combat.Data.maxHealth;
        if (Controller == null)
            Debug.Log("no controller");
    }

    // logic
    private void Update()
    {
        StateMachine.CurrentState.Update();
        Core.Combat.CheckKnockback();
        Vector2 velocity = RigidBody.velocity;
        if ((velocity.x < -0.1f && transform.localScale.x > 0)
        || (velocity.x > 0.1f && transform.localScale.x < 0))
        {
            Core.Movement.Flip();
        }
        if (Core.Combat.damaged)
        {
            StateMachine.ChangeState(DamagedState);
        }
        else if (Core.Combat.Data.currentHealth <= 0)
        {
            StateMachine.ChangeState(DeathState);
        }

    }

    // physics
    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    public void OnDrawGizmos()
    {
        if (StateMachine != null && StateMachine.CurrentState == HangState)
        {
            Vector2 ledgePos = HangState.ledgePos;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(new Vector3(ledgePos.x, ledgePos.y), 0.2f);
            //Debug.Log("position of ledge " + ledgePos.x + " " + ledgePos.y);

        }

         //Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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

    public void StartHangCooldown()
    {
        CanHang = false;
        Invoke("clearHangCooldown", hangCooldown);
    }

    void clearHangCooldown() => CanHang = true;

    public void startDashCoolDown()
    {
        canDashOrEvade = false;
        Invoke("clearDashOrEvadeCooldown", DashCooldown);
    }

    void clearDashOrEvadeCooldown()
    {
        canDashOrEvade = true;
    }

    internal void Respawn()
    {
        transform.position = startPosition;
        StateMachine.ChangeState(IdleState);
    }

    private void Damage(float dmg)
    {
        Core.Combat.Damage(dmg);
    }
}