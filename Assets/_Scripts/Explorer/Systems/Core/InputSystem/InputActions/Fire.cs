using Explorer._Project.Scripts.UniteAustin2017.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Scripts.Explorer.Systems.Core.InputSystem.InputActions
{
    [CreateAssetMenu(fileName = "Fire", menuName = "Input/Actions/Fire")]
    public class Fire : GameInputAction
    {
        public override void OnAction(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Debug.unityLogger.Log("Fire!!");
                    InputData.IsFiring = true;
                    break;
                case InputActionPhase.Canceled:
                    InputData.IsFiring = false;
                    break;
                case InputActionPhase.Disabled:
                case InputActionPhase.Waiting:
                case InputActionPhase.Performed:
                default:
                    break;
            }
        }
    }
}