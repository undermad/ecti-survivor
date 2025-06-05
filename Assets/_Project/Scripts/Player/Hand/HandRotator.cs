using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class HandRotator : ValidatedMonoBehaviour
    {
        [SerializeField] public float radius = 1f;

        // private Vector2 _mouseScreenPosition;

        void FixedUpdate()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = (mousePosition - (Vector2)transform.parent.position).normalized;
            transform.position = (Vector2)transform.parent.position + direction * radius;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            if (transform.parent.localScale.x > 0)
            {
                transform.localScale = direction.x > 0 ? new Vector3(-1, 1, 1) : new Vector3(-1, -1, 1);
            }
            else
            {
                transform.localScale = direction.x > 0 ? new Vector3(1, 1, 1) : new Vector3(1, -1, 1);
            }
        }

        // fix new input system to support pads
        // public void OnLook(InputAction.CallbackContext context)
        // {
        //     _mouseScreenPosition = context.ReadValue<Vector2>();
        // }
    }
}