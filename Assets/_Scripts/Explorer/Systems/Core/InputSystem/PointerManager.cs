using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem
{
    public class PointerManager : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private Camera camera;
        [SerializeField, Anywhere] private InputData inputData;

        private void FixedUpdate()
        {
            if (!camera)
            {
                return;
            }
            
            var worldPosition = camera.ScreenToWorldPoint(new Vector3(
                inputData.PointerScreen.x,
                inputData.PointerScreen.y,
                -camera.transform.position.z
            ));
            inputData.PointerWorld = worldPosition;
        }
    }
}