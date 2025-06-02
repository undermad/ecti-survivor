using Explorer._Project.Scripts.Player;
using UnityEngine;

namespace Explorer._Project.Scripts.FiniteStateMachine.States
{
    public class DashState : BaseState
    {
        private MovementController _movementController;
        private bool _isDashing;
        
        public DashState(MovementController movementController, Animator animator) : base(animator)
        {
            _movementController = movementController;
        }

        public bool IsDashing => _isDashing;

        public override void OnEnter()
        {
            Debug.Log("Dash On Enter");
            _isDashing = true;
            _animator.CrossFade(DashHash, CrossFadeDuration);
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
            _isDashing = false;
        }
    }
}