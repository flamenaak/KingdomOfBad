using UnityEngine;

namespace BadAI
{
    public class EnemySearchState : EnemyHostileSpottedState
    {
        public int FlipCountMax = 2;
        public float FlipPause = 1f;
        private int flipCountCurrent;
        private float lastFlipTime;
        public EnemySearchState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            flipCountCurrent = 0;
            lastFlipTime = startTime;
            enemy.Awarness.GetComponent<Animator>().Play("Base Layer.Searching", 0, 0);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void FixedUpdate()
        {
            DoChecks();

            if (flipCountCurrent >= FlipCountMax)
            {
                stateMachine.ChangeState(enemy.IdleState);
                return;
            }

            if (detectedHostile)
            {
                base.FixedUpdate();
                return;
            }
            else if (Time.time - lastFlipTime > FlipPause)
            {
                enemy.enemyAI.SearchForHostile();
                enemy.Core.Movement.Flip();
                flipCountCurrent++;
                lastFlipTime = Time.time;
            }

        }

        public override void Update()
        {
            base.Update();
        }
    }
}