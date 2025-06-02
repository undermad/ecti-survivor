using UnityEngine;

namespace Explorer._Project.Scripts.FiniteStateMachine
{
    public abstract class BaseState : IState
    {
        protected readonly Animator Animator;
        protected const float CrossFadeDuration = 0.1f;

        protected BaseState(Animator animator)
        {
            Animator = animator;
        }

        public abstract void OnEnter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void OnExit();
    }
}