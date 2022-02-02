using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbowman : Enemy
{
    public CooldownComponent CanDodge;
    public CooldownComponent CanShoot;

    public bool shouldEvade = true;
    public bool canShoot = true;
    public GameObject bolt;
    public CrossbowmanReloadState CrossbowmanReloadState { get; set; }

    public override void Awake()
    {
        base.Awake();
        DodgeState = new CrossbowmanDodgeState(this, StateMachine, "dodge");
        RangedAttackState = new CrossbowmanShootState(this, StateMachine, "shoot");
        CrossbowmanReloadState = new CrossbowmanReloadState(this, StateMachine, "reload");
    }

    public void Fire()
    {
        Instantiate(bolt, base.Core.Combat.AttackPosition.position, Quaternion.identity);
        bolt.transform.position = base.Core.Combat.AttackPosition.transform.position;
        //bolt.GetComponent<Rigidbody2D>().AddForce(new Vector2(base.Core.Movement.GetFacingDirection() * 20f, 0f));       
    }
}
