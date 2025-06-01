using UnityEngine;

namespace _Project.Scripts.StateMachine.States
{
    public class LocomotionState : BaseState
    {
        public LocomotionState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
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
            _playerController.HandleMovement();
        }

        public override void OnExit()
        {
            Debug.Log("Locomotion On Exit");
        }
    }
}