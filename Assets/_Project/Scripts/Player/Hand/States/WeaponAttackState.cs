using Explorer._Project.Scripts.FiniteStateMachine;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.Hand.States
{
    public class WeaponAttackState : BaseState
    {

        private readonly int WeaponAttackHash = Animator.StringToHash("WeaponAttack");
        
        public WeaponAttackState(Animator animator) : base(animator)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Weapon - Attack state enter");
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
            Debug.Log("Weapon - Attack state exit");
        }
    }
}