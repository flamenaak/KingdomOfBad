using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Enemy
{
    [SerializeField]
    private float dodgeCooldown = 2f;
    public bool canDodge = true;

    public new void Awake()
    {
        base.Awake();
        DodgeState = new ThiefDodgeState(this, StateMachine, "dodge");
    }

    private void resetDodge()
    {
        canDodge = true;
    }

    public void StartDodgeCooldown()
    {
        canDodge = false;
        Invoke("resetDodge", dodgeCooldown);
    }
}