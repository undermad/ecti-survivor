using System;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Scripts.Explorer.Components.Character;
using Explorer._Scripts.Explorer.Components.Health.New;
using Explorer._Scripts.Explorer.Objects;
using Explorer._Scripts.Explorer.Systems.CombatSystem;
using Explorer._Scripts.Explorer.Systems.CombatSystem.Events;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Components.Health
{
    [RequireComponent(typeof(PersistentId))]
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField, Anywhere] private FloatingHealthBar floatingHealthBarPrefab;
        [SerializeField] private Vector2 offSet;

        [SerializeField, Anywhere] private PersistentId persistentId;

        private FloatingHealthBar healthBar;
        
        private EventBinding<AttributeChangedEvent> attributeChangedEventBinding;
        
        private float CurrentHealth;
        private float MaxHealth;
        
        private void Awake()
        {
            var position = (Vector2) transform.position + offSet;
            healthBar = Instantiate(floatingHealthBarPrefab, position, Quaternion.identity);
        }

        private void OnEnable()
        {
            attributeChangedEventBinding = new EventBinding<AttributeChangedEvent>(HandleOnAttributeChanged);
            EventBus<AttributeChangedEvent>.Subscribe(persistentId.ID, attributeChangedEventBinding);
        }

        private void OnDisable()
        {
            EventBus<AttributeChangedEvent>.UnSubscribe(persistentId.ID, attributeChangedEventBinding);
        }

        private void HandleOnAttributeChanged(AttributeChangedEvent payload)
        {
            CurrentHealth = payload.NewValue;
            MaxHealth = payload.Max;
            
            SetHealth();
        }

        private void Start()
        {

        }

        private void SetHealth()
        {
            var healthPercents = (CurrentHealth / MaxHealth) * 100;
            healthBar.SetHealth(healthPercents);
        }

        public void ApplyDamage(int damage)
        {
            // healthData.currentHealthPoints -= damage;
            // var healthPercents = healthData.GetHealthPercentage();
            // healthBar.SetHealth(healthPercents);
        }
        
    }
}
