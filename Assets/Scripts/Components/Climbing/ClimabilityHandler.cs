using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimabilityHandler : MonoBehaviour
{
    private ICanClimb parent;
    public LayerMask WhatIsClimable;

    private void Start()
    {
        parent = GetComponentInParent<ICanClimb>();
        if (parent == null)
            Debug.LogError("Parrent is not implementing ICanClimb interface");
    }

    void FixedUpdate()
    {
        var climability = isTouchingClimable();
        if (climability && parent.ClimbInput != 0)
        {
            parent.StartClimbing(parent.ClimbInput);            
        }
    }

    public Climability isTouchingClimable()
    {
        Collider2D climable = Physics2D.OverlapBox(this.transform.position,
        new Vector2(1, 1), 0, WhatIsClimable);
        if (climable != null)
        {
            return climable.GetComponentInParent<Climability>();
        }
        else
        {
            return null;
        }
    }
}
