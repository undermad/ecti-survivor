using Explorer._Project.Scripts.EventBus;
using Explorer._Project.Scripts.FiniteStateMachine;
using Explorer._Project.Scripts.Player.Events;
using Explorer._Project.Scripts.Utils;
using UnityEngine;
using AnimationEvent = Explorer._Project.Scripts.Player.Events.AnimationEvent;

namespace Explorer._Project.Scripts.Player.Hand.States
{
    public class WeaponAttackState : BaseState
    {
        public static readonly int WeaponAttackHash = Animator.StringToHash(AnimationsStatesRegistry.WeaponAttack);
        public static readonly string StateName = "WeaponAttackState";

        public WeaponAttackState(Animator animator, bool canStopAnimation) : base(animator, canStopAnimation)
        {
            
        }

        public override void OnEnter()
        {
            Animator.CrossFade(WeaponAttackHash, CrossFadeDuration);
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void OnExit()
        {
            EventBus<StateEndEvent>.Publish(new StateEndEvent{ StateName = StateName});
        }
    }
}