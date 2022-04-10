using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadAI
{
    public class Enemy : MonoBehaviour, IHasCombat
    {
        public bool aware;
        public Core Core;
        public Combat Combat => Core.Combat;

        public Animator Anim { get; private set; }
        public ProtoAI EnemyAI;
        public GameObject Awarness;
        public Rigidbody2D RigidBody;

        public StateMachine StateMachine {get; private set;}

        // Start is called before the first frame update
        public virtual void Awake()
        {
            Anim = GetComponent<Animator>();
            RigidBody = GetComponent<Rigidbody2D>();

            Core = GetComponentInChildren<Core>();
            if (EnemyAI == null) EnemyAI = GetComponentInChildren<ProtoAI>();
            StateMachine = new StateMachine();
        }

        public virtual void Start()
        {
            Core.Combat.Data.currentHealth = Core.Combat.Data.maxHealth;
            aware = false;
            
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Actor"), true);
            StateMachine.Initialize(new EnemyIdleState(this, StateMachine, "idle"));
        }

        private void Update()
        {
            if (Core.Combat.damaged)
            {
                // StateMachine.ChangeState(DamagedState);
            }
            else if (Core.Combat.Data.currentHealth <= 0)
            {
                // StateMachine.ChangeState(DeathState);
            }
            StateMachine.CurrentState.Update();
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
            GetComponent<BoxCollider2D>().enabled = false;
            RigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            RigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
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

        public void OnDrawGizmos()
        {
            if (EnemyAI != null && EnemyAI.CurrentBehaviour != null)
                Gizmos.DrawSphere(EnemyAI.CurrentBehaviour.Target.GetLocation(), 2);
        }
    }

}