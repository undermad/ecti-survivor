using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class HandRotator : ValidatedMonoBehaviour
    {
        [SerializeField] public float radius = 1f;

        private Vector2 _mousePosition;
        private Vector2 _direction;

        void FixedUpdate()
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _direction = (_mousePosition - (Vector2)transform.parent.position).normalized;
            transform.position = (Vector2)transform.parent.position + _direction * radius;

            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (transform.parent.localScale.x > 0)
            {
                transform.localScale = _direction.x > 0 ? new Vector3(-1, 1, 1) : new Vector3(-1, -1, 1);
            }
            else
            {
                transform.localScale = _direction.x > 0 ? new Vector3(1, 1, 1) : new Vector3(1, -1, 1);
            }
        }
    }
}

