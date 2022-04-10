using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour, IHasCombat
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
    public PlayerSlashState2 SlashState2 { get; private set; }
    public PlayerStabState StabState { get; private set; }
    public PlayerWindUpState WindUpState { get; private set; }
    public PlayerHangState HangState { get; private set; }
    public PlayerClimbState ClimbState { get; private set; }
    public PlayerDamagedState DamagedState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    public PlayerStunState StunState { get; private set; }
    public PlayerClimbMoveState ClimbMoveState { get; private set; }
    public PlayerClimbIdleState ClimbIdleState { get; private set; }
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

    float StaminaCooldown = 1.5f;
    private WaitForSeconds staminaRegenTick = new WaitForSeconds(0.1f);
    private Coroutine staminaRegen;
    public bool canRegen = true;

    #endregion
    public Animator Anim { get; private set; }

    public Rigidbody2D RigidBody;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    public SpriteRenderer SpriteRenderer;
    public CharacterController2D Controller;
    public CooldownComponent CanInteract;
    public ParticleSystem LandDust;
    public ParticleSystem Dust;
    public GameObject InteractButton;
    public Core Core { get; set; }
    public Combat Combat => Core.Combat;

    private CameraMovement camera;

    private Vector2 startPosition;
    public Transform carryPoint;
    Transform carriable;
    public bool isCarrying;
    public bool isAtTop;
    public float fallDamage = 2f;
    public float allowedFallDistance = 4f;
    public float deathFallDistance = 5f;

    public float stunLength = 1f;

    public float xLedgeOffset = 0.43f;
    public float yLedgeOffset = 0f;
    public float xClimbOffset = 0.25f;
    public float yClimbOffset = 0.15f;
    
    // -- helper varuable for handling carrying stuff
    private int oldLayer;
    private Transform oldParent;

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
        SlashState2 = new PlayerSlashState2(this, StateMachine, "slash2");
        StabState = new PlayerStabState(this, StateMachine, "stab");
        WindUpState = new PlayerWindUpState(this, StateMachine, "windUp");
        ClimbState = new PlayerClimbState(this, StateMachine, "climb");
        DeathState = new PlayerDeathState(this, StateMachine, "death");
        DamagedState = new PlayerDamagedState(this, StateMachine, "damaged");
        StunState = new PlayerStunState(this, StateMachine, "stunned");
        ClimbMoveState = new PlayerClimbMoveState(this, StateMachine, "climbMove");
        ClimbIdleState = new PlayerClimbIdleState(this, StateMachine, "climbIdle");
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
        isAtTop = false;
        if (Controller == null)
            Debug.Log("no controller");
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Enemy"), true);
        InteractButton.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 2);
    }

    // logic
    private void Update()
    {
        StateMachine.CurrentState.Update();
        if (Core.CollisionSenses.IsTouchingCarriable() && !isCarrying)
        {
            InteractButton.GetComponent<Animator>().SetBool("touching", true);
        }
        if (!Core.CollisionSenses.IsTouchingCarriable())
        {
            InteractButton.GetComponent<Animator>().SetBool("touching", false);
        }
        InteractButton.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 2);
        Vector2 velocity = RigidBody.velocity;
        if ((velocity.x < -0.5f && Core.Movement.GetFacingDirection() > 0 && Controller.ReadInputX() < 0)
        || (velocity.x > 0.5f && Core.Movement.GetFacingDirection() < 0 && Controller.ReadInputX() > 0))
        {
            Core.Movement.Flip();
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
        Core.Combat.DamageCollider.enabled = true;
    }

    public bool HaveEnoughStamina()
    {
        return Core.Combat.Data.currentHealth >= 1.0;    
    }

    void ResetRegen()
    {
        if (staminaRegen != null)
        {
            StopCoroutine(staminaRegen);
        }
        staminaRegen = StartCoroutine(RegenStamina());
    }

    public void DepleteStamina(float amount)
    {
        Core.Combat.Data.currentHealth -= amount;
        if(Core.Combat.Data.currentHealth < 0.0f)
        {
            Core.Combat.Data.currentHealth = 0.0f;
        }
        Core.Combat.Healthbar.GetComponent<Slider>().value = Core.Combat.Data.currentHealth;
    
        ResetRegen();
    }

    private IEnumerator RegenStamina()
    {
        if (canRegen)
        {
            yield return new WaitForSeconds(StaminaCooldown);

            while (Core.Combat.Data.currentHealth < Core.Combat.Data.maxHealth)
            {
                if (Core.Combat.Data.currentHealth == Core.Combat.Data.maxHealth)
                {
                    yield return null;
                }
                else
                {
                    Core.Combat.Data.currentHealth += Core.Combat.Data.maxHealth / 100;
                    Core.Combat.Healthbar.GetComponent<Slider>().value = Core.Combat.Data.currentHealth;
                    yield return staminaRegenTick;
                }
            }
            staminaRegen = null;
        }
        else 
        {
            yield return null;
        }
    }

    public void Damage(float amount)
    {
        Core.Combat.Damage(amount);
        Core.Combat.Healthbar.GetComponent<Slider>().value = Core.Combat.Data.currentHealth;
        ResetRegen();
        if (Core.Combat.Data.currentHealth > 0.0f)
        {
            Core.Combat.damaged = true;
            StateMachine.ChangeState(DamagedState);
        }
        else if (Core.Combat.Data.currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Die()
    {
        canRegen = false;
        Core.Combat.Die();
        StateMachine.ChangeState(DeathState);
    }

    public void Knockback(Transform attacker, float amount)
    {
        Core.Combat.Knockback();
        if(attacker.position.x < this.transform.position.x)
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
        else if(attacker.position.x > this.transform.position.x)
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


    public void PickUp()
    {
            if (Core.CollisionSenses.IsTouchingCarriable().GetComponent<SpriteRenderer>().sortingLayerName.Equals("Enemy") || 
                Core.CollisionSenses.IsTouchingCarriable().GetComponent<SpriteRenderer>().sortingLayerName.Equals("Wall"))
            isCarrying = true;
            canSlash = false;
            canStab = false;
            canDashOrEvade = false;
            carriable = Core.CollisionSenses.IsTouchingCarriable();
            oldParent= carriable.parent;
            carriable.transform.SetParent(carryPoint);
            oldLayer = carriable.gameObject.layer;
            carriable.gameObject.layer = this.gameObject.layer;            
            carriable.transform.position = carryPoint.transform.position;
            carriable.GetComponent<BoxCollider2D>().enabled = false;
            if (carriable.GetComponent<Rigidbody2D>() != null)
            {
                carriable.GetComponent<Rigidbody2D>().isKinematic = true;
            }
    }

    public void Drop()
    {
        isCarrying = false;
        canSlash = true;
        canStab = true;
        canDashOrEvade = true;
        carriable.gameObject.layer = oldLayer;
        carriable.transform.SetParent(oldParent);
        carriable.GetComponent<BoxCollider2D>().enabled = true;
        if (carriable.GetComponent<Rigidbody2D>() != null)
        {
            carriable.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    public void PickDropHandling()
    {
        //Picking up interactable
        if (Input.GetButton("Interact") && Core.CollisionSenses.IsTouchingCarriable() != null && !isCarrying && CanInteract)
        {
            CanInteract.StartCooldownTimer();
            InteractButton.GetComponent<Animator>().SetBool("pressed", true);
            PickUp();
        }
        //Dropping interactable
        else if (Input.GetButton("Interact") && isCarrying && CanInteract)
        {
            CanInteract.StartCooldownTimer();
            InteractButton.GetComponent<Animator>().SetBool("pressed", false);
            Drop();
        }
    }
}