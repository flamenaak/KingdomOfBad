using System.Collections.Generic;
using UnityEngine;

namespace BadAI
{
    public abstract class BadAI<T> : MonoBehaviour where T : BadAI.Enemy
    {
        protected BadTarget currentTarget;
        public List<BadTarget> Targets;
        public BadBehaviour<T> CurrentBehaviour;

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
            CurrentBehaviour.FixedUpdate();
        }

        public void Update()
        {
            CurrentBehaviour.Update();
        }
    }
}