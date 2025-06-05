using Explorer._Project.Scripts.EventBus;
using Explorer._Project.Scripts.FiniteStateMachine;
using Explorer._Project.Scripts.Player.Events;
using Explorer._Project.Scripts.Player.Hand.States;
using Explorer._Project.Scripts.Player.Weapon.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class HandStateMachineController : MonoBehaviour
    {
        private EventBinding<FireButtonEvent> _fireButtonEventBinding;
        
        private StateMachine _stateMachine;
        private bool _isAttacking;

        public void Initialize(Animator animator)
        {
            var idleState = new WeaponIdleState(animator);
            var attackState = new WeaponAttackState(animator);
            
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
        
        void HandleFireButtonEvent(FireButtonEvent e)
        {
            _isAttacking = e.Phase switch
            {
                InputActionPhase.Started => true,
                InputActionPhase.Canceled => false,
                _ => _isAttacking
            };
        }


        public void Tick() => _stateMachine.Update();
        public void FixedTick() => _stateMachine.FixedUpdate();
    }
}