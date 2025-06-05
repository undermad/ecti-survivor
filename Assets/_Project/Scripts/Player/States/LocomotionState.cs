using Explorer._Project.Scripts.FiniteStateMachine;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.States
{
    public class LocomotionState : BaseState
    {
        
        private readonly MovementController _movementController;
        private static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");

        
        public LocomotionState(MovementController movementController, Animator animator) : base(animator)
        {
            _movementController = movementController;
        }

        public override void OnEnter()
        {
            Debug.Log("Locomotion On Enter");
            Animator.CrossFade(LocomotionHash, CrossFadeDuration);
        }

        public override void Update()
        {
            Animator.SetFloat(SpeedHash, _movementController.GetCurrentSpeed());

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