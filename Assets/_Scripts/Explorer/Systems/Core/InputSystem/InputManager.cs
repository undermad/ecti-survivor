using KBCore.Refs;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.Core.InputSystem
{
    public class InputManager : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private Camera mainCamera;

        private void FixedUpdate()
        {
            if (!mainCamera)
            {
                return;
            }
            
            var worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
                InputData.PointerScreen.x,
                InputData.PointerScreen.y,
                -mainCamera.transform.position.z
            ));
            InputData.PointerWorld = worldPosition;
        }
    }
}