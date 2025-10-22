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
    public class CharacterMoveController : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private Rigidbody2D rigidbody2D;
        [SerializeField, Anywhere] private InputData inputData;
        
        // scriptable object
        private float movementSpeed = 5.0f;

        private void FixedUpdate()
        {
            var currentVelocity = inputData.Direction * movementSpeed;

            if (Mathf.Abs(inputData.Direction.x) > 0.01f)
            {
                var scale = transform.localScale;
                scale.x = inputData.Direction.x > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            rigidbody2D.MovePosition(rigidbody2D.position + currentVelocity * Time.fixedDeltaTime);
        }
    }
}