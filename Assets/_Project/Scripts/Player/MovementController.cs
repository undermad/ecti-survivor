using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player
{
    public class MovementController : ValidatedMonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField, Self] private Rigidbody2D rigidbody;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float acceleration = 100f;
        [SerializeField] private float deceleration = 100f;

        private Vector2 _inputVector;
        private Vector2 _currentVelocity;
        private float _velocityMultiplier = 1f;
        
        public void SetInput(Vector2 input) => _inputVector = input;

        public void SetVelocityMultiplier(float multiplier) => _velocityMultiplier = multiplier;
        
        public void FixedTick()
        {
            if (_inputVector != Vector2.zero)
            {
                _currentVelocity = Vector2.MoveTowards(
                    _currentVelocity,
                    _inputVector * (moveSpeed * _velocityMultiplier),
                    acceleration * Time.fixedDeltaTime
                );
            }
            else
            {
                _currentVelocity = Vector2.MoveTowards(
                    _currentVelocity,
                    Vector2.zero,
                    deceleration * Time.fixedDeltaTime
                );
            }

            rigidbody.MovePosition(rigidbody.position + _currentVelocity * Time.fixedDeltaTime);
        }

        public float GetCurrentSpeed() => _currentVelocity.magnitude;
        
    }
}