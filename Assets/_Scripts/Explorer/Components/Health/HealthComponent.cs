using System;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Components.Health
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField, Anywhere] private HealthData healthDataSO;
        [SerializeField, Anywhere] private HealthBar healthBarPrefab;
        [SerializeField] private Vector2 offSet;

        private HealthData healthData;
        private HealthBar healthBar;
        private void Awake()
        {
            var position = (Vector2) transform.position + offSet;
            healthBar = Instantiate(healthBarPrefab, position, Quaternion.identity);
            healthData = Instantiate(healthDataSO);
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
