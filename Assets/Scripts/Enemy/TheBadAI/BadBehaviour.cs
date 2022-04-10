using System.Collections.Generic;
using UnityEngine;
using BadAI;

public abstract class BadBehaviour<T> where T : BadAI.Enemy
{
    protected T entity;
    public BadTarget Target;
    protected BadAI.BadPathfinder pathFinder;

    public BadBehaviour(T entity, BadTarget target, BadAI.BadPathfinder pathFinder)
    {
        this.entity = entity;
        this.Target = target;
        this.pathFinder = pathFinder;

    }
    public abstract void FixedUpdate();
    public abstract void Update();
}