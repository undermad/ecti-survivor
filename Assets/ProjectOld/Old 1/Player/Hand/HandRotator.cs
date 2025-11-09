using Explorer._Scripts.Explorer.Systems.Core.InputSystem;
using KBCore.Refs;
using UnityEngine;

namespace Explorer.ProjectOld.Old_1.Player.Hand
{
    public class HandRotator : ValidatedMonoBehaviour
    {
        [SerializeField] public float radius = 1f;
        [SerializeField, Anywhere] private Transform attachedTo;

        private Vector2 _direction;

        void FixedUpdate()
        {
            _direction = (InputData.PointerWorld - (Vector2)attachedTo.position).normalized;
            var position = (Vector2)attachedTo.position + _direction * radius;
            transform.position = position;

            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            var quaternion = Quaternion.Euler(0f, 0f, angle - 90f);
            transform.rotation = quaternion;

            InputData.HandPosition = position;
            InputData.HandRotation = quaternion;
            

            // if (transform.parent.localScale.x > 0)
            // {
            //     transform.localScale = _direction.x > 0 ? new Vector3(-1, 1, 1) : new Vector3(-1, -1, 1);
            // }
            // else
            // {
            //     transform.localScale = _direction.x > 0 ? new Vector3(1, 1, 1) : new Vector3(1, -1, 1);
            // }
        }
    }
}