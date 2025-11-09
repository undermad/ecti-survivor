using System;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Project.Scripts.UniteAustin2017.InputSystem;
using Explorer._Scripts.Explorer.Objects;
using Explorer._Scripts.Explorer.Systems.CombatSystem.Events;
using Explorer._Scripts.Explorer.Systems.Core.InputSystem;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Components.PlayerController
{
    public class CharacterMoveController : ValidatedMonoBehaviour
    {
        [SerializeField, Parent] private PersistentId persistentId;
        [SerializeField, Self] private Rigidbody2D rigidbody2D;
        
        // scriptable object
        private float movementSpeed = 5.0f;
        
        private EventBinding<AttributeChangedEvent> attributeChangedEventBinding;
        

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
            if (payload.AttributeName.Equals("MovementSpeed"))
            {
                movementSpeed = payload.NewValue;
            }
        }

        private void FixedUpdate()
        {
            var currentVelocity = InputData.Direction * movementSpeed;

            if (Mathf.Abs(InputData.Direction.x) > 0.01f)
            {
                var scale = transform.localScale;
                scale.x = InputData.Direction.x > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            rigidbody2D.MovePosition(rigidbody2D.position + currentVelocity * Time.fixedDeltaTime);
        }
    }
}