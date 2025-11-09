using Explorer._Scripts.Explorer.Systems.Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem.InputActions
{
    [CreateAssetMenu(fileName = "Pointer", menuName = "Input/Actions/Pointer")]
    public class Pointer : GameInputAction
    {
        public override void OnAction(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            var value = context.ReadValue<Vector2>();
            InputData.PointerScreen = value;
        }
    }
}