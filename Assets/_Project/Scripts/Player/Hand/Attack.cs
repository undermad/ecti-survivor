using System.Collections.Generic;
using Explorer._Project.Scripts.Enemy;
using Explorer._Project.Scripts.EventBus;
using Explorer._Project.Scripts.Player.Events;
using Explorer._Project.Scripts.Player.Hand.States;
using Explorer._Project.Scripts.Utils;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class Attack : ValidatedMonoBehaviour
    {
        [SerializeField, Parent] private Animator animator;
        [SerializeField, Anywhere] private WeaponAnimationDataSO weaponAnimationDataSo;

        private float _lastNormalizedTime;
        private EventBinding<StateEndEvent> _stateEndEventBinding;

        private readonly HashSet<GameObject> _hittedEnemies = new();

        private void OnEnable()
        {
            animator = GetComponentInParent<Animator>();
            
            var runtimeAnimatorController = animator.runtimeAnimatorController;
            if (runtimeAnimatorController is AnimatorOverrideController existingOverride)
            {
                runtimeAnimatorController = existingOverride.runtimeAnimatorController;
            }
            
            var overrideController = new AnimatorOverrideController(runtimeAnimatorController)
            {
                ["Attack1"] = weaponAnimationDataSo.AnimationClip
            };
            animator.runtimeAnimatorController = overrideController;
            
            
            _stateEndEventBinding = new EventBinding<StateEndEvent>(HandleStateEndEvent);
            EventBus<StateEndEvent>.Subscribe(_stateEndEventBinding);
        }

        private void OnDisable()
        {
            EventBus<StateEndEvent>.UnSubscribe(_stateEndEventBinding);
        }

        private void Update()
        {

            var animationState = animator.GetCurrentAnimatorStateInfo(0);
            var normalizedTime = animationState.normalizedTime;
            if (animationState.IsName(AnimationsStatesRegistry.WeaponAttack))
            {
                if (Mathf.FloorToInt(normalizedTime) > Mathf.FloorToInt(_lastNormalizedTime))
                {
                    _hittedEnemies.Clear();
                }
            }

            _lastNormalizedTime = normalizedTime;
        }

        public void ProcessHit(Collider2D other)
        {
            if (!other.CompareTag(TagsRegistry.Enemy) || _hittedEnemies.Contains(other.gameObject))
                return;
            _hittedEnemies.Add(other.gameObject);

            var enemyAttributes = other.GetComponent<Attributes>();
            var healthBar = other.GetComponentInChildren<HealthBar>();

            var currentHealth = enemyAttributes.CurrentHealth;
            if (currentHealth - 10 <= 0)
            {
                Destroy(other.gameObject);
                return;
            }

            Debug.Log("Hit");
            healthBar.UpdateHealthBar(enemyAttributes.MaxHealth, enemyAttributes.CurrentHealth - 10);
            enemyAttributes.CurrentHealth -= 10;
        }

        private void HandleStateEndEvent(StateEndEvent e)
        {
            if (e.StateName == WeaponAttackState.StateName)
            {
                _lastNormalizedTime = 0f;
            }
        }

    }
}