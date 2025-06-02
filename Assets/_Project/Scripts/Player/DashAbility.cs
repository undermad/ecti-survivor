using System;
using Explorer._Project.Scripts.Utils.Timer;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player
{
    public class DashAbility : ValidatedMonoBehaviour
    {
        [Header("Dash Settings")]
        [SerializeField] private float dashCooldownDuration = 2f;
        [SerializeField] private float dashForce = 100f;
        [SerializeField] private float dashDuration = 0.3f;
        
        public float CurrentMultiplier => _isDashing ? dashForce : 1f;

        public bool CanDash => !_cooldown.IsRunning;

        public event Action OnDashStart;
        public event Action OnDashEnd;

        private CountdownTimer _cooldown;
        private CountdownTimer _duration;
        private bool _isDashing;

        private void Awake()
        {
            _cooldown = new CountdownTimer(dashCooldownDuration);
            _duration = new CountdownTimer(dashDuration);

            _duration.OnTimerStart += () =>
            {
                _isDashing = true;
                OnDashStart?.Invoke();
            };

            _duration.OnTimerStop += () =>
            {
                _isDashing = false;
                _cooldown.Start();
                OnDashEnd?.Invoke();
            };
        }

        public void TryDash()
        {
            if (!CanDash) return;
            _duration.Start();
        }

        public void TickTimers(float deltaTime)
        {
            _cooldown.Tick(deltaTime);
            _duration.Tick(deltaTime);
        }

        public bool IsDashing => _isDashing;
        
    }
}