using System;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Project.Scripts.UniteAustin2017.InputSystem.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        public InputActionAsset inputActionAsset;
        private InputAction move;

        private void Awake()
        {
            move = inputActionAsset.FindAction("Move");
        }

        private void OnEnable()
        {
            inputActionAsset.FindActionMap("Player").Enable();
            move.started += OnMovePerformed;
            move.performed += OnMovePerformed;
            move.canceled += OnMovePerformed;
        }

        private void OnDisable()
        {
            move.performed -= OnMovePerformed;
            inputActionAsset.FindActionMap("Player").Disable();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                case InputActionPhase.Started:
                    var value = context.ReadValue<Vector2>();
                    EventBus<MoveEvent>.Publish(new MoveEvent { Direction = value });
                    break;
                case InputActionPhase.Canceled:
                    EventBus<MoveEvent>.Publish(new MoveEvent { Direction = Vector2.zero });
                    break;
                case InputActionPhase.Disabled:
                case InputActionPhase.Waiting:
                default:
                    break;
            }
        }
    }
}