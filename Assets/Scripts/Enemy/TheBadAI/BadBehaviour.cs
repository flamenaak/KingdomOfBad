using System.Collections.Generic;
using UnityEngine;

public abstract class BadBehaviour<T> where T : Enemy
{
    protected T entity;
    public BadTarget Target;
    protected StateMachine stateMachine;
    protected List<State> states;

    public BadBehaviour(T entity, BadTarget target)
    {
        this.entity = entity;
        this.Target = target;

        stateMachine = new StateMachine();
        states = new List<State>();
    }

    public abstract void Start();
    public abstract void FixedUpdate();
    public abstract void Update();
    public abstract void StateUpdate();
    public abstract void StateFixedUpdate();
    public abstract void Exit();
}