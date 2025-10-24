using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Components.Health
{
    [CreateAssetMenu(menuName = "Components/Health/HealthData", fileName = "HealthData")]
    public class HealthData : ScriptableObject
    {
        public int currentHealthPoints;
        public int maxHealthPoints;

        public float GetHealthPercentage() => (float) maxHealthPoints / currentHealthPoints * 100f;
            
    }
}