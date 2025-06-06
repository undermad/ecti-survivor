using System;
using Explorer._Project.Scripts.Utils.AnimationsUtils;
using Unity.VisualScripting;
using UnityEngine;

namespace Explorer._Project.Scripts.FiniteStateMachine
{
    public abstract class BaseState : IState
    {
        protected readonly Animator Animator;
        protected const float CrossFadeDuration = 0.1f;
        private readonly bool CanStopCurrentAnimation;

        protected BaseState(Animator animator, bool canStopCurrentAnimation)
        {
            Animator = animator;
            CanStopCurrentAnimation = canStopCurrentAnimation;
        }
        
        public bool CanStopAnimation()
        {
            return CanStopCurrentAnimation || AnimationsUtils.IsInProgressBetween(Animator, 0.95f, 1);
        }

        public abstract void OnEnter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void OnExit();
    }
}