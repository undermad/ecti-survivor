using KBCore.Refs;
using UnityEngine;
using UnityEngine.UI;

namespace Explorer._Scripts.Explorer.Components.Health
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField, Child] private Slider healthSlider;

        public void SetHealth(float healthPercents)
        {
            healthSlider.value = healthPercents;
        }
    }
}