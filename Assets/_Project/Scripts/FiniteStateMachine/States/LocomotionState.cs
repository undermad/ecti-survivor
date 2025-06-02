using Explorer._Project.Scripts.Player;
using UnityEngine;

namespace Explorer._Project.Scripts.FiniteStateMachine.States
{
    public class LocomotionState : BaseState
    {
        
        private MovementController _movementController;
        
        public LocomotionState(MovementController movementController, Animator animator) : base(animator)
        {
            _movementController = movementController;
        }

        public override void OnEnter()
        {
            Debug.Log("Locomotion On Enter");
            _animator.CrossFade(LocomotionHash, CrossFadeDuration);
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
            Debug.Log("Locomotion On Exit");
        }
    }
}