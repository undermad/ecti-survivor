using Explorer._Scripts.Explorer.Systems.Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem.InputActions
{
    [CreateAssetMenu(fileName = "Look", menuName = "Input/Actions/Look")]
    public class Look : GameInputAction
    {
        public override void OnAction(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            InputData.Look = value;
        }
    }
}