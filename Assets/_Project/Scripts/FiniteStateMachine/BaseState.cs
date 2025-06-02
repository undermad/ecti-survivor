using UnityEngine;

namespace Explorer._Project.Scripts.FiniteStateMachine
{
    public abstract class BaseState : IState
    {
        protected readonly Animator _animator;
        
        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        protected static readonly int DashHash = Animator.StringToHash("Dash");

        protected const float CrossFadeDuration = 0.1f;

        protected BaseState(Animator animator)
        {
            _animator = animator;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}