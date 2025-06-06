using Explorer._Project.Scripts.EventBus;
using Explorer._Project.Scripts.FiniteStateMachine;
using Explorer._Project.Scripts.Player.Events;
using Explorer._Project.Scripts.Player.Hand.States;
using Explorer._Project.Scripts.Player.Weapon.States;
using UnityEngine;
using UnityEngine.InputSystem;
using StateMachine = Explorer._Project.Scripts.FiniteStateMachine.StateMachine;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class HandStateMachine : MonoBehaviour
    {
        private EventBinding<FireButtonEvent> _fireButtonEventBinding;

        private StateMachine _stateMachine;
        private bool _isAttacking;

        public void Initialize(Animator animator)
        {
            var idleState = new WeaponIdleState(animator, true);
            var attackState = new WeaponAttackState(animator, false);

            _stateMachine = new StateMachine();
            _stateMachine.AddTransition(idleState, attackState, new FuncPredicate(() => _isAttacking));
            _stateMachine.AddTransition(attackState, idleState, new FuncPredicate(() => !_isAttacking));
            _stateMachine.SetState(idleState);
        }

        void OnEnable()
        {
            _fireButtonEventBinding = new EventBinding<FireButtonEvent>(HandleFireButtonEvent);
            EventBus<FireButtonEvent>.Subscribe(_fireButtonEventBinding);
        }

        private void OnDisable()
        {
            EventBus<FireButtonEvent>.UnSubscribe(_fireButtonEventBinding);
        }

        private void HandleFireButtonEvent(FireButtonEvent e)
        {
            switch (e.Phase)
            {
                case InputActionPhase.Started:
                    _isAttacking = true;
                    break;
                case InputActionPhase.Canceled:
                    _isAttacking = false;
                    break;
            }
        }

        public void Tick()
        {
            _stateMachine.Update();
        }

        public void FixedTick() => _stateMachine.FixedUpdate();
    }
}