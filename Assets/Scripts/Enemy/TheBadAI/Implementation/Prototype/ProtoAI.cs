using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace BadAI
{

    public class ProtoAI : BadAI<Enemy>
    {
        [SerializeField]
        private float patrolDistance;
        public BadPathfinder PathFinder;

        private IdleWalkerBehaviourProto idleWalkerBehaviour;

        private ProtoTargetProvider targetProvider;

        public void Awake()
        {

            entity = GetComponentInParent<Enemy>();
            if (PathFinder == null)
                PathFinder = GetComponentInChildren<BadPathfinder>();

            if (PathFinder == null)
                Debug.LogError($"Missing BadPathFinder on {entity.name}");
        }

        public void Start()
        {
            idleWalkerBehaviour = new IdleWalkerBehaviourProto(entity, new BadTarget()
            {
                Completed = false,
                GetLocation = () => entity.RigidBody.position,
                Type = BadTargetType.Travel,
                Priority = 1
            }, PathFinder);

            CurrentBehaviour = idleWalkerBehaviour;
        }

        public new void FixedUpdate()
        {
            base.FixedUpdate();
            var targetList = ScanForTargets();
            if (!targetList.Any()) return;

            var priority = targetList.Max(t => t.Priority);
            var chosenTarget = targetList.Find(t => t.Priority == priority);
            var choosenBehaviour = ChooseBehaviour(chosenTarget);
        }

        protected override BadBehaviour<Enemy> ChooseBehaviour(BadTarget target)
        {
            switch (target.Type)
            {
                case BadTargetType.Travel: 
                {
                    CurrentBehaviour = idleWalkerBehaviour;
                    break;
                }
            }
            CurrentBehaviour.Target = target;

            return CurrentBehaviour;
        }


        protected override List<BadTarget> ScanForTargets()
        {
            return targetProvider.GetTargets();
        }
    }
}
