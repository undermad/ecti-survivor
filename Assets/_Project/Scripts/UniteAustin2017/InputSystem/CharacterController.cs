using System;
using Explorer._Project.Scripts.EventBus;
using Explorer._Project.Scripts.Player.Events;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Project.Scripts.UniteAustin2017.InputSystem.Events;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Events;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem
{
    public class CharacterController : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private Rigidbody2D rigidbody2D;
        
        private EventBinding<MoveEvent> _moveBinding;
        private Vector2 currentInputVector = Vector2.zero;
        
        
        // scriptable object
        private float movementSpeed = 5.0f;
        
        private void Awake()
        {
            _moveBinding = new EventBinding<MoveEvent>(SetInput);
            EventBus<MoveEvent>.Subscribe(_moveBinding);
        }

        private void OnDisable()
        {
            EventBus<MoveEvent>.UnSubscribe(_moveBinding);
        }
        
        private void SetInput(MoveEvent e) => currentInputVector = e.Direction;


        private void FixedUpdate()
        {
            var currentVelocity = currentInputVector * movementSpeed;

            if (Mathf.Abs(currentInputVector.x) > 0.01f)
            {
                var scale = transform.localScale;
                scale.x = currentInputVector.x > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            rigidbody2D.MovePosition(rigidbody2D.position + currentVelocity * Time.fixedDeltaTime);
        }
    }
}