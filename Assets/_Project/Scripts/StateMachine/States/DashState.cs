using UnityEngine;

namespace _Project.Scripts.StateMachine.States
{
    public class DashState : BaseState
    {
        public DashState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }


        public override void OnEnter()
        {
            Debug.Log("Dash On Enter");
            _animator.CrossFade(DashHash, CrossFadeDuration);
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
            Debug.Log("Dash On Exit");
        }
    }
}