using Explorer._Project.Scripts.FiniteStateMachine;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.States
{
    public class DashState : BaseState
    {
        private readonly MovementController _movementController;
        private static readonly int DashHash = Animator.StringToHash("Dash");

        public DashState(MovementController movementController, Animator animator) : base(animator)
        {
            _movementController = movementController;
        }
        
        public override void OnEnter()
        {
            Debug.Log("Dash On Enter");
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
            Debug.Log("Dash On Exit");
        }
    }
}