using System;
using System.Collections;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Scripts.Explorer.Systems.CombatSystem;
using Explorer._Scripts.Explorer.Systems.Core.InputSystem;
using Explorer._Scripts.Explorer.Systems.LifecycleManager;
using KBCore.Refs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace Explorer._Scripts.Explorer.Components.Abilities
{
    public class Projectile : ValidatedMonoBehaviour, IUpdateObserver
    {
        [SerializeField, Anywhere] private GameplayAbility gameplayAbility;
        private AbilitySystemComponent ownerAsc;
        private Vector2 direction;
        private Action<Projectile> release;
        
        
        public void SetOwner(AbilitySystemComponent asc) => ownerAsc = asc;

        public void Init(Action<Projectile> releaseAction)
        {
            release = releaseAction;

        }


        private void OnEnable()
        {
            UpdateManager.Register(this);
            var from = (Vector2) transform.position;
            var to = InputData.PointerWorld;
            direction = -(from - to).normalized;
            transform.rotation = InputData.HandRotation;
            StartCoroutine(ReleaseAfterSeconds(5));

        }

        private void OnDisable()
        {
            UpdateManager.Unregister(this);
        }

        public void CustomUpdate()
        {
            transform.Translate(direction * (10 * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherAsc = other.GetComponent<AbilitySystemComponent>();
            if (otherAsc == null || ownerAsc.IsEqual(otherAsc))
            {
                return;
            }
            
            gameplayAbility.Activate(otherAsc, other.gameObject);
            release(this);
        }
        
        private IEnumerator ReleaseAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            release(this);
        }
    }
}