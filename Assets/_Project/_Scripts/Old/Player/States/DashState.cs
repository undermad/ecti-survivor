using Explorer._Project.Scripts.FiniteStateMachine;
using Explorer._Project.Scripts.Player.Movement;
using Explorer._Project.Scripts.Utils;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.States
{
    public class DashState : BaseState
    {
        private readonly MovementController _movementController;
        private static readonly int DashHash = Animator.StringToHash(AnimationsStatesRegistry.Dash);

        public DashState(MovementController movementController, Animator animator, bool canStopAnimation) : base(animator, canStopAnimation)
        {
            _movementController = movementController;
        }
        
        public override void OnEnter()
        {
            Animator.CrossFade(DashHash, CrossFadeDuration);
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            _movementController.FixedTick();
        }

        public override void OnExit()
        {
        }
    }
}