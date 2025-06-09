using Explorer._Project.Scripts.EventBus;
using Explorer._Project.Scripts.FiniteStateMachine;
using Explorer._Project.Scripts.Player.Events;
using Explorer._Project.Scripts.Player.Hand.States;
using UnityEngine;
using UnityEngine.InputSystem;
using StateMachine = Explorer._Project.Scripts.FiniteStateMachine.StateMachine;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class HandStateMachine : MonoBehaviour
    {
        private EventBinding<FireButtonEvent> _fireButtonEventBinding;
        private EventBinding<ChangeWeaponEvent> _changeWeaponEventBinding;

        private StateMachine _stateMachine;
        private bool _isAttacking;
        private bool _isChangingWeapon;
        private bool _canIdle;

        public void Initialize(Animator animator)
        {
            var idleState = new WeaponIdleState(animator, true);
            var attackState = new WeaponAttackState(animator, true);
            var changeWeaponState = new ChangeWeaponState(animator, true);

            _stateMachine = new StateMachine();
            _stateMachine.AddTransition(idleState, attackState, new FuncPredicate(() => _isAttacking));
            _stateMachine.AddTransition(attackState, idleState, new FuncPredicate(() => !_isAttacking));
            _stateMachine.AddAnyTransition(changeWeaponState, new FuncPredicate(() => _isChangingWeapon));
            _stateMachine.AddTransition(changeWeaponState, idleState, new FuncPredicate(() => _canIdle));
            _stateMachine.SetState(idleState);
        }

        void OnEnable()
        {
            _fireButtonEventBinding = new EventBinding<FireButtonEvent>(HandleFireButtonEvent);
            EventBus<FireButtonEvent>.Subscribe(_fireButtonEventBinding);

            _changeWeaponEventBinding = new EventBinding<ChangeWeaponEvent>(HandleChangeWeaponButtonEvent);
            EventBus<ChangeWeaponEvent>.Subscribe(_changeWeaponEventBinding);
        }

        private void OnDisable()
        {
            EventBus<FireButtonEvent>.UnSubscribe(_fireButtonEventBinding);
            EventBus<ChangeWeaponEvent>.UnSubscribe(_changeWeaponEventBinding);
        }

        private void HandleFireButtonEvent(FireButtonEvent e)
        {
            switch (e.Phase)
            {
                case InputActionPhase.Started:
                    _isAttacking = true;
                    break;
            }
        }

        private void HandleChangeWeaponButtonEvent()
        {
            _isChangingWeapon = true;
            _canIdle = false;
        }

        public void Tick()
        {
            _stateMachine.Update();
        }

        public void FixedTick() => _stateMachine.FixedUpdate();

        public void HandleAttackAnimationEndEvent()
        {
            _isAttacking = false;
        }
        
        // attached to animation in unity
        public void HandleChangeWeaponAnimationEvent()
        {
            _isChangingWeapon = false;
            _canIdle = true;
        }
    }
}