using System.Collections.Generic;
using UnityEngine;

public abstract class BadAI<T> : MonoBehaviour where T : Enemy
{
    protected BadTarget currentTarget;
    public List<BadTarget> Targets;
    protected BadBehaviour<T> currentBehaviour;
    
    // have couple of defined behaviours instead of a list of anonymous ones
    // protected List<BadBehaviour<T>> behaviours;
    protected T entity;

    public LayerMask WhatIsPlayer;
    public float LineOfSight;
    [SerializeField]
    protected Transform playerCheck;

    protected abstract List<BadTarget> ScanForTargets();
    protected abstract BadBehaviour<T> ChooseBehaviour(BadTarget target);

    public void FixedUpdate()
    {
        currentBehaviour.FixedUpdate();
        currentBehaviour.StateFixedUpdate();
    }

    public void Update()
    {
        currentBehaviour.Update();
        currentBehaviour.StateUpdate();
    }
}