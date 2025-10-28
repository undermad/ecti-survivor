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
    public class HealthComponent : ValidatedMonoBehaviour
    {
        [SerializeField, Parent] private PersistentId persistentId;
        [SerializeField, Anywhere] private FloatingHealthBar floatingHealthBarPrefab;
        [SerializeField] private Vector2 offSet;

        [SerializeField, Anywhere] private AttributeNameData healthAttributeName;
        [SerializeField, Anywhere] private AttributeNameData maxHealthAttributeName;
        
        

        private FloatingHealthBar healthBar;

        private EventBinding<AttributeChangedEvent> attributeChangedEventBinding;

        private float CurrentHealth;
        private float MaxHealth;

        private void Awake()
        {
            healthBar = Instantiate(floatingHealthBarPrefab, transform);
            var position = (Vector2)transform.position + offSet;
            healthBar.transform.position = position;
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
            if (payload.AttributeName.Equals(healthAttributeName.Value))
            {
                CurrentHealth = payload.NewValue;
                UpdateHealthBar();
            }
            
            if (payload.AttributeName.Equals(maxHealthAttributeName.Value))
            {
                MaxHealth = payload.NewValue;
                UpdateHealthBar();
            }
        }

        private void UpdateHealthBar()
        {
            var healthPercents = (CurrentHealth / MaxHealth) * 100;
            healthBar.SetHealth(healthPercents);
        }
    }
}