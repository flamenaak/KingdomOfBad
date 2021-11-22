using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;                      // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsEnemy;                          // A mask determining what is an enemy
    public LayerMask GetEnemyLayerMask() => m_WhatIsEnemy;


    #region ColissionCheckHelpers
    [SerializeField]
    // A position marking where to check if the player is grounded.
    private Transform m_GroundCheck;
    [SerializeField]
    // A position marking where to check for ceilings
    private Transform m_CeilingCheck;
    [SerializeField]
    // position for checking if there is wall ahead (chest height)
    private Transform wallCheck;
    [SerializeField]
    // position for checking if there is a ledge ahead (tip of the head)
    private Transform ledgeCheck;
    [SerializeField]
    // position for checking area for slash
    public Transform attackPoint;

    #endregion

    private float wallCheckDistance = 0.5f;

    private float safetyOffsetX = 0.3f; // offset used for calculating a target position to prevent clipping

    const float k_GroundedRadius = .25f; // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public bool m_Grounded = true;            // Whether or not the player is grounded.

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;

    #region Debug
        public List<Vector2> stabTargets = new List<Vector2>();
    #endregion

    private void Awake()
    {

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public int ReadInputX()
    {
        int moveX = 0;
        moveX += Input.GetButton("Left") ? -1 : 0;
        moveX += Input.GetButton("Right") ? 1 : 0;
        return moveX;
    }

    public int ReadInputY()
    {
        int moveY = 0;
        // moveY += Input.GetButton("Up") ? 1 : 0;
        moveY += Input.GetButton("Down") ? -1 : 0;
        return moveY;
    }

    public bool GetJumpInput()
    {
        return Input.GetButton("Jump");
    }

    public bool GetDashOrEvadeInput()
    {
        return Input.GetButton("Dash");
    }

    public bool IsTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position,
            Vector2.right * GetFacingDirection(), wallCheckDistance, m_WhatIsGround);
    }

    public bool IsTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position,
            Vector2.right * GetFacingDirection(), wallCheckDistance, m_WhatIsGround);
    }

    public Vector2 DetermineLedgePosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(
            wallCheck.position,
            Vector2.right * GetFacingDirection(),
            wallCheckDistance,
            m_WhatIsGround);
        float xDist = xHit.distance;

        RaycastHit2D yHit = Physics2D.Raycast(
            ledgeCheck.position + (Vector3)(Vector2.right * wallCheckDistance * GetFacingDirection()),
            Vector2.down,
            ledgeCheck.position.y - wallCheck.position.y,
            m_WhatIsGround);

        float yDist = yHit.distance;
        return new Vector2(wallCheck.position.x + (GetFacingDirection() * xDist), ledgeCheck.position.y - yDist);
    }

    public Vector2 DetermineDashDestination(Player player)
    {
        Vector2 dashPosition = new Vector2(player.transform.position.x, player.transform.position.y)
            + (new Vector2(player.DashForce, 0.0005f) * GetFacingDirection());

        RaycastHit2D raycastHit2D = Physics2D.Raycast(player.transform.position,
            Vector2.right * GetFacingDirection(),
            Mathf.Abs(((Vector2)player.transform.position - dashPosition).x),
            (m_WhatIsEnemy | m_WhatIsGround));

        if (raycastHit2D)
        {
            if (raycastHit2D.collider.tag.Equals("Enemy"))
            {
                player.boxCollider2D.enabled = false;
                player.circleCollider2D.enabled = false;
                player.startDashGravityEffect();
                return dashPosition;
            }
            else
            {
                var distance = raycastHit2D.distance;
                distance -= safetyOffsetX;
                distance = distance > 0 ? distance : 0;
                return (Vector2)player.transform.position + (Vector2.right * GetFacingDirection() * distance);
            }
        }
        return dashPosition - (Vector2.right * GetFacingDirection() * safetyOffsetX);
    }

    public Vector2 DetermineEvadePosition(Player player)
    {
        Vector2 dashPosition = new Vector2(player.transform.position.x, player.transform.position.y) 
            - (GetFacingDirection() * new Vector2(player.DashForce/5, 0));

        RaycastHit2D raycastHit2D = Physics2D.Raycast(player.transform.position,
            Vector2.right * GetFacingDirection(),
            m_FacingRight ? dashPosition.x - player.transform.position.x : player.transform.position.x - dashPosition.x, 
            (m_WhatIsEnemy | m_WhatIsGround));

        if (raycastHit2D.collider != null)
        {
            dashPosition = raycastHit2D.point;
        }
        return dashPosition - (Vector2.right * GetFacingDirection() * safetyOffsetX);
    }

    public Vector2 DetermineSlashPosition(Player player)
    {
        Collider2D hitEnemies = Physics2D.OverlapCircle(attackPoint.position, player.attackRange,  m_WhatIsEnemy);
        Vector3 slashPosition = new Vector2(player.transform.position.x, player.transform.position.y) 
        + (new Vector2(player.SlashForce, 0) * GetFacingDirection());

        RaycastHit2D raycastHit2D = Physics2D.Raycast(player.transform.position, 
            Vector2.right * GetFacingDirection(), 
            m_FacingRight ? slashPosition.x - player.transform.position.x : player.transform.position.x - slashPosition.x, 
            (m_WhatIsEnemy | m_WhatIsGround));

        if (raycastHit2D.collider != null)
        {
            slashPosition = raycastHit2D.point;     
        }

        return slashPosition;
    }

    public Vector2 DetermineStabPosition(Player player)
    {
        Vector2 stabPosition = new Vector2(player.transform.position.x, player.transform.position.y) 
            + (new Vector2(player.StabForce, 0) * GetFacingDirection());
        
        RaycastHit2D raycastHit2D = Physics2D.Raycast(player.transform.position, 
            Vector2.right * GetFacingDirection(),
            m_FacingRight ? stabPosition.x - player.transform.position.x : player.transform.position.x - stabPosition.x,
            (m_WhatIsGround | m_WhatIsEnemy));

        if (raycastHit2D)
        {
            stabPosition = raycastHit2D.point;
        }
        stabTargets.Add(stabPosition - (Vector2.right * GetFacingDirection() * safetyOffsetX));
        return stabPosition - (Vector2.right * GetFacingDirection() * safetyOffsetX);
    }

    public int GetFacingDirection()
    {
        return this.m_FacingRight ? 1 : -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
        if (!IsTouchingWall())
        {
            Gizmos
                .DrawWireSphere(wallCheck.position + new Vector3(wallCheckDistance, 0) * GetFacingDirection(), k_GroundedRadius);
        }
        if (!IsTouchingLedge())
        {
            Gizmos
                .DrawWireSphere(ledgeCheck.position + new Vector3(wallCheckDistance, 0) * GetFacingDirection(), k_GroundedRadius);
        }
        else
        {
            // Gizmos.DrawWireSphere((Vector3)DetermineLedgePosition(), k_GroundedRadius);
        }
        foreach (Vector2 target in stabTargets)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target, 0.1f);
        }
    }

    public bool GetSlashInput()
    {
        return Input.GetButton("Slash");
    }

    public bool GetStabInput()
    {

        return Input.GetButtonUp("Stab");

    }

    public bool GetWindUpInput()
    {
        return Input.GetButtonDown("Stab");
    }

    public bool GetRespawnInput()
    {
        return Input.GetButtonDown("Respawn");
    }
}
