using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Project.Scripts.UniteAustin2017.InputSystem.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem.InputActions
{
    public class Move : GameInputAction
    {
        public Move(InputAction inputAction, InputData inputData)
            : base(inputAction, inputData)
        {
        }

        public override void OnAction(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                case InputActionPhase.Performed:
                    var value = context.ReadValue<Vector2>();
                    InputData.Direction = value;
                    break;
                case InputActionPhase.Canceled:
                    InputData.Direction = Vector2.zero;
                    break;
                case InputActionPhase.Disabled:
                case InputActionPhase.Waiting:
                default:
                    break;
            }
        }
    }
}