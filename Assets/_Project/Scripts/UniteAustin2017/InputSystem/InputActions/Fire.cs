using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem.InputActions
{
    public class Fire : GameInputAction
    {
        public Fire(InputAction inputAction, InputData inputData)
            : base(inputAction, inputData)
        {
        }

        public override void OnAction(InputAction.CallbackContext context)
        {
            switch (context.phase)  
            {
                case InputActionPhase.Started:
                    Debug.unityLogger.Log("Fire!!");
                    break;
                case InputActionPhase.Performed:
                case InputActionPhase.Canceled:
                case InputActionPhase.Disabled:
                case InputActionPhase.Waiting:
                default:
                    break;
            }
        }
    }
}