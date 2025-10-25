using System;
using Explorer._Scripts.Explorer.Components.Health.New;
using Explorer._Scripts.Explorer.Systems.CombatSystem;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Components.Health
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField, Anywhere] private HealthData healthDataSo;
        [SerializeField, Anywhere] private FloatingHealthBar floatingHealthBarPrefab;
        [SerializeField] private Vector2 offSet;

        private HealthData healthData;
        private FloatingHealthBar healthBar;
        private void Awake()
        {
            var position = (Vector2) transform.position + offSet;
            healthBar = Instantiate(floatingHealthBarPrefab, position, Quaternion.identity);
            healthData = Instantiate(healthDataSo);
        }

        private void Start()
        {
            var healthPercents = healthData.GetHealthPercentage();
            healthBar.SetHealth(healthPercents);
        }

        public void ApplyDamage(int damage)
        {
            healthData.currentHealthPoints -= damage;
            var healthPercents = healthData.GetHealthPercentage();
            healthBar.SetHealth(healthPercents);
        }
        
    }
}
